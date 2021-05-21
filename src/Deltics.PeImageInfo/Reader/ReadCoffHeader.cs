
using System.Linq;


namespace Deltics.PeImageInfo.Reader
{
    public partial class PeReader
    {
        internal CoffHeader ReadCoffFileHeader()
        {
            var orgPos = GetPosition();
            
            var signature =ReadBytes(4);
            if (!signature.SequenceEqual(PeImage.PE_SIGNATURE))
                return null;
            
            return new()
            {
                Signature            = signature,
                Machine              = ReadUInt16(),
                NumberOfSections     = ReadUInt16(),
                TimeDateStamp        = ReadUInt32(),
                PointerToSymbolTable = ReadUInt32(),
                NumberOfSymbolTable  = ReadUInt32(),
                SizeOfOptionalHeader = ReadUInt16(),
                Characteristics      = ReadUInt16(),
                
                Location = orgPos,
                Size = GetPosition() - orgPos
            };
        }
    }
}