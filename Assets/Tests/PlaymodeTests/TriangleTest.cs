using NUnit.Framework;
using UnityEngine;

namespace SBaier.ThreeD.Test
{
    public class TriangleTest 
    {
		Triangle _face;

		[Test]
		public void Indices_CountEualsThree()
		{
			GivenANewFaceWithTestArguments();
			ThenTheIndicesCountIsThree();
		}

		[Test]
		public void Indices_DuplicatesThrowException()
		{
			TestDelegate test = () => GivenANewFaceWithDoublicateIndices();
			ThenAVertexDouplicateExceptionIsThrown(test);
		}

		[Test]
		public void Indices_CountEqualsVerticesCount()
		{
			GivenANewFaceWithTestArguments();
		}

		private void GivenANewFaceWithDoublicateIndices()
		{
			_face = new Triangle(createDoublicateIndices());
		}

		private void GivenANewFaceWithTestArguments()
		{
			_face = new Triangle(createIndices());
		}

		private void ThenTheIndicesCountIsThree()
		{
			Assert.AreEqual(3, _face.Indices.Length);
		}

		private void ThenAVertexDouplicateExceptionIsThrown(TestDelegate test)
		{
			Assert.Throws<Triangle.DuplicateVerticesException>(test);
		}

		private int[] createIndices()
		{
			return new int[] { 0, 1, 2 };
		}

		private int[] createDoublicateIndices()
		{
			return new int[] { 0, 0, 2 };
		}
	}
}