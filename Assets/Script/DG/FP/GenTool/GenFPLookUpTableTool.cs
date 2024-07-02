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

namespace DG
{
	public class GenFPLookUpTableTool
	{
		internal static void GenerateSinLut()
		{
			using (var writer = new StreamWriter("Lut/FPSinLut.cs"))
			{
				writer.Write(
					@"partial struct FPSinLut 
				{
				     public static readonly long[] SinLut = new[] 
				     {");
				int lineCounter = 0;
				for (int i = 0; i < FPConstInternal.LUT_SIZE; ++i)
				{
					var angle = i * Math.PI * 0.5 / (FPConstInternal.LUT_SIZE - 1);
					if (lineCounter++ % 8 == 0)
					{
						writer.WriteLine();
						writer.Write("        ");
					}

					FP sin = Math.Sin(angle);
					var scaledValue = sin.scaledValue;
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
			using (var writer = new StreamWriter("Lut/FPTanLut.cs"))
			{
				writer.Write(
					@"partial struct Fix64 
				{
				     public static readonly long[] TanLut = new[] 
				     {");
				int lineCounter = 0;
				for (int i = 0; i < FPConstInternal.LUT_SIZE; ++i)
				{
					var angle = i * Math.PI * 0.5 / (FPConstInternal.LUT_SIZE - 1);
					if (lineCounter++ % 8 == 0)
					{
						writer.WriteLine();
						writer.Write("        ");
					}

					var tan = Math.Tan(angle);
					if (tan > (double)FP.MAX_VALUE || tan < 0.0)
						tan = (double)FP.MAX_VALUE;
					FP scaledValue = (((decimal)tan > (decimal)FP.MAX_VALUE || tan < 0.0)
							? FP.MAX_VALUE
							: (FP)tan)
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
}

