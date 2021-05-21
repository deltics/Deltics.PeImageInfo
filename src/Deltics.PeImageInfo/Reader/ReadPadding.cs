
namespace Deltics.PeImageInfo.Reader
{
    public partial class PeReader
    {
        public ushort[] ReadPadding()
        {
            return ((BaseStream.Position % 4) != 0)
                ? new[] {ReadUInt16()}
                : new ushort[] { };
        }
    }
}