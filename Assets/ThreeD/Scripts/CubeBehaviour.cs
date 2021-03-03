using System.Linq;
using UnityEngine;

namespace SBaier.ThreeD
{
	[RequireComponent(typeof(MeshFilter))]
    public class CubeBehaviour : MonoBehaviour
    {
        private MeshFilter _meshFilter;
        [SerializeField]
        private float _initialEdgeLength = 1f;

		public Cube Cube { get; private set; }

        protected virtual void Awake()
		{
            Cube = new Cube(_initialEdgeLength);
			_meshFilter = GetComponent<MeshFilter>();

			_meshFilter.sharedMesh = new Mesh();
        }

		protected virtual void Start()
		{
			UpdateMesh();
		}

		private void UpdateMesh()
		{
			ClearMesh();
			SetMeshVertices();
			SetMeshTriangles();
			RecalculateMeshNormals();
		}

		private void ClearMesh()
		{
			_meshFilter.sharedMesh.Clear();
		}

		private void RecalculateMeshNormals()
		{
			_meshFilter.sharedMesh.RecalculateNormals();
		}

		private void SetMeshVertices()
		{
            _meshFilter.sharedMesh.vertices = Cube.GetVertexPositions().ToArray();
		}
        private void SetMeshTriangles()
        {
            _meshFilter.sharedMesh.triangles = Cube.GetTriangleVertexIndices().ToArray();
        }
    }
}