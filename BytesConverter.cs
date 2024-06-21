using System;
using System.Text;

namespace Count_Min_Sketch
{
    public class BytesConverter
    {
        public virtual byte[] GetBytes(object data)
        {
            switch (data)
            {
                case Guid guidData:
                    return ((Guid)guidData).ToByteArray();
                case int intData:
                    return BitConverter.GetBytes(intData);
                case long longData:
                    return BitConverter.GetBytes(longData);
            }

            return Encoding.Unicode.GetBytes(data.ToString());
        }
    }
}

