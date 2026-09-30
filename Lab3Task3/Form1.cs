namespace lab3;

public partial class Form1 : Form
{
    private Bitmap? bitmap;

    public Form1()
    {
        InitializeComponent();

        Text = "Градиентная закраска треугольника";
        ClientSize = new Size(800, 600);
        DoubleBuffered = true;

        CreateTriangle();
    }

    private void CreateTriangle()
    {
        bitmap?.Dispose();
        bitmap = new Bitmap(ClientSize.Width, ClientSize.Height);

        using (Graphics g = Graphics.FromImage(bitmap))
            g.Clear(Color.White);

        Point A = new Point(150, 100);
        Point B = new Point(650, 150);
        Point C = new Point(400, 500);

        Color colorA = Color.Red;
        Color colorB = Color.Green;
        Color colorC = Color.Blue;

        RasterizeTriangle(bitmap, A, B, C, colorA, colorB, colorC);

        Invalidate();
    }

    private void RasterizeTriangle(
        Bitmap bitmap,
        Point A, Point B, Point C,
        Color colorA, Color colorB, Color colorC)
    {
        int minX = Math.Max(0, Math.Min(A.X, Math.Min(B.X, C.X)));
        int maxX = Math.Min(bitmap.Width - 1, Math.Max(A.X, Math.Max(B.X, C.X)));

        int minY = Math.Max(0, Math.Min(A.Y, Math.Min(B.Y, C.Y)));
        int maxY = Math.Min(bitmap.Height - 1, Math.Max(A.Y, Math.Max(B.Y, C.Y)));

        double denominator =
            (B.Y - C.Y) * (A.X - C.X) +
            (C.X - B.X) * (A.Y - C.Y);

        if (Math.Abs(denominator) < 0.000001)
            return;

        for (int y = minY; y <= maxY; y++)
        {
            for (int x = minX; x <= maxX; x++)
            {
                double alpha =
                    ((B.Y - C.Y) * (x - C.X) +
                     (C.X - B.X) * (y - C.Y)) / denominator;

                double beta =
                    ((C.Y - A.Y) * (x - C.X) +
                     (A.X - C.X) * (y - C.Y)) / denominator;

                double gamma = 1 - alpha - beta;

                if (alpha >= 0 && beta >= 0 && gamma >= 0)
                {
                    int r = (int)Math.Clamp(
                        alpha * colorA.R +
                        beta * colorB.R +
                        gamma * colorC.R, 0, 255);

                    int g = (int)Math.Clamp(
                        alpha * colorA.G +
                        beta * colorB.G +
                        gamma * colorC.G, 0, 255);

                    int b = (int)Math.Clamp(
                        alpha * colorA.B +
                        beta * colorB.B +
                        gamma * colorC.B, 0, 255);

                    bitmap.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        if (bitmap != null)
            e.Graphics.DrawImageUnscaled(bitmap, 0, 0);
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        bitmap?.Dispose();
        base.OnFormClosed(e);
    }
}