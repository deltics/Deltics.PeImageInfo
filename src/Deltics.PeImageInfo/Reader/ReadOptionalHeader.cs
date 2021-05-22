
namespace Deltics.PeImageInfo.Reader
{
    public partial class PeReader
    {
        private ushort ReadMagic(out Format format)
        {
            var magic = ReadUInt16();

            format = magic switch
            {
                CoffHeader.Magic.Pe32     => Format.PE32,
                CoffHeader.Magic.Pe32Plus => Format.PE32_PLUS,
                _                             => Format.UNKNOWN
            };
            
            return magic;
        }


        internal OptionalHeader ReadOptionalHeader()
        {
            Format format;
            
            var magic = ReadMagic(out format);

            if (format == Format.UNKNOWN)
                return null;
            
            var header = new OptionalHeader()
            {
                Magic                   = magic,
                MajorLinkerVersion      = ReadByte(),
                MinorLinkerVersion      = ReadByte(),
                SizeOfCode              = ReadUInt32(),
                SizeOfInitializedData   = ReadUInt32(),
                SizeOfUninitializedData = ReadUInt32(),
                AddressOfEntryPoint     = ReadUInt32(),
                BaseOfCode              = ReadUInt32(),
                BaseOfData              = (format == Format.PE32_PLUS) ? ReadUInt32() : 0
            };

            header.ImageBase             = (format == Format.PE32_PLUS) ? ReadUInt64() : ReadUInt32();
            header.SectionAlignment      = ReadUInt32();
            header.FileAlignment         = ReadUInt32();
            header.MajorOsVersion        = ReadUInt16();
            header.MinorOsVersion        = ReadUInt16();
            header.MajorImageVersion     = ReadUInt16();
            header.MinorImageVersion     = ReadUInt16();
            header.MajorSubsystemVersion = ReadUInt16();
            header.MinorSubsystemVersion = ReadUInt16();
            header.Win32VersionValue     = ReadUInt32();
            header.SizeOfImage           = ReadUInt32();
            header.SizeOfHeaders         = ReadUInt32();
            header.Checksum              = ReadUInt32();
            header.Subsystem             = ReadUInt16();
            header.DllCharacteristics    = ReadUInt16();
            header.SizeOfStackReserve    = (format == Format.PE32_PLUS) ? ReadUInt64() : ReadUInt32();
            header.SizeOfStackCommit     = (format == Format.PE32_PLUS) ? ReadUInt64() : ReadUInt32();
            header.SizeOfHeapReserve     = (format == Format.PE32_PLUS) ? ReadUInt64() : ReadUInt32();
            header.SizeOfHeapCommit      = (format == Format.PE32_PLUS) ? ReadUInt64() : ReadUInt32();
            header.LoaderFlags           = ReadUInt32();
            header.NumberOfRvaAndSizes   = ReadUInt32();
            
            return header;
        }
    }
}