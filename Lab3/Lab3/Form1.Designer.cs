namespace LabRasterGraphics
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.FlowLayoutPanel topPanel;
        private System.Windows.Forms.Button btnChooseColor;
        private System.Windows.Forms.Panel pnlColorPreview;
        private System.Windows.Forms.RadioButton rbSolid;
        private System.Windows.Forms.RadioButton rbPattern;
        private System.Windows.Forms.Button btnLoadPattern;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblStatus;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.topPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.btnChooseColor = new System.Windows.Forms.Button();
            this.pnlColorPreview = new System.Windows.Forms.Panel();
            this.rbSolid = new System.Windows.Forms.RadioButton();
            this.rbPattern = new System.Windows.Forms.RadioButton();
            this.btnLoadPattern = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.topPanel.SuspendLayout();
            this.SuspendLayout();

            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Height = 40;
            this.topPanel.Padding = new System.Windows.Forms.Padding(5);
            this.topPanel.WrapContents = false;
            this.topPanel.AutoScroll = true;
            this.topPanel.BackColor = System.Drawing.Color.LightGray;

            this.btnChooseColor.Text = "Выбрать цвет";
            this.btnChooseColor.Size = new System.Drawing.Size(110, 28);
            this.btnChooseColor.Click += this.BtnChooseColor_Click;

            this.pnlColorPreview.Size = new System.Drawing.Size(28, 28);
            this.pnlColorPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlColorPreview.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);

            this.rbSolid.Text = "Сплошная";
            this.rbSolid.Checked = true;
            this.rbSolid.AutoSize = true;
            this.rbSolid.Margin = new System.Windows.Forms.Padding(8, 5, 5, 5);

            this.rbPattern.Text = "Узором";
            this.rbPattern.AutoSize = true;
            this.rbPattern.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.rbSolid.CheckedChanged += this.RbMode_CheckedChanged;
            this.rbPattern.CheckedChanged += this.RbMode_CheckedChanged;

            this.btnLoadPattern.Text = "Загрузить узор";
            this.btnLoadPattern.Size = new System.Drawing.Size(120, 28);
            this.btnLoadPattern.Click += this.BtnLoadPattern_Click;

            this.btnClear.Text = "Очистить холст";
            this.btnClear.Size = new System.Drawing.Size(110, 28);
            this.btnClear.Click += this.BtnClear_Click;

            this.lblStatus.AutoSize = false;
            this.lblStatus.Size = new System.Drawing.Size(320, 28);
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblStatus.Margin = new System.Windows.Forms.Padding(8, 0, 5, 0);

            this.topPanel.Controls.Add(this.btnChooseColor);
            this.topPanel.Controls.Add(this.pnlColorPreview);
            this.topPanel.Controls.Add(this.rbSolid);
            this.topPanel.Controls.Add(this.rbPattern);
            this.topPanel.Controls.Add(this.btnLoadPattern);
            this.topPanel.Controls.Add(this.btnClear);
            this.topPanel.Controls.Add(this.lblStatus);

            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Cross;
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Normal;
            this.pictureBox1.MouseDown += this.PictureBox1_MouseDown;
            this.pictureBox1.MouseMove += this.PictureBox1_MouseMove;
            this.pictureBox1.MouseUp += this.PictureBox1_MouseUp;
            this.pictureBox1.Resize += this.PictureBox1_Resize;

            this.ClientSize = new System.Drawing.Size(900, 650);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.topPanel);
            this.MinimumSize = new System.Drawing.Size(650, 300);
            this.Text = "Лабораторная работа: Заливка (1а и 1б)";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.topPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}
