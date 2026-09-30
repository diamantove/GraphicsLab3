using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Forms;

namespace LabRasterGraphics
{
    public partial class Form1 : Form
    {
        private Bitmap canvas, pattern;
        private Color fillColor = Color.DarkOrange;
        private Point lastPoint;
        private bool isDrawing;
        private string patternName = "";
        private readonly Pen drawPen = new Pen(Color.Black, 2) { StartCap = LineCap.Round, EndCap = LineCap.Round, LineJoin = LineJoin.Round };

        public Form1()
        {
            InitializeComponent();
            UpdateColorPreview();
            UpdateStatus();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ResizeCanvas(false);
        }

        private void UpdateColorPreview() => pnlColorPreview.BackColor = fillColor;

        private void UpdateStatus() => lblStatus.Text = rbPattern.Checked
            ? (pattern == null ? "Узор не загружен" : $"Узор: {patternName}")
            : $"ЛКМ — рисовать | ПКМ — заливка | {fillColor.Name}";

        private Point ClampPoint(Point p) => new Point(
            Math.Max(0, Math.Min(p.X, canvas.Width - 1)),
            Math.Max(0, Math.Min(p.Y, canvas.Height - 1)));

        private void ResizeCanvas(bool keep)
        {
            int w = pictureBox1.ClientSize.Width, h = pictureBox1.ClientSize.Height;
            if (w <= 0 || h <= 0) return;
            if (canvas != null && canvas.Width == w && canvas.Height == h) return;

            Bitmap next = new Bitmap(w, h, PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(next)) { g.Clear(Color.White); if (keep && canvas != null) g.DrawImageUnscaled(canvas, 0, 0); }
            Bitmap old = canvas;
            canvas = next;
            pictureBox1.Image = canvas;
            old?.Dispose();
            pictureBox1.Invalidate();
        }

