using FastReport.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;

namespace FastReport.Forms
{
    internal partial class PictureEditorAdvancedForm : Form
    {
        public Image Image
        {
            get
            {
                return pictureBox.Image;
            }
        }

        private Image imageOriginal;
        private Image imageGrayscale;
        private Image imageMonochrome;

        private ToolStripLabel lblSize;
        private ToolStripRadioButton rbZoomed;
        private ToolStripRadioButton rbFullSize;
        
        public PictureEditorAdvancedForm(Image image)
        {
            InitializeComponent();
            Localize();

            this.imageOriginal = image;
            pictureBox.Image = image;

            setToolbar();
            reset();

            if (image.Width < panelMiddle.Width && image.Height < panelMiddle.Height)
                rbFullSize.PerformClick();
            else
                rbZoomed.PerformClick();

            rbPercentResize.CheckedChanged += rbPercentResize_CheckedChanged;
            nudHor.ValueChanged += resize;
            nudVer.ValueChanged += resize;

            rbPercentCrop.CheckedChanged += rbPercentCrop_CheckedChanged;
            nudTop.ValueChanged += crop;
            nudLeft.ValueChanged += crop;
            nudRight.ValueChanged += crop;
            nudBottom.ValueChanged += crop;

            this.SizeChanged += delegate(object s, EventArgs e)
            {
                centerImage();
            };
        }

        #region Init & Reset
        private void Localize()
        {
            MyRes res = new MyRes("Forms,PictureEditorAdvanced");
            Text = res.Get("");
            btnOK.Text = Res.Get("Buttons,Ok");
            btnCancel.Text = Res.Get("Buttons,Cancel");
            btnReset.Text = res.Get("Reset");
            gbResize.Text = res.Get("Resize");
            gbCrop.Text = res.Get("Crop");
            gbColor.Text = res.Get("Color");
            lblChange.Text = res.Get("Change");
            lblChange2.Text = res.Get("Change");
            rbPercentResize.Text = res.Get("Percentage");
            rbPercentCrop.Text = res.Get("Percentage");
            rbPixelsResize.Text = res.Get("Pixels");
            rbPixelsCrop.Text = res.Get("Pixels");
            lblHor.Text = res.Get("Horizontal");
            lblVer.Text = res.Get("Vertical");
            cbAspectRatio.Text = res.Get("AspectRatio");
            rbNone.Text = Res.Get("Misc,None");
            rbGrayscale.Text = res.Get("Grayscale");
            rbMonochrome.Text = res.Get("Monochrome");
        }

        private void setToolbar()
        {
            lblSize = new ToolStripLabel();
            setSizeInfo(pictureBox.Image.Width, pictureBox.Image.Height);
            statusBar.Items.Add(lblSize);

            rbZoomed = new ToolStripRadioButton();
            rbZoomed.Image = Res.GetImage(235);
            rbZoomed.Click += delegate(object s, EventArgs e)
            {
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox.Dock = DockStyle.Fill;
                centerImage();
            };
            statusBar.Items.Add(rbZoomed);

            rbFullSize = new ToolStripRadioButton();
            rbFullSize.Image = Res.GetImage(236);
            rbFullSize.Click += delegate(object s, EventArgs e)
            {
                pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
                pictureBox.Dock = DockStyle.None;
                centerImage();
            };
            statusBar.Items.Add(rbFullSize);
        }

        private void reset()
        {
            resetResize();
            resetCrop();
            resetColor();
        }

        private void resetResize()
        {
            rbPercentResize.Checked = true;
            cbAspectRatio.Checked = true;

            nudHor.Minimum = 1;
            nudHor.Maximum = 100;
            nudHor.Value = 100;

            nudVer.Minimum = 1;
            nudVer.Maximum = 100;
            nudVer.Value = 100;
        }

        private void resetCrop()
        {
            rbPercentCrop.Checked = true;

            nudTop.Minimum = 0;
            nudTop.Maximum = 99;
            nudTop.Value = 0;

            nudLeft.Minimum = 0;
            nudLeft.Maximum = 99;
            nudLeft.Value = 0;

            nudRight.Minimum = 0;
            nudRight.Maximum = 99;
            nudRight.Value = 0;

            nudBottom.Minimum = 0;
            nudBottom.Maximum = 99;
            nudBottom.Value = 0;
        }

        private void resetColor()
        {
            rbNone.Checked = true;
        }
        #endregion

        #region Resize
        void rbPercentResize_CheckedChanged(object sender, EventArgs e)
        {
            nudHor.ValueChanged -= resize;
            nudVer.ValueChanged -= resize;

            if (rbPercentResize.Checked)
            {
                nudHor.Maximum = 100;
                nudVer.Maximum = 100;

                nudHor.Value = 100;
                nudVer.Value = 100;
            }
            else
            {
                nudHor.Maximum = imageOriginal.Width;
                nudVer.Maximum = imageOriginal.Height;

                nudHor.Value = imageOriginal.Width;
                nudVer.Value = imageOriginal.Height;
            }

            nudHor.ValueChanged += resize;
            nudVer.ValueChanged += resize;

            redraw();
        }

