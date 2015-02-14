using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Maze : MonoBehaviour {

	public IntVector2 size;
	public Cell cellPrefab;
	public MazePassage passagePrefab;
	public MazeWall wallPrefab;
	public MazeRoom1 room1Prefab;
	public MazeRoom room2Prefab;
	public MazeRoom room3Prefab;
	private Cell[,] cells;
	
	Cell GenerateCell(int i, int j) {
		Cell cell = Instantiate (cellPrefab) as Cell;
		//Tell cell who's its parent (this maze is)
		cell.transform.parent = transform;
		//Name the cell
		cell.name = "Cell (" + i + ", " + j + ")";
		//Tell the cell its position
		cell.transform.localPosition = new Vector3 (i*1.0f, 0.0f, j*1.0f);
		//Tell the cell its coordinates
		cell.coordinates = new IntVector2 (i, j);
		//Add cell to maze array
		cells [i, j] = cell;
		return cell;
	}

	void Prims ()
	{
		//Do Prim's algorithm to break down walls
		List<Cell> cellList = new List<Cell> ();
		List<Cell> visitedList = new List<Cell> ();
		cellList.Add (cells [0, 0]);
		while (cellList.Count != 0) {
			//Pick a random cell from the cell list
			int listSize = cellList.Count - 1;
			if (listSize < 0)
				listSize = 0;
			int randIndex = Random.Range (0, listSize);
			Cell c = cellList [randIndex];
			//Get list of neighbours of that cell
			List<Cell> neighbours = GetNeighbourList (c, visitedList);
			//If list is empty, remove that cell from cell list
			if (neighbours.Count == 0) {
				cellList.RemoveAt (0);
			}
			else {
				//Pick a random neighbour
				int neighbourCount = neighbours.Count - 1;
				if (neighbourCount < 0)
					neighbourCount = 0;
				int randNeighbour = Random.Range (0, neighbourCount);
				Cell neighbour = neighbours [randNeighbour];
				//Break down wall between our cell c and this neighbour
				MazeDirection direction = GetDirection (c, neighbour);
				c.DeleteEdge (direction);
				direction = GetDirection (neighbour, c);
				neighbour.DeleteEdge (direction);
				//Mark this neighbour as visited
				visitedList.Add (neighbour);
				//Add this neighbour to cell list
				cellList.Add (neighbour);
			}
		}
	}

	public void GenerateMaze() {
		cells = new Cell[size.x, size.z];
		for (int i=0; i<size.x; i++) {
			for (int j=0; j<size.z; j++) {
				cells [i, j] = GenerateCell (i, j);
			}
		}
		//Create walls and passages
		//Draw walls with a certain probability given by method chance() 
		for (int i = 0; i< size.x; i++) {
			for (int j=0; j<size.z; j++) { 
				if ((cells[i,j]).GetEdge(MazeDirection.Up)==null && (j+1)<size.z && chance ()) {
	//				Debug.Log("Drawing up wall at " + i + ", " + j);
					CreateWall((cells[i,j]).coordinates, (cells[i,j+1]).coordinates, MazeDirection.Up);
				}
				if ((cells[i,j]).GetEdge(MazeDirection.Down)==null && (j-1)>=0 && chance ()) {
	//				Debug.Log("Drawing down wall at " + i + ", " + j);
					CreateWall((cells[i,j]).coordinates, (cells[i,j-1]).coordinates, MazeDirection.Down);
				}
				if ((cells[i,j]).GetEdge(MazeDirection.Left)==null && (i-1)>=0 && chance ()) {
	//				Debug.Log("Drawing left wall at " + i + ", " + j);
					CreateWall((cells[i,j]).coordinates, (cells[i-1,j]).coordinates, MazeDirection.Left);
				}
				if ((cells[i,j]).GetEdge(MazeDirection.Right)==null && (i+1)<size.x && chance ()) {
	//				Debug.Log("Drawing right wall at " + i + ", " + j);
					CreateWall((cells[i,j]).coordinates, (cells[i+1,j]).coordinates, MazeDirection.Right);
				}
			}
		}
		//Use a Prim-like algorithm to destroy walls to create a path
		Prims ();
		//Create rooms
		CreateRoom1();
		CreateRoom2();
		CreateRoom3 ();
	}
	void CreateRoom1() {
		int x = Random.Range (2, 10);
		int y = Random.Range (2, 5);
		MazeRoom1 rm1 = Instantiate (room1Prefab) as MazeRoom1;
		rm1.Initialize (cells [x,y], MazeDirection.Up);
		//Destroy any cell walls inside room
		foreach (IntVector2 v in rm1.cellSpace) {
			if ((cells[v.x,v.z]).GetEdge(MazeDirection.Up)!= null) {
				(cells[v.x,v.z]).DeleteEdge(MazeDirection.Up);
				(cells[v.x,v.z+1]).DeleteEdge(MazeDirection.Down);
			}
			if ((cells[v.x,v.z]).GetEdge(MazeDirection.Down)!= null) {
				(cells[v.x,v.z]).DeleteEdge(MazeDirection.Down);
				(cells[v.x,v.z-1]).DeleteEdge(MazeDirection.Up);
			}
			if ((cells[v.x,v.z]).GetEdge(MazeDirection.Left)!= null) {
				(cells[v.x,v.z]).DeleteEdge(MazeDirection.Left);
				(cells[v.x-1,v.z]).DeleteEdge(MazeDirection.Right);
			}
			if ((cells[v.x,v.z]).GetEdge(MazeDirection.Right)!= null) {
				(cells[v.x,v.z]).DeleteEdge(MazeDirection.Right);
				(cells[v.x+1,v.z]).DeleteEdge(MazeDirection.Left);
			}
		}
	}
	void CreateRoom2() {
		int x = Random.Range (10, 20);
		int y = Random.Range (15, 20);
		MazeRoom2 rm2 = Instantiate (room2Prefab) as MazeRoom2;
		rm2.Initialize (cells [x,y], MazeDirection.Left);
		//Destroy any cell walls inside room
		foreach (IntVector2 v in rm2.cellSpace) {
			if ((cells[v.x,v.z]).GetEdge(MazeDirection.Up)!= null) {
				(cells[v.x,v.z]).DeleteEdge(MazeDirection.Up);
				(cells[v.x,v.z+1]).DeleteEdge(MazeDirection.Down);
			}
			if ((cells[v.x,v.z]).GetEdge(MazeDirection.Down)!= null) {
				(cells[v.x,v.z]).DeleteEdge(MazeDirection.Down);
				(cells[v.x,v.z-1]).DeleteEdge(MazeDirection.Up);
			}
			if ((cells[v.x,v.z]).GetEdge(MazeDirection.Left)!= null) {
				(cells[v.x,v.z]).DeleteEdge(MazeDirection.Left);
				(cells[v.x-1,v.z]).DeleteEdge(MazeDirection.Right);
			}
			if ((cells[v.x,v.z]).GetEdge(MazeDirection.Right)!= null) {
				(cells[v.x,v.z]).DeleteEdge(MazeDirection.Right);
				(cells[v.x+1,v.z]).DeleteEdge(MazeDirection.Left);
			}
		}
	}
	void CreateRoom3() {;
		MazeRoom3 rm3 = Instantiate (room3Prefab) as MazeRoom3;
		rm3.Initialize (cells [23, 28], MazeDirection.Right);
		//Destroy any cell walls inside room
		foreach (IntVector2 v in rm3.cellSpace) {
			if ((cells[v.x,v.z]).GetEdge(MazeDirection.Up)!= null) {
				(cells[v.x,v.z]).DeleteEdge(MazeDirection.Up);
				(cells[v.x,v.z+1]).DeleteEdge(MazeDirection.Down);
			}
			if ((cells[v.x,v.z]).GetEdge(MazeDirection.Down)!= null) {
				(cells[v.x,v.z]).DeleteEdge(MazeDirection.Down);
				(cells[v.x,v.z-1]).DeleteEdge(MazeDirection.Up);
			}
			if ((cells[v.x,v.z]).GetEdge(MazeDirection.Left)!= null) {
				(cells[v.x,v.z]).DeleteEdge(MazeDirection.Left);
				(cells[v.x-1,v.z]).DeleteEdge(MazeDirection.Right);
			}
			if ((cells[v.x,v.z]).GetEdge(MazeDirection.Right)!= null) {
				(cells[v.x,v.z]).DeleteEdge(MazeDirection.Right);
				(cells[v.x+1,v.z]).DeleteEdge(MazeDirection.Left);
			}
		}
	}
	bool chance() {
		return (Random.Range (1,4) > 1);
	}
	MazeDirection GetDirection(Cell from, Cell to) {
		int fromX = from.coordinates.x;
		int fromZ = from.coordinates.z;
		int toX = to.coordinates.x;
		int toZ = to.coordinates.z;
		if (toX > fromX && toZ == fromZ) {
			return MazeDirection.Right;
		} else if (toX < fromX && toZ == fromZ) {
			return MazeDirection.Left;
		} else if (toX == fromX && toZ > fromZ) {
			return MazeDirection.Up;
		} else {
			return MazeDirection.Down;
		}
	}
	List<Cell> GetNeighbourList(Cell cell, List<Cell> visited) {
		List<Cell> neighbours = new List<Cell> ();
		int x = cell.coordinates.x;
		int z = cell.coordinates.z;
		if ( (x + 1) < size.x && !visited.Contains (cells [x + 1, z])) {
			neighbours.Add (cells[x+1,z]);
		}
		if ( (x - 1) >= 0 && !visited.Contains (cells [x - 1, z])) {
			neighbours.Add (cells[x-1,z]);
		}
		if ( (z + 1) < size.z && !visited.Contains (cells [x , z+1])) {
			neighbours.Add (cells[x,z+1]);
		}
		if ( (z - 1) >= 0 && !visited.Contains (cells [x , z-1])) {
			neighbours.Add (cells[x,z-1]);
		}
		return neighbours;
	}
	void CreateWall(IntVector2 from, IntVector2 to, MazeDirection direction) {
		Cell fromCell = cells [from.x, from.z];
		Cell toCell = cells [to.x, to.z];
		//Create passage for the fromCell
		MazeWall wall = Instantiate (wallPrefab) as MazeWall;
		wall.Initialize (fromCell, toCell, direction);
		//Create passage for the toCell
		wall =  Instantiate (wallPrefab) as MazeWall;
		MazeDirection oppDirection = MazeDirections.Opposite (direction);
		wall.Initialize (toCell, fromCell, oppDirection);
		wall.transform.position += new Vector3 (0f, 0.5f, 0f);
	}
//	void CreatePassage(IntVector2 from, IntVector2 to, MazeDirection direction) {
//		Cell fromCell = cells [from.x, from.z];
//		Cell toCell = cells [to.x, to.z];
//		//Create passage for the fromCell
//		MazePassage passage = Instantiate (passagePrefab) as MazePassage;
//		passage.Initialize (fromCell, toCell, direction);
//		//Create passage for the toCell
//		passage =  Instantiate (passagePrefab) as MazePassage;
//		MazeDirection oppDirection = MazeDirections.Opposite (direction);
//		passage.Initialize (toCell, fromCell, oppDirection);
//	}

}
