using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace PictureViewer_1
{
    public partial class Form1 : Form
    {
        public Image img = null;

        public Form1()
        {
            InitializeComponent();
        }

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }



        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fname;

            OpenFileDialog d = new OpenFileDialog();
            d.Filter = "Acceptable Files|*.tiff;*.bmp;*.png";
            d.ShowDialog();

            fname = d.FileName;

            if (fname != "")
            {
                try
                {
                    img = Image.FromFile(fname);
                    
                    pbImage.Image = ResizeImage(img, this.Width, this.Height);
                    //pbImage.Scale(new SizeF(10, 10));
                }
                catch (Exception)
                {
                    MessageBox.Show("Cannot open file");
                }
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if(img != null)
                pbImage.Image = ResizeImage(img, this.Width, this.Height);
        }
    }
}
