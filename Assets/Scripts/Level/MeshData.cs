using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Stores vertices, triangles, and uvs of a mesh and collision mesh
/// </summary>
public class MeshData {
	public List<Vector3> vertices = new List<Vector3>();
	public List<int> triangles = new List<int>();
	public List<Vector2> uvs = new List<Vector2>();

	public List<Vector3> colVerticies = new List<Vector3>();
	public List<int> colTriangles = new List<int>();

	public bool useRenderDataForCol;

	public MeshData() { }

	/// <summary>
	/// Add a vertex to the meshdata
	/// </summary>
	public void AddVertex(Vector3 vertex) {
		vertices.Add(vertex);

		if (useRenderDataForCol) {
			colVerticies.Add(vertex);
		}
	}

	/// <summary>
	/// Add an array of vertices to the meshdata
	/// </summary>
	public void AddVertices(Vector3[] vertices, Vector3 posOffset, Quaternion rotOffset) {
		foreach (Vector3 v in vertices) {
			AddVertex((rotOffset * v) + posOffset);
		}
	}

	/// <summary>
	/// Add a triangle to the meshdata
	/// </summary>
	public void AddTriangle(int tri) {
		triangles.Add(tri);

		if (useRenderDataForCol) {
			colTriangles.Add(tri);
		}
	}

	/// <summary>
	/// Add an array of triangles to the meshdata
	/// </summary>
	public void AddTriangles(int[] triangles) {
		int index = vertices.Count;

		foreach (int t in triangles) {
			AddTriangle(index + t);
		}
	}

	/// <summary>
	/// Finishes a quad by adding the triangles
	/// </summary>
	public void AddQuadTriangles() {
		triangles.Add(vertices.Count - 4);
		triangles.Add(vertices.Count - 3);
		triangles.Add(vertices.Count - 2);

		triangles.Add(vertices.Count - 4);
		triangles.Add(vertices.Count - 2);
		triangles.Add(vertices.Count - 1);

		if (useRenderDataForCol) {
			colTriangles.Add(vertices.Count - 4);
			colTriangles.Add(vertices.Count - 3);
			colTriangles.Add(vertices.Count - 2);

			colTriangles.Add(vertices.Count - 4);
			colTriangles.Add(vertices.Count - 2);
			colTriangles.Add(vertices.Count - 1);
		}
	}

	/// <summary>
	/// Add an array of uvs to the meshdata
	/// </summary>
	public void AddUVs(Vector2[] u) {
		uvs.AddRange(u);
	}
}
