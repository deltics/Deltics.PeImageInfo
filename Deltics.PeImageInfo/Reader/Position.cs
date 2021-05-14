
namespace Deltics.PeImageInfo.Reader
{
    public partial class PeReader
    {
        public ulong GetPosition()
        {
            return (ulong) BaseStream.Position;
        }

        
        public void SetPosition(ulong position)
        {
            BaseStream.Position = (long) position;
        }

        
        public void SetPosition(ulong basePosition, ulong offset)
        {
            BaseStream.Position = (long) (basePosition + offset);
        }
    }
}