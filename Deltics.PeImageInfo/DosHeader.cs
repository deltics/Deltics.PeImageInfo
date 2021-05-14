using System.Linq;

namespace Deltics.PeImageInfo
{
    public struct DosHeader
    {
        public byte[] Magic                      { get; internal set; } // e_magic (2 bytes)
        public ushort BytesOnLastPage            { get; internal set; } // e_cblp
        public ushort PagesInFile                { get; internal set; } // e_cp
        public ushort Relocations                { get; internal set; } // e_crlc
        public ushort SizeOfParagraphHeaders     { get; internal set; } // e_cparhdr
        public ushort MinExtraParagraphs         { get; internal set; } // e_minalloc
        public ushort MaxExtraParagraphs         { get; internal set; } // e_maxalloc
        public ushort InitialRelativeSs          { get; internal set; } // e_ss
        public ushort InitialSp                  { get; internal set; } // e_sp
        public ushort Checksum                   { get; internal set; } // e_csum
        public ushort InitialIp                  { get; internal set; } // e_ip
        public ushort InitialRelativeCs          { get; internal set; } // e_cs
        public ushort RelocationTableFileAddress { get; internal set; } // e_lfarlc
        public ushort OverlayNumber              { get; internal set; } // e_ovno
        public byte[] Reserved                   { get; internal set; } // e_res (8 bytes)
        public ushort OemId                      { get; internal set; } // e_oemid
        public ushort OemInfo                    { get; internal set; } // e_oeminfo
        public byte[] Reserved2                  { get; internal set; } // e_res2 (20 bytes)
        public ulong  HeaderAddress              { get; internal set; } // e_lfanew

        public readonly bool IsValid => Magic.SequenceEqual(PeHeader.MZ_MAGIC);
    }
}