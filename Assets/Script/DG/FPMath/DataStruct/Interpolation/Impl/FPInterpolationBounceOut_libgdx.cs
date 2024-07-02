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

namespace DG
{
	public class FPInterpolationBounceOut : FPInterpolation
	{

		protected FP[] widths, heights;

		public FPInterpolationBounceOut(FP[] widths, FP[] heights)
		{
			if (widths.Length != heights.Length)
				throw new ArgumentException("Must be the same number of widths and heights.");
			this.widths = widths;
			this.heights = heights;
		}

		public FPInterpolationBounceOut(int bounces)
		{
			if (bounces < 2 || bounces > 5) throw new ArgumentException("bounces cannot be < 2 or > 5: " + bounces);
			widths = new FP[bounces];
			heights = new FP[bounces];
			heights[0] = 1;
			switch (bounces)
			{
				case 2:
					widths[0] = 0.6f;
					widths[1] = 0.4f;
					heights[1] = 0.33f;
					break;
				case 3:
					widths[0] = 0.4f;
					widths[1] = 0.4f;
					widths[2] = 0.2f;
					heights[1] = 0.33f;
					heights[2] = 0.1f;
					break;
				case 4:
					widths[0] = 0.34f;
					widths[1] = 0.34f;
					widths[2] = 0.2f;
					widths[3] = 0.15f;
					heights[1] = 0.26f;
					heights[2] = 0.11f;
					heights[3] = 0.03f;
					break;
				case 5:
					widths[0] = 0.3f;
					widths[1] = 0.3f;
					widths[2] = 0.2f;
					widths[3] = 0.1f;
					widths[4] = 0.1f;
					heights[1] = 0.45f;
					heights[2] = 0.3f;
					heights[3] = 0.15f;
					heights[4] = 0.06f;
					break;
			}
			widths[0] *= 2;
		}

		public override FP Apply(FP a)
		{
			if (a == 1) return 1;
			a += widths[0] / 2;
			FP width = 0, height = 0;
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
			FP z = 4 / width * height * a;
			return 1 - (z - z * a) * width;
		}

	}
}

