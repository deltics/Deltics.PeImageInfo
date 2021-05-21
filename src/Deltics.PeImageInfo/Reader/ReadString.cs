
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace Deltics.PeImageInfo.Reader
{
    public partial class PeReader
    {
        // Use this to read a string from the unicode string table within the resource
        //  data section.  Strings in this area are formatted with a leading length
        //  indicator.  After reading the string from the specified location, the
        //  reader is restored to the position it was at before the method was called.
        public string ReadStringU(ulong pos)
        {
            var result = "";
            var org    = GetPosition();
            try
            {
                SetPosition(pos);

                var len   = ReadUInt16();
                var bytes = ReadBytes(len * 2);

                result = Encoding.Unicode.GetString(bytes);
            }
            finally
            {
                SetPosition(org);
            }

            return result;
        }


        // Use this method when you do NOT know the length of the string to be read
        //  but do know that the string is null-terminated.
        public string ReadStringZ()
        {
            var    bytes = new List<byte>();
            byte[] c;
            do
            {
                c = ReadBytes(2);
                bytes.Add(c[0]);                    
                bytes.Add(c[1]);                    
            } while ((c[0] != 0) || (c[1] != 0));
            
            return Encoding.Unicode.GetString(bytes.Take(bytes.Count - 2).ToArray());
        }

        
        // Use this method when you know the length of the string to be read AND you know
        //  that the string is null-terminated.
        public string ReadStringZ(int len)
        {
            var bytes = ReadBytes((len + 1) * 2);

            return Encoding.Unicode.GetString(bytes.Take(len * 2).ToArray());
        }
    }
}