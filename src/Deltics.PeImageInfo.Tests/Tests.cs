using System.IO;
using System.Linq;
using Deltics.PeImageInfo;
using FluentAssertions;
using Xunit;


namespace PeImageTests
{
    public class Tests
    {
        private static PeImage LoadArtefact(string filename)
        {
            return new (new FileStream($"artefacts/{filename}", FileMode.Open));
        }

        
        [Theory]
        [InlineData("x86.exe", true, false)]
        [InlineData("x86.dll", true, false)]
        [InlineData("x64.exe", false, true)]
        [InlineData("x64.dll", false, true)]
        public void PeImageIsOfExpectedFormat(string filename, bool isPe32, bool isPe32Plus)
        {
            var sut = LoadArtefact(filename);

            sut.IsValid.Should().BeTrue();
            
            sut.IsPE32.Should().Be(isPe32);
            sut.IsPE32Plus.Should().Be(isPe32Plus);
        }

        
        [Theory]
        [InlineData("x86.exe", ".text", ".itext", ".data", ".bss", ".idata", ".tls", ".rdata", ".reloc", ".rsrc", ".debug")]
        [InlineData("x86.dll", ".text", ".itext", ".data", ".bss", ".idata", ".reloc", ".rsrc", ".debug")]
        [InlineData("x64.exe", ".text", ".data", ".bss", ".idata", ".tls", ".rdata", ".pdata", ".rsrc", ".debug")]
        [InlineData("x64.dll", ".text", ".data", ".bss", ".idata", ".reloc", ".pdata", ".rsrc", ".debug")]
        public void PeImageHasExpectedSections(string filename, params string[] sectionNames)
        {
            var sut = LoadArtefact(filename);

            var isDebug       = sut.Sections.Any(s => s.Name == ".debug");
            var expectedNames = isDebug ? sectionNames : sectionNames[0..^1];

            sut.Sections.Length.Should().Be(expectedNames.Length);
            
            foreach (var i in Enumerable.Range(0, expectedNames.Length))
                sut.Sections[i].Name.Should().Be(expectedNames[i]);
        }
    }
}