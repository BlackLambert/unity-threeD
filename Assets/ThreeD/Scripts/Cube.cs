using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SBaier.ThreeD
{
	public class Cube
	{
		private static readonly Vector3 leftButtomFront = new Vector3(-0.5f, -0.5f, -0.5f);
		private static readonly Vector3 leftTopFront = new Vector3(-0.5f, 0.5f, -0.5f);
		private static readonly Vector3 rightTopFront = new Vector3(0.5f, 0.5f, -0.5f);
		private static readonly Vector3 rightBottomFront = new Vector3(0.5f, -0.5f, -0.5f);
		private static readonly Vector3 leftBottomBack = new Vector3(-0.5f, -0.5f, 0.5f);
		private static readonly Vector3 leftTopBack = new Vector3(-0.5f, 0.5f, 0.5f);
		private static readonly Vector3 rightTopBack = new Vector3(0.5f, 0.5f, 0.5f);
		private static readonly Vector3 rightBottomBack = new Vector3(0.5f, -0.5f, 0.5f);

		private static readonly Vector3[] unitCubeVertexPositions = new Vector3[]
		{
			leftButtomFront,
			leftTopFront,
			rightTopFront,
			rightBottomFront,
			leftBottomBack,
			leftTopBack,
			rightTopBack,
			rightBottomBack,
		};

		private static readonly int[] defaultTriangleVertexIndices = new int[]
		{
			0, 2, 1, //Front1
			0, 3, 2, //Front2
			3, 6, 2, //Right1
			3, 7, 6, //Right2
			7, 5, 6, //Back1
			7, 4, 5, //Back2
			4, 1, 5, //Left1
			4, 0, 1, //Left2
			1, 6, 5, //Top1
			1, 2, 6, //Top2
			4, 3, 0, //Bottom1
			4, 7, 3 //Bottom2
		};

		private Vertex[] vertices = new Vertex[8];
		private Triangle[] triangles = new Triangle[12];

		public float EdgeLength { get; private set; }
		public Vector3 Pivot { get; private set; } = Vector3.zero;

		public float Volume => EdgeLength * EdgeLength * EdgeLength;
		public float SurfaceArea => EdgeLength * EdgeLength * 6;
		public int VertexCount => vertices.Length;
		public int TrianglesCount => triangles.Length;

		public Cube()
		{
			EdgeLength = 1;
			CreateVertices();
			CreateTriangles();
		}

		public Cube(float edgeLength)
		{
			EdgeLength = edgeLength;
			CreateVertices();
			CreateTriangles();
		}

		public void SetPivot(Vector3 newPivot)
		{
			Pivot = newPivot;
			UpdateVertexPositions();
		}

		public void SetEdgeLength(float newEdgeLength)
		{
			ValidateSetEdgeLengthValue(newEdgeLength);
			EdgeLength = newEdgeLength;
			UpdateVertexPositions();
		}

		private static void ValidateSetEdgeLengthValue(float newEdgeLength)
		{
			if (newEdgeLength <= 0)
				throw new ArgumentOutOfRangeException();
		}

		public Vertex[] GetVerticesCopy()
		{
			Vertex[] result = new Vertex[8];
			vertices.CopyTo(result, 0);
			return result;
		}

		public Triangle[] GetTrianglesCopy()
		{
			Triangle[] result = new Triangle[12];
			triangles.CopyTo(result, 0);
			return result;
		}

		public ICollection<int> GetTriangleVertexIndices()
		{
			return triangles.ToList().Aggregate(new List<int>(), (result, next) =>
			{
				result.AddRange(next.Indices);
				return result;
			});
		}

		public ICollection<Vector3> GetVertexPositions()
		{
			return vertices.ToList().Aggregate(new List<Vector3>(), (result, next) =>
			{
				result.Add(next.Position);
				return result;
			});
		}

		private void CreateVertices()
		{
			for (int i = 0; i < unitCubeVertexPositions.Length; i++)
				CreateVertexAt(i);
		}

		private void CreateTriangles()
		{
			for(int i = 0; i < TrianglesCount; i++)
				CreateTriangleAt(i);
		}

		private void UpdateVertexPositions()
		{
			CreateVertices();
		}

		private void CreateTriangleAt(int index)
		{
			int[] triangleVertexIndices = new int[3];
			triangleVertexIndices[0] = defaultTriangleVertexIndices[index * 3];
			triangleVertexIndices[1] = defaultTriangleVertexIndices[index * 3 + 1];
			triangleVertexIndices[2] = defaultTriangleVertexIndices[index * 3 + 2];
			triangles[index] = new Triangle(triangleVertexIndices);
		}

		private void CreateVertexAt(int index)
		{
			vertices[index] = new Vertex(GetVertexPositionAt(index));
		}

		private Vector3 GetVertexPositionAt(int index)
		{
			return unitCubeVertexPositions[index] * EdgeLength + Pivot;
		}
	}
}