        private int getWidth()
        {
            if(rbPercentResize.Checked)
            {
                return (int)Math.Round((float)nudHor.Value / 100 * imageOriginal.Width);
            }
            else
            {
                return (int)nudHor.Value;
            }
        }

        private int getHeight()
        {
            if (rbPercentResize.Checked)
            {
                return (int)Math.Round((float)nudVer.Value / 100 * imageOriginal.Height);
            }
            else
            {
                return (int)nudVer.Value;
            }
        }

        private void resize(object sender, EventArgs e)
        {
            if(cbAspectRatio.Checked)
            {
                float x = (float)getWidth() / (float)imageOriginal.Width;
                float y = (float)getHeight() / (float)imageOriginal.Height;

                if(sender == nudHor)
                {
                    if(rbPercentResize.Checked)
                    {
                        nudVer.Value = (int)Math.Round(x * 100);
                    }
                    else
                    {
                        nudVer.Value = (int)Math.Round(x * imageOriginal.Height);
                    }
                }
                else if(sender == nudVer)
                {
                    if (rbPercentResize.Checked)
                    {
                        nudHor.Value = (int)Math.Round(y * 100);
                    }
                    else
                    {
                        nudHor.Value = (int)Math.Round(y * imageOriginal.Width);
                    }
                }
            }

            redraw();
        }
        #endregion

        #region Crop
        private int cropTop()
        {
            if (rbPercentCrop.Checked)
            {
                return (int)Math.Round((float)nudTop.Value / 100 * getHeight());
            }
            else
            {
                return (int)nudTop.Value;
            }
        }

        private int cropLeft()
        {
            if (rbPercentCrop.Checked)
            {
                return (int)Math.Round((float)nudLeft.Value / 100 * getWidth());
            }
            else
            {
                return (int)nudLeft.Value;
            }
        }

        private int cropRight()
        {
            if (rbPercentCrop.Checked)
            {
                return (int)Math.Round((float)nudRight.Value / 100 * getWidth());
            }
            else
            {
                return (int)nudRight.Value;
            }
        }

        private int cropBottom()
        {
            if (rbPercentCrop.Checked)
            {
                return (int)Math.Round((float)nudBottom.Value / 100 * getHeight());
            }
            else
            {
                return (int)nudBottom.Value;
            }
        }

        private void rbPercentCrop_CheckedChanged(object sender, EventArgs e)
        {
            nudTop.ValueChanged -= crop;
            nudLeft.ValueChanged -= crop;
            nudRight.ValueChanged -= crop;
            nudBottom.ValueChanged -= crop;

            if(rbPercentCrop.Checked)
            {
                nudTop.Maximum    = 99;
                nudBottom.Maximum = 99;
                nudLeft.Maximum   = 99;
                nudRight.Maximum  = 99;

                nudTop.Value    = 0;
                nudBottom.Value = 0;
                nudLeft.Value   = 0;
                nudRight.Value  = 0;
            }
            else
            {
                nudTop.Maximum    = getHeight() - 1;
                nudBottom.Maximum = getHeight() - 1;
                nudLeft.Maximum   = getWidth()  - 1;
                nudRight.Maximum  = getWidth()  - 1;

                nudTop.Value    = 0;
                nudBottom.Value = 0;
                nudLeft.Value   = 0;
                nudRight.Value  = 0;
            }

            nudTop.ValueChanged += crop;
            nudLeft.ValueChanged += crop;
            nudRight.ValueChanged += crop;
            nudBottom.ValueChanged += crop;

            redraw();
        }

        private void crop(object sender, EventArgs e)
        {
            if(rbPercentCrop.Checked)
            {
                nudTop.Maximum    = 99 - nudBottom.Value;
                nudBottom.Maximum = 99 - nudTop.Value;
                nudLeft.Maximum   = 99 - nudRight.Value;
                nudRight.Maximum  = 99 - nudLeft.Value;
            }
            else
            {
                nudTop.Maximum    = getHeight() - nudBottom.Value - 1;
                nudBottom.Maximum = getHeight() - nudTop.Value    - 1;
                nudLeft.Maximum   = getWidth()  - nudRight.Value  - 1;
                nudRight.Maximum  = getWidth()  - nudLeft.Value   - 1;
            }

            redraw();
        }
        #endregion

        #region Color
        private Image monochrome(Image image)
        {
            Bitmap mono = ((Bitmap)image).Clone(new Rectangle(0, 0, image.Width, image.Height), image.PixelFormat);

            for (int x = 0; x < mono.Width; x++)
            {
                for (int y = 0; y < mono.Height; y++)
                {
                    Color c = mono.GetPixel(x, y);
                    
                    if (c.GetBrightness() >= 0.5)
                    {
                        c = Color.FromArgb(c.A, 255, 255, 255);
                    }
                    else
                    {
                        c = Color.FromArgb(c.A, 0, 0, 0);
                    }

                    mono.SetPixel(x, y, c);
                }
            }

            return mono;
        }

        private Bitmap __monochrome(Bitmap image)
        {
            int w = image.Width;
            int h = image.Height;

            Bitmap bmp = new Bitmap(w, h, PixelFormat.Format1bppIndexed);
            BitmapData data = bmp.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadWrite, PixelFormat.Format1bppIndexed);
            
