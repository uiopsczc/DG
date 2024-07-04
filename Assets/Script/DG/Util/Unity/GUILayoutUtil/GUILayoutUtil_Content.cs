#if UNITY_EDITOR
namespace DG
{
	public partial class GUILayoutUtil
	{
		public GUILayoutContentScope Content()
		{
			return new GUILayoutContentScope();
		}
	}
}
#endif