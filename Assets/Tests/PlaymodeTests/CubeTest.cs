
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SBaier.ThreeD.Test
{
    public class CubeTest
    {
		private Cube _cube;
		private Vector3 _newPivot = Vector3.one;
		private float newEdgeLength = 3.1f;
		private const float testEdgeLength = 2.4f;
		private const int trianglesCount = 12;
		private const int vertexCount = 8;
		private readonly float negativeEdgeLength = -1.3f;

		private Vector3 LeftButtomFront => new Vector3(-0.5f, -0.5f, -0.5f);
		private Vector3 LeftTopFront => new Vector3(-0.5f, 0.5f, -0.5f);
		private Vector3 RightTopFront => new Vector3(0.5f, 0.5f, -0.5f);
		private Vector3 RightBottomFront => new Vector3(0.5f, -0.5f, -0.5f);
		private Vector3 LeftBottomBack => new Vector3(-0.5f, -0.5f, 0.5f);
		private Vector3 LeftTopBack => new Vector3(-0.5f, 0.5f, 0.5f);
		private Vector3 RightTopBack => new Vector3(0.5f, 0.5f, 0.5f);
		private Vector3 RightBottomBack => new Vector3(0.5f, -0.5f, 0.5f);

		private Vector3[] UnitCubeVertexPositions => new Vector3[] { LeftButtomFront, LeftTopFront, 
			RightTopFront, RightBottomFront, LeftBottomBack, LeftTopBack, RightTopBack, RightBottomBack };

		private int[] TriangleVertexIndices => new int[]
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

		[Test]
        public void EdgeLength_New_EqualsConstructionArgument()
        {
            GivenACubeWithTestArguments();
            ThenEdgeLengthEqualsTestEdgeLength();
        }

		[Test]
		public void Volume_EqualsExpectedValue()
		{
			GivenACubeWithTestArguments();
			ThenVolumeEqualsEdgeLengthPowerOfThree();
		}

		[Test]
		public void SurfaceArea_EqualsExpectedValue()
		{
			GivenACubeWithTestArguments();
			ThenTheSurfaceAreaEqualsEdgeLengthPowerOfTwoTimesSix();
		}

		[Test]
		public void Pivot_New_EqualsVectorZero()
		{
			GivenACubeWithTestArguments();
			ThenThePivotEqualsVectorZero();
		}

		[Test]
		public void VertexCount_EqualsEight()
		{
			GivenACubeWithTestArguments();
			ThenTheVertexCountIsEight();
		}

		[Test]
		public void VertexCount_EqualsVerticesCount()
		{
			GivenACubeWithTestArguments();
			ThenTheVertexCountEqualsTheVerticesCollectionCount();
		}

		[Test]
		public void Vertices_HaveExpectedPositions()
		{
			GivenACubeWithTestArguments();
			ThenTheVerticesHaveExpectedPositions();
		}

		[Test]
		public void Vertices_HaveAdjustedPositionsAfterPivotChange()
		{
			GivenACubeWithTestArguments();
			WhenPivotIsChanged();
			ThenTheVerticesHaveTheirFormerPositionPlusThePivot();
		}

		[Test]
		public void TrianglesCount_EqualsTwelve()
		{
			GivenACubeWithTestArguments();
			ThenTheFacesCountIsTwelve();
		}

		[Test]
		public void Triangles_CountEqualsTianglesCount()
		{
			GivenACubeWithTestArguments();
			ThenTheTrianglesCollectionCountMatchesTrianglesCount();
		}

		[Test]
		public void GetTriangleVertexIndices_CountEqualsThreeTimesTrianglesCount()
		{
			GivenACubeWithTestArguments();
			ThenTheNumberOfTriangleVertexIndicesEqualsThreeTimesTrianglesCount();
		}

		[Test]
		public void GetTriangleVertexIndices_ReturnsExpectedValue()
		{
			GivenACubeWithTestArguments();
			ThenTheTriangleVertexIndicesEqualsExpectedValue();
		}

		[Test]
		public void Triangles_IndicesEqualExepectedValues()
		{
			GivenACubeWithTestArguments();
			ThenTheTriangleIndicesEqualExpectedValue();
		}

		[Test]
		public void SetEdgeLength_VertexPositionsAreUpdated()
		{
			GivenACubeWithTestArguments();
			WhenTheEdgeLengthIsChanged();
			ThenTheVertexPositionsAreUpdated();
		}

		[Test]
		public void SetEdgeLength_NegativeValueThrowsExcpetion()
		{
			GivenACubeWithTestArguments();
			TestDelegate test = () => WhenSettingANegativeValueToEdgeLength();
			ThenThrowsArgumentOutOfRangeException(test);
		}

		[Test]
		public void SetEdgeLength_ZeroValueThrowsExcpetion()
		{
			GivenACubeWithTestArguments();
			TestDelegate test = () => WhenSettingZeroValueToEdgeLength();
			ThenThrowsArgumentOutOfRangeException(test);
		}

		[Test]
		public void GetVertexPositions_ReturnsPositionsOfTheVertices()
		{
			GivenACubeWithTestArguments();
			ThenGetVertexPositionsReturnsTheVertexPositions();
		}

		private void GivenACubeWithTestArguments()
		{
			_cube = new Cube(testEdgeLength);
		}

		private void WhenPivotIsChanged()
		{
			_cube.SetPivot(_newPivot);
		}

		private void WhenTheEdgeLengthIsChanged()
		{
			_cube.SetEdgeLength(newEdgeLength);
		}

		private void WhenSettingANegativeValueToEdgeLength()
		{
			_cube.SetEdgeLength(negativeEdgeLength);
		}

		private void WhenSettingZeroValueToEdgeLength()
		{
			_cube.SetEdgeLength(0);
		}

		private void ThenEdgeLengthEqualsTestEdgeLength()
		{
			Assert.AreEqual(testEdgeLength, _cube.EdgeLength);
		}

		private void ThenVolumeEqualsEdgeLengthPowerOfThree()
		{
			float expected = Mathf.Pow(testEdgeLength, 3);
			Assert.AreEqual(expected, _cube.Volume);
		}

		private void ThenTheSurfaceAreaEqualsEdgeLengthPowerOfTwoTimesSix()
		{
			float expected = Mathf.Pow(testEdgeLength, 2) * 6;
			Assert.AreEqual(expected, _cube.SurfaceArea);
		}

		private void ThenThePivotEqualsVectorZero()
		{
			Assert.AreEqual(Vector3.zero, _cube.Pivot);
		}

		private void ThenTheVertexCountIsEight()
		{
			Assert.AreEqual(vertexCount, _cube.VertexCount);
		}

		private void ThenTheVertexCountEqualsTheVerticesCollectionCount()
		{
			Assert.AreEqual(_cube.VertexCount, _cube.GetVerticesCopy().Length);
		}

		private void ThenTheVerticesHaveExpectedPositions()
		{
			Vector3[] vertexPositions = UnitCubeVertexPositions;
			Vertex[] verticesCopy = _cube.GetVerticesCopy();
			for (int i = 0; i< vertexPositions.Length; i++)
				Assert.AreEqual(vertexPositions[i]*testEdgeLength, verticesCopy[i].Position);
		}

		private void ThenTheVerticesHaveTheirFormerPositionPlusThePivot()
		{
			Vector3[] vertexPositions = UnitCubeVertexPositions;
			Vertex[] verticesCopy = _cube.GetVerticesCopy();
			for (int i = 0; i < vertexPositions.Length; i++)
				Assert.AreEqual(vertexPositions[i]*testEdgeLength + _newPivot, verticesCopy[i].Position);
		}

		private void ThenTheFacesCountIsTwelve()
		{
			Assert.AreEqual(trianglesCount, _cube.TrianglesCount);
		}

		private void ThenTheTrianglesCollectionCountMatchesTrianglesCount()
		{
			Assert.AreEqual(_cube.TrianglesCount, _cube.GetTrianglesCopy().Length);
		}

		private void ThenTheNumberOfTriangleVertexIndicesEqualsThreeTimesTrianglesCount()
		{
			Assert.AreEqual(_cube.TrianglesCount * 3, _cube.GetTriangleVertexIndices().Count);
		}

		private void ThenTheTriangleVertexIndicesEqualsExpectedValue()
		{
			Assert.AreEqual(TriangleVertexIndices, _cube.GetTriangleVertexIndices());
		}

		private void ThenTheTriangleIndicesEqualExpectedValue()
		{
			Triangle[] triangles = _cube.GetTrianglesCopy();
			int[] indices = TriangleVertexIndices;
			for (int i = 0; i < indices.Length; i++)
			{
				int expected = indices[i];
				int actual = triangles[i / 3].Indices[i%3];
				Assert.AreEqual(expected, actual);
			}
		}

		private void ThenTheVertexPositionsAreUpdated()
		{
			Vector3[] vertexPositions = UnitCubeVertexPositions;
			Vertex[] vertices = _cube.GetVerticesCopy();
			for (int i = 0; i < vertexPositions.Length; i++)
				Assert.AreEqual(vertexPositions[i] * newEdgeLength, vertices[i].Position);
		}

		private void ThenThrowsArgumentOutOfRangeException(TestDelegate test)
		{
			Assert.Throws<ArgumentOutOfRangeException>(test);
		}

		private void ThenGetVertexPositionsReturnsTheVertexPositions()
		{
			ICollection<Vector3> positions = _cube.GetVertexPositions();
			Vertex[] vertices = _cube.GetVerticesCopy();
			for (int i = 0; i < vertices.Length; i++)
				Assert.AreEqual(vertices[i].Position, positions.ElementAt(i));
		}
	}
}