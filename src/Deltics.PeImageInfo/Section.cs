namespace Deltics.PeImageInfo
{
    public class Section
    {
        public string Name                 { get; internal set; }
        public ulong  VirtualSize          { get; internal set; }
        public ulong  VirtualAddress       { get; internal set; }
        public ulong  SizeOfRawData        { get; internal set; }
        public ulong  PointerToRawData     { get; internal set; }
        public ulong  PointerToRelocations { get; internal set; }
        public ulong  PointerToLineNumbers { get; internal set; }
        public ushort NumberOfRelocations  { get; internal set; }
        public ushort NumberOfLineNumbers  { get; internal set; }
        public ulong  Characteristics      { get; internal set; }

        public int   SectionNumber { get; internal set; }
    }
}