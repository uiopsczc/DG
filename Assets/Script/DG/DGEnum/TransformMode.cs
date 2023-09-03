using System;

namespace DG
{
	[Flags]
	public enum TransformMode
	{
		LocalPosition = 1 << 0,
		LocalRotation = 1 << 1,
		LocalScale = 1 << 2,
		Position = 1 << 3,
		Rotation = 1 << 4,
		Scale = 1 << 5
	}
}