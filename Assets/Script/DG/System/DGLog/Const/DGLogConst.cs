using UnityEngine;

namespace DG
{
	public static class DGLogConst
	{
		public static string LogBasePath = Application.persistentDataPath + "/Log/";
		public static DGLogLevel GUI_Log_Level = DGLogLevel.Error; //正式版本的时候这里改成LogCatType.None
		public static DGLogLevel Write_Log_Level = DGLogLevel.Error;
	}
}