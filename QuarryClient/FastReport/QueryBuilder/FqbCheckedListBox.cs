using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace FastReport.FastQueryBuilder
{
    class FqbCheckedListBox : CheckedListBox
    {
		private const int WM_HSCROLL = 0x114;
		private const int WM_VSCROLL = 0x115;

		public event ScrollEventHandler HorzScrollValueChanged;
		public event ScrollEventHandler VertScrollValueChanged;
	
		protected override void WndProc(ref Message m)
		{
			base.WndProc (ref m);

			if ( m.Msg == WM_HSCROLL ) 
			{
				if ( HorzScrollValueChanged != null ) 
				{
					uint wParam = (uint)m.WParam.ToInt32();
					HorzScrollValueChanged( this, 
						new ScrollEventArgs( 
							GetEventType( wParam & 0xffff), (int)(wParam >> 16) ) );
				}
			} 
			else if ( m.Msg == WM_VSCROLL )
			{
				if ( VertScrollValueChanged != null )
				{
					uint wParam = (uint)m.WParam.ToInt32();
					VertScrollValueChanged( this, 
						new ScrollEventArgs( 
						GetEventType( wParam & 0xffff), (int)(wParam >> 16) ) );
				}
			}
		}

		private static ScrollEventType [] _events =
			new ScrollEventType[] {
									  ScrollEventType.SmallDecrement,
									  ScrollEventType.SmallIncrement,
									  ScrollEventType.LargeDecrement,
									  ScrollEventType.LargeIncrement,
									  ScrollEventType.ThumbPosition,
									  ScrollEventType.ThumbTrack,
									  ScrollEventType.First,
									  ScrollEventType.Last,
									  ScrollEventType.EndScroll
								  };
		private ScrollEventType GetEventType( uint wParam )
		{
			if ( wParam < _events.Length )
				return _events[wParam];
			else
				return ScrollEventType.EndScroll;
		}
	}    
}
