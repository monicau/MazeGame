using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MazeRoom : MonoBehaviour {
	public Cell root;
	public MazeDirection direction;
	public List<IntVector2> cellSpace = new List<IntVector2>();
	public GameObject ammunitionPrefab;

	public void Initialize(Cell root, MazeDirection direction) {
		//Set the room's entrance cell
		this.root = root;
		//Set direction of room
		this.direction = direction;
		//Set room's parent and rotation
		transform.parent = root.transform;
		transform.localRotation = (direction).RoomRotation ();

		int x = root.coordinates.x;
		int z = root.coordinates.z;
		int xFrom;
		int xTo;
		int zFrom;
		int zTo;

		//Find which cells this room occupies and add it to our list cellSpace
		if (direction == MazeDirection.Right) {
			transform.localPosition = new Vector3(4.5f,0f,-0.5f);
			xFrom = x+1;
			zFrom = z-1;
			xTo = xFrom+6;
			zTo = zFrom+3;
		} else if (direction == MazeDirection.Left) {
			transform.localPosition = new Vector3(-4.5f,0f,0.5f);
			xFrom = x-6;
			zFrom = z-1;
			xTo = xFrom+6;
			zTo = zFrom+3;
		} else if (direction == MazeDirection.Down) {
			transform.localPosition = new Vector3(-0.5f,0f,-4.5f);
			xFrom = x-1;
			zFrom = z-6;
			xTo = xFrom+3;
			zTo = zFrom+6;
		} else {
			transform.localPosition = new Vector3(0.5f,0f,4.5f);
			xFrom = x-1;
			zFrom = z+1;
			xTo = xFrom+3;
			zTo = zFrom+6;
		}
		for (int i = xFrom; i<xTo; i++) {
			for (int j = zFrom; j<zTo; j++) {
				cellSpace.Add(new IntVector2(i,j));
			}
		}
		//Generate the corresponding ammunition in the secondary room
		if (ammunitionPrefab!=null) {
			GameObject ammunition = Instantiate (ammunitionPrefab) as GameObject;
			if (direction==MazeDirection.Up) {
				ammunition.transform.position = transform.position + new Vector3(-0.5f, 0.5f, 1f);
			} else if (direction==MazeDirection.Left) {
				ammunition.transform.position = transform.position + new Vector3(-1f, 0.5f, -0.5f);
			}
		}
	}
}