            for (int y = 0; y < h; y++)
            {
                byte[] scan = new byte[(w + 7) / 8];

                for (int x = 0; x < w; x++)
                {
                    Color c = image.GetPixel(x, y);
                    if (c.GetBrightness() >= 0.5) scan[x / 8] |= (byte)(0x80 >> (x % 8));
                }

                System.Runtime.InteropServices.Marshal.Copy(scan, 0, (IntPtr)((int)data.Scan0 + data.Stride * y), scan.Length);
            }

            bmp.UnlockBits(data);

            return bmp;
        }
        #endregion

        #region Main & Etc
        private void redraw()
        {
            pictureBox.Image = update(getWidth(), getHeight());
            setSizeInfo(Image.Width, Image.Height);
            centerImage();
        }

        private void centerImage()
        {
            //if (rbFullSize.Checked)
            {
                pictureBox.Left = Math.Max(0, (pictureBox.Parent.Width  - pictureBox.Width)  / 2);
                pictureBox.Top  = Math.Max(0, (pictureBox.Parent.Height - pictureBox.Height) / 2);
            }
        }

        private void setSizeInfo(int x, int y)
        {
            lblSize.Text = " " + x + " × " + y + " ";
        }

        private Image update(int width, int height)
        {
            Image image = this.imageOriginal;

            if (rbGrayscale.Checked)
            {
                if (imageGrayscale == null)
                    imageGrayscale = ImageHelper.GetGrayscaleBitmap(image);

                image = imageGrayscale;
            }
            else if (rbMonochrome.Checked)
            {
                if (imageMonochrome == null)
                    imageMonochrome = monochrome(image);

                image = imageMonochrome;
            }

            Rectangle destRect = new Rectangle(0, 0, width, height);
            Bitmap destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (Graphics graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (ImageAttributes wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            Rectangle crop = new Rectangle((int)cropLeft(),
                                     (int)cropTop (),
                                     Math.Max(1, width  - (int)cropLeft() - (int)cropRight()),
                                     Math.Max(1, height - (int)cropTop()  - (int)cropBottom()));

            try
            {
                destImage = destImage.Clone(crop, destImage.PixelFormat);
            }
            catch
            {
                resetCrop();
            }

            return destImage;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            reset();
            redraw();
        }

        private void rbNone_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;

            if (rb != null && rb.Checked)
            {
                redraw();
            }
        }
        #endregion
    }

    internal class ToolStripRadioButton : ToolStripButton
    {
        private int radioButtonGroupId = 0;
        private bool updateButtonGroup = true;

        private Color checkedColor1 = Color.FromArgb(150, 200, 230);
        private Color checkedColor2 = Color.FromArgb(150, 200, 230);

        public ToolStripRadioButton()
        {
            this.CheckOnClick = true;
        }

        [Category("Behavior")]
        public int RadioButtonGroupId
        {
            get
            {
                return radioButtonGroupId;
            }
            set
            {
                radioButtonGroupId = value;

                // Make sure no two radio buttons are checked at the same time
                UpdateGroup();
            }
        }

        [Category("Appearance")]
        public Color CheckedColor1
        {
            get { return checkedColor1; }
            set { checkedColor1 = value; }
        }

        [Category("Appearance")]
        public Color CheckedColor2
        {
            get { return checkedColor2; }
            set { checkedColor2 = value; }
        }

        // Set check value without updating (disabling) other radio buttons in the group
        private void SetCheckValue(bool checkValue)
        {
            updateButtonGroup = false;
            this.Checked = checkValue;
            updateButtonGroup = true;
        }

        // To make sure no two radio buttons are checked at the same time
        private void UpdateGroup()
        {
            if (this.Parent != null)
            {
                // Get number of checked radio buttons in group
                int checkedCount = 0;

                foreach (ToolStripItem item in Parent.Items)
                {
                    ToolStripRadioButton radio = item as ToolStripRadioButton;

                    if (radio != null && radio.RadioButtonGroupId == RadioButtonGroupId && radio.Checked)
                        checkedCount++;
                }

                if (checkedCount > 1)
                {
                    this.Checked = false;
                }
            }
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            this.Checked = true;
        }

        protected override void OnCheckedChanged(EventArgs e)
        {
            if (this.Parent != null && updateButtonGroup)
            {
                foreach (ToolStripItem item in Parent.Items)
                {
                    ToolStripRadioButton radioButton = item as ToolStripRadioButton;

                    // Disable all other radio buttons with same group id
                    if (radioButton != null && radioButton != this && radioButton.RadioButtonGroupId == this.RadioButtonGroupId)
                    {
                        radioButton.SetCheckValue(false);
                    }
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.Checked)
            {
                LinearGradientBrush checkedBackgroundBrush = new LinearGradientBrush(new Point(0, 0), new Point(0, this.Height), CheckedColor1, CheckedColor2);
                e.Graphics.FillRectangle(checkedBackgroundBrush, new Rectangle(new Point(0, 0), this.Size));
            }

            base.OnPaint(e);
        }
    }

}
