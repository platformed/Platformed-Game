using UnityEngine;

public class Block {
	public static Block air = new Block(0, "Air", null, true);
	public static Block testBlock1 = new Block(1, "Block 1", "testblock1", false);
	public static Block testBlock2 = new Block(2, "Block 2", "testblock2", false);
	public static Block testBlock3 = new Block(3, "Block 3", "testblock3", false);
	public static Block stone1 = new Block(4, "Stone 1", "stone1", false);

	public static Block[] blocks = new Block[] {air, testBlock1, testBlock2, testBlock3, stone1};

	string name;
	public ushort id;
	bool transparent;
	string texture;

	public Block(ushort i, string n, string te, bool t){
		name = n;
		id = i;
		transparent = t;
		texture = te;
	}

	public bool isTransparent(){
		return transparent;
	}

	public string getName(){
		return name;
	}

	public ushort getID(){
		return id;
	}
	
	public string getTexture(){
		return texture;
	}

	public MeshData draw(Chunk chunk, int x, int y, int z, bool cursor){
		if(this.id == 0){
			return new MeshData();
		}

		Vector3[] v = new Vector3[]{
			new Vector3(0, 0, 0),	//0
			new Vector3(0, 0, 1),	//1
			new Vector3(0, 1, 0),	//2
			new Vector3(0, 1, 1),	//3
			new Vector3(1, 0, 0),	//4
			new Vector3(1, 0, 1),	//5
			new Vector3(1, 1, 0),	//6
			new Vector3(1, 1, 1)	//7
		};

		int[] t = new int[]{0, 2, 1, 3, 1, 2};

		Rect u = TextureManager.uvs[id - 1];

		float cWidth = 1f / TextureManager.atlasWidth;
		float cHeight = 1f / TextureManager.atlasHeight;

		Vector2[] uvs = new Vector2[]{
			new Vector2(u.xMin + cWidth, u.yMax - cHeight),
			new Vector2(u.xMin + cWidth, u.yMin + cHeight),
			new Vector2(u.xMax - cWidth, u.yMax - cHeight),
			new Vector2(u.xMax - cWidth, u.yMin + cHeight)
		};

		MeshData data = new MeshData();

		if (cursor || chunk.getBlock(x, y - 1, z).isTransparent()){
			Face bottom = new Face (new Vector3[]{v[0], v[1], v[4], v[5]}, t, uvs);
			data.add (bottom.getMeshData());
		}
		
		if (cursor || chunk.getBlock(x, y, z + 1).isTransparent()){
			Face right = new Face (new Vector3[]{v[7], v[5], v[3], v[1]}, t, uvs);
			data.add (right.getMeshData());
		}
		
		if (cursor || chunk.getBlock (x, y, z - 1).isTransparent ()) {
			Face left = new Face (new Vector3[]{v [2], v [0], v [6], v [4]}, t, uvs);
			data.add (left.getMeshData ());
		}
		
		if (cursor || chunk.getBlock (x + 1, y, z).isTransparent ()) {
			Face back = new Face (new Vector3[]{v [6], v [4], v [7], v [5]}, t, uvs);
			data.add (back.getMeshData ());
		}
		
		if (cursor || chunk.getBlock (x - 1, y, z).isTransparent ()) {
			Face front = new Face (new Vector3[]{v [3], v [1], v [2], v [0]}, t, uvs);
			data.add (front.getMeshData ());
		}
		
		if (cursor || chunk.getBlock (x, y + 1, z).isTransparent ()) {
			Face top = new Face (new Vector3[]{v [6], v [7], v [2], v [3]}, t, uvs);
			data.add (top.getMeshData ());
		}

		return data;
	}
}
