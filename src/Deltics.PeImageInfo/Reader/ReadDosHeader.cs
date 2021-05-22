
namespace Deltics.PeImageInfo.Reader
{
    public partial class PeReader
    {
        internal DosHeader ReadDosHeader()
        {
            return new()
            {
                Magic                      = ReadBytes(2),
                BytesOnLastPage            = ReadUInt16(),
                PagesInFile                = ReadUInt16(),
                Relocations                = ReadUInt16(),
                SizeOfParagraphHeaders     = ReadUInt16(),
                MinExtraParagraphs         = ReadUInt16(),
                MaxExtraParagraphs         = ReadUInt16(),
                InitialRelativeSs          = ReadUInt16(),
                InitialSp                  = ReadUInt16(),
                Checksum                   = ReadUInt16(),
                InitialIp                  = ReadUInt16(),
                InitialRelativeCs          = ReadUInt16(),
                RelocationTableFileAddress = ReadUInt16(),
                OverlayNumber              = ReadUInt16(),
                Reserved                   = ReadBytes(8),
                OemId                      = ReadUInt16(),
                OemInfo                    = ReadUInt16(),
                Reserved2                  = ReadBytes(20),
                HeaderAddress              = ReadUInt32()
            };
        }
    }
}