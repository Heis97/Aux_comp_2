using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Graphic;
using Objects;
using OpenGL;
using Model;
using Geometry;
using System.Threading;
using AuxComp;
using System.IO;

namespace Aux_gpu
{
    public partial class Scene : Form
    {
        private GraphicGL GL1 = new GraphicGL();
        float[] mesh = null;
        public Scene()
        {
            InitializeComponent();
            glControl1.MouseWheel += GlControl1_MouseWheel;
            
            PreInitializeScene();
            
        }

        public void PreInitializeScene()
        {
            label6.Text = "\u03B8, мм";
            GL1.addGlobalFrame(2);
            var h = Convert.ToDouble(textBox_g_h.Text);
            var l = Convert.ToDouble(textBox_g_l.Text);
            var t = Convert.ToDouble(textBox_g_t.Text);
            var theta = Convert.ToDouble(textBox_g_theta.Text);

            var mesh = Generate_stl.gen_aucs(h, l, t, theta);
            GL1.addMesh(mesh, PrimitiveType.Triangles);
        }
        
        #region gl_control

        private void glControl1_Render(object sender, GlControlEventArgs e)
        {
            GL1.glControl_Render(sender, e);

        }

        private void glControl1_ContextCreated(object sender, GlControlEventArgs e)
        {
            GL1.glControl_ContextCreated(sender, e);
            var send = (Control)sender;
            var w = send.Width;
            var h = send.Height;
            Console.WriteLine(w + " " + h);
            GL1.addMonitor(new Rectangle(0, 0, w, h), 0);
            GL1.SortObj();
        }
      
        private void GlControl1_MouseWheel(object sender, MouseEventArgs e)
        {
            GL1.Form1_mousewheel(sender, e);
        }
        private void glControl1_MouseDown(object sender, MouseEventArgs e)
        {
            GL1.glControl_MouseDown(sender, e);
        }

        private void glControl1_MouseMove(object sender, MouseEventArgs e)
        {

            GL1.glControl_MouseMove(sender, e);

        }
        #endregion

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            var val = ((TrackBar)sender).Value;
            var pore_size = Convert.ToSingle(t_box_pore_size.Text);
            GL1.show_cur_porose((float)val / 5f,pore_size);
        }

        private void but_comp_Click(object sender, EventArgs e)
        {
            try
            {
                var porose = Convert.ToSingle(t_box_porose.Text);
                var pore_size = Convert.ToSingle(t_box_pore_size.Text);
                GL1.show_cur_porose(porose, pore_size);
            }
            catch
            {
            }
        }

       

        private void but_comp_def_Click(object sender, EventArgs e)
        {
            
            try
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();
                var porose = Convert.ToSingle(t_box_porose.Text);
                var pore_size = Convert.ToSingle(t_box_pore_size.Text);
                Console.WriteLine("beg");
                GL1.filtr_dist = Convert.ToSingle(tb_filtr_dist.Text);
                GL1.porose_eps = Convert.ToSingle(tb_porose_eps.Text);
                var ret = GL1.gpuCompute_Aux_def_2(porose, pore_size);
                Console.WriteLine("end");
                //richTextBox1.Text = AuxProc.data_to_str(ret);
                fill_table(ret);
                saveData(ret, porose+" "+ pore_size + " "+ GL1.filtr_dist + " "+ GL1.porose_eps);
            }
            catch
            {
            }
        }

        void fill_table(float[][] data)
        {
            //dataGridView1.Dock = DockStyle.Fill;
            if (data.Length==0)
            {
                label_no_sol.Text = "solutions not found";
                Console.WriteLine("solutions not found");
                return;
            }
            label_no_sol.Text = "";
            dataGridView1.RowCount = data.Length;
            dataGridView1.ColumnCount = data[0].Length;
            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < data[i].Length; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = data[i][j];
                }
            }
            dataGridView1.Refresh();
           
        }
        void saveData(float[][] data,string name)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < data[i].Length; j++)
                {
                    sb.Append(data[i][j]+" ");
                }
                sb.Append("\n");
            }
            var wr = new StreamWriter(name + ".txt");
            wr.Write(sb);
            Console.WriteLine("stopWRITE");
            wr.Close();
        }
        private void but_planeXY_Click(object sender, EventArgs e)
        {
            GL1.planeXY();
        }

        private void but_stl_gen_Click(object sender, EventArgs e)
        {
            var h = Convert.ToDouble(textBox_g_h.Text);
            var l = Convert.ToDouble(textBox_g_l.Text);
            var t = Convert.ToDouble(textBox_g_t.Text);
            var theta = Convert.ToDouble(textBox_g_theta.Text);
            mesh = Generate_stl.gen_aucs(h, l, t,theta);
            //GL1.add_buff_gl(mesh, mesh, mesh, PrimitiveType.Triangles);
            GL1.buffersGl.objs = new List<openGlobj>();
            GL1.addGlobalFrame(2);
            GL1.addMesh(mesh, PrimitiveType.Triangles);
            //GL1.addMesh(mesh, PrimitiveType.Lines);
            GL1.SortObj();
        }

       

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            var row = dataGridView1.CurrentCell.RowIndex;
            var col = dataGridView1.CurrentCell.ColumnIndex;
            var ret = new float[dataGridView1.Rows[row].Cells.Count];
            for (int i = 0; i < dataGridView1.Rows[row].Cells.Count; i++) 
                ret[i] = Convert.ToSingle( dataGridView1.Rows[row].Cells[i].Value);

            if(ret.Length>3)
            {
                textBox_g_h.Text = ret[0].ToString();
                textBox_g_l.Text = ret[1].ToString();
                textBox_g_t.Text = ret[2].ToString();
                textBox_g_theta.Text = ret[3].ToString();
            }

        }

        private void but_save_stl_Click(object sender, EventArgs e)
        {
            Console.WriteLine(mesh);
            if(mesh!=null)
            {
                STLmodel.saveMesh(mesh, tb_model_name.Text);
            }
        }
    }
}
