using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.ThreeD
{
    public struct Vertex 
    {
        public Vector3 Position { get; }

        public Vertex(Vector3 position)
		{
            Position = position;
        }

		public Vertex(float x, float y, float z)
		{
            Position = new Vector3(x, y, z);
        }

		public override string ToString()
		{
			return $"{nameof(Vertex)}({Position.ToString()})";
		}

		public static bool operator== (Vertex v1, Vertex v2)
		{
			return v1.Position == v2.Position;
		}

		public static bool operator!= (Vertex v1, Vertex v2)
		{
			return v1.Position != v2.Position;
		}

		public override bool Equals(object obj)
		{
			if(obj is Vertex)
				return Position == ((Vertex)obj).Position;
			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			return -425505606 + Position.GetHashCode();
		}
	}
}