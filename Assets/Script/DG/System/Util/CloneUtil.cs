using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace DG
{
    public class CloneUtil
    {
        //浅复制
        public static T Clone<T>(T source)
        {
            Type type = typeof(T);
            T clone = type.CreateInstance<T>();
            for (var i = 0;
                 i < type.GetFields(BindingFlagsConst.INSTANCE_PUBLIC |
                                    BindingFlagsConst.INSTANCE_PRIVATE).Length;
                 i++)
            {
                var fieldInfo = type.GetFields(BindingFlagsConst.INSTANCE_PUBLIC |
                                               BindingFlagsConst.INSTANCE_PRIVATE)[i];
                fieldInfo.SetValue(clone, fieldInfo.GetValue(source));
            }

            return clone;
        }

        //深复制
        public static T CloneDeep<T>(T source)
        {
            if (!typeof(T).IsSerializable)
                //需要在类前面加[Serializable]
                throw new ArgumentException("The type must be serializable.", source.ToString());

            // Don't serialize a null object, simply return the default for that object
            if (ReferenceEquals(source, null)) return default;

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }
    }
}