/*************************************************************************************
 * ��    ��:  ����https://github.com/asik/FixedMath.Net�����滹��fix16��fix8��ʵ��
 * �� �� ��:  czq
 * ����ʱ��:  2023/5/8
 * ======================================
 * ��ʷ���¼�¼
 * �汾:V          �޸�ʱ��:         �޸���:
 * �޸�����:
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