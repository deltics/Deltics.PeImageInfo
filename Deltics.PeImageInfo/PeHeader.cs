using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using Deltics.PeImageInfo.Reader;


namespace Deltics.PeImageInfo
{
    public enum Format
    {
        UNKNOWN,
        PE32,
        PE32_PLUS
    }


    public class PeHeader
    {
        internal static readonly byte[] MZ_MAGIC     = {0x4d, 0x5a};
        internal static readonly byte[] PE_SIGNATURE = {0x50, 0x45, 0x00, 0x00};

        public PeReader Reader { get; private set; }

        public DosHeader               DosHeader      { get; internal set; }
        public CoffHeader              CoffHeader     { get; internal set; }
        public OptionalHeader          OptionalHeader { get; internal set; }
        public ImmutableArray<Section> Sections       { get; }

        public Format HeaderFormat { get; internal set; }
        public bool   IsPE32       => HeaderFormat == Format.PE32;
        public bool   IsPE32Plus   => HeaderFormat == Format.PE32_PLUS;
        public bool   IsValid      => HeaderFormat != Format.UNKNOWN;


        public PeHeader(Stream stream)
        {
            Reader = new PeReader(stream);

            Reader.SetPosition(0);

            DosHeader = Reader.ReadDosHeader();
            if (!DosHeader.IsValid)
                return;

            Reader.SetPosition(DosHeader.HeaderAddress);

            CoffHeader     = Reader.ReadCoffFileHeader();
            OptionalHeader = Reader.ReadOptionalHeader();

            HeaderFormat = OptionalHeader?.Magic switch
            {
                CoffHeader.Magic.Pe32     => Format.PE32,
                CoffHeader.Magic.Pe32Plus => Format.PE32_PLUS,
                _                         => Format.UNKNOWN
            };

            Reader.SetPosition(CoffHeader.Location + CoffHeader.Size + CoffHeader.SizeOfOptionalHeader);

            Sections = Reader.ReadSectionTable(CoffHeader.NumberOfSections);
        }


        public override string ToString()
        {
            // ReSharper disable once HeapView.ObjectAllocation.Evident
            var builder = new StringBuilder(1024);
            
            builder.AppendLine("DOS Header is Valid: " + (DosHeader.IsValid ? "YES" : "no"));
            builder.AppendLine("PE Header is Valid: " + (CoffHeader != null ? "YES" : "no"));
            builder.AppendLine("Number of Sections: " + CoffHeader.NumberOfSections);

            foreach (var section in Sections)
                builder.AppendLine($"Section #{section.SectionNumber} {section.Name} -> {section.PointerToRawData}");

            return builder.ToString();
        }
    }
}