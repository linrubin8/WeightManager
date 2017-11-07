using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LB.Controls.LBMainTabControl
{
    public class TabPageClose
    {
        private TabPage _TabPage = null;
        private Rectangle _RectTab = new Rectangle();
        public TabPageClose( TabPage tabPage)
        {
            _TabPage = tabPage;
        }

        public void Draw(Graphics g)
        {
            TabControl tc = _TabPage.Parent as TabControl;
            try
            {
                if (tc.TabCount > _TabPage.TabIndex)
                {
                    Rectangle rect = tc.GetTabRect(_TabPage.TabIndex);
                    _RectTab = new Rectangle(rect.X + rect.Width - 12, rect.Y, 16, 16);
                    g.DrawIcon(LB.Properties.Resources.ICO_Delete, _RectTab);
                }
            }
            catch (Exception ex)
            {

            }
        }

        public TabPage HitLocation(Point pMouseLocation)
        {
            if (_RectTab.Contains(pMouseLocation))
            {
                return _TabPage;
            }
            return null;
        }
    }
}
