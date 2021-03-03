using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TestTools;

namespace SBaier.ThreeD.Test
{
    public class CubeBehaviourTest 
    {
		private const string prefabPath = "Body/TestCube";
		private CubeBehaviour cubeBehaviour;
		private MeshFilter meshFilter;

		[TearDown]
		public void Dispose()
		{
			if (cubeBehaviour)
				GameObject.Destroy(cubeBehaviour.gameObject);
		}

        [Test]
        public void New_MeshNotNull()
		{
            GivenANewCube();
            ThenMeshOfTheMeshFilterIsNotNull();
		}

		[UnityTest]
		public IEnumerator New_MeshVerticesEqualCubeVertices()
		{
			GivenANewCube();
			yield return 0;
			ThenTheMeshVerticesEqualCubeVertices();
		}

		[UnityTest]
		public IEnumerator New_MeshTrianglesEqualCubeTriangles()
		{
			GivenANewCube();
			yield return 0;
			ThenTheMeshTrianglesEqualCubeTriangles();
		}

		[Test]
		public void New_MissingMeshFilterIsAddedAutomaticaly()
		{
			GivenACubeWithMissingMeshFilter();
			ThenMeshFilterIsAdded();
		}

		private void GivenANewCube()
		{
			CubeBehaviour prefab = Resources.Load<CubeBehaviour>(prefabPath);
			cubeBehaviour = GameObject.Instantiate(prefab);
			meshFilter = cubeBehaviour.GetComponent<MeshFilter>();
		}

		private void GivenACubeWithMissingMeshFilter()
		{
			GameObject obj = new GameObject();
			cubeBehaviour = obj.AddComponent<CubeBehaviour>();
		}

		private void ThenMeshOfTheMeshFilterIsNotNull()
		{
			Assert.IsNotNull(meshFilter.sharedMesh);
		}

		private void ThenTheMeshVerticesEqualCubeVertices()
		{
			Vertex[] vertices = cubeBehaviour.Cube.GetVerticesCopy();
			for (int i = 0; i < vertices.Length; i++)
				Assert.AreEqual(vertices[i].Position, meshFilter.sharedMesh.vertices[i]);
		}

		private void ThenTheMeshTrianglesEqualCubeTriangles()
		{
			int[] indices = cubeBehaviour.Cube.GetTriangleVertexIndices().ToArray();
			Assert.AreEqual(indices, meshFilter.sharedMesh.triangles);
		}

		private void ThenMeshFilterIsAdded()
		{
			MeshFilter filter = cubeBehaviour.GetComponent<MeshFilter>();
			Assert.NotNull(filter);
		}
	}
}