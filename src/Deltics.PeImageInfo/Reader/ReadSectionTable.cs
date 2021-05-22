
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;


namespace Deltics.PeImageInfo.Reader
{
    public partial class PeReader
    {
        internal ImmutableArray<Section> ReadSectionTable(ushort numberOfSections)
        {
            var sections = new List<Section>();

            for (var i = 1; i <= numberOfSections; i++)
                sections.Add(ReadSection(i));

            return sections.ToImmutableArray();
        }


        private Section ReadSection(int num)
        {
            var nameBytes = ReadBytes(8);
            var name      = "";
            
            for (var i = 0; i <= 8; i++)
            {
                if (i == 8)
                {
                    name = Encoding.ASCII.GetString(nameBytes);
                }
                else if (nameBytes[i] == 0)
                {
                    name = Encoding.ASCII.GetString(nameBytes, 0, i);
                    break;
                }
            }

            return new()
            {
                SectionNumber = num,
                Name          = name,

                VirtualSize          = ReadUInt32(),
                VirtualAddress       = ReadUInt32(),
                SizeOfRawData        = ReadUInt32(),
                PointerToRawData     = ReadUInt32(),
                PointerToRelocations = ReadUInt32(),
                PointerToLineNumbers = ReadUInt32(),
                NumberOfRelocations  = ReadUInt16(),
                NumberOfLineNumbers  = ReadUInt16(),
                Characteristics      = ReadUInt32()
            };
        }
    }
}