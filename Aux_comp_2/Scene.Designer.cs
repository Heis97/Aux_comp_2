
namespace Aux_gpu
{
    partial class Scene
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.glControl1 = new OpenGL.GlControl();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.t_box_porose = new System.Windows.Forms.TextBox();
            this.t_box_pore_size = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.but_comp = new System.Windows.Forms.Button();
            this.but_planeXY = new System.Windows.Forms.Button();
            this.but_comp_def = new System.Windows.Forms.Button();
            this.but_stl_gen = new System.Windows.Forms.Button();
            this.textBox_g_h = new System.Windows.Forms.TextBox();
            this.textBox_g_l = new System.Windows.Forms.TextBox();
            this.textBox_g_t = new System.Windows.Forms.TextBox();
            this.textBox_g_theta = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // glControl1
            // 
            this.glControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.glControl1.Animation = true;
            this.glControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.glControl1.ColorBits = ((uint)(24u));
            this.glControl1.DebugContext = OpenGL.GlControl.AttributePermission.DonCare;
            this.glControl1.DepthBits = ((uint)(24u));
            this.glControl1.Location = new System.Drawing.Point(421, 0);
            this.glControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.glControl1.MultisampleBits = ((uint)(8u));
            this.glControl1.Name = "glControl1";
            this.glControl1.Size = new System.Drawing.Size(1480, 978);
            this.glControl1.StencilBits = ((uint)(0u));
            this.glControl1.TabIndex = 0;
            this.glControl1.ContextCreated += new System.EventHandler<OpenGL.GlControlEventArgs>(this.glControl1_ContextCreated);
            this.glControl1.Render += new System.EventHandler<OpenGL.GlControlEventArgs>(this.glControl1_Render);
            this.glControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseDown);
            this.glControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseMove);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 290);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(402, 652);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(17, 984);
            this.trackBar1.Maximum = 500;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(1875, 45);
            this.trackBar1.TabIndex = 3;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // t_box_porose
            // 
            this.t_box_porose.Location = new System.Drawing.Point(314, 12);
            this.t_box_porose.Name = "t_box_porose";
            this.t_box_porose.Size = new System.Drawing.Size(100, 20);
            this.t_box_porose.TabIndex = 4;
            this.t_box_porose.Text = "1";
            // 
            // t_box_pore_size
            // 
            this.t_box_pore_size.Location = new System.Drawing.Point(314, 38);
            this.t_box_pore_size.Name = "t_box_pore_size";
            this.t_box_pore_size.Size = new System.Drawing.Size(100, 20);
            this.t_box_pore_size.TabIndex = 5;
            this.t_box_pore_size.Text = "1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(227, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Пористость, %";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(219, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Размер пор, мм";
            // 
            // but_comp
            // 
            this.but_comp.Location = new System.Drawing.Point(314, 64);
            this.but_comp.Name = "but_comp";
            this.but_comp.Size = new System.Drawing.Size(100, 30);
            this.but_comp.TabIndex = 8;
            this.but_comp.Text = "Расчёт";
            this.but_comp.UseVisualStyleBackColor = true;
            this.but_comp.Click += new System.EventHandler(this.but_comp_Click);
            // 
            // but_planeXY
            // 
            this.but_planeXY.Location = new System.Drawing.Point(314, 948);
            this.but_planeXY.Name = "but_planeXY";
            this.but_planeXY.Size = new System.Drawing.Size(100, 30);
            this.but_planeXY.TabIndex = 9;
            this.but_planeXY.Text = "XY";
            this.but_planeXY.UseVisualStyleBackColor = true;
            this.but_planeXY.Click += new System.EventHandler(this.but_planeXY_Click);
            // 
            // but_comp_def
            // 
            this.but_comp_def.Location = new System.Drawing.Point(314, 100);
            this.but_comp_def.Name = "but_comp_def";
            this.but_comp_def.Size = new System.Drawing.Size(100, 30);
            this.but_comp_def.TabIndex = 10;
            this.but_comp_def.Text = "Расчёт def";
            this.but_comp_def.UseVisualStyleBackColor = true;
            this.but_comp_def.Click += new System.EventHandler(this.but_comp_def_Click);
            // 
            // but_stl_gen
            // 
            this.but_stl_gen.Location = new System.Drawing.Point(314, 243);
            this.but_stl_gen.Name = "but_stl_gen";
            this.but_stl_gen.Size = new System.Drawing.Size(100, 41);
            this.but_stl_gen.TabIndex = 11;
            this.but_stl_gen.Text = "Генерировать модель";
            this.but_stl_gen.UseVisualStyleBackColor = true;
            this.but_stl_gen.Click += new System.EventHandler(this.but_stl_gen_Click);
            // 
            // textBox_g_h
            // 
            this.textBox_g_h.Location = new System.Drawing.Point(208, 183);
            this.textBox_g_h.Name = "textBox_g_h";
            this.textBox_g_h.Size = new System.Drawing.Size(100, 20);
            this.textBox_g_h.TabIndex = 12;
            this.textBox_g_h.Text = "5.8";
            // 
            // textBox_g_l
            // 
            this.textBox_g_l.Location = new System.Drawing.Point(208, 209);
            this.textBox_g_l.Name = "textBox_g_l";
            this.textBox_g_l.Size = new System.Drawing.Size(100, 20);
            this.textBox_g_l.TabIndex = 13;
            this.textBox_g_l.Text = "1.43";
            // 
            // textBox_g_t
            // 
            this.textBox_g_t.Location = new System.Drawing.Point(208, 235);
            this.textBox_g_t.Name = "textBox_g_t";
            this.textBox_g_t.Size = new System.Drawing.Size(100, 20);
            this.textBox_g_t.TabIndex = 14;
            this.textBox_g_t.Text = "2.03";
            // 
            // textBox_g_theta
            // 
            this.textBox_g_theta.Location = new System.Drawing.Point(208, 261);
            this.textBox_g_theta.Name = "textBox_g_theta";
            this.textBox_g_theta.Size = new System.Drawing.Size(100, 20);
            this.textBox_g_theta.TabIndex = 15;
            this.textBox_g_theta.Text = "55.6";
            // 
            // Scene
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.textBox_g_theta);
            this.Controls.Add(this.textBox_g_t);
            this.Controls.Add(this.textBox_g_l);
            this.Controls.Add(this.textBox_g_h);
            this.Controls.Add(this.but_stl_gen);
            this.Controls.Add(this.but_comp_def);
            this.Controls.Add(this.but_planeXY);
            this.Controls.Add(this.but_comp);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.t_box_pore_size);
            this.Controls.Add(this.t_box_porose);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.glControl1);
            this.Name = "Scene";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OpenGL.GlControl glControl1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.TextBox t_box_porose;
        private System.Windows.Forms.TextBox t_box_pore_size;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button but_comp;
        private System.Windows.Forms.Button but_planeXY;
        private System.Windows.Forms.Button but_comp_def;
        private System.Windows.Forms.Button but_stl_gen;
        private System.Windows.Forms.TextBox textBox_g_h;
        private System.Windows.Forms.TextBox textBox_g_l;
        private System.Windows.Forms.TextBox textBox_g_t;
        private System.Windows.Forms.TextBox textBox_g_theta;
    }
}

