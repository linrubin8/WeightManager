using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LB.SmartClient
{
    public class FileSplitStreamReader:FileStream
    {
        private string mstrSourceFileName;
        private int isplitSize;
        public FileSplitStreamReader(string strSourceFileName)
            :base(strSourceFileName, FileMode.Open, FileAccess.Read)
        {

        }

        public FileSplitStreamReader(string strSourceFileName, int splitSize)
            :base(strSourceFileName, FileMode.Open,FileAccess.Read)
        {
            this.mstrSourceFileName = strSourceFileName;
            this.isplitSize = splitSize;
        }

        public string SourceFileName
        {
            get
            {
                return mstrSourceFileName;
            }
        }

        public int SplitSize
        {
            get
            {
                return isplitSize;
            }
        }

        public long FileSize
        {
            get
            {
                return this.Length;
            }
        }

        private long readTimes = 1;

        public long ReadTimes
        {
            get
            {
                return readTimes;
            }
        }

        private bool judge = false;
        public bool Judge
        {
            get
            {
                return judge;
            }
            set
            {
                judge = value;
            }
        }

        public long FileReadSize
        {
            get
            {
                if (isplitSize == 0)
                {
                    return 1024 * 1024 * 3;
                }
                else
                {
                    return this.FileSize - (this.FileSize / (long)this.isplitSize) * (long)this.isplitSize;
                }
            }
        }

        public int iCurrentReadSize;
        public byte[] SplitRead()
        {
            FileBlockStreamReaderArgs args = new SmartClient.FileBlockStreamReaderArgs();
            byte[] timeReadContent;
            this.Seek(isplitSize * (readTimes - 1), SeekOrigin.Begin);
            if (readTimes < (this.FileSize / this.isplitSize + 1))
            {
                args.ReadPercent = (int)(((float)this.Position / this.FileSize) * 100);
                timeReadContent = new byte[this.isplitSize];
            }
            else
            {
                timeReadContent = new byte[this.FileReadSize];
                judge = true;
                args.ReadPercent = 100;
            }
            iCurrentReadSize = timeReadContent.Length;
            this.Read(timeReadContent, 0, timeReadContent.Length);
            FileBlockReadEndEvent(args);
            readTimes++;
            return timeReadContent;
        }

        public delegate void FileBlockStreamReaderEventHandle(object sender, FileBlockStreamReaderArgs e);
        public event FileBlockStreamReaderEventHandle FileBlockStreamReaderEvent;
        public event EventHandler FinishAllReadEvent;

        public void FileBlockReadEndEvent(FileBlockStreamReaderArgs e)
        {
            if (FileBlockStreamReaderEvent != null)
            {
                this.FileBlockStreamReaderEvent(this, e);
            }
        }

        public void FileFinishAllReadEvent()
        {
            if (FileBlockStreamReaderEvent != null)
            {
                this.FinishAllReadEvent(this, new EventArgs());
            }
        }
    }

    public class FileBlockStreamReaderArgs
    {
        public FileBlockStreamReaderArgs()
        {

        }
        public int ReadPercent;
    }
}
