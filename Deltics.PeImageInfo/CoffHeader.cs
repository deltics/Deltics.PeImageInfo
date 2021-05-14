using System.Linq;

namespace Deltics.PeImageInfo
{
    public class CoffHeader
    {
        internal class Magic
        {
            internal const ushort Pe32     = 0x010b;
            internal const ushort Pe32Plus = 0x020b;
        }

        public byte[] Signature            { get; internal set; }
        public ushort Machine              { get; internal set; }
        public ushort NumberOfSections     { get; internal set; }
        public uint   TimeDateStamp        { get; internal set; }
        public uint   PointerToSymbolTable { get; internal set; }
        public uint   NumberOfSymbolTable  { get; internal set; }
        public ushort SizeOfOptionalHeader { get; internal set; }
        public ushort Characteristics      { get; internal set; }

        public bool  IsValid  => Signature.SequenceEqual(PeHeader.PE_SIGNATURE);
        public ulong Location { get; internal set; }
        public ulong Size     { get; internal set; }
    }
}