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
	public class DGFixedPointTest
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
			Debug.LogWarning(msg);
#endif
		}


		public void Precision()
		{
			Log(0.00000000023283064365386962890625m == DGFixedPoint.Precision);
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
				var f = (DGFixedPoint) sources[i];
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
				AreEqualWithinPrecision(value, (double) (DGFixedPoint) value);
			}
		}

		void AreEqualWithinPrecision(decimal value1, decimal value2)
		{
			Log(Math.Abs(value2 - value1) < (decimal) DGFixedPoint.Precision);
		}

		void AreEqualWithinPrecision(double value1, double value2)
		{
			Log(Math.Abs(value2 - value1) < (double) DGFixedPoint.Precision);
		}

		public void DecimalToFix64AndBack()
		{
			Log(DGFixedPoint.MaxValue == (DGFixedPoint) (decimal) DGFixedPoint.MaxValue);
			Log(DGFixedPoint.MinValue == (DGFixedPoint) (decimal) DGFixedPoint.MinValue);

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
				AreEqualWithinPrecision(value, (decimal) (DGFixedPoint) value);
			}
		}

		public void Addition()
		{
			var terms1 = new[]
			{
				DGFixedPoint.MinValue, (DGFixedPoint) (-1), DGFixedPoint.Zero, DGFixedPoint.One, DGFixedPoint.MaxValue
			};
			var terms2 = new[]
				{(DGFixedPoint) (-1), (DGFixedPoint) 2, (DGFixedPoint) (-1.5m), (DGFixedPoint) (-2), DGFixedPoint.One};
			var expecteds = new[]
			{
				DGFixedPoint.MinValue, DGFixedPoint.One, (DGFixedPoint) (-1.5m), (DGFixedPoint) (-1),
				DGFixedPoint.MaxValue
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
				var actual = (long) ((DGFixedPoint) term1s[i] * (DGFixedPoint) term2s[i]);
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
					var x = DGFixedPoint.CreateByScaledValue(m_testCases[i]);
					var y = DGFixedPoint.CreateByScaledValue(m_testCases[j]);
					var xM = (decimal) x;
					var yM = (decimal) y;
					var expected = xM * yM;
					expected =
						expected > (decimal) DGFixedPoint.MaxValue
							? (decimal) DGFixedPoint.MaxValue
							: expected < (decimal) DGFixedPoint.MinValue
								? (decimal) DGFixedPoint.MinValue
								: expected;
					var actual = x * y;
					var actualM = (decimal) actual;
					var maxDelta = (decimal) DGFixedPoint.CreateByScaledValue(1);
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
					var x = DGFixedPoint.CreateByScaledValue(m_testCases[i]);
					var y = DGFixedPoint.CreateByScaledValue(m_testCases[j]);
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
							expected > (decimal) DGFixedPoint.MaxValue
								? (decimal) DGFixedPoint.MaxValue
								: expected < (decimal) DGFixedPoint.MinValue
									? (decimal) DGFixedPoint.MinValue
									: expected;
						var actual = x / y;
						var actualM = (decimal) actual;
						var maxDelta = (decimal) DGFixedPoint.CreateByScaledValue(1);
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
			Debug.LogWarning(failures < 1);
		}

		public void Sign()
		{
			var sources = new[]
			{
				DGFixedPoint.MinValue, (DGFixedPoint) (-1), DGFixedPoint.Zero, DGFixedPoint.One, DGFixedPoint.MaxValue
			};
			var expecteds = new[] {-1, -1, 0, 1, 1};
			for (int i = 0; i < sources.Length; ++i)
			{
				var actual = DGFixedPoint.Sign(sources[i]);
				var expected = expecteds[i];
				Log(expected == actual);
			}
		}

		public void Abs()
		{
			Log(DGFixedPoint.MaxValue == DGFixedPoint.Abs(DGFixedPoint.MinValue));
			var sources = new[] {-1, 0, 1, int.MaxValue};
			var expecteds = new[] {1, 0, 1, int.MaxValue};
			for (int i = 0; i < sources.Length; ++i)
			{
				var actual = DGFixedPoint.Abs((DGFixedPoint) sources[i]);
				var expected = (DGFixedPoint) expecteds[i];
				Log(expected == actual);
			}
		}

		public void FastAbs()
		{
			Log(DGFixedPoint.MinValue == DGFixedPoint.FastAbs(DGFixedPoint.MinValue));
			var sources = new[] {-1, 0, 1, int.MaxValue};
			var expecteds = new[] {1, 0, 1, int.MaxValue};
			for (int i = 0; i < sources.Length; ++i)
			{
				var actual = DGFixedPoint.FastAbs((DGFixedPoint) sources[i]);
				var expected = (DGFixedPoint) expecteds[i];
				Log(expected == actual);
			}
		}

		public void Floor()
		{
			var sources = new[] {-5.1m, -1, 0, 1, 5.1m};
			var expecteds = new[] {-6m, -1, 0, 1, 5m};
			for (int i = 0; i < sources.Length; ++i)
			{
				var actual = (decimal) DGFixedPoint.Floor((DGFixedPoint) sources[i]);
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
				var actual = (decimal) DGFixedPoint.Ceiling((DGFixedPoint) sources[i]);
				var expected = expecteds[i];
				Log(expected == actual);
			}

			Log(DGFixedPoint.MaxValue == DGFixedPoint.Ceiling(DGFixedPoint.MaxValue));
		}

		public void Round()
		{
			var sources = new[] {-5.5m, -5.1m, -4.5m, -4.4m, -1, 0, 1, 4.5m, 4.6m, 5.4m, 5.5m};
			var expecteds = new[] {-6m, -5m, -4m, -4m, -1, 0, 1, 4m, 5m, 5m, 6m};
			for (int i = 0; i < sources.Length; ++i)
			{
				var actual = (decimal) DGFixedPoint.Round((DGFixedPoint) sources[i]);
				var expected = expecteds[i];
				Log(expected == actual);
			}

			Log(DGFixedPoint.MaxValue == DGFixedPoint.Round(DGFixedPoint.MaxValue));
		}

		public void Sqrt()
		{
			for (int i = 0; i < m_testCases.Length; ++i)
			{
				var f = DGFixedPoint.CreateByScaledValue(m_testCases[i]);
				if (DGFixedPoint.Sign(f) < 0)
				{
					//				Assert.Throws<ArgumentOutOfRangeException>(() => DGFixedPoint.Sqrt(f));
				}
				else
				{
					var expected = Math.Sqrt((double) f);
					var actual = (double) DGFixedPoint.Sqrt(f);
					var delta = (decimal) Math.Abs(expected - actual);
					Log(delta <= DGFixedPoint.Precision);
				}
			}
		}

		public void Log2()
		{
			double maxDelta = (double) (DGFixedPoint.Precision * 4);

			for (int j = 0; j < m_testCases.Length; ++j)
			{
				var b = DGFixedPoint.CreateByScaledValue(m_testCases[j]);

				if (b <= DGFixedPoint.Zero)
				{
					//				Assert.Throws<ArgumentOutOfRangeException>(() => DGFixedPoint.Log2(b));
				}
				else
				{
					var expected = Math.Log((double) b) / Math.Log(2);
					var actual = (double) DGFixedPoint.Log2(b);
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
				var b = DGFixedPoint.CreateByScaledValue(m_testCases[j]);

				if (b <= DGFixedPoint.Zero)
				{
					//				Assert.Throws<ArgumentOutOfRangeException>(() => DGFixedPoint.Ln(b));
				}
				else
				{
					var expected = Math.Log((double) b);
					var actual = (double) DGFixedPoint.Ln(b);
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
				var e = DGFixedPoint.CreateByScaledValue(m_testCases[i]);

				var expected = Math.Min(Math.Pow(2, (double) e), (double) DGFixedPoint.MaxValue);
				var actual = (double) DGFixedPoint.Pow2(e);
				var delta = Math.Abs(expected - actual);

				Log(delta <= maxDelta);
			}
		}

		public void Pow()
		{
			for (int i = 0; i < m_testCases.Length; ++i)
			{
				var b = DGFixedPoint.CreateByScaledValue(m_testCases[i]);

				for (int j = 0; j < m_testCases.Length; ++j)
				{
					var e = DGFixedPoint.CreateByScaledValue(m_testCases[j]);

					if (b == DGFixedPoint.Zero && e < DGFixedPoint.Zero)
					{
						//					Assert.Throws<DivideByZeroException>(() => DGFixedPoint.Pow(b, e));
					}
					else if (b < DGFixedPoint.Zero && e != DGFixedPoint.Zero)
					{
						//					Assert.Throws<ArgumentOutOfRangeException>(() => DGFixedPoint.Pow(b, e));
					}
					else
					{
						var expected = e == DGFixedPoint.Zero ? 1 :
							b == DGFixedPoint.Zero ? 0 :
							Math.Min(Math.Pow((double) b, (double) e), (double) DGFixedPoint.MaxValue);

						// Absolute precision deteriorates with large result values, take this into account
						// Similarly, large exponents reduce precision, even if result is small.
						double maxDelta = Math.Abs((double) e) > 100000000 ? 0.5 :
							expected > 100000000 ? 10 :
							expected > 1000 ? 0.5 : 0.00001;

						var actual = (double) DGFixedPoint.Pow(b, e);
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
					var f1 = DGFixedPoint.CreateByScaledValue(operand1);
					var f2 = DGFixedPoint.CreateByScaledValue(operand2);

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
						Log(delta <= 60 * DGFixedPoint.Precision);
					}
				}
			}

			Console.WriteLine("Max error: {0} ({1} times precision)", deltas.Max(),
				deltas.Max() / DGFixedPoint.Precision);
			Console.WriteLine("Average precision: {0} ({1} times precision)", deltas.Average(),
				deltas.Average() / DGFixedPoint.Precision);
			Console.WriteLine("failed: {0}%", deltas.Count(d => d > DGFixedPoint.Precision) * 100.0 / deltas.Count);
		}


		public void Sin()
		{
			Log(DGFixedPoint.Sin(DGFixedPoint.Zero) == DGFixedPoint.Zero);
			Log(DGFixedPoint.Sin(DGFixedPoint.HalfPi) == DGFixedPoint.One);
			Log(DGFixedPoint.Sin(DGFixedPoint.Pi) == DGFixedPoint.Zero);
			Log(DGFixedPoint.Sin(DGFixedPoint.Pi + DGFixedPoint.HalfPi) == -DGFixedPoint.One);
			Log(DGFixedPoint.Sin(DGFixedPoint.TwoPi) == DGFixedPoint.Zero);
			Log(DGFixedPoint.Sin(-DGFixedPoint.HalfPi) == -DGFixedPoint.One);
			Log(DGFixedPoint.Sin(-DGFixedPoint.Pi) == DGFixedPoint.Zero);
			Log(DGFixedPoint.Sin(-DGFixedPoint.Pi - DGFixedPoint.HalfPi) == DGFixedPoint.One);
			Log(DGFixedPoint.Sin(-DGFixedPoint.TwoPi) == DGFixedPoint.Zero);


			for (double angle = -2 * Math.PI; angle <= 2 * Math.PI; angle += 0.0001)
			{
				var f = (DGFixedPoint) angle;
				var actualF = DGFixedPoint.Sin(f);
				var expected = (decimal) Math.Sin(angle);
				var delta = Math.Abs(expected - (decimal) actualF);
				Log(delta <= 3 * DGFixedPoint.Precision);
			}

			var deltas = new List<decimal>();
			foreach (var val in m_testCases)
			{
				var f = DGFixedPoint.CreateByScaledValue(val);
				var actualF = DGFixedPoint.Sin(f);
				var expected = (decimal) Math.Sin((double) f);
				var delta = Math.Abs(expected - (decimal) actualF);
				Log(delta <= 0.0000001M);
			}
		}

		public void FastSin()
		{
			for (double angle = -2 * Math.PI; angle <= 2 * Math.PI; angle += 0.0001)
			{
				var f = (DGFixedPoint) angle;
				var actualF = DGFixedPoint.FastSin(f);
				var expected = (decimal) Math.Sin(angle);
				var delta = Math.Abs(expected - (decimal) actualF);
				Log(delta <= 50000 * DGFixedPoint.Precision);
			}

			foreach (var val in m_testCases)
			{
				var f = DGFixedPoint.CreateByScaledValue(val);
				var actualF = DGFixedPoint.FastSin(f);
				var expected = (decimal) Math.Sin((double) f);
				var delta = Math.Abs(expected - (decimal) actualF);
				Log(delta <= 0.01M);
			}
		}

		public void Acos()
		{
			var maxDelta = 0.00000001m;
			var deltas = new List<decimal>();

			Log(DGFixedPoint.Zero == DGFixedPoint.Acos(DGFixedPoint.One));
			Log(DGFixedPoint.HalfPi == DGFixedPoint.Acos(DGFixedPoint.Zero));
			Log(DGFixedPoint.Pi == DGFixedPoint.Acos(-DGFixedPoint.One));

			// Precision
			for (var x = -1.0; x < 1.0; x += 0.001)
			{
				var xf = (DGFixedPoint) x;
				var actual = (decimal) DGFixedPoint.Acos(xf);
				var expected = (decimal) Math.Acos((double) xf);
				var delta = Math.Abs(actual - expected);
				deltas.Add(delta);
				Log(delta <= maxDelta);
			}

			for (int i = 0; i < m_testCases.Length; ++i)
			{
				var b = DGFixedPoint.CreateByScaledValue(m_testCases[i]);

				if (b < -DGFixedPoint.One || b > DGFixedPoint.One)
				{
					//				Assert.Throws<ArgumentOutOfRangeException>(() => DGFixedPoint.Acos(b));
				}
				else
				{
					var expected = (decimal) Math.Acos((double) b);
					var actual = (decimal) DGFixedPoint.Acos(b);
					var delta = Math.Abs(expected - actual);
					deltas.Add(delta);
					Log(delta <= maxDelta);
				}
			}

			Console.WriteLine("Max error: {0} ({1} times precision)", deltas.Max(),
				deltas.Max() / DGFixedPoint.Precision);
			Console.WriteLine("Average precision: {0} ({1} times precision)", deltas.Average(),
				deltas.Average() / DGFixedPoint.Precision);
		}

		public void Cos()
		{
			Log(DGFixedPoint.Cos(DGFixedPoint.Zero) == DGFixedPoint.One);

			Log(DGFixedPoint.Cos(DGFixedPoint.HalfPi) == DGFixedPoint.Zero);
			Log(DGFixedPoint.Cos(DGFixedPoint.Pi) == -DGFixedPoint.One);
			Log(DGFixedPoint.Cos(DGFixedPoint.Pi + DGFixedPoint.HalfPi) == DGFixedPoint.Zero);
			Log(DGFixedPoint.Cos(DGFixedPoint.TwoPi) == DGFixedPoint.One);

			Log(DGFixedPoint.Cos(-DGFixedPoint.HalfPi) == -DGFixedPoint.Zero);
			Log(DGFixedPoint.Cos(-DGFixedPoint.Pi) == -DGFixedPoint.One);
			Log(DGFixedPoint.Cos(-DGFixedPoint.Pi - DGFixedPoint.HalfPi) == DGFixedPoint.Zero);
			Log(DGFixedPoint.Cos(-DGFixedPoint.TwoPi) == DGFixedPoint.One);


			for (double angle = -2 * Math.PI; angle <= 2 * Math.PI; angle += 0.0001)
			{
				var f = (DGFixedPoint) angle;
				var actualF = DGFixedPoint.Cos(f);
				var expected = (decimal) Math.Cos(angle);
				var delta = Math.Abs(expected - (decimal) actualF);
				Log(delta <= 3 * DGFixedPoint.Precision);
			}

			foreach (var val in m_testCases)
			{
				var f = DGFixedPoint.CreateByScaledValue(val);
				var actualF = DGFixedPoint.Cos(f);
				var expected = (decimal) Math.Cos((double) f);
				var delta = Math.Abs(expected - (decimal) actualF);
				Log(delta <= 0.0000001M);
			}
		}

		public void FastCos()
		{
			for (double angle = -2 * Math.PI; angle <= 2 * Math.PI; angle += 0.0001)
			{
				var f = (DGFixedPoint) angle;
				var actualF = DGFixedPoint.FastCos(f);
				var expected = (decimal) Math.Cos(angle);
				var delta = Math.Abs(expected - (decimal) actualF);
				Log(delta <= 50000 * DGFixedPoint.Precision);
			}

			foreach (var val in m_testCases)
			{
				var f = DGFixedPoint.CreateByScaledValue(val);
				var actualF = DGFixedPoint.FastCos(f);
				var expected = (decimal) Math.Cos((double) f);
				var delta = Math.Abs(expected - (decimal) actualF);
				Log(delta <= 0.01M);
			}
		}

		public void Tan()
		{
			Log(DGFixedPoint.Tan(DGFixedPoint.Zero) == DGFixedPoint.Zero);
			Log(DGFixedPoint.Tan(DGFixedPoint.Pi) == DGFixedPoint.Zero);
			Log(DGFixedPoint.Tan(-DGFixedPoint.Pi) == DGFixedPoint.Zero);

			Log(DGFixedPoint.Tan(DGFixedPoint.HalfPi - (DGFixedPoint) 0.001) > DGFixedPoint.Zero);
			Log(DGFixedPoint.Tan(DGFixedPoint.HalfPi + (DGFixedPoint) 0.001) < DGFixedPoint.Zero);
			Log(DGFixedPoint.Tan(-DGFixedPoint.HalfPi - (DGFixedPoint) 0.001) > DGFixedPoint.Zero);
			Log(DGFixedPoint.Tan(-DGFixedPoint.HalfPi + (DGFixedPoint) 0.001) < DGFixedPoint.Zero);

			for (double angle = 0; /*-2 * Math.PI;*/ angle <= 2 * Math.PI; angle += 0.0001)
			{
				var f = (DGFixedPoint) angle;
				var actualF = DGFixedPoint.Tan(f);
				var expected = (decimal) Math.Tan(angle);
				Log(actualF > DGFixedPoint.Zero);
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

			Log(DGFixedPoint.Zero == DGFixedPoint.Atan(DGFixedPoint.Zero));

			// Precision
			for (var x = -1.0; x < 1.0; x += 0.0001)
			{
				var xf = (DGFixedPoint) x;
				var actual = (decimal) DGFixedPoint.Atan(xf);
				var expected = (decimal) Math.Atan((double) xf);
				var delta = Math.Abs(actual - expected);
				deltas.Add(delta);
				Log(delta <= maxDelta);
			}

			// Scalability and edge cases
			foreach (var x in m_testCases)
			{
				var xf = (DGFixedPoint) x;
				var actual = (decimal) DGFixedPoint.Atan(xf);
				var expected = (decimal) Math.Atan((double) xf);
				var delta = Math.Abs(actual - expected);
				deltas.Add(delta);
				Log(delta <= maxDelta);
			}

			Console.WriteLine("Max error: {0} ({1} times precision)", deltas.Max(),
				deltas.Max() / DGFixedPoint.Precision);
			Console.WriteLine("Average precision: {0} ({1} times precision)", deltas.Average(),
				deltas.Average() / DGFixedPoint.Precision);
		}


		public void Atan2()
		{
			var deltas = new List<decimal>();
			// Identities
			Log(DGFixedPoint.Atan2(DGFixedPoint.Zero, -DGFixedPoint.One) == DGFixedPoint.Pi);
			Log(DGFixedPoint.Atan2(DGFixedPoint.Zero, DGFixedPoint.Zero) == DGFixedPoint.Zero);
			Log(DGFixedPoint.Atan2(DGFixedPoint.Zero, DGFixedPoint.One) == DGFixedPoint.Zero);
			Log(DGFixedPoint.Atan2(DGFixedPoint.One, DGFixedPoint.Zero) == DGFixedPoint.HalfPi);
			Log(DGFixedPoint.Atan2(-DGFixedPoint.One, DGFixedPoint.Zero) == -DGFixedPoint.HalfPi);

			// Precision
			for (var y = -1.0; y < 1.0; y += 0.01)
			{
				for (var x = -1.0; x < 1.0; x += 0.01)
				{
					var yf = (DGFixedPoint) y;
					var xf = (DGFixedPoint) x;
					var actual = DGFixedPoint.Atan2(yf, xf);
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
					var yf = (DGFixedPoint) y;
					var xf = (DGFixedPoint) x;
					var actual = (decimal) DGFixedPoint.Atan2(yf, xf);
					var expected = (decimal) Math.Atan2((double) yf, (double) xf);
					var delta = Math.Abs(actual - expected);
					deltas.Add(delta);
					Log(delta <= 0.005M);
				}
			}

			Console.WriteLine("Max error: {0} ({1} times precision)", deltas.Max(),
				deltas.Max() / DGFixedPoint.Precision);
			Console.WriteLine("Average precision: {0} ({1} times precision)", deltas.Average(),
				deltas.Average() / DGFixedPoint.Precision);
		}


		public void Negation()
		{
			foreach (var operand1 in m_testCases)
			{
				var f = DGFixedPoint.CreateByScaledValue(operand1);
				if (f == DGFixedPoint.MinValue)
				{
					Log(-f == DGFixedPoint.MaxValue);
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
			var sources = m_testCases.Select(DGFixedPoint.CreateByScaledValue).ToList();
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
			var nums = m_testCases.Select(DGFixedPoint.CreateByScaledValue).ToArray();
			var numsDecimal = nums.Select(t => (decimal) t).ToArray();
			Array.Sort(nums);
			Array.Sort(numsDecimal);
			Log(nums.Select(t => (decimal) t).SequenceEqual(numsDecimal));
		}
	}
}