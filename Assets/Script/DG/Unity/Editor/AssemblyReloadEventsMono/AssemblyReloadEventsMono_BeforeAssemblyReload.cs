namespace DG
{
	public partial class AssemblyReloadEventsMono
	{
		static void BeforeAssemblyReload()
		{
			PausableCoroutineManager.instance.gameObject.Destroy();
		}
	}
}