using System;

namespace DG
{
    [Serializable]
    public class ValueParse
    {
        public string assembleName;

        [NonSerialized] public object tmp;

        public string typeName;
        public string value;

        public ValueParse()
        {
        }

        public ValueParse(object tmp)
        {
            this.tmp = tmp;
        }

        public object Parse()
        {
            if (tmp != null)
                return tmp;

            var targetType = TypeUtil.GetType(typeName, assembleName);
            var list = ValueParseUtil.GetValueParseList();
            for (var i = 0; i < list.Count; i++)
            {
                var hashtable = list[i];
                var type = (Type)hashtable[StringConst.STRING_TYPE];
                var parseFunc = (Delegate)hashtable[StringConst.STRING_PARSE_FUNC];
                if (type == targetType)
                    return parseFunc.DynamicInvoke(value);
            }

            return null;
        }

        public T Parse<T>()
        {
            return (T)Parse();
        }
    }
}