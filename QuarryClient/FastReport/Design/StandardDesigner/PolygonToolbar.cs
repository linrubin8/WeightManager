using FastReport.DevComponents.DotNetBar;
using FastReport.Utils;
using System;

namespace FastReport.Design.StandardDesigner
{
    class PolygonToolbar : ToolbarBase
    {
        #region Fields
        public ButtonItem btnPointer;
        public ButtonItem btnAddPointToStart;
        public ButtonItem btnAddPointToEnd;
        public ButtonItem btnAddPoint;
        public ButtonItem btnRemovePoint;
        #endregion

        #region Properties
        #endregion

        #region Private Methods
        private void UpdateControls()
        {
            bool enabled = (Designer.SelectedObjects.Count == 1) && (Designer.SelectedObjects[0] is PolyLineObject);
            btnPointer.Enabled = enabled;
            btnAddPointToStart.Enabled = enabled;
            btnAddPointToEnd.Enabled = enabled;
            btnAddPoint.Enabled = enabled;
            btnRemovePoint.Enabled = enabled;
            if (!enabled)
                selectBtn(0);
            else
            {
                PolyLineObject plobj = (Designer.SelectedObjects[0] as PolyLineObject);
                selectBtn(plobj.polygonSelectionMode);
            }
        }

        private void selectBtn(PolyLineObject.PolygonSelectionMode index)
        {
            PolyLineObject plobj = null;
            if ((Designer.SelectedObjects.Count == 1) && (Designer.SelectedObjects[0] is PolyLineObject))
                plobj = (Designer.SelectedObjects[0] as PolyLineObject);
            btnPointer.Checked = false;
            btnAddPointToStart.Checked = false;
            btnAddPointToEnd.Checked = false;
            btnAddPoint.Checked = false;
            btnRemovePoint.Checked = false;
            switch (index)
            {
                case PolyLineObject.PolygonSelectionMode.Normal:
                    btnPointer.Checked = true;
                    break;
                case PolyLineObject.PolygonSelectionMode.AddToStart:
                    btnAddPointToStart.Checked = true;
                    break;
                case PolyLineObject.PolygonSelectionMode.AddToEnd:
                    btnAddPointToEnd.Checked = true;
                    break;
                case PolyLineObject.PolygonSelectionMode.AddToLine:
                    btnAddPoint.Checked = true;
                    break;
                case PolyLineObject.PolygonSelectionMode.Delete:
                    btnRemovePoint.Checked = true;
                    break;
                case PolyLineObject.PolygonSelectionMode.Scale:
                    btnPointer.Checked = false;
                    break;
            }
        }
        #endregion

        #region Public Methods
        public override void SelectionChanged()
        {
            base.SelectionChanged();
            UpdateControls();
        }

        public override void UpdateContent()
        {
            base.UpdateContent();
            UpdateControls();
        }
        #endregion


        public PolygonToolbar(Designer designer) : base(designer)
        {
            Name = "PolygonToolbar";

            btnPointer = CreateButton("btnPolygonPointer", Res.GetImage(100), btnPointer_Click);
            btnAddPointToStart = CreateButton("btnPolygonAddPointToStart", Res.GetImage(151), btnAddPointToStart_Click);
            btnAddPointToEnd = CreateButton("btnPolygonAddPointToEnd", Res.GetImage(150), btnAddPointToEnd_Click);
            btnAddPoint = CreateButton("btnPolygonAddPoint", Res.GetImage(152), btnAddPoint_Click);
            btnRemovePoint = CreateButton("btnPolygonRemovePoint", Res.GetImage(51), btnRemovePoint_Click);


            Items.AddRange(new BaseItem[] {
        btnPointer, btnAddPointToStart, btnAddPointToEnd, btnAddPoint,
        btnRemovePoint });

            selectBtn(PolyLineObject.PolygonSelectionMode.Normal);

            Localize();
        }

        private void btnRemovePoint_Click(object sender, EventArgs e)
        {
            if ((Designer.SelectedObjects.Count == 1) && (Designer.SelectedObjects[0] is PolyLineObject))
            {
                PolyLineObject plobj = (Designer.SelectedObjects[0] as PolyLineObject);
                plobj.polygonSelectionMode = PolyLineObject.PolygonSelectionMode.Delete;
                selectBtn(plobj.polygonSelectionMode);
                Designer.Refresh();Designer.SelectionChanged(this);
            }
            //selectBtn(PolyLineObject.PolygonSelectionMode.Delete);
        }

        private void btnAddPoint_Click(object sender, EventArgs e)
        {
            if ((Designer.SelectedObjects.Count == 1) && (Designer.SelectedObjects[0] is PolyLineObject))
            {
                PolyLineObject plobj = (Designer.SelectedObjects[0] as PolyLineObject);
                plobj.polygonSelectionMode = PolyLineObject.PolygonSelectionMode.AddToLine;
                selectBtn(plobj.polygonSelectionMode);
                Designer.Refresh();//Designer.SelectionChanged(this);
            }
        }

        private void btnAddPointToEnd_Click(object sender, EventArgs e)
        {
            if ((Designer.SelectedObjects.Count == 1) && (Designer.SelectedObjects[0] is PolyLineObject))
            {
                PolyLineObject plobj = (Designer.SelectedObjects[0] as PolyLineObject);
                plobj.polygonSelectionMode = PolyLineObject.PolygonSelectionMode.AddToEnd;
                selectBtn(plobj.polygonSelectionMode);
                Designer.Refresh();//Designer.SelectionChanged(this);
            }
        }

        private void btnAddPointToStart_Click(object sender, EventArgs e)
        {
            if ((Designer.SelectedObjects.Count == 1) && (Designer.SelectedObjects[0] is PolyLineObject))
            {
                PolyLineObject plobj = (Designer.SelectedObjects[0] as PolyLineObject);
                plobj.polygonSelectionMode = PolyLineObject.PolygonSelectionMode.AddToStart;
                selectBtn(plobj.polygonSelectionMode);
                Designer.Refresh();//Designer.SelectionChanged(this);
            }
        }

        private void btnPointer_Click(object sender, EventArgs e)
        {
            if (btnPointer.Checked)
            {
                if ((Designer.SelectedObjects.Count == 1) && (Designer.SelectedObjects[0] is PolyLineObject))
                {
                    PolyLineObject plobj = (Designer.SelectedObjects[0] as PolyLineObject);
                    plobj.polygonSelectionMode = PolyLineObject.PolygonSelectionMode.Scale;
                    selectBtn(plobj.polygonSelectionMode);
                    Designer.Refresh();//Designer.SelectionChanged(this);
                }
            }
            else
            {
                if ((Designer.SelectedObjects.Count == 1) && (Designer.SelectedObjects[0] is PolyLineObject))
                {
                    PolyLineObject plobj = (Designer.SelectedObjects[0] as PolyLineObject);
                    plobj.polygonSelectionMode = PolyLineObject.PolygonSelectionMode.Normal;
                    selectBtn(plobj.polygonSelectionMode);
                    Designer.Refresh();//Designer.SelectionChanged(this);
                }
            }
        }

        public override void Localize()
        {
            base.Localize();
            MyRes res = new MyRes("Designer,Toolbar,Polygon");
            Text = res.Get("");

            SetItemText(btnPointer, res.Get("Pointer"));
            SetItemText(btnAddPointToStart, res.Get("AddPointToStart"));
            SetItemText(btnAddPointToEnd, res.Get("AddPointToEnd"));
            SetItemText(btnAddPoint, res.Get("AddPoint"));
            SetItemText(btnRemovePoint, res.Get("RemovePoint"));
        }
    }
}