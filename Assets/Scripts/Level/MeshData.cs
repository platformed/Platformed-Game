using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Stores vertices, triangles, and uvs of a mesh and collision mesh
/// </summary>
public class MeshData {
	public List<Vector3> vertices = new List<Vector3>();
	public List<Vector3> normals = new List<Vector3>();
	public List<int> triangles = new List<int>();
	public List<Vector2> uvs = new List<Vector2>();

	public List<Vector3> colVerticies = new List<Vector3>();
	public List<int> colTriangles = new List<int>();

	public bool useRenderDataForCol;

	public MeshData() { }

	/// <summary>
	/// Add a vertex to the meshdata
	/// </summary>
	public void AddVertex(Vector3 vertex, Vector3 normal) {
		vertices.Add(vertex);
		normals.Add(normal);

		if (useRenderDataForCol) {
			colVerticies.Add(vertex);
		}
	}

	/// <summary>
	/// Add an array of vertices to the meshdata
	/// </summary>
	public void AddVertices(Vector3[] vertices, Vector3[] normals, Vector3 posOffset, Quaternion rotOffset) {
		for (int i = 0; i < vertices.Length; i++) {
			AddVertex((rotOffset * vertices[i]) + posOffset, rotOffset * normals[i]);
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
		AddTriangle(vertices.Count - 4);
		AddTriangle(vertices.Count - 3);
		AddTriangle(vertices.Count - 2);

		AddTriangle(vertices.Count - 4);
		AddTriangle(vertices.Count - 2);
		AddTriangle(vertices.Count - 1);
	}

	/// <summary>
	/// Add an array of uvs to the meshdata
	/// </summary>
	public void AddUVs(Vector2[] u) {
		uvs.AddRange(u);
	}
}
