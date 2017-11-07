using DMSkin.Metro.Controls;
using LB.Controls.Args;
using LB.Controls.LBMainTabControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LB.Controls.LBTabControl
{
    public partial class LBMainTabControl : MetroTabControl
    {
        public event TabPageClosingEventHandle TabPageClosingEvent;
        public event TabPageClosedEventHandle TabPageClosedEvent;

        private Dictionary<Control, TabPageClose> _DictPage = new Dictionary<Control, TabPageClose>();

        public LBMainTabControl()
        {
            InitializeComponent();
        }

        public LBMainTabControl(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);

            TabPageClose tabClose = new LB.Controls.LBMainTabControl.TabPageClose((TabPage)e.Control);
            _DictPage.Add(e.Control, tabClose);
            ReCalculateTabIndex();
        }

        protected override void OnControlRemoved(ControlEventArgs e)
        {
            base.OnControlRemoved(e);
            lock (_DictPage)
            {
                if (_DictPage.ContainsKey(e.Control))
                {
                    _DictPage.Remove(e.Control);
                }
                ReCalculateTabIndex((TabPage)e.Control);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            lock (_DictPage)
            {
                foreach (KeyValuePair<Control, TabPageClose> keyvalue in _DictPage)
                {
                    if (keyvalue.Key.TabIndex > 0)//第一个页签为主界面，无需添加关闭按钮
                    {
                        keyvalue.Value.Draw(e.Graphics);
                    }
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            Point pMouselocation = new Point(e.X, e.Y);
            TabPage tpSelected = null;
            foreach (KeyValuePair<Control, TabPageClose> keyvalue in _DictPage)
            {
                TabPage tp = keyvalue.Value.HitLocation(pMouselocation);
                if (tp != null)
                {
                    tpSelected = tp;
                    break;
                }
            }
            if (tpSelected != null)
            {
                this.Cursor = Cursors.Hand;
            }
            else
            {
                this.Cursor = Cursors.Default;
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            Point pMouselocation = new Point(e.X, e.Y);
            TabPage tpSelected = null;
            foreach (KeyValuePair<Control, TabPageClose> keyvalue in _DictPage)
            {
                TabPage tp = keyvalue.Value.HitLocation(pMouselocation);
                if (tp != null)
                {
                    tpSelected = tp;
                    break;
                }
            }
            if (tpSelected != null)
            {
                #region -- 关闭页签时触发，如果返回的Cancel=true时，终止关闭 --
                if (TabPageClosingEvent != null)
                {
                    TabPageClosingEventArgs args = new Args.TabPageClosingEventArgs(tpSelected, false);
                    TabPageClosingEvent(this, args);
                }
                #endregion -- 关闭页签时触发，如果返回的Cancel=true时，终止关闭 --

                if (this.TabPages.Contains(tpSelected))
                {
                    #region -- 关闭页签时触发，如果返回的Cancel=true时，终止关闭 --
                    if (TabPageClosedEvent != null)
                    {
                        TabPageClosedEventArgs args = new Args.TabPageClosedEventArgs(tpSelected);
                        TabPageClosedEvent(this, args);
                    }
                    #endregion -- 关闭页签时触发，如果返回的Cancel=true时，终止关闭 --
                    this.TabPages.Remove(tpSelected);
                }
            }
        }

        #region --重新计算每一个页签的顺序号TabIndex --
        /// <summary>
        /// 添加页签后重算
        /// </summary>
        private void ReCalculateTabIndex()
        {
            int iTalIndex = 0;
            foreach(TabPage tp in this.TabPages)
            {
                tp.TabIndex = iTalIndex;
                iTalIndex++;
            }
        }
        /// <summary>
        /// 删除页签后重算
        /// </summary>
        /// <param name="tpRemoved"></param>
        private void ReCalculateTabIndex(TabPage tpRemoved)
        {
            int iTalIndex = 0;
            foreach (TabPage tp in this.TabPages)
            {
                if (tp != tpRemoved)
                {
                    tp.TabIndex = iTalIndex;
                    iTalIndex++;
                }
            }
        }
        #endregion --重新计算每一个页签的顺序号TabIndex --
    }
}
