using System;
using System.Collections.Generic;
using System.Text;
using FastReport.Data;

namespace FastReport.Engine
{
  public partial class ReportEngine
  {
    private void ShowDataHeader(GroupHeaderBand groupBand)
    {
      groupBand.RowNo = 0;

      DataHeaderBand header = groupBand.Header;
      if (header != null)
      {
        ShowBand(header);
        if (header.RepeatOnEveryPage)
          AddReprint(header);
      }

      DataFooterBand footer = groupBand.Footer;
      if (footer != null)
      {
        if (footer.RepeatOnEveryPage)
          AddReprint(footer);
      }
    }

    private void ShowDataFooter(GroupHeaderBand groupBand)
    {
      DataFooterBand footer = groupBand.Footer;
      RemoveReprint(footer);
      ShowBand(footer);
      RemoveReprint(groupBand.Header);
    }

    private void ShowGroupHeader(GroupHeaderBand header)
    {
      header.AbsRowNo++;
      header.RowNo++;
      
      if (header.ResetPageNumber && (header.FirstRowStartsNewPage || header.RowNo > 1))
        ResetLogicalPageNumber();
      if (header.KeepTogether)
        StartKeep(header);
      if (header.KeepWithData)
        StartKeep(header.GroupDataBand);

      // start group event
      OnStateChanged(header, EngineState.GroupStarted);

      ShowBand(header);
      if (header.RepeatOnEveryPage)
        AddReprint(header);

      GroupFooterBand footer = header.GroupFooter;
      if (footer != null)
      {
        if (footer.RepeatOnEveryPage)
          AddReprint(footer);
      }
    }

    private void ShowGroupFooter(GroupHeaderBand header)
    {
      // finish group event
      OnStateChanged(header, EngineState.GroupFinished);

      // rollback to previous data row to print the header condition in the footer.
      DataBand dataBand = header.GroupDataBand;
      DataSourceBase dataSource = dataBand.DataSource;
      dataSource.Prior();

      GroupFooterBand footer = header.GroupFooter;
      if (footer != null)
      {
        footer.AbsRowNo++;
        footer.RowNo++;
      }
      RemoveReprint(footer);
      ShowBand(footer);
      RemoveReprint(header);

      // restore current row
      dataSource.Next();

      OutlineUp(header);
      if (header.KeepTogether)
        EndKeep();
      if (footer != null && footer.KeepWithData)
        EndKeep();
    }

    private void InitGroupItem(GroupHeaderBand header, GroupTreeItem curItem)
    {
      while (header != null)
      {
        header.ResetGroupValue();
        header.AbsRowNo = 0;
        header.RowNo = 0;

        curItem = curItem.AddItem(new GroupTreeItem(header));
        curItem.RowNo = header.DataSource.CurrentRowNo;
        curItem.RowCount++;
        header = header.NestedGroup;
      }
    }

    private void CheckGroupItem(GroupHeaderBand header, GroupTreeItem curItem)
    {
      while (header != null)
      {
        if (header.GroupValueChanged())
        {
          InitGroupItem(header, curItem);
          break;
        }

        header = header.NestedGroup;
        curItem = curItem.LastItem;
        curItem.RowCount++;
      }
    }

    private GroupTreeItem MakeGroupTree(GroupHeaderBand groupBand)
    {
      GroupTreeItem rootItem = new GroupTreeItem(null);
      DataSourceBase dataSource = groupBand.DataSource;
      bool isFirstRow = true;

      // cycle through rows
      dataSource.First();
      while (dataSource.HasMoreRows)
      {
        if (isFirstRow)
          InitGroupItem(groupBand, rootItem);
        else
          CheckGroupItem(groupBand, rootItem);

        dataSource.Next();
        isFirstRow = false;
        if (Report.Aborted)
          break;
      }
      
      return rootItem;
    }

    private void ShowGroupTree(GroupTreeItem root)
    {
      if (root.Band != null)
      {
        root.Band.GroupDataBand.DataSource.CurrentRowNo = root.RowNo;
        ShowGroupHeader(root.Band);
      }

      if (root.Items.Count == 0)
      {
        if (root.RowCount != 0)
        {
          int rowCount = root.RowCount;
          int maxRows = root.Band.GroupDataBand.MaxRows;
          if (maxRows > 0 && rowCount > maxRows)
            rowCount = maxRows;
          bool keepFirstRow = NeedKeepFirstRow(root.Band);
          bool keepLastRow = NeedKeepLastRow(root.Band.GroupDataBand);
          RunDataBand(root.Band.GroupDataBand, rowCount, keepFirstRow, keepLastRow);
        }
      }
      else
      {
        ShowDataHeader(root.FirstItem.Band);

        for (int i = 0; i < root.Items.Count; i++)
        {
          GroupTreeItem item = root.Items[i];
          item.Band.IsFirstRow = i == 0;
          item.Band.IsLastRow = i == root.Items.Count - 1;

          ShowGroupTree(item);
          if (Report.Aborted)
            break;
        }

        ShowDataFooter(root.FirstItem.Band);
      }

      if (root.Band != null)
        ShowGroupFooter(root.Band);
    }

    private void RunGroup(GroupHeaderBand groupBand)
    {
      DataSourceBase dataSource = groupBand.DataSource;
      if (dataSource != null)
      {
        // init the datasource - set group conditions to sort data rows
        groupBand.InitDataSource();

        // show the group tree
        ShowGroupTree(MakeGroupTree(groupBand));

        // finalize the datasource, remove the group condition 
        // from the databand sort
        groupBand.FinalizeDataSource();

        // do not leave the datasource in EOF state to allow print something in the footer
        dataSource.Prior();
      }
    }


    private class GroupTreeItem
    {
      private GroupHeaderBand FBand;
      private List<GroupTreeItem> FItems;
      private int FRowNo;
      private int FRowCount;

      public GroupHeaderBand Band
      {
        get { return FBand; }
      }

      public List<GroupTreeItem> Items
      {
        get { return FItems; }
      }

      public GroupTreeItem FirstItem
      {
        get { return Items.Count == 0 ? null : Items[0]; }
      }

      public GroupTreeItem LastItem
      {
        get { return Items.Count == 0 ? null : Items[Items.Count - 1]; }
      }

      public int RowNo
      {
        get { return FRowNo; }
        set { FRowNo = value; }
      }
      
      public int RowCount
      {
        get { return FRowCount; }
        set { FRowCount = value; }
      }

      public GroupTreeItem AddItem(GroupTreeItem item)
      {
        Items.Add(item);
        return item;
      }

      public GroupTreeItem(GroupHeaderBand band)
      {
        FBand = band;
        FItems = new List<GroupTreeItem>();
      }
    }
  }
}
