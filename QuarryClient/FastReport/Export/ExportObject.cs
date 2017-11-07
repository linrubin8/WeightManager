using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace FastReport.Export
{
    class ExportIEMObject
    {
        #region Private fields

        private bool FHtmlTags;
        private string FText;
        private string FURL;
        private int FStyleIndex;
        private ExportIEMStyle FStyle;
        private bool FIsText;
        private bool FIsRichText;
        private float FLeft;
        private float FTop;
        private float FWidth;
        private float FHeight;
        private ExportIEMObject FParent;
        private int FCounter;
        private System.Drawing.Image FMetafile;
        private MemoryStream FPictureStream;
        private bool FIsBand;
        private List<string> FWrappedText;
        private string FHash;
        private bool FBase;
        private object FValue;
        private bool FIsNumeric;
        private bool FIsDateTime;

        private int Fx;
        private int Fy;
        private int Fdx;
        private int Fdy;
        private bool FExist;

        #endregion

        #region Private methods

        #endregion

        #region Public properties

        public bool Base
        {
            get { return FBase; }
            set { FBase = value; }
        }

        public string Hash
        {
            get { return FHash; }
            set { FHash = value; }
        }

        public bool HtmlTags
        {
            get { return FHtmlTags; }
            set { FHtmlTags = value; }
        }
        public string Text
        {
            get { return FText; }
            set { FText = value; }
        }
        public List<string> WrappedText
        {
            get { return FWrappedText; }
            set { FWrappedText = value; }
        }
        public string URL
        {
            get { return FURL; }
            set { FURL = value; }
        }
        public int StyleIndex
        {
            get { return FStyleIndex; }
            set { FStyleIndex = value; }
        }
        public bool IsText
        {
            get { return FIsText; }
            set { FIsText = value; }
        }
        public bool IsRichText
        {
            get { return FIsRichText; }
            set { FIsRichText = value; }
        }
        public float Left
        {
            get { return FLeft; }
            set { FLeft = value; }
        }
        public float Top
        {
            get { return FTop; }
            set { FTop = value; }
        }
        public float Width
        {
            get { return FWidth; }
            set { FWidth = value; }
        }
        public float Height
        {
            get { return FHeight; }
            set { FHeight = value; }
        }
        public ExportIEMObject Parent
        {
            get { return FParent; }
            set { FParent = value; }
        }
        public ExportIEMStyle Style
        {
            get { return FStyle; }
            set { FStyle = value; }
        }
        public int Counter
        {
            get { return FCounter; }
            set { FCounter = value; }
        }
        public System.Drawing.Image Metafile
        {
            get { return FMetafile; }
            set { FMetafile = value; }
        }
        public MemoryStream PictureStream
        {
            get { return FPictureStream; }
            set { FPictureStream = value; }
        }
        public bool IsBand
        {
            get { return FIsBand; }
            set { FIsBand = value; }
        }
        public object Value
        {
            get { return FValue; }
            set { FValue = value; }
        }
        public bool IsNumeric
        {
            get { return FIsNumeric; }
            set { FIsNumeric = value; }
        }
        public bool IsDateTime
        {
            get { return FIsDateTime; }
            set { FIsDateTime = value; }
        }

        public int x
        {
            get { return Fx; }
            set { Fx = value; }
        }
        public int y
        {
            get { return Fy; }
            set { Fy = value; }
        }
        public int dx
        {
            get { return Fdx; }
            set { Fdx = value; }
        }
        public int dy
        {
            get { return Fdy; }
            set { Fdy = value; }
        }

        public bool Exist
        {
            get { return FExist; }
            set { FExist = value; }
        }


        #endregion

        public ExportIEMObject()
        {
            FIsText = true;
            FIsNumeric = false;
            FText = String.Empty;
            FBase = true;
        }
    }
}
