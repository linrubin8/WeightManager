using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace FastReport.Export
{
    class ExportIEMPage
    {
        #region Private fields
        private float FValue;
        private bool FLandscape;
        private float FWidth;
        private float FHeight;
        private int FRawPaperSize;
        private float FLeftMargin;
        private float FTopMargin;
        private float FBottomMargin;
        private float FRightMargin;
        private MemoryStream FWatermarkPictureStream;
        #endregion

        #region Public properties
        public MemoryStream WatermarkPictureStream
        {
            get { return FWatermarkPictureStream; }
            set { FWatermarkPictureStream = value; }
        }
        public float Value
        {
            get { return FValue; }
            set { FValue = value; }
        }
        public bool Landscape
        {
            get { return FLandscape; }
            set { FLandscape = value; }
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
        public int RawPaperSize
        {
          get { return FRawPaperSize; }
          set 
          {
            if (value == 0)
              FRawPaperSize = 9;
            else
              FRawPaperSize = value; 
          }
        }
        public float LeftMargin
        {
            get { return FLeftMargin; }
            set { FLeftMargin = value; }
        }
        public float RightMargin
        {
            get { return FRightMargin; }
            set { FRightMargin = value; }
        }
        public float TopMargin
        {
            get { return FTopMargin; }
            set { FTopMargin = value; }
        }
        public float BottomMargin
        {
            get { return FBottomMargin; }
            set { FBottomMargin = value; }
        }
        #endregion
    }
}
