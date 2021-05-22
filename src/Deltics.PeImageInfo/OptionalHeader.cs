namespace Deltics.PeImageInfo
{
    public class OptionalHeader
    {
        // Nb. Magic value is implementation dependent and so unlike the Signature
        //  field in the Header, Magic cannot be used to test for "validity"
        //  (without knowing what implementation we are testing validity for/of).

        public ushort Magic                   { get; internal set; }
        public byte   MajorLinkerVersion      { get; internal set; }
        public byte   MinorLinkerVersion      { get; internal set; }
        public uint   SizeOfCode              { get; internal set; }
        public uint   SizeOfInitializedData   { get; internal set; }
        public uint   SizeOfUninitializedData { get; internal set; }
        public uint   AddressOfEntryPoint     { get; internal set; }
        public uint   BaseOfCode              { get; internal set; }
        public uint   BaseOfData              { get; internal set; }

        // Windows Specific Fields
        public ulong ImageBase        { get; internal set; }
        public uint  SectionAlignment { get; internal set; }
        public uint  FileAlignment    { get; internal set; }

        public ushort MajorOsVersion        { get; internal set; }
        public ushort MinorOsVersion        { get; internal set; }
        public ushort MajorImageVersion     { get; internal set; }
        public ushort MinorImageVersion     { get; internal set; }
        public ushort MajorSubsystemVersion { get; internal set; }
        public ushort MinorSubsystemVersion { get; internal set; }

        public uint Win32VersionValue { get; internal set; }
        public uint SizeOfImage       { get; internal set; }
        public uint SizeOfHeaders     { get; internal set; }
        public uint Checksum          { get; internal set; }

        public ushort Subsystem          { get; internal set; }
        public ushort DllCharacteristics { get; internal set; }

        public ulong SizeOfStackReserve  { get; internal set; }
        public ulong SizeOfStackCommit   { get; internal set; }
        public ulong SizeOfHeapReserve   { get; internal set; }
        public ulong SizeOfHeapCommit    { get; internal set; }
        public uint  LoaderFlags         { get; internal set; }
        public uint  NumberOfRvaAndSizes { get; internal set; }

        public DataDirectories  DataDirectories  { get; internal set; }
    }
}