[System.Serializable]
//Our custom Vector2 that uses integers
public struct IntVector2 {

	public int x;
	public int z;

	public IntVector2(int x, int z) {
		this.x = x;
		this.z = z;
	}
	public static IntVector2 operator + (IntVector2 i, IntVector2 j) {
		i.x = i.x + j.x;
		i.z = i.z + j.z;
		return i;
	}
}
