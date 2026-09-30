namespace Graphics3
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pictureBox1 = new PictureBox();
            label1 = new Label();
            label2 = new Label();
            buttonDraw = new Button();
            label3 = new Label();
            comboBoxAlg = new ComboBox();
            textBoxM = new TextBox();
            textBoxC = new TextBox();
            label4 = new Label();
            label5 = new Label();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            buttonLoad = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(95, 164);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1281, 733);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(95, 30);
            label1.Name = "label1";
            label1.Size = new Size(213, 32);
            label1.TabIndex = 1;
            label1.Text = "Отрисовка линии:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(95, 99);
            label2.Name = "label2";
            label2.Size = new Size(156, 32);
            label2.TabIndex = 2;
            label2.Text = "Параметр m:";
            // 
            // buttonDraw
            // 
            buttonDraw.Location = new Point(1186, 92);
            buttonDraw.Name = "buttonDraw";
            buttonDraw.Size = new Size(150, 46);
            buttonDraw.TabIndex = 3;
            buttonDraw.Text = "Отрисовать";
            buttonDraw.UseVisualStyleBackColor = true;
            buttonDraw.Click += buttonDraw_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(492, 99);
            label3.Name = "label3";
            label3.Size = new Size(146, 32);
            label3.TabIndex = 4;
            label3.Text = "Параметр c:";
            // 
            // comboBoxAlg
            // 
            comboBoxAlg.Cursor = Cursors.No;
            comboBoxAlg.FormattingEnabled = true;
            comboBoxAlg.Location = new Point(883, 95);
            comboBoxAlg.Name = "comboBoxAlg";
            comboBoxAlg.Size = new Size(242, 40);
            comboBoxAlg.TabIndex = 5;
            // 
            // textBoxM
            // 
            textBoxM.Location = new Point(257, 96);
            textBoxM.Name = "textBoxM";
            textBoxM.Size = new Size(176, 39);
            textBoxM.TabIndex = 6;
            // 
            // textBoxC
            // 
            textBoxC.Location = new Point(644, 96);
            textBoxC.Name = "textBoxC";
            textBoxC.Size = new Size(183, 39);
            textBoxC.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(95, 924);
            label4.Name = "label4";
            label4.Size = new Size(338, 32);
            label4.TabIndex = 8;
            label4.Text = "Оригинальное изображение:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(962, 924);
            label5.Name = "label5";
            label5.Size = new Size(244, 32);
            label5.TabIndex = 9;
            label5.Text = "Выделение границы:";
            // 
            // pictureBox2
            // 
            pictureBox2.Location = new Point(95, 959);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(835, 596);
            pictureBox2.TabIndex = 10;
            pictureBox2.TabStop = false;
            pictureBox2.MouseClick += pictureBox2_MouseClick;
            // 
            // pictureBox3
            // 
            pictureBox3.Location = new Point(962, 959);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(835, 596);
            pictureBox3.TabIndex = 11;
            pictureBox3.TabStop = false;
            // 
            // buttonLoad
            // 
            buttonLoad.Location = new Point(336, 1561);
            buttonLoad.Name = "buttonLoad";
            buttonLoad.Size = new Size(327, 46);
            buttonLoad.TabIndex = 12;
            buttonLoad.Text = "Загрузить изображение";
            buttonLoad.UseVisualStyleBackColor = true;
            buttonLoad.Click += buttonLoad_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1819, 1625);
            Controls.Add(buttonLoad);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(textBoxC);
            Controls.Add(textBoxM);
            Controls.Add(comboBoxAlg);
            Controls.Add(label3);
            Controls.Add(buttonDraw);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Name = "Form1";
            Text = "Выделение границы и рисование отрезка";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Label label1;
        private Label label2;
        private Button buttonDraw;
        private Label label3;
        private ComboBox comboBoxAlg;
        private TextBox textBoxM;
        private TextBox textBoxC;
        private Label label4;
        private Label label5;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private Button buttonLoad;
    }
}
