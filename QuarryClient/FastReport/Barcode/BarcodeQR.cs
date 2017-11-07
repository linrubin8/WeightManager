using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using FastReport.Barcode.QRCode;

namespace FastReport.Barcode
{
  /// <summary>
  /// Specifies the QR code error correction level.
  /// </summary>
  public enum QRCodeErrorCorrection
  {
    /// <summary>
    /// L = ~7% correction.
    /// </summary>
    L,

    /// <summary>
    /// M = ~15% correction.
    /// </summary>
    M,

    /// <summary>
    /// Q = ~25% correction.
    /// </summary>
    Q,

    /// <summary>
    /// H = ~30% correction.
    /// </summary>
    H
  }

  /// <summary>
  /// Specifies the QR Code encoding.
  /// </summary>
  public enum QRCodeEncoding
  {
    /// <summary>
    /// UTF-8 encoding.
    /// </summary>
    UTF8,
    /// <summary>
    /// ISO 8859-1 encoding.
    /// </summary>
    ISO8859_1,
    /// <summary>
    /// Shift_JIS encoding.
    /// </summary>
    Shift_JIS,
    /// <summary>
    /// Windows-1251 encoding.
    /// </summary>
    Windows_1251,
    /// <summary>
    /// cp866 encoding.
    /// </summary>
    cp866
  }

  /// <summary>
  /// Generates the 2D QR code barcode.
  /// </summary>
  public class BarcodeQR : Barcode2DBase
  {
    #region Fields
    private QRCodeErrorCorrection FErrorCorrection;
    private QRCodeEncoding FEncoding;
    private bool FQuietZone;
    private ByteMatrix FMatrix;
    private const int PixelSize = 4;
    #endregion

    #region Properties
    /// <summary>
    /// Gets or sets the error correction.
    /// </summary>
    [DefaultValue(QRCodeErrorCorrection.L)]
    public QRCodeErrorCorrection ErrorCorrection
    {
      get { return FErrorCorrection; }
      set { FErrorCorrection = value; }
    }
    
    /// <summary>
    /// Gets or sets the encoding used for text conversion.
    /// </summary>
    [DefaultValue(QRCodeEncoding.UTF8)]
    public QRCodeEncoding Encoding
    {
      get { return FEncoding; }
      set { FEncoding = value; }
    }

    /// <summary>
    /// Gets or sets the value indicating that quiet zone must be shown.
    /// </summary>
    [DefaultValue(true)]
    public bool QuietZone
    {
      get { return FQuietZone; }
      set { FQuietZone = value; }
    }
    #endregion

    #region Private Methods
    private ErrorCorrectionLevel GetErrorCorrectionLevel()
    {
      switch (FErrorCorrection)
      {
        case QRCodeErrorCorrection.L:
          return ErrorCorrectionLevel.L;
        
        case QRCodeErrorCorrection.M:
          return ErrorCorrectionLevel.M;
        
        case QRCodeErrorCorrection.Q:
          return ErrorCorrectionLevel.Q;

        case QRCodeErrorCorrection.H:
          return ErrorCorrectionLevel.H;
      }
      
      return ErrorCorrectionLevel.L;
    }

    private string GetEncoding()
    {
      switch (FEncoding)
      {
        case QRCodeEncoding.UTF8:
          return "UTF-8";

        case QRCodeEncoding.ISO8859_1:
          return "ISO-8859-1";

        case QRCodeEncoding.Shift_JIS:
          return "Shift_JIS";

        case QRCodeEncoding.Windows_1251:
          return "Windows-1251";

        case QRCodeEncoding.cp866:
          return "cp866";
      }

      return "";
    }
    #endregion

    #region Public Methods
    /// <inheritdoc/>
    public override void Assign(BarcodeBase source)
    {
      base.Assign(source);
      BarcodeQR src = source as BarcodeQR;

      ErrorCorrection = src.ErrorCorrection;
      Encoding = src.Encoding;
      QuietZone = src.QuietZone;
    }

    internal override void Serialize(FastReport.Utils.FRWriter writer, string prefix, BarcodeBase diff)
    {
      base.Serialize(writer, prefix, diff);
      BarcodeQR c = diff as BarcodeQR;

      if (c == null || ErrorCorrection != c.ErrorCorrection)
        writer.WriteValue(prefix + "ErrorCorrection", ErrorCorrection);
      if (c == null || Encoding != c.Encoding)
        writer.WriteValue(prefix + "Encoding", Encoding);
      if (c == null || QuietZone != c.QuietZone)
        writer.WriteBool(prefix + "QuietZone", QuietZone);
    }

    internal override void Initialize(string text, bool showText, int angle, float zoom)
    {
      base.Initialize(text, showText, angle, zoom);
      FMatrix = QRCodeWriter.encode(Text, 0, 0, GetErrorCorrectionLevel(), GetEncoding(), QuietZone);
    }
    
    internal override SizeF CalcBounds()
    {
      int textAdd = ShowText ? 18 : 0;
      return new SizeF(FMatrix.Width * PixelSize, FMatrix.Height * PixelSize + textAdd);
    }

    internal override void Draw2DBarcode(Graphics g, float kx, float ky)
    {
      Brush light = Brushes.White;
      Brush dark = new SolidBrush(Color);

      for (int y = 0; y < FMatrix.Height; y++)
      {
        for (int x = 0; x < FMatrix.Width; x++)
        {
          int b = FMatrix.get_Renamed(x, y);

          Brush brush = b == 0 ? dark : light;
          g.FillRectangle(brush, x * PixelSize * kx, y * PixelSize * ky,
            PixelSize * kx, PixelSize * ky);
        }
      }
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="BarcodeQR"/> class with default settings.
    /// </summary>
    public BarcodeQR()
    {
      Encoding = QRCodeEncoding.UTF8;
      QuietZone = true;
    }
  }
}
