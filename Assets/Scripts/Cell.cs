using UnityEngine;
using System.Collections;

public class Cell : MonoBehaviour {
	public Transform pathTilePrefab1;
	public Transform pathTilePrefab2;
	public Transform pathTilePrefab3;
	//Array storing each direction's edge type (wall or passage)
	private MazeCellEdge[] myEdges = new MazeCellEdge[4];
	//Its coordinate in the maze
	public IntVector2 coordinates;

	public void SetEdge(MazeDirection direction, MazeCellEdge edge) {
		myEdges [(int)direction] = edge;
	}
	public MazeCellEdge GetEdge(MazeDirection direction) {
		return myEdges [(int)direction];
	}
	public void DeleteEdge(MazeDirection direction) {
		MazeCellEdge edge = myEdges [(int)direction];
		if (edge != null) edge.DestroySelf ();
		myEdges [(int)direction] = null;
	}
	public void SetPathFloor(bool path, int num) {
		if (path && num==1) {
			Instantiate (pathTilePrefab1, new Vector3 (coordinates.x, 0.01f, coordinates.z), Quaternion.Euler(90f,0f,0f));
		} else if (path && num==2) {
			Instantiate (pathTilePrefab2, new Vector3 (coordinates.x, 0.01f, coordinates.z), Quaternion.Euler(90f,0f,0f));
		} else {
			Instantiate (pathTilePrefab3, new Vector3 (coordinates.x, 0.01f, coordinates.z), Quaternion.Euler(90f,0f,0f));
		}
	}
}
