using System;

namespace SBaier.ThreeD
{
	public struct Triangle
	{
		public int[] Indices { get; }

		public Triangle(int[] indices)
		{
			checkHasNoDublicates(indices);
			Indices = indices;
		}

		private static void checkHasNoDublicates(int[] indices)
		{
			if (indices[0] == indices[1] ||
				indices[0] == indices[2] ||
				indices[1] == indices[2])
				throw new DuplicateVerticesException();
		}

		public class DuplicateVerticesException : ArgumentException
		{
		}
	}
}