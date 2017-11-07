using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace FastReport.Map.Import.Shp
{
  /// <summary>
  /// Represents loading map data from dbf-file.
  /// </summary>
  public class DbfFileImport
  {
    #region Fields

    private string filter;
    private Stream stream;
    private MapLayer layer;
    private List<DBaseFieldDescription> fields;
    private Encoding encoding;

    #endregion // Fields

    #region Properties

    /// <summary>
    /// Gets or sets the filter string used in an open file dialog.
    /// </summary>
    public string Filter
    {
      get { return filter; }
      protected set { filter = value; }
    }

    #endregion // Properties

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="DbfFileImport"/> class.
    /// </summary>
    public DbfFileImport()
    {
      filter = GetFilter();
      fields = new List<DBaseFieldDescription>();
      encoding = Encoding.ASCII;
    }

    #endregion // Constructors

    #region Private Methods

    private void LoadFieldsDescription()
    {
      byte[] buffer = new byte[11];
      string name = "";
      string type = "";
      int length = 0;

      stream.Seek(32, SeekOrigin.Begin);
      for (int i = 0; i < 128; i++)
      {
        stream.Read(buffer, 0, 11);
        name = encoding.GetString(buffer);
        name = name.Remove(name.IndexOf("\0"));

        stream.Read(buffer, 0, 1);
        type = encoding.GetString(buffer, 0, 1);

        stream.Seek(4, SeekOrigin.Current);
        stream.Read(buffer, 0, 1);
        length = (int)buffer[0];

        fields.Add(new DBaseFieldDescription(name, type, length));

        stream.Seek(15, SeekOrigin.Current);
        stream.Read(buffer, 0, 1);
        if (buffer[0] == 0x0D)
        {
          break;
        }
        else
        {
          stream.Seek(-1, SeekOrigin.Current);
        }
      }
    }

    private string LoadField(int length)
    {
      string field = "";
      byte[] buffer = new byte[length];
      stream.Read(buffer, 0, length);
      field = encoding.GetString(buffer);
      return field;
    }

    private string LoadChar(int length)
    {
      string field = LoadField(length);
      field = field.TrimEnd(null);
      return field;
    }

    private string LoadDate(int length)
    {
      string field = LoadField(length);
      char[] f = field.ToCharArray();
      field = f[6].ToString() + f[7].ToString() + "." + f[4].ToString() + f[5].ToString() + "." + f[0].ToString() + f[1].ToString() + f[2].ToString() + f[3].ToString();
      return field;
    }

    private string LoadNumeric(int length)
    {
      string field = LoadField(length);
      field = field.Trim();
      return field;
    }

    private string LoadLogical(int length)
    {
      string field = LoadField(length).ToUpper();
      if (field == "T" || field == "Y")
      {
        field = "true";
      }
      else if (field == "F" || field == "N")
      {
        field = "false";
      }
      else
      {
        field = "";
      }
      return field;
    }

    private void LoadRecords(int numRecords, int recordLength)
    {
      for (int i = 0; i < numRecords; i++)
      {
        stream.Seek(1, SeekOrigin.Current);
        foreach (DBaseFieldDescription f in fields)
        {
          string value = "";
          switch (f.Type)
          {
            case DBaseFieldType.Char:
              value = LoadChar(f.Length);
              break;
            case DBaseFieldType.Date:
              value = LoadDate(f.Length);
              break;
            case DBaseFieldType.Numeric:
              value = LoadNumeric(f.Length);
              break;
            case DBaseFieldType.Logical:
              value = LoadLogical(f.Length);
              break;
          }

          if (i < layer.Shapes.Count)
            layer.Shapes[i].SpatialData.SetValue(f.Name, value);
        }
      }
    }

    private Encoding GetEncoding(int id)
    {
        int codepage = 0;

        #region switch
        
        switch (id)
        {
            case 1:
                codepage = 437;
                break;
            case 2:
                codepage = 850;
                break;
            case 3:
                codepage = 1252;
                break;
            case 4:
                codepage = 10000;
                break;
            case 8:
                codepage = 865;
                break;
            case 9:
                codepage = 437;
                break;
            case 10:
                codepage = 850;
                break;
            case 11:
                codepage = 437;
                break;
            case 13:
                codepage = 437;
                break;
            case 14:
                codepage = 850;
                break;
            case 15:
                codepage = 437;
                break;
            case 16:
                codepage = 850;
                break;
            case 17:
                codepage = 437;
                break;
            case 18:
                codepage = 850;
                break;
            case 19:
                codepage = 932;
                break;
            case 20:
                codepage = 850;
                break;
            case 21:
                codepage = 437;
                break;
            case 22:
                codepage = 850;
                break;
            case 23:
                codepage = 865;
                break;
            case 24:
                codepage = 437;
                break;
            case 25:
                codepage = 437;
                break;
            case 26:
                codepage = 850;
                break;
            case 27:
                codepage = 437;
                break;
            case 28:
                codepage = 863;
                break;
            case 29:
                codepage = 850;
                break;
            case 31:
                codepage = 852;
                break;
            case 34:
                codepage = 852;
                break;
            case 35:
                codepage = 852;
                break;
            case 36:
                codepage = 860;
                break;
            case 37:
                codepage = 850;
                break;
            case 38:
                codepage = 866;
                break;
            case 55:
                codepage = 850;
                break;
            case 64:
                codepage = 852;
                break;
            case 77:
                codepage = 936;
                break;
            case 78:
                codepage = 949;
                break;
            case 79:
                codepage = 950;
                break;
            case 80:
                codepage = 874;
                break;
            case 87:
                codepage = 0;
                break;
            case 88:
                codepage = 1252;
                break;
            case 89:
                codepage = 1252;
                break;
            case 100:
                codepage = 852;
                break;
            case 101:
                codepage = 866;
                break;
            case 102:
                codepage = 865;
                break;
            case 103:
                codepage = 861;
                break;
            case 104:
                codepage = 895;
                break;
            case 105:
                codepage = 620;
                break;
            case 106:
                codepage = 737;
                break;
            case 107:
                codepage = 857;
                break;
            case 108:
                codepage = 863;
                break;
            case 120:
                codepage = 950;
                break;
            case 121:
                codepage = 949;
                break;
            case 122:
                codepage = 936;
                break;
            case 123:
                codepage = 932;
                break;
            case 124:
                codepage = 874;
                break;
            case 134:
                codepage = 737;
                break;
            case 135:
                codepage = 852;
                break;
            case 136:
                codepage = 857;
                break;
            case 150:
                codepage = 10007;
                break;
            case 151:
                codepage = 10029;
                break;
            case 152:
                codepage = 10006;
                break;
            case 200:
                codepage = 1250;
                break;
            case 201:
                codepage = 1251;
                break;
            case 202:
                codepage = 1254;
                break;
            case 203:
                codepage = 1253;
                break;
            case 204:
                codepage = 1257;
                break;
            default:
                codepage = 0;
                break;
        }

        #endregion // switch

        if (codepage == 0)
        {
            return Encoding.ASCII;
        }
        else
        {
            return Encoding.GetEncoding(codepage);
        }
    }

    private void LoadFileHeader()
    {
      byte[] buffer = new byte[4];

      stream.Seek(0, SeekOrigin.Begin);
      stream.Read(buffer, 0, 1);

      stream.Seek(4, SeekOrigin.Begin);
      stream.Read(buffer, 0, 4);
      if (!BitConverter.IsLittleEndian)
      {
        Array.Reverse(buffer, 0, 4);
      }
      int numRecords = BitConverter.ToInt32(buffer, 0);

      stream.Read(buffer, 0, 2);
      if (!BitConverter.IsLittleEndian)
      {
        Array.Reverse(buffer, 0, 2);
      }
      int headerLength = (int)BitConverter.ToInt16(buffer, 0);

      stream.Read(buffer, 0, 2);
      if (!BitConverter.IsLittleEndian)
      {
        Array.Reverse(buffer, 0, 2);
      }
      int recordLength = (int)BitConverter.ToInt16(buffer, 0);

      stream.Seek(29, SeekOrigin.Begin);
      stream.Read(buffer, 0, 1);
      encoding = GetEncoding((int)buffer[0]);

      LoadFieldsDescription();
      LoadRecords(numRecords, recordLength);
    }

    private void LoadFile(MapLayer layer, Stream stream)
    {
      this.stream = stream;
      this.layer = layer;
      LoadFileHeader();
    }

    #endregion // Private Methods

    #region Protected Methods

    /// <summary>
    /// Returns a file filter for an open file dialog.
    /// </summary>
    /// <returns>String that contains a file filter.</returns>
    protected string GetFilter()
    {
      return new FastReport.Utils.MyRes("FileFilters").Get("DbfFile");
    }

    #endregion // Protected Methods

    #region Public Methods

    /// <summary>
    /// Imports the map data from a specified file into a specfied layer.
    /// </summary>
    /// <param name="layer">The MapObject for an importing map.</param>
    /// <param name="filename">The name of a file that contains map.</param>
    public void ImportFile(MapLayer layer, string filename)
    {
      using (FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
      {
        LoadFile(layer, stream);
      }
    }

    #endregion // Public Methods
  }

  /// <summary>
  /// Represents the description of dBase field.
  /// </summary>
  public class DBaseFieldDescription
  {
    #region Fields

    private string name;
    private DBaseFieldType type;
    private int length;

    #endregion // Fields

    #region Properties

    /// <summary>
    /// Gets the field name.
    /// </summary>
    public string Name
    {
      get { return name; }
    }

    /// <summary>
    /// Gets the field type.
    /// </summary>
    public DBaseFieldType Type
    {
      get { return type; }
    }

    /// <summary>
    /// Gets the field length.
    /// </summary>
    public int Length
    {
      get { return length; }
    }

    #endregion // Properties

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="DBaseFieldDescription"/> class.
    /// </summary>
    public DBaseFieldDescription()
    {
      name = "";
      type = DBaseFieldType.Char;
      length = 0;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DBaseFieldDescription"/> class with a specified parameters.
    /// </summary>
    /// <param name="name">The field name.</param>
    /// <param name="type">The field type.</param>
    /// <param name="length">The field length.</param>
    public DBaseFieldDescription(string name, DBaseFieldType type, int length)
    {
      this.name = name;
      this.type = type;
      this.length = length;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DBaseFieldDescription"/> class with a specified parameters.
    /// </summary>
    /// <param name="name">The field name.</param>
    /// <param name="type">The field type.</param>
    /// <param name="length">The field length.</param>
    public DBaseFieldDescription(string name, string type, int length)
    {
      this.name = name;
      InitType(type);
      this.length = length;
    }

    #endregion // Constructors

    #region Private Methods

    private void InitType(string type)
    {
      if (type == "C")
      {
        this.type = DBaseFieldType.Char;
      }
      else if (type == "D")
      {
        this.type = DBaseFieldType.Date;
      }
      else if (type == "N")
      {
        this.type = DBaseFieldType.Numeric;
      }
      else if (type == "L")
      {
        this.type = DBaseFieldType.Logical;
      }
      else if (type == "M")
      {
        this.type = DBaseFieldType.Memo;
      }
      else
      {
        this.type = DBaseFieldType.Char;
      }
    }

    #endregion // Private Methods
  }

  /// <summary>
  /// The type of dBase field.
  /// </summary>
  public enum DBaseFieldType
  {
    /// <summary>
    /// Character field.
    /// </summary>
    Char,

    /// <summary>
    /// Date field.
    /// </summary>
    Date,

    /// <summary>
    /// Numeric field.
    /// </summary>
    Numeric,

    /// <summary>
    /// Logical field.
    /// </summary>
    Logical,

    /// <summary>
    /// Memo field.
    /// </summary>
    Memo
  }
}
