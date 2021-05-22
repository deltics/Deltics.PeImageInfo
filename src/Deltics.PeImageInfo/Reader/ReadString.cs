
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Runtime.Serialization;
using System.Text;


namespace Deltics.PeImageInfo.Reader
{
    public partial class PeReader
    {
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