        private void PictureBox1_Resize(object sender, EventArgs e)
        {
            if (canvas != null) ResizeCanvas(true);
        }

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (canvas == null) return;
            if (e.Button == MouseButtons.Left)
            {
                isDrawing = true;
                pictureBox1.Capture = true;
                lastPoint = ClampPoint(e.Location);
                DrawLine(lastPoint, lastPoint);
            }
            else if (e.Button == MouseButtons.Right) PerformFill(e.X, e.Y);
        }

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isDrawing || canvas == null) return;
            Point p = ClampPoint(e.Location);
            DrawLine(lastPoint, p);
            lastPoint = p;
        }

        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || canvas == null) return;
            Point p = ClampPoint(e.Location);
            DrawLine(lastPoint, p);
            lastPoint = p;
            isDrawing = false;
            pictureBox1.Capture = false;
        }

        private void DrawLine(Point a, Point b)
        {
            using (Graphics g = Graphics.FromImage(canvas)) g.DrawLine(drawPen, a, b);
            pictureBox1.Invalidate();
        }

        private void BtnChooseColor_Click(object sender, EventArgs e)
        {
            using (ColorDialog cd = new ColorDialog { Color = fillColor })
                if (cd.ShowDialog() == DialogResult.OK) { fillColor = cd.Color; UpdateColorPreview(); UpdateStatus(); }
        }

        private void BtnLoadPattern_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog d = new OpenFileDialog { Filter = "Изображения|*.png;*.bmp;*.jpg;*.jpeg|Все файлы|*.*", Title = "Выберите узор" })
            {
                if (d.ShowDialog() != DialogResult.OK) return;
                try
                {
                    Bitmap next;
                    using (Bitmap loaded = new Bitmap(d.FileName)) next = loaded.Clone(new Rectangle(0, 0, loaded.Width, loaded.Height), PixelFormat.Format32bppArgb);
                    Bitmap old = pattern;
                    pattern = next;
                    old?.Dispose();
                    patternName = Path.GetFileName(d.FileName);
                    rbPattern.Checked = true;
                    UpdateStatus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка загрузки изображения:\n\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void RbMode_CheckedChanged(object sender, EventArgs e)
        {
            if (!rbPattern.Checked) { UpdateStatus(); return; }
            if (pattern == null)
            {
                rbSolid.Checked = true;
                MessageBox.Show("Сначала загрузите узор.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            UpdateStatus();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (canvas == null) { ResizeCanvas(false); return; }
            using (Graphics g = Graphics.FromImage(canvas)) g.Clear(Color.White);
            pictureBox1.Invalidate();
        }

        private void PerformFill(int startX, int startY)
        {
            if (canvas == null) return;
            Point s = ClampPoint(new Point(startX, startY));
            bool usePattern = rbPattern.Checked;
            if (usePattern && pattern == null)
            {
                rbSolid.Checked = true;
                MessageBox.Show("Сначала загрузите узор.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Cursor oldCursor = Cursor.Current;
            BitmapData cd = null, pd = null;
            Cursor = Cursors.WaitCursor;
            try
            {
                int w = canvas.Width, h = canvas.Height;
                cd = canvas.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
                int[] pixels = ReadPixels(cd, w, h, w, h);
                int target = pixels[s.Y * w + s.X], fill = fillColor.ToArgb();

                if (!usePattern && target == fill) return;

                int[] pat = null; int pw = 0, ph = 0;
                if (usePattern)
                {
                    pw = Math.Min(pattern.Width, w); ph = Math.Min(pattern.Height, h);
                    pd = pattern.LockBits(new Rectangle(0, 0, pattern.Width, pattern.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                    pat = ReadPixels(pd, pattern.Width, pattern.Height, pw, ph);
                }

                FillScanline(pixels, w, h, s.X, s.Y, target, fill, pat, pw, ph, new bool[w * h]);
                WritePixels(cd, pixels, w, h);
                pictureBox1.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при заливке:\n\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (pd != null) pattern.UnlockBits(pd);
                if (cd != null) canvas.UnlockBits(cd);
                Cursor = oldCursor;
            }
        }

        private void FillScanline(int[] p, int w, int h, int x, int y, int target, int fill, int[] pat, int pw, int ph, bool[] used)
        {
            if (x < 0 || x >= w || y < 0 || y >= h) return;
            int idx = y * w + x;
            if (p[idx] != target || used[idx]) return;

            int l = x, r = x;
            while (l > 0 && p[y * w + l - 1] == target && !used[y * w + l - 1]) l--;
            while (r < w - 1 && p[y * w + r + 1] == target && !used[y * w + r + 1]) r++;

            for (int i = l; i <= r; i++)
            {
                idx = y * w + i;
                used[idx] = true;
                p[idx] = pat == null ? fill : pat[(y % ph) * pw + (i % pw)];
            }

            ScanSegments(p, used, w, h, l, r, y - 1, target, fill, pat, pw, ph);
            ScanSegments(p, used, w, h, l, r, y + 1, target, fill, pat, pw, ph);
        }

        private void ScanSegments(int[] p, bool[] used, int w, int h, int l, int r, int y, int target, int fill, int[] pat, int pw, int ph)
        {
            if (y < 0 || y >= h) return;
            for (int x = l; x <= r; x++)
            {
                int idx = y * w + x;
                if (p[idx] == target && !used[idx]) FillScanline(p, w, h, x, y, target, fill, pat, pw, ph, used);
            }
        }

        private static IntPtr RowPtr(BitmapData d, int y, int height) => d.Stride >= 0
            ? IntPtr.Add(d.Scan0, y * d.Stride)
            : IntPtr.Add(d.Scan0, (height - 1 - y) * -d.Stride);

        private static int[] ReadPixels(BitmapData d, int bitmapW, int bitmapH, int readW, int readH)
        {
            int[] a = new int[readW * readH];
            for (int y = 0; y < readH; y++) Marshal.Copy(RowPtr(d, y, bitmapH), a, y * readW, readW);
            return a;
        }

        private static void WritePixels(BitmapData d, int[] a, int w, int h)
        {
            for (int y = 0; y < h; y++) Marshal.Copy(a, y * w, RowPtr(d, y, h), w);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            pictureBox1.Image = null;
            canvas?.Dispose();
            pattern?.Dispose();
            drawPen.Dispose();
            base.OnFormClosed(e);
        }
    }
}
