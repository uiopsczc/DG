/*************************************************************************************
 * 描    述:  
 * 创 建 者:  czq
 * 创建时间:  2023/5/12
 * ======================================
 * 历史更新记录
 * 版本:V          修改时间:         修改人:
 * 修改内容:
 * ======================================
*************************************************************************************/

using System;
//libgdx
public class DGInterpolationBounceOut : DGInterpolation
{

	protected DGFixedPoint[] widths, heights;

	public DGInterpolationBounceOut(DGFixedPoint[] widths, DGFixedPoint[] heights)
	{
		if (widths.Length != heights.Length)
			throw new ArgumentException("Must be the same number of widths and heights.");
		this.widths = widths;
		this.heights = heights;
	}

	public DGInterpolationBounceOut(int bounces)
	{
		if (bounces < 2 || bounces > 5) throw new ArgumentException("bounces cannot be < 2 or > 5: " + bounces);
		widths = new DGFixedPoint[bounces];
		heights = new DGFixedPoint[bounces];
		heights[0] = (DGFixedPoint)1;
		switch (bounces)
		{
			case 2:
				widths[0] = (DGFixedPoint)0.6f;
				widths[1] = (DGFixedPoint)0.4f;
				heights[1] = (DGFixedPoint)0.33f;
				break;
			case 3:
				widths[0] = (DGFixedPoint)0.4f;
				widths[1] = (DGFixedPoint)0.4f;
				widths[2] = (DGFixedPoint)0.2f;
				heights[1] = (DGFixedPoint)0.33f;
				heights[2] = (DGFixedPoint)0.1f;
				break;
			case 4:
				widths[0] = (DGFixedPoint)0.34f;
				widths[1] = (DGFixedPoint)0.34f;
				widths[2] = (DGFixedPoint)0.2f;
				widths[3] = (DGFixedPoint)0.15f;
				heights[1] = (DGFixedPoint)0.26f;
				heights[2] = (DGFixedPoint)0.11f;
				heights[3] = (DGFixedPoint)0.03f;
				break;
			case 5:
				widths[0] = (DGFixedPoint)0.3f;
				widths[1] = (DGFixedPoint)0.3f;
				widths[2] = (DGFixedPoint)0.2f;
				widths[3] = (DGFixedPoint)0.1f;
				widths[4] = (DGFixedPoint)0.1f;
				heights[1] = (DGFixedPoint)0.45f;
				heights[2] = (DGFixedPoint)0.3f;
				heights[3] = (DGFixedPoint)0.15f;
				heights[4] = (DGFixedPoint)0.06f;
				break;
		}
		widths[0] *= (DGFixedPoint)2;
	}

	public override DGFixedPoint Apply(DGFixedPoint a)
	{
		if (a == (DGFixedPoint)1) return (DGFixedPoint)1;
		a += widths[0] / (DGFixedPoint)2;
		DGFixedPoint width = (DGFixedPoint)0, height = (DGFixedPoint)0;
		for (int i = 0, n = widths.Length; i < n; i++)
		{
			width = widths[i];
			if (a <= width)
			{
				height = heights[i];
				break;
			}
			a -= width;
		}
		a /= width;
		DGFixedPoint z = (DGFixedPoint)4 / width * height * a;
		return (DGFixedPoint)1 - (z - z * a) * width;
	}

}
