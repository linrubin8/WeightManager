using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace LB.Controls
{
    public interface ILBTextBox
    {
        bool CanBeEmpty { get; set; }
        string Caption { get; set; }
        bool IsEmptyValue { get; }
    }
}
