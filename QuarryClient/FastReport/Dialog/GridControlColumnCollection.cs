using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using FastReport.Utils;

namespace FastReport.Dialog
{
  /// <summary>
  /// Represents the collection of <b>GridControl</b> columns.
  /// </summary>
  public class GridControlColumnCollection : FRCollectionBase, IFRSerializable
  {
    /// <summary>
    /// Gets or sets a column.
    /// </summary>
    /// <param name="index">The index of a column in this collection.</param>
    /// <returns>The column with specified index.</returns>
    public GridControlColumn this[int index]  
    {
      get { return List[index] as GridControlColumn; }
      set { List[index] = value; }
    }

    /// <inheritdoc/>
    protected override void OnInsert(int index, Object value)  
    {
      base.OnInsert(index, value);
      if (Owner != null)
      {
        GridControlColumn column = value as GridControlColumn;
        (Owner as GridControl).DataGridView.Columns.Insert(index, column.Column);
      }
    }

    /// <inheritdoc/>
    protected override void OnRemove(int index, object value)
    {
      base.OnRemove(index, value);
      if (Owner != null)
      {
        GridControlColumn column = value as GridControlColumn;
        (Owner as GridControl).DataGridView.Columns.Remove(column.Column);
      }  
    }

    /// <summary>
    /// Serializes the collection.
    /// </summary>
    /// <param name="writer">Writer object.</param>
    /// <remarks>
    /// This method is for internal use only.
    /// </remarks>
    public void Serialize(FRWriter writer)
    {
      writer.ItemName = "Columns";
      foreach (GridControlColumn c in this)
      {
        writer.Write(c);
      }
    }

    /// <summary>
    /// Deserializes the collection.
    /// </summary>
    /// <param name="reader">Reader object.</param>
    /// <remarks>
    /// This method is for internal use only.
    /// </remarks>
    public void Deserialize(FRReader reader)
    {
      Clear();
      while (reader.NextItem())
      {
        GridControlColumn c = new GridControlColumn();
        reader.Read(c);
        Add(c);
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GridControlColumnCollection"/> class with default settings.
    /// </summary>
    public GridControlColumnCollection() : this(null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GridControlColumnCollection"/> class with default settings.
    /// </summary>
    /// <param name="owner">The owner of this collection.</param>
    public GridControlColumnCollection(GridControl owner) : base(owner)
    {
    }
  }
}
