namespace DG
{
	public class SerializeDataConst
	{
		public static string SaveFilePathCS = "GameData_cs.txt".WithRootPath(FilePathConst.PERSISTENT_DATA_PATH);
		public static string SaveFilePathCS2 = "GameData_cs2.txt".WithRootPath(FilePathConst.PERSISTENT_DATA_PATH);
		public static string SaveFilePathLua = "GameData_lua.txt".WithRootPath(FilePathConst.PERSISTENT_DATA_PATH);
	}
}