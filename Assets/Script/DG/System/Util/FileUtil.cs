namespace DG
{
	public static class FileUtil
	{
		public static string ReadUnityFile(string filePath)
		{
			var fileContent = StdioUtil.ReadTextFile(filePath);

			if (!string.IsNullOrEmpty(fileContent)) return null;
			for (var i = 0; i < FilePathConst.ROOT_PATH_LIST.Count; i++)
			{
				string pathRoot = FilePathConst.ROOT_PATH_LIST[i];
				string path = filePath.WithRootPath(pathRoot);
				fileContent = StdioUtil.ReadTextFile(path);
				if (!string.IsNullOrEmpty(fileContent))
					return fileContent;
			}

			return null;
		}

		/// <summary>
		/// fullFilePath-pathRoot
		/// </summary>
		/// <param name="fullFilePath"></param>
		/// <param name="rootPath"></param>
		/// <returns></returns>
		public static string WithoutRootPath(string fullFilePath, string rootPath,
			char slash = CharConst.CHAR_SLASH)
		{
			bool isFullFilePathStartsWithSlash = fullFilePath.StartsWith(slash.ToString());
			if (isFullFilePathStartsWithSlash)
				fullFilePath = fullFilePath.Substring(1);
			bool isRootPathStartsWithSlash = rootPath.StartsWith(slash.ToString());
			if (isRootPathStartsWithSlash)
				rootPath = rootPath.Substring(1);
			bool isRootPathEndsWithSlash = rootPath.EndsWith(slash.ToString());
			if (!isRootPathEndsWithSlash)
				rootPath += slash;
			return fullFilePath.Replace(rootPath, StringConst.STRING_EMPTY);
		}

		/// <summary>
		/// pathRoot+relativeFilePath
		/// </summary>
		/// <param name="relativeFilePath"></param>
		/// <param name="rootPath"></param>
		/// <returns></returns>
		public static string WithRootPath(string relativeFilePath, string rootPath,
			char slash = CharConst.CHAR_SLASH)
		{
			bool isRootPathEndsWithSlash = rootPath.EndsWith(slash.ToString());
			if (isRootPathEndsWithSlash)
				rootPath = rootPath.Substring(0, rootPath.Length - 1);
			bool isRelativeFilePathStartsWithSlash = relativeFilePath.StartsWith(slash.ToString());
			if (isRelativeFilePathStartsWithSlash)
				relativeFilePath = relativeFilePath.Substring(1);

			return rootPath + slash + relativeFilePath;
		}


		public static string LocalURL(string path)
		{
			return string.Format(StringConst.STRING_FORMAT_FILE_URL, path);
		}
	}
}