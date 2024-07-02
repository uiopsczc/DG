/*************************************************************************************
 * 描    述:  
 * 创 建 者:  czq
 * 创建时间:  2023/8/16
 * ======================================
 * 历史更新记录
 * 版本:V          修改时间:         修改人:
 * 修改内容:
 * ======================================
*************************************************************************************/

using System.Collections.Generic;

namespace DG
{
	/// <summary>
	/// This class represents a cumulative distribution. It can be used in scenarios where there are values with different
	/// probabilities and it's required to pick one of those respecting the probability. For example one could represent the frequency
	/// of the alphabet letters using a cumulative distribution and use it to randomly pick a letter respecting their probabilities
	/// (useful when generating random words). Another example could be point generation on a mesh surface: one could generate a
	/// cumulative distribution using triangles areas as interval size, in this way triangles with a large area will be picked more
	/// often than triangles with a smaller one. See
	/// <a href="http://en.wikipedia.org/wiki/Cumulative_distribution_function">Wikipedia</a> for a detailed explanation.
	/// @author Inferno
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public partial class FPCumulativeDistribution<T>
	{
		private List<FPCumulativeValue<T>> values;

		public FPCumulativeDistribution()
		{
			values = new List<FPCumulativeValue<T>>(10);
		}

		/** Adds a value with a given interval size to the distribution */
		public void add(T value, FP intervalSize)
		{
			values.Add(new FPCumulativeValue<T>(value, 0, intervalSize));
		}

		/** Adds a value with interval size equal to zero to the distribution */
		public void add(T value)
		{
			values.Add(new FPCumulativeValue<T>(value, 0, 0));
		}

		/** Generate the cumulative distribution */
		public void generate()
		{
			FP sum = 0;
			for (int i = 0; i < values.Count; ++i)
			{
				sum += values[i].interval;
				var d = values[i];
				d.frequency = sum;
				values[i].frequency = sum;
			}
		}

		/** Generate the cumulative distribution in [0,1] where each interval will get a frequency between [0,1] */
		public void generateNormalized()
		{
			FP sum = 0;
			for (int i = 0; i < values.Count; ++i)
			{
				sum += values[i].interval;
			}

			FP intervalSum = 0;
			for (int i = 0; i < values.Count; ++i)
			{
				intervalSum += values[i].interval / sum;
				values[i].frequency = intervalSum;
			}
		}

		/** Generate the cumulative distribution in [0,1] where each value will have the same frequency and interval size */
		public void generateUniform()
		{
			FP freq = 1f / values.Count;
			for (int i = 0; i < values.Count; ++i)
			{
				// reset the interval to the normalized frequency
				values[i].interval = freq;
				values[i].frequency = (i + 1) * freq;
			}
		}

		/** Finds the value whose interval contains the given probability Binary search algorithm is used to find the value.
		 * @param probability
		 * @return the value whose interval contains the probability */
		public T value(FP probability)
		{
			FPCumulativeValue<T> value = null;
			int imax = values.Count - 1, imin = 0, imid;
			while (imin <= imax)
			{
				imid = imin + ((imax - imin) / 2);
				value = values[imid];
				if (probability < value.frequency)
					imax = imid - 1;
				else if (probability > value.frequency)
					imin = imid + 1;
				else
					break;
			}

			return values[imin].value;
		}

		/** @return the value whose interval contains a random probability in [0,1] */
		//	public T value()
		//	{
		//		return value(MathUtils.random());
		//	}

		/** @return the amount of values */
		public int size()
		{
			return values.Count;
		}

		/** @return the interval size for the value at the given position */
		public FP getInterval(int index)
		{
			return values[index].interval;
		}

		/** @return the value at the given position */
		public T getValue(int index)
		{
			return values[index].value;
		}

		/** Set the interval size on the passed in object. The object must be present in the distribution. */
		public void setInterval(T obj, FP intervalSize)
		{
			for (int i = 0; i < values.Count; i++)
			{
				var value = values[i];
				if (value.value.Equals(obj))
				{
					value.interval = intervalSize;
					return;
				}
			}
		}

		/** Sets the interval size for the value at the given index */
		public void setInterval(int index, FP intervalSize)
		{
			values[index].interval = intervalSize;
		}

		/** Removes all the values from the distribution */
		public void clear()
		{
			values.Clear();
		}
	}
}
