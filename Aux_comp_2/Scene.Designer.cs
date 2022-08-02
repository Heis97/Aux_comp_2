
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
            this.label_fps = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.t_box_point_size = new System.Windows.Forms.TextBox();
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
            this.glControl1.Location = new System.Drawing.Point(1, 0);
            this.glControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.glControl1.MultisampleBits = ((uint)(8u));
            this.glControl1.Name = "glControl1";
            this.glControl1.Size = new System.Drawing.Size(1900, 1039);
            this.glControl1.StencilBits = ((uint)(0u));
            this.glControl1.TabIndex = 0;
            this.glControl1.ContextCreated += new System.EventHandler<OpenGL.GlControlEventArgs>(this.glControl1_ContextCreated);
            this.glControl1.Render += new System.EventHandler<OpenGL.GlControlEventArgs>(this.glControl1_Render);
            this.glControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseDown);
            this.glControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseMove);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(1490, 12);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(402, 477);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            this.richTextBox1.Visible = false;
            // 
            // label_fps
            // 
            this.label_fps.AutoSize = true;
            this.label_fps.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label_fps.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label_fps.ForeColor = System.Drawing.Color.Lime;
            this.label_fps.Location = new System.Drawing.Point(13, 13);
            this.label_fps.Name = "label_fps";
            this.label_fps.Size = new System.Drawing.Size(31, 20);
            this.label_fps.TabIndex = 2;
            this.label_fps.Text = "fps";
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
            // t_box_point_size
            // 
            this.t_box_point_size.Location = new System.Drawing.Point(1792, 495);
            this.t_box_point_size.Name = "t_box_point_size";
            this.t_box_point_size.Size = new System.Drawing.Size(100, 20);
            this.t_box_point_size.TabIndex = 4;
            this.t_box_point_size.Text = "1";
            this.t_box_point_size.DoubleClick += new System.EventHandler(this.t_box_point_size_DoubleClick);
            // 
            // Scene
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.t_box_point_size);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.label_fps);
            this.Controls.Add(this.richTextBox1);
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
        private System.Windows.Forms.Label label_fps;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.TextBox t_box_point_size;
    }
}

