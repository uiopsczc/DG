using System.IO;
using System.Text;

namespace DG
{
    public static class FileInfoUtil
    {
        /// <summary>
        ///   后缀
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        public static string Suffix(FileInfo fileInfo)
        {
            return fileInfo.Extension;
        }

        /// <summary>
        ///   不带后缀的name
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        public static string NameWithoutSuffix(FileInfo fileInfo)
        {
            return fileInfo.Name.Substring(0, fileInfo.Name.LastIndexOf(CharConst.CHAR_DOT));
        }

        /// <summary>
        ///   不带后缀的name（全路径）
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        public static string FullNameWithoutSuffix(FileInfo fileInfo)
        {
            return fileInfo.FullName.Substring(0, fileInfo.FullName.LastIndexOf(CharConst.CHAR_DOT));
        }


        /// <summary>
        ///   将data写入文件file中(append:是否追加到文件末尾)
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <param name="data"></param>
        /// <param name="isAppend">是否追加到文件末尾</param>
        /// <returns></returns>
        public static void WriteFile(FileInfo fileInfo, byte[] data, bool isAppend)
        {
            var fos = new FileStream(fileInfo.FullName, isAppend ? FileMode.Append : FileMode.Truncate,
                FileAccess.Write);
            try
            {
                fos.Write(data, 0, data.Length);
            }
            finally
            {
                fos.Close();
            }
        }

        /// <summary>
        ///   读取文件file的内容
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        public static byte[] ReadBytes(FileInfo fileInfo)
        {
            var fis = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read);
            try
            {
                var data = new byte[(int)fileInfo.Length];
                fis.Read(data, 0, data.Length);
                return data;
            }
            finally
            {
                fis.Close();
            }
        }


        /// <summary>
        ///   向文件file写入content内容(append:是否追加到文件末尾)
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <param name="content"></param>
        /// <param name="isAppend">是否追加到文件末尾</param>
        /// <returns></returns>
        public static void WriteTextFile(FileInfo fileInfo, string content, bool isWriteLine, bool isAppend)
        {
            var fw = new StreamWriter(fileInfo.FullName, isAppend);
            try
            {
                if (!isWriteLine)
                    fw.Write(content);
                else
                    fw.WriteLine(content);
            }
            finally
            {
                fw.Close();
            }
        }


        /// <summary>
        ///   读取文件file，返回字符串内容
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        public static string ReadTextFile(FileInfo fileInfo)
        {
            var fr = new StreamReader(fileInfo.FullName);

            var stringBuilder = new StringBuilder();
            var chars = new char[1024];
            try
            {
                int n;
                while ((n = fr.Read(chars, 0, chars.Length)) != 0)
                    stringBuilder.Append(chars, 0, n);
                return stringBuilder.ToString();
            }
            finally
            {
                fr.Close();
            }
        }

        /// <summary>
        ///   将data写入文件file中
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static void WriteFile(FileInfo fileInfo, byte[] data)
        {
            fileInfo.WriteFile(data, false);
        }
    }
}