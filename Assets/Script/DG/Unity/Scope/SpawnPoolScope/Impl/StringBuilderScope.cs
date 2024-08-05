using System.Text;

namespace DG
{
    public class StringBuilderScope : SpawnPoolScope<StringBuilder>
    {
        public StringBuilder stringBuilder => spawn;

        public StringBuilderScope()
        {
        }

        public StringBuilderScope(int? capacity = null)
        {
            Init(capacity);
        }

        public void Init(int? capacity = null)
        {
            if (capacity != null)
                stringBuilder.Capacity = capacity.Value;
        }

        public override void Dispose()
        {
            stringBuilder.Clear();
            base.Dispose();
        }
    }
}