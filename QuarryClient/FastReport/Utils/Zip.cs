using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace FastReport.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public class ZipArchive
    {
        string FRootFolder;
        string FErrors;
        List<ZipFileItem> FFileList;
        List<ZipLocalFile> FFileObjects;
        string FComment;

        private uint CopyStream(Stream source, Stream target)
        {
            source.Position = 0;
            int bufflength = 8192;
            uint crc = Crc32.Begin();
            byte[] buff = new byte[bufflength];            
            int i;
            while ((i = source.Read(buff, 0, bufflength)) > 0)
            {
                target.Write(buff, 0, i);
                crc = Crc32.Update(crc, buff, 0, i);
            }
            return Crc32.End(crc);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Clear()        
        {
            foreach (ZipFileItem item in FFileList)
                item.Clear();
            foreach (ZipLocalFile item in FFileObjects)
                item.Clear();
            FFileList.Clear();
            FFileObjects.Clear();            
            FErrors = "";
            FRootFolder = "";
            FComment = "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FileName"></param>
        public void AddFile(string FileName)
        {
            if (File.Exists(FileName))
            {
                FFileList.Add(new ZipFileItem(FileName));
                if (FRootFolder == String.Empty)
                    FRootFolder = Path.GetDirectoryName(FileName);
            }
            else
                FErrors += "File " + FileName + " not found\r";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DirName"></param>
        public void AddDir(string DirName)
        {
            List<string> files = new List<string>();
            files.AddRange(Directory.GetFiles(DirName));
            foreach (string file in files)
            {
                if ((File.GetAttributes(file) & FileAttributes.Hidden) != 0)
                    continue;
                AddFile(file);
            }

            List<string> folders = new List<string>();
            folders.AddRange(Directory.GetDirectories(DirName));
            foreach (string folder in folders)
            {
                if ((File.GetAttributes(folder) & FileAttributes.Hidden) != 0)
                    continue;
                AddDir(folder);
            }            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="stream"></param>
        public void AddStream(string fileName, Stream stream)
        {
            FFileList.Add(new ZipFileItem(fileName, stream));
        }

        private void AddStreamToZip(Stream stream, ZipLocalFile ZipFile)
        {
            if (stream.Length > 128)
            {
                using (DeflateStream deflate = new DeflateStream(ZipFile.FileData, CompressionMode.Compress, true))
                    ZipFile.LocalFileHeader.Crc32 = CopyStream(stream, deflate);
                ZipFile.LocalFileHeader.CompressionMethod = 8;
            }
            else
            {
                ZipFile.LocalFileHeader.Crc32 = CopyStream(stream, ZipFile.FileData);
                ZipFile.LocalFileHeader.CompressionMethod = 0;
            }
            ZipFile.LocalFileHeader.CompressedSize = (uint)ZipFile.FileData.Length;
            ZipFile.LocalFileHeader.UnCompressedSize = (uint)stream.Length;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Stream"></param>
        public void SaveToStream(Stream Stream)
        {
            ZipLocalFile ZipFile;
            ZipCentralDirectory ZipDir;
            ZipFileHeader ZipFileHeader;
            long CentralStartPos, CentralEndPos;

            for (int i = 0; i < FFileList.Count; i++)
            {
                ZipFile = new ZipLocalFile();
                using (ZipFile.FileData = new MemoryStream())
                {
                    if (FFileList[i].Disk)
                    {
                        ZipFile.LocalFileHeader.FileName = FFileList[i].Name.Replace(FRootFolder + "\\", "");
                        using (FileStream file = new FileStream(FFileList[i].Name, FileMode.Open))
                            AddStreamToZip(file, ZipFile);
                    }
                    else
                    {
                        ZipFile.LocalFileHeader.FileName = FFileList[i].Name;
                        FFileList[i].Stream.Position = 0;
                        AddStreamToZip(FFileList[i].Stream, ZipFile);
                    }
                    ZipFile.Offset = (uint)Stream.Position;
                    ZipFile.LocalFileHeader.LastModFileDate = FFileList[i].FileDateTime;
                    ZipFile.SaveToStream(Stream);
                }
                ZipFile.FileData = null;
                FFileObjects.Add(ZipFile);
            }
            
            CentralStartPos = Stream.Position;
            for (int i = 0; i < FFileObjects.Count; i++)
            {
                ZipFile = FFileObjects[i];
                ZipFileHeader = new ZipFileHeader();
                ZipFileHeader.CompressionMethod = ZipFile.LocalFileHeader.CompressionMethod;
                ZipFileHeader.LastModFileDate = ZipFile.LocalFileHeader.LastModFileDate;
                ZipFileHeader.GeneralPurpose = ZipFile.LocalFileHeader.GeneralPurpose;
                ZipFileHeader.Crc32 = ZipFile.LocalFileHeader.Crc32;
                ZipFileHeader.CompressedSize = ZipFile.LocalFileHeader.CompressedSize;
                ZipFileHeader.UnCompressedSize = ZipFile.LocalFileHeader.UnCompressedSize;
                ZipFileHeader.RelativeOffsetLocalHeader = ZipFile.Offset;
                ZipFileHeader.FileName = ZipFile.LocalFileHeader.FileName;
                ZipFileHeader.SaveToStream(Stream);
            }
            CentralEndPos = Stream.Position;
            ZipDir = new ZipCentralDirectory();
            ZipDir.TotalOfEntriesCentralDirOnDisk = (ushort)FFileList.Count;
            ZipDir.TotalOfEntriesCentralDir = (ushort)FFileList.Count;
            ZipDir.SizeOfCentralDir = (uint)(CentralEndPos - CentralStartPos);
            ZipDir.OffsetStartingDiskDir = (uint)CentralStartPos;
            ZipDir.SaveToStream(Stream);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FileName"></param>
        public void SaveToFile(string FileName)
        {
            using (FileStream file = new FileStream(FileName, FileMode.Create))
                SaveToStream(file);
        }

        /// <summary>
        /// 
        /// </summary>
        public string RootFolder
        {
            get { return FRootFolder; }
            set { FRootFolder = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Errors
        {
            get { return FErrors; }
            set { FErrors = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Comment
        {
            get { return FComment; }
            set { FComment = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int FileCount
        {
            get { return FFileList.Count; }
        }

        /// <summary>
        /// 
        /// </summary>
        public ZipArchive()
        {
            FFileList = new List<ZipFileItem>();
            FFileObjects = new List<ZipLocalFile>();
            Clear();
        }                
    }

    internal class ZipFileItem
    {
        private string FName;
        private Stream FStream;
        private bool FDisk;
        private uint FFileDateTime;

        private uint GetDosDateTime(DateTime date)
        {
            return (uint)(
                ((date.Year - 1980 & 0x7f) << 25) |
                ((date.Month & 0xF) << 21) |
                ((date.Day & 0x1F) << 16) |
                ((date.Hour & 0x1F) << 11) |
                ((date.Minute & 0x3F) << 5) |
                (date.Second >> 1));
        }        

        public string Name
        {
            get { return FName; }
            set { FName = value; }
        }

        public Stream Stream
        {
            get { return FStream; }
        }

        public bool Disk
        {
            get { return FDisk; }
            set { FDisk = value; }
        }

        public uint FileDateTime
        {
            get { return FFileDateTime; }
            set { FFileDateTime = value; }
        }

        public void Clear()
        {
            if (FStream != null)
            {
                FStream.Dispose();
                FStream = null;
            }
        }

        public ZipFileItem()
        {
            FStream = new MemoryStream();
            FFileDateTime = GetDosDateTime(DateTime.Now);
            FDisk = false;
        }

        public ZipFileItem(string fileName, Stream stream)
        {
            FStream = stream;
            FName = fileName;
            FFileDateTime = GetDosDateTime(DateTime.Now);
            FDisk = false;
        }

        public ZipFileItem(string fileName)
        {
            FName = fileName;
            FFileDateTime = GetDosDateTime(File.GetLastWriteTime(fileName));
            FDisk = true;
        }
    }

    internal class ZipLocalFileHeader
    {
        private uint  FLocalFileHeaderSignature;
        private ushort FVersion;
        private ushort FGeneralPurpose;
        private ushort FCompressionMethod;
        private uint FCrc32;
        private uint FLastModFileDate;
        private uint FCompressedSize;
        private uint FUnCompressedSize;
        private string FExtraField;
        private string FFileName;
        private ushort FFileNameLength;
        private ushort FExtraFieldLength;

        public void SaveToStream(Stream Stream)
        {
            Stream.Write(BitConverter.GetBytes(FLocalFileHeaderSignature), 0, 4);
            Stream.Write(BitConverter.GetBytes(FVersion), 0, 2);
            Stream.Write(BitConverter.GetBytes(FGeneralPurpose), 0, 2);
            Stream.Write(BitConverter.GetBytes(FCompressionMethod), 0, 2);
            Stream.Write(BitConverter.GetBytes(FLastModFileDate), 0, 4);
            Stream.Write(BitConverter.GetBytes(FCrc32), 0, 4);
            Stream.Write(BitConverter.GetBytes(FCompressedSize), 0, 4);
            Stream.Write(BitConverter.GetBytes(FUnCompressedSize), 0, 4);
            Stream.Write(BitConverter.GetBytes(FFileNameLength), 0, 2);
            Stream.Write(BitConverter.GetBytes(FExtraFieldLength), 0, 2);            
            if (FFileNameLength > 0)
                Stream.Write(Converter.StringToByteArray(FFileName), 0, FFileNameLength);
            if (FExtraFieldLength > 0)
                Stream.Write(Converter.StringToByteArray(FExtraField), 0, FExtraFieldLength);
        }

        public uint LocalFileHeaderSignature
        {
            get { return FLocalFileHeaderSignature; }
        }

        public ushort Version
        {
            get { return FVersion; }
            set { FVersion = value; }
            
        }

        public ushort GeneralPurpose
        {
            get { return FGeneralPurpose; }
            set { FGeneralPurpose = value; }
        }
    
        public ushort CompressionMethod
        {
            get { return FCompressionMethod; }
            set { FCompressionMethod =  value; }
        }

        public uint LastModFileDate
        {
            get { return FLastModFileDate; }
            set { FLastModFileDate = value; }
        }
    
        public uint Crc32
        {
            get { return FCrc32; }
            set { FCrc32 = value; }
        }

        public uint CompressedSize
        {
            get { return FCompressedSize; }
            set { FCompressedSize = value; }
        }

        public uint UnCompressedSize
        {
            get { return FUnCompressedSize; }
            set { FUnCompressedSize = value; }
        }

        public ushort FileNameLength
        {
            get { return FFileNameLength; }
            set { FFileNameLength = value; }
        }
        public ushort ExtraFieldLength
        {
            get { return FExtraFieldLength; }
            set { FExtraFieldLength = value; }
        }

        public string FileName
        {
            get { return FFileName; }
            set 
            {                
                FFileName = value.Replace('\\', '/');
                FFileNameLength = (ushort)value.Length;            
            }
        }

        public string ExtraField
        {
            get { return FExtraField; }
            set 
            {   
                FExtraField = value;
                FExtraFieldLength = (ushort)value.Length;
            }
        }

        // constructor
        public ZipLocalFileHeader()
        {
            FLocalFileHeaderSignature = 0x04034b50;
            FVersion = 20;
            FGeneralPurpose = 0;
            FCompressionMethod = 0;
            FCrc32 = 0;
            FLastModFileDate = 0;
            FCompressedSize = 0;
            FUnCompressedSize = 0;
            FExtraField = "";
            FFileName = "";
            FFileNameLength = 0;
            FExtraFieldLength = 0;
        }
    }

    internal class ZipCentralDirectory
    {
        private uint FEndOfChentralDirSignature;
        private ushort FNumberOfTheDisk;
        private ushort FTotalOfEntriesCentralDirOnDisk;
        private ushort FNumberOfTheDiskStartCentralDir;
        private ushort FTotalOfEntriesCentralDir;
        private uint FSizeOfCentralDir;
        private uint FOffsetStartingDiskDir;
        private string FComment;
        private ushort FCommentLength;

        public void SaveToStream(Stream Stream)
        {
            Stream.Write(BitConverter.GetBytes(FEndOfChentralDirSignature), 0, 4);
            Stream.Write(BitConverter.GetBytes(FNumberOfTheDisk), 0, 2);
            Stream.Write(BitConverter.GetBytes(FNumberOfTheDiskStartCentralDir), 0, 2);
            Stream.Write(BitConverter.GetBytes(FTotalOfEntriesCentralDirOnDisk), 0, 2);
            Stream.Write(BitConverter.GetBytes(FTotalOfEntriesCentralDir), 0, 2);
            Stream.Write(BitConverter.GetBytes(FSizeOfCentralDir), 0, 4);
            Stream.Write(BitConverter.GetBytes(FOffsetStartingDiskDir), 0, 4);
            Stream.Write(BitConverter.GetBytes(FCommentLength), 0, 2);
            if (FCommentLength > 0)
                Stream.Write(Converter.StringToByteArray(FComment), 0, FCommentLength);
        }

        public uint EndOfChentralDirSignature
        {
            get { return FEndOfChentralDirSignature; }
        }

        public ushort NumberOfTheDisk
        {
            get { return FNumberOfTheDisk; }
            set { FNumberOfTheDisk = value; }
        }

        public ushort NumberOfTheDiskStartCentralDir
        {
            get { return FNumberOfTheDiskStartCentralDir; }
            set { FNumberOfTheDiskStartCentralDir = value; }
        }
        
        public ushort TotalOfEntriesCentralDirOnDisk
        {
            get { return FTotalOfEntriesCentralDirOnDisk; }
            set { FTotalOfEntriesCentralDirOnDisk = value; }
        }
        
        public ushort TotalOfEntriesCentralDir
        {
            get { return FTotalOfEntriesCentralDir; }
            set { FTotalOfEntriesCentralDir = value; }
        }
        
        public uint SizeOfCentralDir
        {
            get { return FSizeOfCentralDir; }
            set { FSizeOfCentralDir = value; }
        }

        public uint OffsetStartingDiskDir
        {
            get { return FOffsetStartingDiskDir; }
            set { FOffsetStartingDiskDir = value; }
        }

        public ushort CommentLength
        {
            get { return FCommentLength; }
            set { FCommentLength = value; }
        }

        public string Comment
        {
            get { return FComment; }
            set 
            {
                FComment = value;
                FCommentLength = (ushort)value.Length;
            }
        }

        // constructor
        public ZipCentralDirectory()
        {
            FEndOfChentralDirSignature = 0x06054b50;
            FNumberOfTheDisk = 0;
            FNumberOfTheDiskStartCentralDir = 0;
            FTotalOfEntriesCentralDirOnDisk = 0;
            FTotalOfEntriesCentralDir = 0;
            FSizeOfCentralDir = 0;
            FOffsetStartingDiskDir = 0;
            FCommentLength = 0;
            FComment = "";
        }
    }

    internal class ZipFileHeader
    {
        private uint FCentralFileHeaderSignature;
        private uint FRelativeOffsetLocalHeader;
        private uint FUnCompressedSize;
        private uint FCompressedSize;
        private uint FCrc32;
        private uint FExternalFileAttribute;
        private string FExtraField;
        private string FFileComment;
        private string FFileName;
        private ushort FCompressionMethod;
        private ushort FDiskNumberStart;
        private uint FLastModFileDate;
        private ushort FVersionMadeBy;
        private ushort FGeneralPurpose;
        private ushort FFileNameLength;
        private ushort FInternalFileAttribute;
        private ushort FExtraFieldLength;
        private ushort FVersionNeeded;
        private ushort FFileCommentLength;

        public void SaveToStream(Stream Stream)
        {
            Stream.Write(BitConverter.GetBytes(FCentralFileHeaderSignature), 0, 4);
            Stream.Write(BitConverter.GetBytes(FVersionMadeBy), 0, 2);
            Stream.Write(BitConverter.GetBytes(FVersionNeeded), 0, 2);
            Stream.Write(BitConverter.GetBytes(FGeneralPurpose), 0, 2);
            Stream.Write(BitConverter.GetBytes(FCompressionMethod), 0, 2);
            Stream.Write(BitConverter.GetBytes(FLastModFileDate), 0, 4);
            Stream.Write(BitConverter.GetBytes(FCrc32), 0, 4);
            Stream.Write(BitConverter.GetBytes(FCompressedSize), 0, 4);
            Stream.Write(BitConverter.GetBytes(FUnCompressedSize), 0, 4);
            Stream.Write(BitConverter.GetBytes(FFileNameLength), 0, 2);
            Stream.Write(BitConverter.GetBytes(FExtraFieldLength), 0, 2);
            Stream.Write(BitConverter.GetBytes(FFileCommentLength), 0, 2);
            Stream.Write(BitConverter.GetBytes(FDiskNumberStart), 0, 2);
            Stream.Write(BitConverter.GetBytes(FInternalFileAttribute), 0, 2);
            Stream.Write(BitConverter.GetBytes(FExternalFileAttribute), 0, 4);
            Stream.Write(BitConverter.GetBytes(FRelativeOffsetLocalHeader), 0, 4);
            Stream.Write(Converter.StringToByteArray(FFileName), 0, FFileNameLength);
            Stream.Write(Converter.StringToByteArray(FExtraField), 0, FExtraFieldLength);
            Stream.Write(Converter.StringToByteArray(FFileComment), 0, FFileCommentLength);
        }

        public uint CentralFileHeaderSignature
        {
            get { return FCentralFileHeaderSignature; }
        }

        public ushort VersionMadeBy
        {
            get { return FVersionMadeBy; }
        }

        public ushort VersionNeeded
        {
            get { return FVersionNeeded; }
        }

        public ushort GeneralPurpose
        {
            get { return FGeneralPurpose; }
            set { FGeneralPurpose = value; }
        }

        public ushort CompressionMethod
        {
            get { return FCompressionMethod; }
            set { FCompressionMethod = value; }
        }

        public uint LastModFileDate
        {
            get { return FLastModFileDate; }
            set { FLastModFileDate = value; }
        }

        public uint Crc32
        {
            get { return FCrc32; }
            set { FCrc32 = value; }
        }

        public uint CompressedSize
        { 
            get { return FCompressedSize; }
            set { FCompressedSize = value; }
        }

        public uint UnCompressedSize
        {
            get { return FUnCompressedSize; }
            set { FUnCompressedSize =value; }
        }
        
        public ushort FileNameLength
        {
            get { return FFileNameLength; }
            set { FFileNameLength = value; }
        }

        public ushort ExtraFieldLength
        {
            get { return FExtraFieldLength; }
            set { FExtraFieldLength = value; }
        }

        public ushort FileCommentLength
        {
            get { return FFileCommentLength; }
            set { FFileCommentLength = value; }
        }

        public ushort DiskNumberStart
        {
            get { return FDiskNumberStart; }
            set { FDiskNumberStart = value; }
        }

        public ushort InternalFileAttribute
        {
            get { return FInternalFileAttribute; }
            set { FInternalFileAttribute = value; }
        }
        
        public uint ExternalFileAttribute
        {
            get { return FExternalFileAttribute; }
            set { FExternalFileAttribute = value; }
        }
        
        public uint RelativeOffsetLocalHeader
        {
            get { return FRelativeOffsetLocalHeader; }
            set { FRelativeOffsetLocalHeader = value; }
        }
        
        public string FileName
        {
            get { return FFileName; }
            set 
            { 
                FFileName = value.Replace('\\', '/');
                FFileNameLength = (ushort)value.Length;
            }
        }
        
        public string ExtraField
        {
            get { return FExtraField; }
            set 
            { 
                FExtraField = value;
                FExtraFieldLength = (ushort)value.Length; 
            }
        }
        
        public string FileComment
        {
            get { return FFileComment; }
            set
            {
                FFileComment = value;
                FFileCommentLength = (ushort)value.Length;
            }
        }

        // constructor
        public ZipFileHeader()
        {
            FCentralFileHeaderSignature = 0x02014b50;
            FRelativeOffsetLocalHeader = 0;
            FUnCompressedSize = 0;
            FCompressedSize = 0;
            FCrc32 = 0;
            FExternalFileAttribute = 0;
            FExtraField = "";
            FFileComment = "";
            FFileName = "";
            FCompressionMethod = 0;
            FDiskNumberStart = 0;
            FLastModFileDate = 0;
            FVersionMadeBy = 20;
            FGeneralPurpose = 0;
            FFileNameLength = 0;
            FInternalFileAttribute = 0;
            FExtraFieldLength = 0;
            FVersionNeeded = 20;
            FFileCommentLength = 0;
        }
    }

    internal class ZipLocalFile
    {
        ZipLocalFileHeader FLocalFileHeader;
        MemoryStream FFileData;
        uint FOffset;

        public void SaveToStream(Stream Stream)
        {
            FLocalFileHeader.SaveToStream(Stream);
            FFileData.Position = 0;
            FFileData.WriteTo(Stream);
            FFileData.Dispose();
            FFileData = null;
        }

        public ZipLocalFileHeader LocalFileHeader
        {
            get { return FLocalFileHeader; }
        }

        public MemoryStream FileData
        {
            get { return FFileData; }
            set { FFileData = value; }
        }

        public uint Offset
        {
            get { return FOffset; }
            set { FOffset = value; }
        }

        public void Clear()
        {
            if (FFileData != null)
            {
                FFileData.Dispose();
                FFileData = null;
            }
        }

        // constructor
        public ZipLocalFile()
        {
            FLocalFileHeader = new ZipLocalFileHeader();
            FOffset = 0;
        }
    }

}