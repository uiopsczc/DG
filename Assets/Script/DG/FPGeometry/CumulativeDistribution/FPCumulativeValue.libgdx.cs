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

namespace DG
{
    public class FPCumulativeValue<T>
    {
        public T value;
        public FP frequency;
        public FP interval;

        public FPCumulativeValue(T value, FP frequency, FP interval)
        {
            this.value = value;
            this.frequency = frequency;
            this.interval = interval;
        }
    }
}