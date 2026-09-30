using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Graphics3
{
    public partial class Form1 : Form
    {
        private Bitmap? bitmap;
        public Form1()
        {
            InitializeComponent();

            comboBoxAlg.Items.AddRange( new string[] { "Брезенхем", "By" });
            comboBoxAlg.SelectedIndex = 0;
        }
        private void buttonLoad_Click(object sender, EventArgs e)
        {
            using OpenFileDialog dialog = new OpenFileDialog();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                bitmap = new Bitmap(dialog.FileName);
                pictureBox2.Image = bitmap;
            }
        }
        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if (bitmap == null)
                return;

            Point start = e.Location;
            Color borderColor = bitmap.GetPixel(start.X, start.Y);

            List<Point> border = new List<Point>();
            
            int[] dx = { 1, 1, 0, -1, -1, -1, 0, 1 };
            int[] dy = { 0, -1, -1, -1, 0, 1, 1, 1 };

            Point current = start;
            
            int direction = 6;

            do {
                border.Add(current);
                bool found = false;
                
                int begin = (direction + 2) % 8;
                for (int i = 0; i < 8; i++) {
                    int dir = (begin - i + 8) % 8;

                    int x = current.X + dx[dir];
                    int y = current.Y + dy[dir];

                    if (x < 0 || x >= bitmap.Width || y < 0 || y >= bitmap.Height)
                        continue;

                    if (bitmap.GetPixel(x, y).ToArgb() == borderColor.ToArgb()) {
                        current = new Point(x, y);
                        direction = dir;
                        found = true;
                        break;
                    }
                }

                if (!found)
                    break;

            } while (current != start);
            
            Bitmap result = new Bitmap(bitmap);

            foreach (Point p in border)
                result.SetPixel(p.X, p.Y, Color.Red);

            pictureBox3.Image?.Dispose();
            pictureBox3.Image = result;
        }

        private void buttonDraw_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(textBoxM.Text, out double m) ||
        !double.TryParse(textBoxC.Text, out double c))
            {
                MessageBox.Show("Введите корректные значения m и c:");
                return;
            }

            Bitmap bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);
            }

            int centerX = bitmap.Width / 2;
            int centerY = bitmap.Height / 2;

            // Левая граница PictureBox
            double x1 = -centerX;
            double y1 = m * x1 + c;

            // Правая граница PictureBox
            double x2 = bitmap.Width - centerX - 1;
            double y2 = m * x2 + c;

            // Перевод математических координат
            // в координаты PictureBox
            int px1 = (int)Math.Round(x1 + centerX);
            int py1 = (int)Math.Round(centerY - y1);

            int px2 = (int)Math.Round(x2 + centerX);
            int py2 = (int)Math.Round(centerY - y2);

            if (comboBoxAlg.SelectedItem?.ToString() == "Брезенхем")
            {
                DrawBresenham(bitmap, px1, py1, px2, py2);
            }
            else
            {
                DrawBy(bitmap, px1, py1, px2, py2);
            }

            pictureBox1.Image?.Dispose();
            pictureBox1.Image = bitmap;
        }
        private void SetPixel(byte[] pixels, int stride, int width, int height, int x, int y, byte r, byte g, byte b) {
            if (x < 0 || x >= width ||
                y < 0 || y >= height)
            {
                return;
            }

            int index = y * stride + x * 4;

            pixels[index] = b;
            pixels[index + 1] = g;
            pixels[index + 2] = r;
            pixels[index + 3] = 255;
        }
        private void DrawBresenham(Bitmap bitmap, int x1, int y1, int x2, int y2) {
            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

            BitmapData data = bitmap.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

            int bytesCount = Math.Abs(data.Stride) * bitmap.Height;
            byte[] pixels = new byte[bytesCount];

            Marshal.Copy(data.Scan0, pixels, 0, bytesCount);

            int dx = Math.Abs(x2 - x1);
            int dy = Math.Abs(y2 - y1);

            int sx = x1 < x2 ? 1 : -1;
            int sy = y1 < y2 ? 1 : -1;

            int error = dx - dy;

            while (true) {
                SetPixel(pixels, data.Stride, bitmap.Width, bitmap.Height, x1, y1, 0, 0, 0);

                if (x1 == x2 && y1 == y2)
                    break;

                int error2 = 2 * error;

                if (error2 > -dy) {
                    error -= dy;
                    x1 += sx;
                }

                if (error2 < dx) {
                    error += dx;
                    y1 += sy;
                }
            }

            Marshal.Copy(pixels, 0, data.Scan0, bytesCount);

            bitmap.UnlockBits(data);
        }
        private void SetPixelBrightness(byte[] pixels, int stride, int width, int height, int x, int y, double brightness){
            if (x < 0 || x >= width || y < 0 || y >= height) {
                return;
            }

            brightness = Math.Clamp(brightness, 0.0, 1.0);

            byte value = (byte)(255 * (1.0 - brightness));

            int index = y * stride + x * 4;
            
            pixels[index] = value;       // B
            pixels[index + 1] = value;   // G
            pixels[index + 2] = value;   // R
            pixels[index + 3] = 255;     // A
        }
        private void Swap(ref int a, ref int b) {
            int temp = a;
            a = b;
            b = temp;
        }
        private void DrawBy(Bitmap bitmap, int x0, int y0, int x1, int y1) {
            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

            BitmapData data = bitmap.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

            int bytesCount = Math.Abs(data.Stride) * bitmap.Height;
            byte[] pixels = new byte[bytesCount];

            Marshal.Copy(data.Scan0, pixels, 0, bytesCount);

            bool steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);

            // Если линия крутая, меняем x и y местами
            if (steep) {
                Swap(ref x0, ref y0);
                Swap(ref x1, ref y1);
            }

            // Рисуем всегда слева направо
            if (x0 > x1) {
                Swap(ref x0, ref x1);
                Swap(ref y0, ref y1);
            }

            double dx = x1 - x0;
            double dy = y1 - y0;

            double gradient = (dx == 0) ? 1: dy / dx;

            double y = y0;

            for (int x = x0; x <= x1; x++) {
                int yInteger = (int)Math.Floor(y);

                double fraction = y - yInteger;

                double brightness1 = 1.0 - fraction;
                double brightness2 = fraction;

                if (steep) {
                    SetPixelBrightness(
                        pixels,
                        data.Stride,
                        bitmap.Width,
                        bitmap.Height,
                        yInteger,
                        x,
                        brightness1);

                    SetPixelBrightness(
                        pixels,
                        data.Stride,
                        bitmap.Width,
                        bitmap.Height,
                        yInteger + 1,
                        x,
                        brightness2);
                }
                else
                {
                    SetPixelBrightness(
                        pixels,
                        data.Stride,
                        bitmap.Width,
                        bitmap.Height,
                        x,
                        yInteger,
                        brightness1);

                    SetPixelBrightness(
                        pixels,
                        data.Stride,
                        bitmap.Width,
                        bitmap.Height,
                        x,
                        yInteger + 1,
                        brightness2);
                }

                y += gradient;
            }

            Marshal.Copy(pixels, 0, data.Scan0, bytesCount);

            bitmap.UnlockBits(data);
        }

    }    
}
