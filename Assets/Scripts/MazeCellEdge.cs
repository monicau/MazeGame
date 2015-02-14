using UnityEngine;
using System.Collections;

public class MazeCellEdge : MonoBehaviour {
	public Cell cell, neighbourCell;
	public MazeDirection direction;

	//Initialize the edge
	public void Initialize(Cell cell, Cell neighbourCell, MazeDirection direction) {
		this.cell = cell;
		this.neighbourCell = neighbourCell;
		this.direction = direction;
		cell.SetEdge(direction, this);
		transform.parent = cell.transform;
		transform.localPosition = DirectionPosition (direction);
		transform.localRotation = (direction).ToRotation ();
	}
	public void DestroySelf() {
		Destroy (gameObject);
	}
	Vector3 DirectionPosition(MazeDirection d) {
		if (d == MazeDirection.Up) {
						return new Vector3 (0f, 0f, 0.5f);
				} else if (d == MazeDirection.Down) {
						return new Vector3 (0f, 0f, -0.5f);
				} else if (d == MazeDirection.Left) {
						return new Vector3 (-0.5f, 0f, 0f);
				} else {
						return new Vector3 (0.5f, 0f, 0f);
				}
	}
}	
