
using System.IO;
using System.Text;


namespace Deltics.PeImageInfo.Reader
{
    public partial class PeReader : BinaryReader
    {
        private static Stream InitStream(Stream stream)
        {
            
            // BinaryReader seems to have some problems with streams that are not
            //  MemoryStreams, so if necessary, create a MemoryStream copy of
            //  the input stream to use with this reader.
            
            if (stream.GetType() != typeof(MemoryStream))
            {
                var orgPos       = stream.Position;
                var memoryStream = new MemoryStream();

                stream.Position = 0;
                stream.CopyTo(memoryStream);

                stream = memoryStream;
                stream.Position = orgPos;
            }

            return stream;
        }

        
        internal PeReader(Stream stream) : base(InitStream(stream), Encoding.Unicode, true)
        {
        }
    }
}