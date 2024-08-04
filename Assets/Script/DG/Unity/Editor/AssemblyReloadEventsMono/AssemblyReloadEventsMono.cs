using UnityEditor;

namespace DG
{
	[InitializeOnLoad]
	public partial class AssemblyReloadEventsMono
	{
		static AssemblyReloadEventsMono()
		{
			AssemblyReloadEvents.beforeAssemblyReload += BeforeAssemblyReload;
			AssemblyReloadEvents.afterAssemblyReload += AfterAssemblyReload;
		}
	}
}