using System;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;

namespace FastReport.Utils
{
  /// <summary>
  /// Specifies the main mode of the designer's workspace.
  /// </summary>
  public enum WorkspaceMode1 
  { 
    /// <summary>
    /// Specifies selection mode.
    /// </summary>
    Select, 
    
    /// <summary>
    /// Specifies insertion mode.
    /// </summary>
    Insert, 
    
    /// <summary>
    /// Specifies drag-drop mode.
    /// </summary>
    DragDrop 
  }
  
  /// <summary>
  /// Specifies the additional mode of the designer's workspace.
  /// </summary>
  public enum WorkspaceMode2 
  { 
    /// <summary>
    /// Specifies default mode.
    /// </summary>
    None,

    /// <summary>
    /// Indicates that user moves the selected objects.
    /// </summary>
    Move,

    /// <summary>
    /// Indicates that user resizes the selected objects.
    /// </summary>
    Size,

    /// <summary>
    /// Indicates that user draw the selection rectangle.
    /// </summary>
    SelectionRect, 
    
    /// <summary>
    /// Specifies a custom mode handled by the object.
    /// </summary>
    Custom
  }

  /// <summary>
  /// Provides a data for mouse events.
  /// </summary>
  public class FRMouseEventArgs
  {
    /// <summary>
    /// The X mouse coordinate.
    /// </summary>
    public float X;

    /// <summary>
    /// The Y mouse coordinate.
    /// </summary>
    public float Y;

    /// <summary>
    /// Current state of mouse buttons.
    /// </summary>
    public MouseButtons Button;

    /// <summary>
    /// Current keyboard state.
    /// </summary>
    public Keys ModifierKeys;
    
    /// <summary>
    /// Indicates that current object was handled the mouse message.
    /// </summary>
    public bool Handled;
    
    /// <summary>
    /// The delta of the mouse movement.
    /// </summary>
    public PointF Delta;

    /// <summary>
    /// The mouse wheel delta.
    /// </summary>
    public int WheelDelta;

    /// <summary>
    /// Current cursor shape.
    /// </summary>
    public Cursor Cursor;

    /// <summary>
    /// Additional mode of the designer's workspace.
    /// </summary>
    public WorkspaceMode2 Mode;
    
    /// <summary>
    /// Current sizing point if <b>Mode</b> is set to <b>Size</b>.
    /// </summary>
    public SizingPoint SizingPoint;
    
    /// <summary>
    /// Current selection rectangle if mode is set to <b>SelectionRect</b>.
    /// </summary>
    public RectangleF SelectionRect;
    
    /// <summary>
    /// Active object that handles the mouse event.
    /// </summary>
    public ComponentBase ActiveObject;
    
    /// <summary>
    /// The source object of drag-drop operation.
    /// </summary>
    public ComponentBase DragSource
    {
        get
        {
            if (DragSources != null && DragSources.Length > 0)
                return DragSources[DragSources.Length - 1];
            return null;
        }
        set
        {
            if (value != null)
                DragSources = new ComponentBase[] { value };
            else
                DragSources = null;
        }
    }

    /// <summary>
    /// Multiple sources objects of drag-drop operation.
    /// </summary>
    public ComponentBase[] DragSources;
    
    /// <summary>
    /// The target object of drag-drop operation.
    /// </summary>
    public ComponentBase DragTarget;
    
    /// <summary>
    /// The message to show when drag source is over the object.
    /// </summary>
    public string DragMessage;
    
    /// <summary>
    /// Additional data supplied and handled by report objects.
    /// </summary>
    public object Data;
  }

  /// <summary>
  /// Specifies the sizing point used to resize an object by mouse.
  /// </summary>
  public enum SizingPoint 
  { 
    /// <summary>
    /// No sizing point.
    /// </summary>
    None, 
    
    /// <summary>
    /// Specifies left-top sizing point.
    /// </summary>
    LeftTop,

    /// <summary>
    /// Specifies left-bottom sizing point.
    /// </summary>
    LeftBottom,

    /// <summary>
    /// Specifies right-top sizing point.
    /// </summary>
    RightTop,

    /// <summary>
    /// Specifies right-bottom sizing point.
    /// </summary>
    RightBottom,

    /// <summary>
    /// Specifies top-center sizing point.
    /// </summary>
    TopCenter,

    /// <summary>
    /// Specifies bottom-center sizing point.
    /// </summary>
    BottomCenter,

    /// <summary>
    /// Specifies left-center sizing point.
    /// </summary>
    LeftCenter,

    /// <summary>
    /// Specifies right-center sizing point.
    /// </summary>
    RightCenter 
  }

  internal static class SizingPointHelper
  {
    // sizing points and its numbers:
    // LeftTop (1)      TopCenter (5)      RightTop (3)
    //
    // LeftCenter (7)                      RightCenter (8)
    //
    // LeftBottom (2)   BottomCenter (6)   RightBottom (4)

    public static SizingPoint SwapDiagonally(SizingPoint p)
    {
      int[] swap = new int[] { 0, 4, 3, 2, 1, 5, 6, 7, 8 };
      return (SizingPoint)swap[(int)p];
    }

    public static SizingPoint SwapHorizontally(SizingPoint p)
    {
      int[] swap = new int[] { 0, 3, 4, 1, 2, 5, 6, 8, 7 };
      return (SizingPoint)swap[(int)p];
    }

    public static SizingPoint SwapVertically(SizingPoint p)
    {
      int[] swap = new int[] { 0, 2, 1, 4, 3, 6, 5, 7, 8 };
      return (SizingPoint)swap[(int)p];
    }

    public static Cursor ToCursor(SizingPoint p)
    {
      Cursor[] cursors = new Cursor[] { Cursors.Default, Cursors.SizeNWSE, Cursors.SizeNESW, Cursors.SizeNESW,
        Cursors.SizeNWSE, Cursors.SizeNS, Cursors.SizeNS, Cursors.SizeWE, Cursors.SizeWE };
      return cursors[(int)p];
    }
  }

  /// <summary>
  /// Specifies a selection point used to resize an object.
  /// </summary>
  public class SelectionPoint
  {
    /// <summary>
    /// The X coordinate of the point.
    /// </summary>
    public float X;

    /// <summary>
    /// The Y coordinate of the point.
    /// </summary>
    public float Y;
    
    /// <summary>
    /// The size mode.
    /// </summary>
    public SizingPoint SizingPoint;

    /// <summary>
    /// Initializes a new instance of the <b>SelectionPoint</b> class with specified location and size mode.
    /// </summary>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <param name="pt">Size mode.</param>
    public SelectionPoint(float x, float y, SizingPoint pt)
    {
      X = x;
      Y = y;
      SizingPoint = pt;
    }
  }
}