/*************************************************************************************
 * 描    述:  
 * 创 建 者:  czq
 * 创建时间:  2023/8/17
 * ======================================
 * 历史更新记录
 * 版本:V          修改时间:         修改人:
 * 修改内容:
 * ======================================
*************************************************************************************/

using System;

public partial class DGSplitTriangle
{
	public DGFixedPoint[] front;
	public DGFixedPoint[] back;
	public DGFixedPoint[] edgeSplit;
	public int numFront;
	public int numBack;
	public int total;
	bool frontCurrent = false;
	int frontOffset = 0;
	int backOffset = 0;

	/** Creates a new instance, assuming numAttributes attributes per triangle vertex.
	 * @param numAttributes must be >= 3 */
	public DGSplitTriangle(int numAttributes)
	{
		front = new DGFixedPoint[numAttributes * 3 * 2];
		back = new DGFixedPoint[numAttributes * 3 * 2];
		edgeSplit = new DGFixedPoint[numAttributes];
	}

	public override string ToString()
	{
		return "DGSplitTriangle [front=" + front.DGToString() + ", back=" + back.DGToString() + ", numFront=" + numFront
		       + ", numBack=" + numBack + ", total=" + total + "]";
	}

	public void setSide(bool front)
	{
		frontCurrent = front;
	}

	public bool getSide()
	{
		return frontCurrent;
	}

	public void add(DGFixedPoint[] vertex, int offset, int stride)
	{
		if (frontCurrent)
		{
			Array.Copy(vertex, offset, front, frontOffset, stride);
			frontOffset += stride;
		}
		else
		{
			Array.Copy(vertex, offset, back, backOffset, stride);
			backOffset += stride;
		}
	}

	public void reset()
	{
		frontCurrent = false;
		frontOffset = 0;
		backOffset = 0;
		numFront = 0;
		numBack = 0;
		total = 0;
	}
}