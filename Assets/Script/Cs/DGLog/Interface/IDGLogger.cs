/*************************************************************************************
 * 描    述:  
 * 创 建 者:  czq
 * 创建时间:  2023/8/15
 * ======================================
 * 历史更新记录
 * 版本:V          修改时间:         修改人:
 * 修改内容:
 * ======================================
*************************************************************************************/

using System;

public interface IDGLogger 
{
	void Info(string msg, DGLogColor? logColor = default);
	void Warn(string msg, DGLogColor? logColor = default);
	void Error(string msg, DGLogColor? logColor = default);
}
