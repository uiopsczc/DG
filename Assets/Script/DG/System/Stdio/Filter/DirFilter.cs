using System.IO;

namespace DG
{
	public class DirFilter : IFileSystemInfoFilter
	{
		public bool Accept(FileSystemInfo fileSystemInfo)
		{
			return fileSystemInfo.IsDirectory();
		}
	}
}