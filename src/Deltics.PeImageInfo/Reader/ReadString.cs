
using System;
using System.Collections.Generic;
using System.Linq;
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
            var bytes = ReadBytes(len * 2);

            var nullTerm = ReadUInt16();    // Read two additional bytes which are expected to be a null terminator
            if (nullTerm != 0)
                throw new InvalidOperationException("Expected null-terminator was not present");

            return Encoding.Unicode.GetString(bytes.Take(len * 2).ToArray());
        }
    }
}