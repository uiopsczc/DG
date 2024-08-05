using System.IO;

namespace DG
{
    public interface IFileNameFilter
    {
        bool Accept(DirectoryInfo dir, string fileName);
    }
}