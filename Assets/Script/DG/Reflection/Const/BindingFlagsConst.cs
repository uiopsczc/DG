using System.Reflection;

namespace DG
{
	public class BindingFlagsConst
	{
		public const BindingFlags ALL = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static |
										BindingFlags.Instance;

		public const BindingFlags ALL_PUBLIC = BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance;
		public const BindingFlags ALL_PRIVATE = BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance;

		public const BindingFlags INSTANCE = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
		public const BindingFlags INSTANCE_PUBLIC = BindingFlags.Public | BindingFlags.Instance;
		public const BindingFlags INSTANCE_PRIVATE = BindingFlags.NonPublic | BindingFlags.Instance;

		public const BindingFlags STATIC = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
		public const BindingFlags STATIC_PUBLIC = BindingFlags.Public | BindingFlags.Static;
		public const BindingFlags STATIC_PRIVATE = BindingFlags.NonPublic | BindingFlags.Static;
	}
}