/*************************************************************************************
 * 描    述:  抄自https://github.com/asik/FixedMath.Net，里面还有fix16，fix8的实现
 * 创 建 者:  czq
 * 创建时间:  2023/5/8
 * ======================================
 * 历史更新记录
 * 版本:V          修改时间:         修改人:
 * 修改内容:
 * ======================================
*************************************************************************************/

using System;
using System.IO;

public class GenDGFixedPointLookUpTableTool
{
	internal static void GenerateSinLut()
	{
		using (var writer = new StreamWriter("Lut/DGFixedPointSinLut.cs"))
		{
			writer.Write(
				@"partial struct DGFixedPointSinLut 
				{
				     public static readonly long[] SinLut = new[] 
				     {");
			int lineCounter = 0;
			for (int i = 0; i < DGFixedPointConstInternal.LUT_SIZE; ++i)
			{
				var angle = i * Math.PI * 0.5 / (DGFixedPointConstInternal.LUT_SIZE - 1);
				if (lineCounter++ % 8 == 0)
				{
					writer.WriteLine();
					writer.Write("        ");
				}

				var sin = Math.Sin(angle);
				var scaledValue = ((DGFixedPoint) sin).scaledValue;
				writer.Write(string.Format("0x{0:X}L, ", scaledValue));
			}

			writer.Write(
				@"
			    };
			}");
		}
	}

	internal static void GenerateTanLut()
	{
		using (var writer = new StreamWriter("Lut/DGFixedPointTanLut.cs"))
		{
			writer.Write(
				@"partial struct Fix64 
				{
				     public static readonly long[] TanLut = new[] 
				     {");
			int lineCounter = 0;
			for (int i = 0; i < DGFixedPointConstInternal.LUT_SIZE; ++i)
			{
				var angle = i * Math.PI * 0.5 / (DGFixedPointConstInternal.LUT_SIZE - 1);
				if (lineCounter++ % 8 == 0)
				{
					writer.WriteLine();
					writer.Write("        ");
				}

				var tan = Math.Tan(angle);
				if (tan > (double) DGFixedPoint.MaxValue || tan < 0.0)
					tan = (double) DGFixedPoint.MaxValue;
				var scaledValue = (((decimal) tan > (decimal) DGFixedPoint.MaxValue || tan < 0.0)
						? DGFixedPoint.MaxValue
						: (DGFixedPoint) tan)
					.scaledValue;
				writer.Write(string.Format("0x{0:X}L, ", scaledValue));
			}

			writer.Write(
				@"
			    };
			}");
		}
	}
}