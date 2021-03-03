using System;
using NUnit.Framework;
using UnityEngine;

namespace SBaier.ThreeD.Test
{
    public class VertexTest
    {
		private Vertex _vertex;
		private Vector3 _startPosition = new Vector3(32.4f, -3.5f);
		private Vector3 _secondStartPosition = new Vector3(5.0f, 4.5f);
		private Vector3 _newPosition = new Vector3(-2.0f, 12.7f);

		[Test]
        public void New_HasExpectedPosition()
		{
            GivenANewVertexWithStartPosition();
            ThenPositionReturnsStartPosition();

			GivenANewVertexWithSecondStartPosition();
			ThenPositionReturnsSecondStartPosition();
		}

		[Test]
		public void NewSepatateValues_HasExpectedPosition()
		{
			GivenANewSepatateValuesVertexWithStartPosition();
			ThenPositionReturnsStartPosition();

			GivenANewSepatateValuesVertexWithSecondStartPosition();
			ThenPositionReturnsSecondStartPosition();
		}

		[Test]
		public void ToString_ContainsPositionString()
		{
			GivenANewVertexWithStartPosition();
			ThenToStringContainsStartPositionString();
		}

		[Test]
		public void ToString_ContainsClassName()
		{
			GivenANewVertexWithStartPosition();
			ThenToStringContainsClassName();
		}

		[Test]
		public void Equals_GetsSameResultAsEqualsOperator()
		{
			GivenANewVertexWithStartPosition();
			ThenEqualsAndEqualsOperatorReturnTrueForSimilarVertex();
		}

		[Test]
		public void Equals_ReturnsTrueForAVertexWithTheSamePosition()
		{
			GivenANewVertexWithStartPosition();
			ThenEqualsReturnsTrueForSimilarVertex();
		}

		[Test]
		public void Equals_ReturnsFalseForDissimilarVertex()
		{
			GivenANewVertexWithStartPosition();
			ThenEqualsReturnsFalseForDissimilarVertex();
		}

		private void GivenANewVertexWithSecondStartPosition()
		{
			_vertex = CreateVertexWithSecondStartPositionByVector();
		}

		private void GivenANewVertexWithStartPosition()
		{
			_vertex = CreateVertexWithStartPositionByVector();
		}

		private void GivenANewSepatateValuesVertexWithSecondStartPosition()
		{
			_vertex = new Vertex(_secondStartPosition.x, _secondStartPosition.y, _secondStartPosition.z);
		}

		private void GivenANewSepatateValuesVertexWithStartPosition()
		{
			_vertex = new Vertex(_startPosition.x, _startPosition.y, _startPosition.z);
		}

		private void ThenToStringContainsStartPositionString()
		{
			string vertexString = _vertex.ToString();
			string expected = _startPosition.ToString();
			Assert.True(vertexString.Contains(expected));
		}

		private void ThenPositionReturnsNewPosition()
		{
			Assert.AreEqual(_newPosition, _vertex.Position);
		}

		private void ThenPositionReturnsSecondStartPosition()
		{
			Assert.AreEqual(_secondStartPosition, _vertex.Position);
		}

		private void ThenPositionReturnsStartPosition()
		{
			Assert.AreEqual(_startPosition, _vertex.Position);
		}

		private void ThenToStringContainsClassName()
		{
			Assert.True(_vertex.ToString().Contains(nameof(Vertex)));
		}

		private void ThenEqualsAndEqualsOperatorReturnTrueForSimilarVertex()
		{
			Vertex similar = CreateVertexWithStartPositionByVector();
			Assert.True(_vertex.Equals(similar) == (_vertex == similar));
		}

		private void ThenEqualsReturnsTrueForSimilarVertex()
		{
			Vertex similar = CreateVertexWithStartPositionByVector();
			Assert.True(_vertex.Equals(similar));
		}

		private void ThenEqualsReturnsFalseForDissimilarVertex()
		{
			Vertex dissimilar = CreateVertexWithSecondStartPositionByVector();
			Assert.False(_vertex.Equals(dissimilar));
		}

		private Vertex CreateVertexWithStartPositionByVector()
		{
			return new Vertex(_startPosition);
		}

		private Vertex CreateVertexWithSecondStartPositionByVector()
		{
			return new Vertex(_secondStartPosition);
		}
	}
}