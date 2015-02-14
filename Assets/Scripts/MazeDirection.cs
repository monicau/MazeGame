using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum MazeDirection {
	Up,
	Left,
	Down,
	Right
}
public static class MazeDirections {
	private static Quaternion[] rotations = {
		Quaternion.identity,
		Quaternion.Euler (0f, 90f, 0f),
		Quaternion.Euler (0f, 180f, 0f),
		Quaternion.Euler (0f, 270f, 0f)
	};
	private static Quaternion[] roomRotations = {
		Quaternion.Euler (0f, 270f, 0f),
		Quaternion.Euler (0f, 180f, 0f),
		Quaternion.Euler (0f, 90f, 0f),

		Quaternion.identity,
	};

	public static IntVector2 PickFrontier(IList<IntVector2> frontiers) {
		int listSize = frontiers.Count;
		int randIndex = Random.Range (0, listSize);
		IntVector2 pickedCoord = frontiers[randIndex];
		frontiers.RemoveAt (randIndex);
		return pickedCoord;
	}
	public static MazeDirection Direction(IntVector2 from, IntVector2 to) {
		if (from.x == to.x && to.z > from.z) {
				return MazeDirection.Up;
			} else if (to.x<from.x && to.z==from.z) {
				return MazeDirection.Left;
			} else if (to.x==from.x && to.z<from.z) {
				return MazeDirection.Down;
			} else {//if (to.x>from.x && to.z==from.z) {
				return MazeDirection.Right;
			} 
	}
	public static MazeDirection Opposite(MazeDirection direction) {
		if (direction == MazeDirection.Up) {
						return MazeDirection.Down;
				} else if (direction == MazeDirection.Down) {
						return MazeDirection.Up;
				} else if (direction == MazeDirection.Left) {
						return MazeDirection.Right;
				} else {
						return MazeDirection.Left;
				}
	}

	private static bool WithinGridSpace(IntVector2 v) {
		if (v.x >=0 && v.x<=29 && v.z>=0 && v.z<=29) {
			return true;
		} else {
			return false;
		}
	}
	public static Quaternion ToRotation(this MazeDirection direction) {
		return rotations[(int) direction];
	}
	public static Quaternion RoomRotation(this MazeDirection direction) {
		return roomRotations [(int)direction];
	}
}