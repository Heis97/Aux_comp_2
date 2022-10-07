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

namespace Aux_gpu
{
    public partial class Scene : Form
    {
        private GraphicGL GL1 = new GraphicGL();

        public Scene()
        {
            InitializeComponent();
            glControl1.MouseWheel += GlControl1_MouseWheel;
            PreInitializeScene();
        }

        public void PreInitializeScene()
        {
            GL1.addGlobalFrame(100);
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
                var porose = Convert.ToSingle(t_box_porose.Text);
                var pore_size = Convert.ToSingle(t_box_pore_size.Text);
                GL1.show_cur_porose(porose, pore_size);
            }
            catch
            {
            }
        }

        private void but_planeXY_Click(object sender, EventArgs e)
        {
            GL1.planeXY();
        }
    }
}
