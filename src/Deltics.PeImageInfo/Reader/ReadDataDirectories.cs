
namespace Deltics.PeImageInfo.Reader
{
    public partial class PeReader
    {
        internal DataDirectories ReadDataDirectories()
        {
            return new()
            {
                ExportDataDirectory         = ReadDataDirectory(),
                ImportDataDirectory         = ReadDataDirectory(),
                ResourceDataDirectory       = ReadDataDirectory(),
                ExceptionDataDirectory      = ReadDataDirectory(),
                CertificateDataDirectory    = ReadDataDirectory(),
                BaseRelocationDataDirectory = ReadDataDirectory(),
                Debug                       = ReadDataDirectory(),
                ArchitectureData            = ReadDataDirectory(),
                GlobalPtr                   = ReadUInt64(),
                Reserved                    = ReadBytes(4),
                TlsDataDirectory            = ReadDataDirectory(),
                LoadConfigDataDirectory     = ReadDataDirectory(),
                BoundImport                 = ReadDataDirectory(),
                ImportAddressDataDirectory  = ReadDataDirectory(),
                DelayImportDescriptor       = ReadDataDirectory(),
                ClrRuntimeHeader            = ReadDataDirectory(),
                Reserved2                   = ReadBytes(8)
            };
        }
        
        
        internal DataDirectory ReadDataDirectory()
        {
            return new()
            {
                Address = ReadUInt32(),
                Size    = ReadUInt32()
            };
        }
        
        
    }
}