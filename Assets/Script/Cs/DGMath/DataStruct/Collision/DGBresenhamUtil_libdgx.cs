/*************************************************************************************
 * 描    述:  
 * 创 建 者:  czq
 * 创建时间:  2023/5/21
 * ======================================
 * 历史更新记录
 * 版本:V          修改时间:         修改人:
 * 修改内容:
 * ======================================
*************************************************************************************/


using System;
using System.Collections.Generic;

public static class DGBresenhamUtil
{

	/** Returns a list of {@link GridPoint2} instances along the given line, at integer coordinates.
	 * @param start the start of the line
	 * @param end the end of the line
	 * @return the list of points on the line at integer coordinates */
	public static List<DGGridPoint2> line(DGGridPoint2 start, DGGridPoint2 end)
	{
		return line(start.x, start.y, end.x, end.y);
	}

	/** Returns a list of {@link GridPoint2} instances along the given line, at integer coordinates.
	 * @param startX the start x coordinate of the line
	 * @param startY the start y coordinate of the line
	 * @param endX the end x coordinate of the line
	 * @param endY the end y coordinate of the line
	 * @param pool the pool from which GridPoint2 instances are fetched
	 * @param output the output array, will be cleared in this method
	 * @return the list of points on the line at integer coordinates */
	public static List<DGGridPoint2> line(int startX, int startY, int endX, int endY)
	{
		List<DGGridPoint2> output = new List<DGGridPoint2>();
		int w = endX - startX;
		int h = endY - startY;
		int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
		if (w < 0)
		{
			dx1 = -1;
			dx2 = -1;
		}
		else if (w > 0)
		{
			dx1 = 1;
			dx2 = 1;
		}

		if (h < 0)
			dy1 = -1;
		else if (h > 0) dy1 = 1;
		int longest = Math.Abs(w);
		int shortest = Math.Abs(h);
		if (longest < shortest)
		{
			longest = Math.Abs(h);
			shortest = Math.Abs(w);
			if (h < 0)
				dy2 = -1;
			else if (h > 0) dy2 = 1;
			dx2 = 0;
		}

		int shortest2 = shortest << 1;
		int longest2 = longest << 1;
		int numerator = 0;
		for (int i = 0; i <= longest; i++)
		{
			DGGridPoint2 point = new DGGridPoint2();
			point.set(startX, startY);
			output.Add(point);
			numerator += shortest2;
			if (numerator > longest)
			{
				numerator -= longest2;
				startX += dx1;
				startY += dy1;
			}
			else
			{
				startX += dx2;
				startY += dy2;
			}
		}

		return output;
	}
}