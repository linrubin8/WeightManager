using System.Windows.Forms;

namespace FastReport.Forms
{
    /// <summary>
    /// Represents the Splash Screen showing during loading designer
    /// </summary>
    public partial class SplashForm : Form, IMessageFilter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SplashForm"/> class.
        /// </summary>
        public SplashForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Filters mouse events.
        /// For internal use only.
        /// </summary>
        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == 0x201 || m.Msg == 0x202 || m.Msg == 0x203 ||
                m.Msg == 0x204 || m.Msg == 0x205 || m.Msg == 0x206)
                return true;

            return false;
        }
    }
}
