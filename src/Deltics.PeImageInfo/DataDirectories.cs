namespace Deltics.PeImageInfo
{
    public struct DataDirectory
    {
        public uint Address { get; internal set; }
        public uint Size    { get; internal set; }
    }

    
    public class DataDirectories
    {
        public DataDirectory ExportDataDirectory         { get; internal set; }
        public DataDirectory ImportDataDirectory         { get; internal set; }
        public DataDirectory ResourceDataDirectory       { get; internal set; }
        public DataDirectory ExceptionDataDirectory      { get; internal set; }
        public DataDirectory CertificateDataDirectory    { get; internal set; }
        public DataDirectory BaseRelocationDataDirectory { get; internal set; }
        public DataDirectory Debug               { get; internal set; }
        public DataDirectory ArchitectureData    { get; internal set; }
        public ulong GlobalPtr           { get; internal set; }

        public byte[] Reserved { get; internal set; }

        public DataDirectory TlsDataDirectory              { get; internal set; }
        public DataDirectory LoadConfigDataDirectory       { get; internal set; }
        public DataDirectory BoundImport           { get; internal set; }
        public DataDirectory ImportAddressDataDirectory    { get; internal set; }
        public DataDirectory DelayImportDescriptor { get; internal set; }
        public DataDirectory ClrRuntimeHeader      { get; internal set; }

        public byte[] Reserved2 { get; internal set; }
    }
}