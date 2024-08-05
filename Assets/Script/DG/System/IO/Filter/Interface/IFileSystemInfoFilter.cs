using System.IO;

namespace DG
{
    public interface IFileSystemInfoFilter
    {
        /// <summary>
        /// 筛选出指定路径下的文件
        /// </summary>
        /// <param name="fileSystemInfo"></param>
        /// <returns></returns>
        bool Accept(FileSystemInfo fileSystemInfo);
    }
}