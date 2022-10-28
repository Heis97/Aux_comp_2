
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
            this.t_box_porose = new System.Windows.Forms.TextBox();
            this.t_box_pore_size = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.but_comp = new System.Windows.Forms.Button();
            this.but_comp_def = new System.Windows.Forms.Button();
            this.but_stl_gen = new System.Windows.Forms.Button();
            this.textBox_g_h = new System.Windows.Forms.TextBox();
            this.textBox_g_l = new System.Windows.Forms.TextBox();
            this.textBox_g_t = new System.Windows.Forms.TextBox();
            this.textBox_g_theta = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.but_save_stl = new System.Windows.Forms.Button();
            this.tb_model_name = new System.Windows.Forms.TextBox();
            this.tb_filtr_dist = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tb_porose_eps = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
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
            this.glControl1.Location = new System.Drawing.Point(769, 0);
            this.glControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.glControl1.MultisampleBits = ((uint)(8u));
            this.glControl1.Name = "glControl1";
            this.glControl1.Size = new System.Drawing.Size(1132, 1029);
            this.glControl1.StencilBits = ((uint)(0u));
            this.glControl1.TabIndex = 0;
            this.glControl1.ContextCreated += new System.EventHandler<OpenGL.GlControlEventArgs>(this.glControl1_ContextCreated);
            this.glControl1.Render += new System.EventHandler<OpenGL.GlControlEventArgs>(this.glControl1_Render);
            this.glControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseDown);
            this.glControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseMove);
            // 
            // t_box_porose
            // 
            this.t_box_porose.Location = new System.Drawing.Point(100, 5);
            this.t_box_porose.Name = "t_box_porose";
            this.t_box_porose.Size = new System.Drawing.Size(100, 20);
            this.t_box_porose.TabIndex = 4;
            this.t_box_porose.Text = "50";
            // 
            // t_box_pore_size
            // 
            this.t_box_pore_size.Location = new System.Drawing.Point(100, 31);
            this.t_box_pore_size.Name = "t_box_pore_size";
            this.t_box_pore_size.Size = new System.Drawing.Size(100, 20);
            this.t_box_pore_size.TabIndex = 5;
            this.t_box_pore_size.Text = "0.6";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Пористость, %";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Размер пор, мм";
            // 
            // but_comp
            // 
            this.but_comp.Location = new System.Drawing.Point(100, 118);
            this.but_comp.Name = "but_comp";
            this.but_comp.Size = new System.Drawing.Size(100, 30);
            this.but_comp.TabIndex = 8;
            this.but_comp.Text = "Расчёт";
            this.but_comp.UseVisualStyleBackColor = true;
            this.but_comp.Visible = false;
            this.but_comp.Click += new System.EventHandler(this.but_comp_Click);
            // 
            // but_comp_def
            // 
            this.but_comp_def.Location = new System.Drawing.Point(100, 118);
            this.but_comp_def.Name = "but_comp_def";
            this.but_comp_def.Size = new System.Drawing.Size(100, 30);
            this.but_comp_def.TabIndex = 10;
            this.but_comp_def.Text = "Расчитать";
            this.but_comp_def.UseVisualStyleBackColor = true;
            this.but_comp_def.Click += new System.EventHandler(this.but_comp_def_Click);
            // 
            // but_stl_gen
            // 
            this.but_stl_gen.Location = new System.Drawing.Point(662, 118);
            this.but_stl_gen.Name = "but_stl_gen";
            this.but_stl_gen.Size = new System.Drawing.Size(100, 41);
            this.but_stl_gen.TabIndex = 11;
            this.but_stl_gen.Text = "Генерировать модель";
            this.but_stl_gen.UseVisualStyleBackColor = true;
            this.but_stl_gen.Click += new System.EventHandler(this.but_stl_gen_Click);
            // 
            // textBox_g_h
            // 
            this.textBox_g_h.Location = new System.Drawing.Point(662, 14);
            this.textBox_g_h.Name = "textBox_g_h";
            this.textBox_g_h.Size = new System.Drawing.Size(100, 20);
            this.textBox_g_h.TabIndex = 12;
            this.textBox_g_h.Text = "0.58";
            // 
            // textBox_g_l
            // 
            this.textBox_g_l.Location = new System.Drawing.Point(662, 40);
            this.textBox_g_l.Name = "textBox_g_l";
            this.textBox_g_l.Size = new System.Drawing.Size(100, 20);
            this.textBox_g_l.TabIndex = 13;
            this.textBox_g_l.Text = "0.143";
            // 
            // textBox_g_t
            // 
            this.textBox_g_t.Location = new System.Drawing.Point(662, 66);
            this.textBox_g_t.Name = "textBox_g_t";
            this.textBox_g_t.Size = new System.Drawing.Size(100, 20);
            this.textBox_g_t.TabIndex = 14;
            this.textBox_g_t.Text = "0.2";
            // 
            // textBox_g_theta
            // 
            this.textBox_g_theta.Location = new System.Drawing.Point(662, 92);
            this.textBox_g_theta.Name = "textBox_g_theta";
            this.textBox_g_theta.Size = new System.Drawing.Size(100, 20);
            this.textBox_g_theta.TabIndex = 15;
            this.textBox_g_theta.Text = "55.6";
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(3, 165);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 20;
            this.dataGridView1.Size = new System.Drawing.Size(759, 650);
            this.dataGridView1.TabIndex = 16;
            this.dataGridView1.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEnter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(621, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "h, мм";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(625, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "l, мм";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(624, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "t, мм";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(621, 95);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 13);
            this.label6.TabIndex = 20;
            // 
            // but_save_stl
            // 
            this.but_save_stl.BackColor = System.Drawing.SystemColors.Control;
            this.but_save_stl.Location = new System.Drawing.Point(515, 43);
            this.but_save_stl.Name = "but_save_stl";
            this.but_save_stl.Size = new System.Drawing.Size(100, 41);
            this.but_save_stl.TabIndex = 21;
            this.but_save_stl.Text = "Сохранить STL";
            this.but_save_stl.UseVisualStyleBackColor = false;
            this.but_save_stl.Click += new System.EventHandler(this.but_save_stl_Click);
            // 
            // tb_model_name
            // 
            this.tb_model_name.Location = new System.Drawing.Point(423, 17);
            this.tb_model_name.Name = "tb_model_name";
            this.tb_model_name.Size = new System.Drawing.Size(192, 20);
            this.tb_model_name.TabIndex = 22;
            this.tb_model_name.Text = "model_name";
            // 
            // tb_filtr_dist
            // 
            this.tb_filtr_dist.Location = new System.Drawing.Point(100, 62);
            this.tb_filtr_dist.Name = "tb_filtr_dist";
            this.tb_filtr_dist.Size = new System.Drawing.Size(100, 20);
            this.tb_filtr_dist.TabIndex = 23;
            this.tb_filtr_dist.Text = "10.1";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 60);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 26);
            this.label7.TabIndex = 24;
            this.label7.Text = "Разреженность\r\nрешений";
            // 
            // tb_porose_eps
            // 
            this.tb_porose_eps.Location = new System.Drawing.Point(232, 5);
            this.tb_porose_eps.Name = "tb_porose_eps";
            this.tb_porose_eps.Size = new System.Drawing.Size(100, 20);
            this.tb_porose_eps.TabIndex = 25;
            this.tb_porose_eps.Text = "1";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(206, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(24, 13);
            this.label8.TabIndex = 26;
            this.label8.Text = "eps";
            // 
            // Scene
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tb_porose_eps);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tb_filtr_dist);
            this.Controls.Add(this.tb_model_name);
            this.Controls.Add(this.but_save_stl);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_g_theta);
            this.Controls.Add(this.textBox_g_t);
            this.Controls.Add(this.textBox_g_l);
            this.Controls.Add(this.textBox_g_h);
            this.Controls.Add(this.but_stl_gen);
            this.Controls.Add(this.but_comp_def);
            this.Controls.Add(this.but_comp);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.t_box_pore_size);
            this.Controls.Add(this.t_box_porose);
            this.Controls.Add(this.glControl1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Scene";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OpenGL.GlControl glControl1;
        private System.Windows.Forms.TextBox t_box_porose;
        private System.Windows.Forms.TextBox t_box_pore_size;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button but_comp;
        private System.Windows.Forms.Button but_comp_def;
        private System.Windows.Forms.Button but_stl_gen;
        private System.Windows.Forms.TextBox textBox_g_h;
        private System.Windows.Forms.TextBox textBox_g_l;
        private System.Windows.Forms.TextBox textBox_g_t;
        private System.Windows.Forms.TextBox textBox_g_theta;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button but_save_stl;
        private System.Windows.Forms.TextBox tb_model_name;
        private System.Windows.Forms.TextBox tb_filtr_dist;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tb_porose_eps;
        private System.Windows.Forms.Label label8;
    }
}

