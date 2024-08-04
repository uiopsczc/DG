using UnityEngine;

namespace DG
{
	public static class DGLogConst
	{
		public static string LOG_BASE_PATH = Application.persistentDataPath + "/Log/";
		public static EDGLogLevel GUI_LOG_LEVEL = EDGLogLevel.Error; //正式版本的时候这里改成LogCatType.None
		public static EDGLogLevel WRITE_LOG_LEVEL = EDGLogLevel.Error;
	}
}