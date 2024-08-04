/*************************************************************************************
 * 描    述:  
 * 创 建 者:  czq
 * 创建时间:  2023/5/11
 * ======================================
 * 历史更新记录
 * 版本:V          修改时间:         修改人:
 * 修改内容:
 * ======================================
*************************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
#if UNITY_STANDALONE
using Debug = UnityEngine.Debug;

#endif

namespace DG
{
	public class FPTest
	{
		long[] m_testCases = new[]
		{
			// Small numbers
			0L, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
			-1, -2, -3, -4, -5, -6, -7, -8, -9, -10,

			// Integer numbers
			0x100000000, -0x100000000, 0x200000000, -0x200000000, 0x300000000, -0x300000000,
			0x400000000, -0x400000000, 0x500000000, -0x500000000, 0x600000000, -0x600000000,

			// Fractions (1/2, 1/4, 1/8)
			0x80000000, -0x80000000, 0x40000000, -0x40000000, 0x20000000, -0x20000000,

			// Problematic carry
			0xFFFFFFFF, -0xFFFFFFFF, 0x1FFFFFFFF, -0x1FFFFFFFF, 0x3FFFFFFFF, -0x3FFFFFFFF,

			// Smallest and largest values
			long.MaxValue, long.MinValue,

			// Large random numbers
			6791302811978701836, -8192141831180282065, 6222617001063736300, -7871200276881732034,
			8249382838880205112, -7679310892959748444, 7708113189940799513, -5281862979887936768,
			8220231180772321456, -5204203381295869580, 6860614387764479339, -9080626825133349457,
			6658610233456189347, -6558014273345705245, 6700571222183426493,

			// Small random numbers
			-436730658, -2259913246, 329347474, 2565801981, 3398143698, 137497017, 1060347500,
			-3457686027, 1923669753, 2891618613, 2418874813, 2899594950, 2265950765, -1962365447,
			3077934393

			// Tiny random numbers
			- 171,
			-359, 491, 844, 158, -413, -422, -737, -575, -330,
			-376, 435, -311, 116, 715, -1024, -487, 59, 724, 993
		};

		void Log(object msg)
		{
#if UNITY_STANDALONE
			DGLog.Warn(msg);
#endif
		}


		public void Precision()
		{
			Log(0.00000000023283064365386962890625m == FP.PRECISION);
		}

		public void LongToFix64AndBack()
		{
			var sources = new[]
			{
				long.MinValue, int.MinValue - 1L, int.MinValue, -1L, 0L, 1L, int.MaxValue, int.MaxValue + 1L,
				long.MaxValue
			};
			var expecteds = new[] {0L, int.MaxValue, int.MinValue, -1L, 0L, 1L, int.MaxValue, int.MinValue, -1L};
			for (int i = 0; i < sources.Length; ++i)
			{
				var expected = expecteds[i];
				FP f = sources[i];
				var actual = (long) f;
				Log(expected == actual);
			}
		}

		public void DoubleToFix64AndBack()
		{
			var sources = new[]
			{
				(double) int.MinValue,
				-(double) Math.PI,
				-(double) Math.E,
				-1.0,
				-0.0,
				0.0,
				1.0,
				(double) Math.PI,
				(double) Math.E,
				(double) int.MaxValue
			};

			foreach (var value in sources)
			{
				AreEqualWithinPrecision(value, (FP) value);
			}
		}

		void AreEqualWithinPrecision(decimal value1, decimal value2)
		{
			Log(Math.Abs(value2 - value1) < (decimal) FP.PRECISION);
		}

		void AreEqualWithinPrecision(double value1, double value2)
		{
			Log(Math.Abs(value2 - value1) < (double) FP.PRECISION);
		}

		public void DecimalToFix64AndBack()
		{
			FP t = 2f;
			Log(FP.MAX_VALUE ==  (decimal) FP.MAX_VALUE);
			Log(FP.MIN_VALUE ==  (decimal) FP.MIN_VALUE);

			var sources = new[]
			{
				int.MinValue,
				-(decimal) Math.PI,
				-(decimal) Math.E,
				-1.0m,
				-0.0m,
				0.0m,
				1.0m,
				(decimal) Math.PI,
				(decimal) Math.E,
				int.MaxValue
			};

			foreach (var value in sources)
			{
				AreEqualWithinPrecision(value, (FP) value);
			}
		}

		public void Addition()
		{
			var terms1 = new[]
			{
				FP.MIN_VALUE,  (-1), FP.ZERO, FP.ONE, FP.MAX_VALUE
			};
			FP[] terms2 = new FP[]
				{ -1,  2, -1.5m, -2, FP.ONE};
			FP[] expecteds = new FP[]
			{
				FP.MIN_VALUE, FP.ONE, -1.5m, -1,
				FP.MAX_VALUE
			};
			for (int i = 0; i < terms1.Length; ++i)
			{
				var actual = terms1[i] + terms2[i];
				var expected = expecteds[i];
				Log(expected == actual);
			}
		}

		public void BasicMultiplication()
		{
			var term1s = new[] {0m, 1m, -1m, 5m, -5m, 0.5m, -0.5m, -1.0m};
			var term2s = new[] {16m, 16m, 16m, 16m, 16m, 16m, 16m, -1.0m};
			var expecteds = new[] {0L, 16, -16, 80, -80, 8, -8, 1};
			for (int i = 0; i < term1s.Length; ++i)
			{
				var expected = expecteds[i];
				var actual = (long) ( term1s[i] *  term2s[i]);
				Log(expected == actual);
			}
		}

		public void MultiplicationTestCases()
		{
			int failures = 0;
			for (int i = 0; i < m_testCases.Length; ++i)
			{
				for (int j = 0; j < m_testCases.Length; ++j)
				{
					var x = FP.CreateByScaledValue(m_testCases[i]);
					var y = FP.CreateByScaledValue(m_testCases[j]);
					var xM = (decimal) x;
					var yM = (decimal) y;
					var expected = xM * yM;
					expected =
						expected > (decimal) FP.MAX_VALUE
							? (decimal) FP.MAX_VALUE
							: expected < (decimal) FP.MIN_VALUE
								? (decimal) FP.MIN_VALUE
								: expected;
					var actual = x * y;
					var actualM = (decimal) actual;
					var maxDelta = (decimal) FP.CreateByScaledValue(1);
					if (Math.Abs(actualM - expected) > maxDelta)
					{
						//					Console.WriteLine("Failed for FromRaw({0}) * FromRaw({1}): expected {2} but got {3}",
						//						m_testCases[i],
						//						m_testCases[j],
						//						(DGFixedPoint)expected,
						//						actualM);
						++failures;
					}
				}
			}

			//		Console.WriteLine("{0} total, {1} per multiplication", sw.ElapsedMilliseconds, (double)sw.Elapsed.Milliseconds / (m_testCases.Length * m_testCases.Length));
			Log(failures < 1);
		}


		public void DivisionTestCases()
		{
			int failures = 0;
			for (int i = 0; i < m_testCases.Length; ++i)
			{
				for (int j = 0; j < m_testCases.Length; ++j)
				{
					var x = FP.CreateByScaledValue(m_testCases[i]);
					var y = FP.CreateByScaledValue(m_testCases[j]);
					var xM = (decimal) x;
					var yM = (decimal) y;

					if (m_testCases[j] == 0)
					{
						//					Assert.Throws<DivideByZeroException>(() => Ignore(x / y));
					}
					else
					{
						var expected = xM / yM;
						expected =
							expected > (decimal) FP.MAX_VALUE
								? (decimal) FP.MAX_VALUE
								: expected < (decimal) FP.MIN_VALUE
									? (decimal) FP.MIN_VALUE
									: expected;
						var actual = x / y;
						var actualM = (decimal) actual;
						var maxDelta = (decimal) FP.CreateByScaledValue(1);
						if (Math.Abs(actualM - expected) > maxDelta)
						{
							//						Console.WriteLine("Failed for FromRaw({0}) / FromRaw({1}): expected {2} but got {3}",
							//										  m_testCases[i],
							//										  m_testCases[j],
							//										  (DGFixedPoint)expected,
							//										  actualM);
							++failures;
						}
					}
				}
			}

			//		Console.WriteLine("{0} total, {1} per division", sw.ElapsedMilliseconds, (double)sw.Elapsed.Milliseconds / (m_testCases.Length * m_testCases.Length));
			DGLog.Warn(failures < 1);
		}

		public void Sign()
		{
			var sources = new[]
			{
				FP.MIN_VALUE,  (-1), FP.ZERO, FP.ONE, FP.MAX_VALUE
			};
			var expecteds = new[] {-1, -1, 0, 1, 1};
			for (int i = 0; i < sources.Length; ++i)
			{
				var actual = FP.Sign(sources[i]);
				var expected = expecteds[i];
				Log(expected == actual);
			}
		}

		public void Abs()
		{
			Log(FP.MAX_VALUE == FP.Abs(FP.MIN_VALUE));
			var sources = new[] {-1, 0, 1, int.MaxValue};
			var expecteds = new[] {1, 0, 1, int.MaxValue};
			for (int i = 0; i < sources.Length; ++i)
			{
				var actual = FP.Abs( sources[i]);
				var expected =  expecteds[i];
				Log(expected == actual);
			}
		}

		public void FastAbs()
		{
			Log(FP.MIN_VALUE == FP.FastAbs(FP.MIN_VALUE));
			var sources = new[] {-1, 0, 1, int.MaxValue};
			var expecteds = new[] {1, 0, 1, int.MaxValue};
			for (int i = 0; i < sources.Length; ++i)
			{
				var actual = FP.FastAbs( sources[i]);
				FP expected =  expecteds[i];
				Log(expected == actual);
			}
		}

		public void Floor()
		{
			var sources = new[] {-5.1m, -1, 0, 1, 5.1m};
			var expecteds = new[] {-6m, -1, 0, 1, 5m};
			for (int i = 0; i < sources.Length; ++i)
			{
				var actual = (decimal) FP.Floor( sources[i]);
				var expected = expecteds[i];
				Log(expected == actual);
			}
		}

		public void Ceiling()
		{
			var sources = new[] {-5.1m, -1, 0, 1, 5.1m};
			var expecteds = new[] {-5m, -1, 0, 1, 6m};
			for (int i = 0; i < sources.Length; ++i)
			{
				var actual = (decimal) FP.Ceiling( sources[i]);
				var expected = expecteds[i];
				Log(expected == actual);
			}

			Log(FP.MAX_VALUE == FP.Ceiling(FP.MAX_VALUE));
		}

		public void Round()
		{
			var sources = new[] {-5.5m, -5.1m, -4.5m, -4.4m, -1, 0, 1, 4.5m, 4.6m, 5.4m, 5.5m};
			var expecteds = new[] {-6m, -5m, -4m, -4m, -1, 0, 1, 4m, 5m, 5m, 6m};
			for (int i = 0; i < sources.Length; ++i)
			{
				var actual = (decimal) FP.Round( sources[i]);
				var expected = expecteds[i];
				Log(expected == actual);
			}

			Log(FP.MAX_VALUE == FP.Round(FP.MAX_VALUE));
		}

		public void Sqrt()
		{
			for (int i = 0; i < m_testCases.Length; ++i)
			{
				var f = FP.CreateByScaledValue(m_testCases[i]);
				if (FP.Sign(f) < 0)
				{
					//				Assert.Throws<ArgumentOutOfRangeException>(() => DGFixedPoint.Sqrt(f));
				}
				else
				{
					var expected = Math.Sqrt((double) f);
					var actual = (double) FP.Sqrt(f);
					var delta = (decimal) Math.Abs(expected - actual);
					Log(delta <= FP.PRECISION);
				}
			}
		}

		public void Log2()
		{
			double maxDelta = (double) (FP.PRECISION * 4);

			for (int j = 0; j < m_testCases.Length; ++j)
			{
				var b = FP.CreateByScaledValue(m_testCases[j]);

				if (b <= FP.ZERO)
				{
					//				Assert.Throws<ArgumentOutOfRangeException>(() => DGFixedPoint.Log2(b));
				}
				else
				{
					var expected = Math.Log((double) b) / Math.Log(2);
					var actual = (double) FP.Log2(b);
					var delta = Math.Abs(expected - actual);

					Log(delta <= maxDelta);
				}
			}
		}

		public void Ln()
		{
			double maxDelta = 0.00000001;

			for (int j = 0; j < m_testCases.Length; ++j)
			{
				var b = FP.CreateByScaledValue(m_testCases[j]);

				if (b <= FP.ZERO)
				{
					//				Assert.Throws<ArgumentOutOfRangeException>(() => DGFixedPoint.Ln(b));
				}
				else
				{
					var expected = Math.Log((double) b);
					var actual = (double) FP.Ln(b);
					var delta = Math.Abs(expected - actual);

					Log(delta <= maxDelta);
				}
			}
		}

		public void Pow2()
		{
			double maxDelta = 0.0000001;
			for (int i = 0; i < m_testCases.Length; ++i)
			{
				var e = FP.CreateByScaledValue(m_testCases[i]);

				var expected = Math.Min(Math.Pow(2, (double) e), (double) FP.MAX_VALUE);
				var actual = (double) FP.Pow2(e);
				var delta = Math.Abs(expected - actual);

				Log(delta <= maxDelta);
			}
		}

		public void Pow()
		{
			for (int i = 0; i < m_testCases.Length; ++i)
			{
				var b = FP.CreateByScaledValue(m_testCases[i]);

				for (int j = 0; j < m_testCases.Length; ++j)
				{
					var e = FP.CreateByScaledValue(m_testCases[j]);

					if (b == FP.ZERO && e < FP.ZERO)
					{
						//					Assert.Throws<DivideByZeroException>(() => DGFixedPoint.Pow(b, e));
					}
					else if (b < FP.ZERO && e != FP.ZERO)
					{
						//					Assert.Throws<ArgumentOutOfRangeException>(() => DGFixedPoint.Pow(b, e));
					}
					else
					{
						var expected = e == FP.ZERO ? 1 :
							b == FP.ZERO ? 0 :
							Math.Min(Math.Pow((double) b, (double) e), (double) FP.MAX_VALUE);

						// Absolute precision deteriorates with large result values, take this into account
						// Similarly, large exponents reduce precision, even if result is small.
						double maxDelta = Math.Abs((double) e) > 100000000 ? 0.5 :
							expected > 100000000 ? 10 :
							expected > 1000 ? 0.5 : 0.00001;

						var actual = (double) FP.Pow(b, e);
						var delta = Math.Abs(expected - actual);

						Log(delta <= maxDelta);
					}
				}
			}
		}


		public void Modulus()
		{
			var deltas = new List<decimal>();
			foreach (var operand1 in m_testCases)
			{
				foreach (var operand2 in m_testCases)
				{
					var f1 = FP.CreateByScaledValue(operand1);
					var f2 = FP.CreateByScaledValue(operand2);

					if (operand2 == 0)
					{
						//					Assert.Throws<DivideByZeroException>(() => Ignore(f1 / f2));
					}
					else
					{
						var d1 = (decimal) f1;
						var d2 = (decimal) f2;
						var actual = (decimal) (f1 % f2);
						var expected = d1 % d2;
						var delta = Math.Abs(expected - actual);
						deltas.Add(delta);
						Log(delta <= 60 * FP.PRECISION);
					}
				}
			}

			Console.WriteLine("MAX error: {0} ({1} times precision)", deltas.Max(),
				deltas.Max() / FP.PRECISION);
			Console.WriteLine("Average precision: {0} ({1} times precision)", deltas.Average(),
				deltas.Average() / FP.PRECISION);
			Console.WriteLine("failed: {0}%", deltas.Count(d => d > FP.PRECISION) * 100.0 / deltas.Count);
		}


		public void Sin()
		{
			Log(FP.Sin(FP.ZERO) == FP.ZERO);
			Log(FP.Sin(FP.HALF_PI) == FP.ONE);
			Log(FP.Sin(FP.PI) == FP.ZERO);
			Log(FP.Sin(FP.PI + FP.HALF_PI) == -FP.ONE);
			Log(FP.Sin(FP.TWO_PI) == FP.ZERO);
			Log(FP.Sin(-FP.HALF_PI) == -FP.ONE);
			Log(FP.Sin(-FP.PI) == FP.ZERO);
			Log(FP.Sin(-FP.PI - FP.HALF_PI) == FP.ONE);
			Log(FP.Sin(-FP.TWO_PI) == FP.ZERO);


			for (double angle = -2 * Math.PI; angle <= 2 * Math.PI; angle += 0.0001)
			{
				FP f =  angle;
				var actualF = FP.Sin(f);
				var expected = (decimal) Math.Sin(angle);
				var delta = Math.Abs(expected - (decimal) actualF);
				Log(delta <= 3 * FP.PRECISION);
			}

			var deltas = new List<decimal>();
			foreach (var val in m_testCases)
			{
				var f = FP.CreateByScaledValue(val);
				var actualF = FP.Sin(f);
				var expected = (decimal) Math.Sin((double) f);
				var delta = Math.Abs(expected - (decimal) actualF);
				Log(delta <= 0.0000001M);
			}
		}

		public void FastSin()
		{
			for (double angle = -2 * Math.PI; angle <= 2 * Math.PI; angle += 0.0001)
			{
				FP f =  angle;
				var actualF = FP.FastSin(f);
				var expected = (decimal) Math.Sin(angle);
				var delta = Math.Abs(expected - (decimal) actualF);
				Log(delta <= 50000 * FP.PRECISION);
			}

			foreach (var val in m_testCases)
			{
				var f = FP.CreateByScaledValue(val);
				var actualF = FP.FastSin(f);
				var expected = (decimal) Math.Sin((double) f);
				var delta = Math.Abs(expected - (decimal) actualF);
				Log(delta <= 0.01M);
			}
		}

		public void Acos()
		{
			var maxDelta = 0.00000001m;
			var deltas = new List<decimal>();

			Log(FP.ZERO == FP.Acos(FP.ONE));
			Log(FP.HALF_PI == FP.Acos(FP.ZERO));
			Log(FP.PI == FP.Acos(-FP.ONE));

			// Precision
			for (var x = -1.0; x < 1.0; x += 0.001)
			{
				FP xf =  x;
				var actual = (decimal) FP.Acos(xf);
				var expected = (decimal) Math.Acos((double) xf);
				var delta = Math.Abs(actual - expected);
				deltas.Add(delta);
				Log(delta <= maxDelta);
			}

			for (int i = 0; i < m_testCases.Length; ++i)
			{
				var b = FP.CreateByScaledValue(m_testCases[i]);

				if (b < -FP.ONE || b > FP.ONE)
				{
					//				Assert.Throws<ArgumentOutOfRangeException>(() => DGFixedPoint.Acos(b));
				}
				else
				{
					var expected = (decimal) Math.Acos((double) b);
					var actual = (decimal) FP.Acos(b);
					var delta = Math.Abs(expected - actual);
					deltas.Add(delta);
					Log(delta <= maxDelta);
				}
			}

			Console.WriteLine("MAX error: {0} ({1} times precision)", deltas.Max(),
				deltas.Max() / FP.PRECISION);
			Console.WriteLine("Average precision: {0} ({1} times precision)", deltas.Average(),
				deltas.Average() / FP.PRECISION);
		}

		public void Cos()
		{
			Log(FP.Cos(FP.ZERO) == FP.ONE);

			Log(FP.Cos(FP.HALF_PI) == FP.ZERO);
			Log(FP.Cos(FP.PI) == -FP.ONE);
			Log(FP.Cos(FP.PI + FP.HALF_PI) == FP.ZERO);
			Log(FP.Cos(FP.TWO_PI) == FP.ONE);

			Log(FP.Cos(-FP.HALF_PI) == -FP.ZERO);
			Log(FP.Cos(-FP.PI) == -FP.ONE);
			Log(FP.Cos(-FP.PI - FP.HALF_PI) == FP.ZERO);
			Log(FP.Cos(-FP.TWO_PI) == FP.ONE);


			for (double angle = -2 * Math.PI; angle <= 2 * Math.PI; angle += 0.0001)
			{
				FP f =  angle;
				var actualF = FP.Cos(f);
				var expected = (decimal) Math.Cos(angle);
				var delta = Math.Abs(expected - (decimal) actualF);
				Log(delta <= 3 * FP.PRECISION);
			}

			foreach (var val in m_testCases)
			{
				var f = FP.CreateByScaledValue(val);
				var actualF = FP.Cos(f);
				var expected = (decimal) Math.Cos((double) f);
				var delta = Math.Abs(expected - (decimal) actualF);
				Log(delta <= 0.0000001M);
			}
		}

		public void FastCos()
		{
			for (double angle = -2 * Math.PI; angle <= 2 * Math.PI; angle += 0.0001)
			{
				FP f =  angle;
				var actualF = FP.FastCos(f);
				var expected = (decimal) Math.Cos(angle);
				var delta = Math.Abs(expected - (decimal) actualF);
				Log(delta <= 50000 * FP.PRECISION);
			}

			foreach (var val in m_testCases)
			{
				var f = FP.CreateByScaledValue(val);
				var actualF = FP.FastCos(f);
				var expected = (decimal) Math.Cos((double) f);
				var delta = Math.Abs(expected - (decimal) actualF);
				Log(delta <= 0.01M);
			}
		}

		public void Tan()
		{
			Log(FP.Tan(FP.ZERO) == FP.ZERO);
			Log(FP.Tan(FP.PI) == FP.ZERO);
			Log(FP.Tan(-FP.PI) == FP.ZERO);

			Log(FP.Tan(FP.HALF_PI -  0.001) > FP.ZERO);
			Log(FP.Tan(FP.HALF_PI +  0.001) < FP.ZERO);
			Log(FP.Tan(-FP.HALF_PI -  0.001) > FP.ZERO);
			Log(FP.Tan(-FP.HALF_PI +  0.001) < FP.ZERO);

			for (double angle = 0; /*-2 * Math.PI;*/ angle <= 2 * Math.PI; angle += 0.0001)
			{
				FP f =  angle;
				var actualF = FP.Tan(f);
				var expected = (decimal) Math.Tan(angle);
				Log(actualF > FP.ZERO);
				//TODO figure out a real way to test this function
			}

			//foreach (var val in m_testCases) {
			//    var f = (DGFixedPoint)val;
			//    var actualF = DGFixedPoint.Tan(f);
			//    var expected = (decimal)Math.Tan((double)f);
			//    var delta = Math.Abs(expected - (decimal)actualF);
			//    Assert.True(delta <= 0.01, string.Format("Tan({0}): expected {1} but got {2}", f, expected, actualF));
			//}
		}


		public void Atan()
		{
			var maxDelta = 0.00000001m;
			var deltas = new List<decimal>();

			Log(FP.ZERO == FP.Atan(FP.ZERO));

			// Precision
			for (var x = -1.0; x < 1.0; x += 0.0001)
			{
				FP xf =  x;
				var actual = (decimal) FP.Atan(xf);
				var expected = (decimal) Math.Atan((double) xf);
				var delta = Math.Abs(actual - expected);
				deltas.Add(delta);
				Log(delta <= maxDelta);
			}

			// Scalability and edge cases
			foreach (var x in m_testCases)
			{
				FP xf =  x;
				var actual = (decimal) FP.Atan(xf);
				var expected = (decimal) Math.Atan((double) xf);
				var delta = Math.Abs(actual - expected);
				deltas.Add(delta);
				Log(delta <= maxDelta);
			}

			Console.WriteLine("MAX error: {0} ({1} times precision)", deltas.Max(),
				deltas.Max() / FP.PRECISION);
			Console.WriteLine("Average precision: {0} ({1} times precision)", deltas.Average(),
				deltas.Average() / FP.PRECISION);
		}


		public void Atan2()
		{
			var deltas = new List<decimal>();
			// Identities
			Log(FP.Atan2(FP.ZERO, -FP.ONE) == FP.PI);
			Log(FP.Atan2(FP.ZERO, FP.ZERO) == FP.ZERO);
			Log(FP.Atan2(FP.ZERO, FP.ONE) == FP.ZERO);
			Log(FP.Atan2(FP.ONE, FP.ZERO) == FP.HALF_PI);
			Log(FP.Atan2(-FP.ONE, FP.ZERO) == -FP.HALF_PI);

			// Precision
			for (var y = -1.0; y < 1.0; y += 0.01)
			{
				for (var x = -1.0; x < 1.0; x += 0.01)
				{
					FP yf =  y;
					FP xf =  x;
					var actual = FP.Atan2(yf, xf);
					var expected = (decimal) Math.Atan2((double) yf, (double) xf);
					var delta = Math.Abs((decimal) actual - expected);
					deltas.Add(delta);
					Log(delta <= 0.005M);
				}
			}

			// Scalability and edge cases
			foreach (var y in m_testCases)
			{
				foreach (var x in m_testCases)
				{
					FP yf =  y;
					FP xf =  x;
					var actual = (decimal) FP.Atan2(yf, xf);
					var expected = (decimal) Math.Atan2((double) yf, (double) xf);
					var delta = Math.Abs(actual - expected);
					deltas.Add(delta);
					Log(delta <= 0.005M);
				}
			}

			Console.WriteLine("MAX error: {0} ({1} times precision)", deltas.Max(),
				deltas.Max() / FP.PRECISION);
			Console.WriteLine("Average precision: {0} ({1} times precision)", deltas.Average(),
				deltas.Average() / FP.PRECISION);
		}


		public void Negation()
		{
			foreach (var operand1 in m_testCases)
			{
				var f = FP.CreateByScaledValue(operand1);
				if (f == FP.MIN_VALUE)
				{
					Log(-f == FP.MAX_VALUE);
				}
				else
				{
					var expected = -((decimal) f);
					var actual = (decimal) (-f);
					Log(expected == actual);
				}
			}
		}

		public void EqualsTests()
		{
			foreach (var op1 in m_testCases)
			{
				foreach (var op2 in m_testCases)
				{
					var d1 = (decimal) op1;
					var d2 = (decimal) op2;
					Log(op1.Equals(op2) == d1.Equals(d2));
				}
			}
		}


		public void EqualityAndInequalityOperators()
		{
			var sources = m_testCases.Select(FP.CreateByScaledValue).ToList();
			foreach (var op1 in sources)
			{
				foreach (var op2 in sources)
				{
					var d1 = (double) op1;
					var d2 = (double) op2;
					Log((op1 == op2) == (d1 == d2));
					Log((op1 != op2) == (d1 != d2));
					Log(!((op1 == op2) && (op1 != op2)));
				}
			}
		}

		public void CompareTo()
		{
			var nums = m_testCases.Select(FP.CreateByScaledValue).ToArray();
			var numsDecimal = nums.Select(t => (decimal) t).ToArray();
			Array.Sort(nums);
			Array.Sort(numsDecimal);
			Log(nums.Select(t => (decimal) t).SequenceEqual(numsDecimal));
		}
	}
}