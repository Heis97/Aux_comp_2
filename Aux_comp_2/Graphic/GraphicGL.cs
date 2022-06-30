using OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Geometry;
using Objects;
using Model;

namespace Graphic
{   
    public enum modeGL { Paint, View}
    public enum viewType { Perspective, Ortho }
    public class TextureGL
    {
        public uint id;
        public int binding;
        public int w, h,ch;
        public PixelFormat pixelFormat;
        InternalFormat internalFormat;
        public float[] data;
        public TextureGL()
        {
        }
        public TextureGL(int _binding, int _w, int _h = 1, PixelFormat _pixelFormat = PixelFormat.Red, float[] _data = null)
        {
            Console.WriteLine("genTexture");    
            if(_data!=null)
            {
                data = (float[])_data.Clone();
            }
            else
            {
                data = null;
            }
            var buff = genTexture(_binding, _w, _h, _pixelFormat,data);
            id = buff;
            binding = _binding;
            w = _w;
            h = _h;
            pixelFormat = _pixelFormat;
            Console.WriteLine("bind "+binding+"; w "+ w + " h " + h + " ch " + ch+"; "+ pixelFormat);
        }
        public float[] getData()
        {
            Gl.BindTexture(TextureTarget.Texture2d, id);
            float[] dataf = new float[w*h*ch ];
            Gl.GetTexImage(TextureTarget.Texture2d, 0, pixelFormat, PixelType.Float, dataf);
            //Console.WriteLine(w+" "+h+" "+ch+" "+ dataf.Length);
            return dataf;
        }
        public void setData(float[] data)
        {
            Gl.BindTexture(TextureTarget.Texture2d, id);
            Gl.TexImage2D(TextureTarget.Texture2d, 0, internalFormat, w, h, 0,pixelFormat, PixelType.Float, data);
        }
        uint genTexture(int binding, int w, int h = 1, PixelFormat pixelFormat = PixelFormat.Red, float[] data = null)
        {
            if (pixelFormat == PixelFormat.Red)
            {
                ch = 1;
            }
            else if (pixelFormat == PixelFormat.Rg)
            {
                ch = 2;
            }
            else if (pixelFormat == PixelFormat.Rgb)
            {
                ch = 3;
            }
            else if (pixelFormat == PixelFormat.Rgba)
            {
                ch = 4;
            }
            else
            {
                ch = 1;
            }
            var buff_texture = Gl.GenTexture();
            Gl.ActiveTexture(TextureUnit.Texture0 + binding);
            Gl.BindTexture(TextureTarget.Texture2d, buff_texture);

            Gl.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureMagFilter, Gl.NEAREST);
            Gl.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureMinFilter, Gl.NEAREST);
            internalFormat = InternalFormat.R32f;
            if(pixelFormat == PixelFormat.Rgb || pixelFormat == PixelFormat.Rgba)
            {
                internalFormat = InternalFormat.Rgba32f;
            }
            Gl.TexImage2D(TextureTarget.Texture2d, 0, internalFormat, w, h, 0, pixelFormat, PixelType.Float, data);
            
            Gl.BindImageTexture((uint)binding, buff_texture, 0, false, 0, BufferAccess.ReadWrite, internalFormat);
            return buff_texture;
        }

        private void useTexture()
        {
            Gl.ActiveTexture(TextureUnit.Texture0 + binding);
            Gl.BindTexture(TextureTarget.Texture2d, id);
        }
    }
    public  class IDs
    {
        public uint buff_tex;
        public uint buff_tex1;
        public uint programID;
        public uint vert;
        public int[] LocationVPs = new int[4];
        public int[] LocationVs = new int[4];
        public int[] LocationPs = new int[4];
        public int LocationM;
        public int LocationRotM;
        public int LightID;
        public int textureVisID;
        public int LightPowerID;
        public int MaterialDiffuseID;
        public int MaterialAmbientID;
        public int currentMonitor = 1;
        public int MaterialSpecularID;
        public int TextureID;
        public int MouseLocID;
        public int MouseLocGLID;
        public int translMeshID;
        public int targetCamID;
        public int targetCamIndID;
        public int colorOneID;
        public int stindID;

        public int limits_h_ID;
        public int limits_l_ID;
        public int limits_t_ID;
        public int limits_theta_ID;
        public int cur_l_ID;
        public int sizeXY_ID;

        public int pore_size_inp_ID;
        public int porosity_inp_ID;

    }
    public class GraphicGL
    {
        #region vars
        int model_count = 0;
        static float PI = 3.1415926535f;
        public int startGen = 0;
        public int saveImagesLen = 0;
        public int renderdelim = 5;
        public int rendercout = 0;
        public viewType typeProj = viewType.Perspective;
        Size sizeControl;
        Point lastPos;

        int currentMonitor = 1;
        public int textureVis = 0;
        float LightPower = 1.0f;
        public BuffersGl buffersGl = new BuffersGl();
        public Matrix4x4f[] VPs;
        public Matrix4x4f[] Vs;
        public Matrix4x4f[] Ps;
        public Vertex2f MouseLoc;
        public Vertex2f MouseLocGL;
        Vertex3f translMesh = new Vertex3f(0.0f, 0.0f, 0.0f);
        Vertex3f lightPos = new Vertex3f(0f, 0.0f, 0.0f);
        Vertex3f MaterialDiffuse = new Vertex3f(0.5f, 0.5f, 0.5f);
        Vertex3f MaterialAmbient = new Vertex3f(0.2f, 0.2f, 0.2f);
        Vertex3f MaterialSpecular = new Vertex3f(0.1f, 0.1f, 0.1f);

        public modeGL modeGL = modeGL.View;
        List<Point3d_GL> pointsPaint = new List<Point3d_GL>();
        Point3d_GL curPointPaint = new Point3d_GL(0, 0, 0);
        public List<TransRotZoom> transRotZooms = new List<TransRotZoom>();
        public List<TransRotZoom[]> trzForSave;
        public int[] monitorsForGenerate;
        public string pathForSave;
        public Size size = new Size(1,1);
        Size textureSize;
        public Bitmap bmp;




        TextureGL posTimeData, chooseData, objData, auxData, auxData_2d_in;

        
        public ObjectMassGL[] dataComputeShader = new ObjectMassGL[0];
        public ObjectAuxGL[] dataComputeShaderAux = new ObjectAuxGL[0];
        bool initComputeShader = false;
        public float[] resultComputeShader;
        int orb_p_count = 200;


        #endregion

        IDs idsAux_comp_map = new IDs();
        IDs idsAux_ret = new IDs();

        TextureGL auxData_2d_out, porosity_map;
        Vertex2f limits_h = new Vertex2f(5f, 10f);
        Vertex2f limits_l = new Vertex2f(1f, 10f);
        Vertex2f limits_t = new Vertex2f(2f, 3f);
        Vertex2f limits_theta = new Vertex2f(10f, 80f);

        Vertex2f limits_l_norm = new Vertex2f(1f, 10f);
        Vertex2f limits_t_norm = new Vertex2f(2f, 3f);

        Vertex4i sizeXY = new Vertex4i(1, 1, 1, 1);
        int qual_comp = 100;
        int qual_map = 250;
        int depth_map = 30;
        public void glControl_Render(object sender, GlControlEventArgs e)
        {
            gpuCompute_Aux();
        }
        public void glControl_ContextCreated(object sender, GlControlEventArgs e)
        {
            #region initGL
            sizeControl = ((Control)sender).Size;
            Gl.Initialize();
            Gl.Enable(EnableCap.Multisample);
            #endregion

            #region loadShaders
            var ComputeSourceGL = assembCode(new string[] { @"Graphic\Shaders\Comp\CompSh_Aux_2d.glsl" });
            idsAux_comp_map.programID = createShaderCompute(ComputeSourceGL);
            init_vars_gl(idsAux_comp_map);

            #endregion

            init_textures_aux_2d(qual_comp);
            Gl.Enable(EnableCap.DepthTest);
        }
        static float[] getDataFromObjs(ObjectMassGL[] objects)
        {
            var len = ObjectMassGL.getLength();
            var data = new float[objects.Length * len];
            for(int i=0;i< objects.Length;i++)
            {
                var obData = objects[i].getData();
                for (int j = 0; j < len; j++)
                {
                    data[len * i + j] = obData[j];
                }
            }
            return data;
        }

        public void SortObj()
        {
            buffersGl.sortObj();
            if (buffersGl.objs_static != null)
            {
                if (buffersGl.objs_static.Count != 0)
                {
                    for(int i=0; i< buffersGl.objs_static.Count;i++)
                    {
                        buffersGl.objs_static[i] = buffersGl.objs_static[i].setBuffersObj();
                    }
                }
            }
        }

        private bool init_textures(float[] data)
        {


            var len = ObjectMassGL.getLength();

            objData = new TextureGL(0, len/4, data.Length / len, PixelFormat.Rgba, data);
            posTimeData = new TextureGL(1, orb_p_count, data.Length/len ,  PixelFormat.Rgba);
            chooseData = new TextureGL(2, 2, data.Length / len, PixelFormat.Rgba);


            return true;
        }

        private bool init_textures_aux(ObjectAuxGL[] data_aux)
        {
            var data = ObjectAuxGL.getDataFromObjs(data_aux);
            var len = ObjectAuxGL.getLength();
            auxData = new TextureGL(0,len / 4, data_aux.Length,  PixelFormat.Rgba, data);
            Console.WriteLine(toStringBuf(auxData.getData(), 8, 4, "aux"));
            return true;
        }

        private bool init_textures_aux_2d(int qual)
        {

            auxData_2d_out = new TextureGL(0, depth_map, 1, PixelFormat.Rgba);
            porosity_map = new TextureGL(1, qual_map, depth_map, PixelFormat.Rgba);
            //Console.WriteLine(toStringBuf(auxData_2d_out.getData(), qual * 4, 4, "aux_2d"));
            return true;
        }

        private void init_vars_gl(IDs ids)
        {
            Gl.UseProgram(ids.programID);
            
            ids.limits_h_ID = Gl.GetUniformLocation(ids.programID, "limits_h");
            ids.limits_l_ID = Gl.GetUniformLocation(ids.programID, "limits_l");
            ids.limits_t_ID = Gl.GetUniformLocation(ids.programID, "limits_t");
            ids.limits_theta_ID = Gl.GetUniformLocation(ids.programID, "limits_theta");
            ids.cur_l_ID = Gl.GetUniformLocation(ids.programID, "cur_l");
            ids.sizeXY_ID = Gl.GetUniformLocation(ids.programID, "sizeXY");


        }
        private void load_vars_gl(IDs ids)
        {
            Gl.UseProgram(ids.programID);

            Gl.Uniform2f(ids.limits_h_ID, 1, limits_h);
            Gl.Uniform2f(ids.limits_l_ID, 1, limits_l_norm);
            Gl.Uniform2f(ids.limits_t_ID, 1, limits_t_norm);
            Gl.Uniform2f(ids.limits_theta_ID, 1, limits_theta);
            Gl.Uniform4i(ids.sizeXY_ID, 1, sizeXY);
            Gl.Uniform1f(ids.cur_l_ID, 1, 1f);
        }

        private void load_vars_intern_gl(IDs ids,float cur_l)
        {
            Gl.UseProgram(ids.programID);

            Gl.Uniform1f(ids.cur_l_ID, 1, cur_l);
        }
        void gpuCompute_Aux()
        {
            limits_l_norm.x = limits_l.x/ limits_h.x;
            limits_l_norm.y = limits_l.y / limits_h.y;

            limits_t_norm.x = limits_t.x / limits_h.x;
            limits_t_norm.y = limits_t.y / limits_h.y;

            Console.WriteLine(limits_l.x + " " + limits_t.x);
            sizeXY = new Vertex4i(qual_comp, qual_comp,qual_map,depth_map);
            load_vars_gl(idsAux_comp_map);
            for (int i =0; i< qual_comp; i++)
            {
                float l = limits_l.x + (limits_l.y - limits_l.x)*(float)i / (float)qual_comp;
                load_vars_intern_gl(idsAux_comp_map, l);
                Gl.DispatchCompute((uint)auxData_2d_out.w, (uint)auxData_2d_out.h, 1);
                Gl.MemoryBarrier(MemoryBarrierMask.ShaderImageAccessBarrierBit);                
                Console.WriteLine("map create____________________"+(i+1)+"/"+ qual_comp + " done");
            }
            var map_por = porosity_map.getData();
            Console.WriteLine(toStringBuf(porosity_map.getData(),qual_map * 4, 4, "porosity_map"));
            Console.WriteLine("_______________________");
            //Console.WriteLine(toStringBuf(auxData.getData(), 8, 4, "aux"));
        }

        void gpuCompute_Aux_ret()
        {

            load_vars_gl(idsAux_ret);

            Gl.DispatchCompute((uint)auxData_2d_out.w, (uint)auxData_2d_out.h, 1);
            Gl.MemoryBarrier(MemoryBarrierMask.ShaderImageAccessBarrierBit);

            //var map_por = porosity_map.getData();
           // Console.WriteLine(toStringBuf(porosity_map.getData(), qual_map * 4, 4, "porosity_map"));
            Console.WriteLine("_______________________");
            //Console.WriteLine(toStringBuf(auxData.getData(), 8, 4, "aux"));
        }

        #region texture

        Bitmap byteToBitmap(byte[] arr, Size size)
        {
            var bmp = new Bitmap(size.Width, size.Height);
            for (int i = 0; i < size.Width; i++)
            {
                for (int j = 0; j < size.Height; j++)
                {
                    // Console.WriteLine(3 * (j * size.Width + j));
                    var color = Color.FromArgb(
                        arr[3 * (j * size.Width + i)],
                        arr[3 * (j * size.Width + i) + 1],
                        arr[3 * (j * size.Width + i) + 2]
                        );
                    bmp.SetPixel(i, j, color);
                }
            }
            return bmp;
        }
        private uint bindTexture(byte[] arrB)
        {
            var buff_texture = Gl.GenTexture();
            Gl.ActiveTexture(TextureUnit.Texture0);
            Gl.BindTexture(TextureTarget.Texture2d, buff_texture);
            // Gl.TexParameter(TextureTarget.Texture2d, TextureParameterName.TextureWrapS, Gl.REPEAT);

            Gl.TexImage2D(TextureTarget.Texture2d, 0, InternalFormat.Rgb, textureSize.Width, textureSize.Height, 0, PixelFormat.Bgr, PixelType.UnsignedByte, arrB);
            Gl.GenerateMipmap(TextureTarget.Texture2d);
            return buff_texture;
        }

        #endregion

        #region util
        void bufferToCompute(float[] data, int locat)
        {
            var dat_buff = Gl.GenBuffer();
            Gl.BindBuffer(BufferTarget.ShaderStorageBuffer, dat_buff);
            Gl.BufferData(BufferTarget.ShaderStorageBuffer, (uint)(4 * data.Length), data, BufferUsage.StaticDraw);
            Gl.BindBufferBase(BufferTarget.ShaderStorageBuffer, (uint)locat, dat_buff);
            // Gl.BindBuffer(BufferTarget.ShaderStorageBuffer, 0);
        }
        static ObjectMassGL[] setDataToObjs(ObjectMassGL[] objects, float[] data)
        {
            var objs = (ObjectMassGL[])objects.Clone();
            var len = ObjectMassGL.getLength();
            for (int i = 0; i < objects.Length; i++)
            {
                var obData = new float[len];
                for (int j = 0; j < len; j++)
                {
                    obData[j] = data[len * i + j];
                }
                objs[i].setData(obData);
            }
            return objs;
        }

        
        Geometry.PointF toGLcord(Geometry.PointF pf)
        {
            var sizeView = transRotZooms[0].rect;

            var x = (sizeView.Width / 2) * pf.X + sizeView.Width / 2;
            var y = -((sizeView.Width / 2) * pf.Y) + sizeView.Height / 2;
            return new Geometry.PointF(x, y);
        }
        Geometry.PointF toTRZcord(Geometry.PointF pf)
        {
            var sizeView = transRotZooms[0].rect;

            var x = (sizeView.Width / 2) * pf.X + sizeView.Width / 2;
            var y = (sizeView.Width / 2) * pf.Y + sizeView.Height / 2;
            return new Geometry.PointF(x, y);
        }
        int selectTRZ_id(int id)
        {
            int ind = 0;
            foreach (var trz in transRotZooms)
            {
                if (trz.id == id)
                {
                    return ind;
                }
                ind++;
            }
            return -1;
        }

        
        /// <summary>
        /// 3dGL->2dIm
        /// </summary>
        /// <param name="point"></param>
        /// <param name="mvp"></param>
        /// <returns></returns>
        public Geometry.PointF calcPixel(Vertex4f point, int id)
        {
            var p2 = VPs[id] * point;
            p2.Normalize();
            var p3 = toTRZcord(new Geometry.PointF(p2.x, p2.y));
            
            //Console.WriteLine("v: " + p3.X + " " + p3.Y + " " + p2.z + " " + p2.w + " mvp_len: " + MVPs[0].ToString());
            return p3;
        }

        public Geometry.PointF calcPixelInv(Vertex4f point, Matrix4x4f mvp)
        {
            var p2 = mvp.Inverse * point;
            var p4 = p2 / p2.w;
            var p3 = toGLcord(new Geometry.PointF(p4.x, p4.y));
            //Console.WriteLine("v: " + p3.X + " " + p3.Y + " " + p2.z + " " + p2.w + " mvp_len: " + MVPs[0].ToString());
            return p3;
        }

        public void printDebug(RichTextBox box)
        {
            string txt = "";
            txt += "\n______STATIC_________\n";
            foreach (var ob in buffersGl.objs_static)
            {
                txt += toStringBuf(ob.vertex_buffer_data, 3,0, "vert");
                txt += toStringBuf(ob.color_buffer_data, 3, 0, "color");
                txt += toStringBuf(ob.normal_buffer_data, 3, 0, "normal");
                txt += toStringBuf(ob.texture_buffer_data, 2, 0, "textUV");
                txt += "\n________________________\n";
            }
            txt += "\n______DYNAMIC_________\n";
            foreach (var ob in buffersGl.objs_dynamic)
            {
                txt += toStringBuf(ob.vertex_buffer_data, 3, 0, "vert");
                txt += toStringBuf(ob.color_buffer_data, 3, 0, "color");
                txt += toStringBuf(ob.normal_buffer_data, 3, 0, "normal");
                txt += toStringBuf(ob.texture_buffer_data, 2, 0, "textUV");
                txt += "\n________________________\n";
            }
            
            box.Text = txt;
        }

        string toStringBuf(float[] buff, int strip,int substrip,string name)
        {
            if (buff == null)
                return name + " null ";
            string txt = name +" "+buff.Length;
            for (int i = 0; i < buff.Length / strip; i++)
            {
                txt += "  | \n";
                for(int j=0; j<strip; j++)
                {
                    if (j % substrip == 0)
                    {
                        txt += "  | ";
                    }
                    txt += buff[i * strip+j].ToString() + ", ";
                    if(substrip!=0)
                    {
                        
                    }
                    
                }
            }
            txt += " |\n--------------------------------\n";
            return txt;
       
        }
        #endregion

        #region mouse

        public void addMonitor(Rectangle rect,int id)
        {
            transRotZooms.Add(new TransRotZoom(rect,id));
        }
        public void addMonitor(Rectangle rect, int id, Vertex3f rotVer, Vertex3f transVer, int _idMast)
        {
            transRotZooms.Add(new TransRotZoom(rect, id, rotVer, transVer, _idMast));
        }
        int selectTRZ(MouseEventArgs e)
        {
            int ind = 0;
            foreach(var trz in transRotZooms)
            {
                var recGL = new Rectangle(trz.rect.X, sizeControl.Height - trz.rect.Y - trz.rect.Height, trz.rect.Width, sizeControl.Height - trz.rect.Y);
                if(recGL.Contains(e.Location))
                {
                    return ind;
                }
                ind++;
            }
            return -1;
        }


        public void glControl_MouseDown(object sender, MouseEventArgs e)
        {           
            switch(modeGL)
            {
                case modeGL.View:
                    lastPos = e.Location;
                    break;
                case modeGL.Paint:
                    if (e.Button == MouseButtons.Left)
                    {
                        pointsPaint.Add(curPointPaint);
                        
                    }
                    else if (e.Button == MouseButtons.Right)
                    {
                        pointsPaint.Clear();
                    }
                    break;
            }
        }
        public void glControl_MouseMove(object sender, MouseEventArgs e)
        {

            var cont = (Control)sender;
            MouseLocGL = new Vertex2f((float)e.X / (0.5f * (float)cont.Width) - 1f, -((float)e.Y / (0.5f * (float)cont.Height) - 1f));
            int sel_trz = selectTRZ(e);
            if(sel_trz < 0)
            {
                return;
            }
            var trz = transRotZooms[sel_trz];

            
            int w = trz.rect.Width;
            int h = trz.rect.Height;

            transRotZooms[sel_trz] = trz;
        }
        public void Form1_mousewheel(object sender, MouseEventArgs e)
        {
            //Console.WriteLine("P m = " + Pm);
            //Console.WriteLine("V m = " + Vm);
            // var invVm = Vm.Inverse;
            //Console.WriteLine("invV m = " + invVm);
            int sel_trz = selectTRZ(e);
            if (sel_trz < 0)
            {
                return;
            }
            var trz = transRotZooms[sel_trz];
            var angle = e.Delta;
            if (angle > 0)
            { 
                    trz.zoom = 0.7 * trz.zoom;
 
                
            }
            else
            {
                trz.zoom = 1.3 * trz.zoom;
            }
            transRotZooms[sel_trz] = trz;
        }


        public void lightPowerScroll(int value)
        {
            var f = (float)value*100;
            LightPower = f;
        }
        public void diffuseScroll(int value)
        {
            var f = (float)value / 10;
            MaterialDiffuse.x = f;
            MaterialDiffuse.y = f;
            MaterialDiffuse.z = f;
        }
        public void ambientScroll(int value)
        {
            var f = (float)value / 10;
            MaterialAmbient.x = f;
            MaterialAmbient.y = f;
            MaterialAmbient.z = f;
        }
        public void specularScroll(int value)
        {

            var f = (float)value / 10;
            MaterialSpecular.x = f;
            MaterialSpecular.y = f;
            MaterialSpecular.z = f;
        }
        
        public void lightXscroll(int value)
        {
            lightPos.x = (float)value * 10;
        }
        public void lightYscroll(int value)
        {
            lightPos.y = (float)value * 10;

        }
        public void lightZscroll(int value)
        {
            lightPos.z = (float)value * 10;
        }
        public void orientXscroll(int value)
        {
            var trz = transRotZooms[currentMonitor];
            trz.setxRot(value);
            transRotZooms[currentMonitor] = trz;
        }
        public void orientYscroll(int value)
        {
            var trz = transRotZooms[currentMonitor];
            trz.setyRot(value);
            transRotZooms[currentMonitor] = trz;
        }
        public void orientZscroll(int value)
        {
            var trz = transRotZooms[currentMonitor];
            trz.setzRot(value);
            transRotZooms[currentMonitor] = trz;
        }

        public void planeXY()
        {
            var trz = transRotZooms[currentMonitor];
            trz.setRot(0, 0, 0);
            transRotZooms[currentMonitor] = trz;
        }
        public void planeYZ()
        {
            var trz = transRotZooms[currentMonitor];
            trz.setRot(0, 90, 0);
            transRotZooms[currentMonitor] = trz;
        }
        public void planeZX()
        {
            var trz = transRotZooms[currentMonitor];
            trz.setRot(90, 0, 0);
            transRotZooms[currentMonitor] = trz;
        }
        public void changeViewType(int ind)
        {
            if (ind >= 0 && ind<transRotZooms.Count)
            {
                var trz = transRotZooms[ind];
                if(trz.viewType_==viewType.Ortho)
                {
                    trz.viewType_ = viewType.Perspective;
                    transRotZooms[ind] = trz;
                }
                else if (trz.viewType_ == viewType.Perspective)
                {
                    trz.viewType_ = viewType.Ortho;
                    transRotZooms[ind] = trz;
                }

            }
        }

        public void changeVisible(int ind)
        {
            if (ind >= 0 && ind < transRotZooms.Count)
            {
                var trz = transRotZooms[ind];
                if (trz.visible == true)
                {
                    trz.visible = false;
                    transRotZooms[ind] = trz;
                }
                else if (trz.visible == false)
                {
                    trz.visible = true;
                    transRotZooms[ind] = trz;
                }

            }
        }
        public void setMode(modeGL mode)
        {
            modeGL = mode;
        }
        #endregion
        #region mesh
 
        public ObjectMassGL[] loadObjs(ObjectMassGL[] objects, Model3d[] models)
        {
            var objects_s = new ObjectMassGL[objects.Length];//для O(m*n)->O(n) ипользовать двумерный массив
            int ind = 0;
            model_count = models.Length;
            for (int i =0; i<models.Length;i++)
            {
                var cnt_ind = 0;
                var st_ind = ind;
                for (int j = 0; j < objects.Length; j++)
                {
                    if(objects[j].mesh_number == i)
                    {
                        objects_s[ind] = objects[j].Clone();
                        ind++;
                        cnt_ind++;
                    }
                }
                loadModel(models[i], cnt_ind,st_ind,i);
            }
            var data = getDataFromObjs(objects_s);
            //Console.WriteLine(toStringBuf(data, 32, "models"));
            if (!initComputeShader)
            {
                initComputeShader = init_textures(data);
            }
            else
            {
                objData.setData(data);
            }
            return objects_s;
        }



        public void loadModel(Model3d model,int count, int st_ind, int model_ind)
        {
            if (count!=0)
            {
                if (model.normale == null)
                {
                    model.normale = computeNormals(model.mesh);  
                }
                var objgl = new openGlobj(model.mesh, null, model.normale, model.texture, PrimitiveType.Triangles, 1, count);
                objgl.modelind = model_ind;
                objgl.stind = st_ind;
                buffersGl.add_obj(objgl.setBuffersObj());
            } 
        }

        
        float[] toFloat(Point3d_GL[] points)
        {
            var fl = new float[points.Length * 3];
            for(int i=0; i< points.Length; i++)
            {
                fl[3 * i] = (float)points[i].x;
                fl[3 * i+1] = (float)points[i].y;
                fl[3 * i+2] = (float)points[i].z;
            }
            return fl;
        }
        float[] toFloat(Vertex4f[] points)
        {
            var fl = new float[points.Length * 3];
            for (int i = 0; i < points.Length; i++)
            {
                fl[3 * i] = points[i].x;
                fl[3 * i + 1] = points[i].y;
                fl[3 * i + 2] = points[i].z;
            }
            return fl;
        }

        void add_buff_gl_obj(float[] data_v, float[] data_t, float[] data_n, PrimitiveType tp)
        {
            buffersGl.add_obj(new openGlobj(data_v, null, data_n, data_t, tp));
        }
        public void add_buff_gl(float[] data_v, float[] data_c, float[] data_n, PrimitiveType tp)
        {            
            buffersGl.add_obj(new openGlobj(data_v, data_c, data_n,null,  tp));
        }
        public int addSTL(float[] data_v, PrimitiveType tp,int count =1)
        {
            var data_n = computeNormals(data_v);
            var glObj = new openGlobj(data_v, null, data_n, null, tp,1,count);
      
            return buffersGl.add_obj(glObj.setBuffersObj());
        }

       void add_buff_gl_id(float[] data_v, float[] data_c, float[] data_n, PrimitiveType tp,int id)
        {
            buffersGl.add_obj(new openGlobj(data_v, data_c, data_n, null,tp,id));
        }

        void remove_buff_gl_id(int id)
        {
            buffersGl.removeObj(id);
        }
        public void addFrame(Point3d_GL pos, Point3d_GL x, Point3d_GL y, Point3d_GL z)
        {
            addLineMesh(new Point3d_GL[] { pos, x }, 1.0f, 0, 0);
            addLineMesh(new Point3d_GL[] { pos, y }, 0, 1.0f, 0);
            addLineMesh(new Point3d_GL[] { pos, z }, 0, 0, 1.0f);
        }

      
        public void addGLMesh(float[] _mesh, PrimitiveType primitiveType, float x = 0, float y = 0, float z = 0, float r = 0.1f, float g = 0.1f, float b = 0.1f, float scale = 1f)
        {
            // addMesh(cube_buf, PrimitiveType.Points);
            if (x == 0 && y == 0 && z == 0)
            {
                addMesh(_mesh, primitiveType, r, g, b);
            }
            else
            {
                addMesh(translateMesh(scaleMesh(_mesh, scale), x, y, z), primitiveType, r, g, b);
            }

        }
        public float[] translateMesh(float[] _mesh, float x=0, float y=0, float z=0)
        {
            var mesh = new float[_mesh.Length];
            for (int i = 0; i < mesh.Length; i += 3)
            {
                mesh[i] = _mesh[i] + x;
                mesh[i + 1] = _mesh[i + 1] + y;
                mesh[i + 2] = _mesh[i + 2] + z;
            }
            return mesh;
        }
        public float[] scaleMesh(float[] _mesh, float k, float kx = 1.0f, float ky = 1.0f, float kz = 1.0f)
        {
            var mesh = new float[_mesh.Length];
            for (int i = 0; i < mesh.Length; i += 3)
            {
                mesh[i] = _mesh[i] * k * kx;
                mesh[i + 1] = _mesh[i + 1] * k * ky;
                mesh[i + 2] = _mesh[i + 2] * k * kz;
            }
            return mesh;
        }
        void addPointMesh(Point3d_GL[] points, float r = 0.1f, float g = 0.1f, float b = 0.1f)
        {
            var mesh = new List<float>();
            foreach (var p in points)
            {
                mesh.Add((float)p.x);
                mesh.Add((float)p.y);
                mesh.Add((float)p.z);
            }
            addMeshWithoutNorm(mesh.ToArray(), PrimitiveType.Points, r, g, b);
        }
        void addLineFanMesh(float[] startpoint, float[] points, float r = 0.1f, float g = 0.1f, float b = 0.1f)
        {
            var mesh = new float[points.Length * 2];
            var j = 0;
            for(int i=0; i<points.Length-3;i+=3)
            {
                mesh[j] = startpoint[0]; j++;
                mesh[j] = startpoint[1]; j++;
                mesh[j] = startpoint[2]; j++;
                mesh[j] = points[i]; j++;
                mesh[j] = points[i+1]; j++;
                mesh[j] = points[i+2]; j++;
            }
            addMeshWithoutNorm(mesh.ToArray(), PrimitiveType.Lines, r, g, b);
        }
        public void addLineMesh(Point3d_GL[] points, float r = 0.1f, float g = 0.1f, float b = 0.1f)
        {
            var mesh = new List<float>();
            foreach (var p in points)
            {
                mesh.Add((float)p.x);
                mesh.Add((float)p.y);
                mesh.Add((float)p.z);
            }
            addMeshWithoutNorm(mesh.ToArray(), PrimitiveType.Lines, r, g, b);
        }
        void addLineMesh(Vertex4f[] points, float r = 0.1f, float g = 0.1f, float b = 0.1f)
        {
            var mesh = new float[points.Length * 3];
            int ind = 0;
            foreach (var p in points)
            {
                mesh[ind] = p.x; ind++;
                mesh[ind] = p.y; ind++;
                mesh[ind] = p.z; ind++;
            }
            addMeshWithoutNorm(mesh, PrimitiveType.Lines, r, g, b);
        }
        public void addMeshWithoutNorm(float[] gl_vertex_buffer_data, PrimitiveType primitiveType, float r = 0.1f, float g = 0.1f, float b = 0.1f)
        {
            var normal_buffer_data = new float[gl_vertex_buffer_data.Length];
            var color_buffer_data = new float[gl_vertex_buffer_data.Length];
            for (int i = 0; i < color_buffer_data.Length; i += 3)
            {
                color_buffer_data[i] = r;
                color_buffer_data[i + 1] = g;
                color_buffer_data[i + 2] = b;

                normal_buffer_data[i] = 0.1f;
                normal_buffer_data[i + 1] = 0.1f;
                normal_buffer_data[i + 2] = 0.1f;
            }
            add_buff_gl(gl_vertex_buffer_data, color_buffer_data, normal_buffer_data, primitiveType);
        }
        void addMeshColor(float[] gl_vertex_buffer_data, float[] gl_color_buffer_data, PrimitiveType primitiveType, float r = 0.1f, float g = 0.1f, float b = 0.1f)
        {
            var normal_buffer_data = new float[gl_vertex_buffer_data.Length];
            Point3d_GL p1, p2, p3, U, V, Norm1, Norm;
            for (int i = 0; i < normal_buffer_data.Length; i += 9)
            {
                p1 = new Point3d_GL(gl_vertex_buffer_data[i], gl_vertex_buffer_data[i + 1], gl_vertex_buffer_data[i + 2]);
                p2 = new Point3d_GL(gl_vertex_buffer_data[i + 3], gl_vertex_buffer_data[i + 4], gl_vertex_buffer_data[i + 5]);
                p3 = new Point3d_GL(gl_vertex_buffer_data[i + 6], gl_vertex_buffer_data[i + 7], gl_vertex_buffer_data[i + 8]);
                U = p1 - p2;
                V = p1 - p3;
                Norm = new Point3d_GL(
                    U.y * V.z - U.z * V.y,
                    U.z * V.x - U.x * V.z,
                    U.x * V.y - U.y * V.x);
                Norm1 = Norm.normalize();
                normal_buffer_data[i] = (float)Norm1.x;
                normal_buffer_data[i + 1] = (float)Norm1.y;
                normal_buffer_data[i + 2] = (float)Norm1.z;

                normal_buffer_data[i + 3] = (float)Norm1.x;
                normal_buffer_data[i + 4] = (float)Norm1.y;
                normal_buffer_data[i + 5] = (float)Norm1.z;

                normal_buffer_data[i + 6] = (float)Norm1.x;
                normal_buffer_data[i + 7] = (float)Norm1.y;
                normal_buffer_data[i + 8] = (float)Norm1.z;
            }
            // Console.WriteLine("vert len " + gl_vertex_buffer_data.Length);
            add_buff_gl(gl_vertex_buffer_data, gl_color_buffer_data, normal_buffer_data, primitiveType);
        }
        public void addMesh(float[] gl_vertex_buffer_data, PrimitiveType primitiveType, float r = 0.1f, float g = 0.1f, float b = 0.1f)
        {
            var normal_buffer_data = computeNormals(gl_vertex_buffer_data);
            var color_buffer_data = new float[gl_vertex_buffer_data.Length];
            for (int i = 0; i < color_buffer_data.Length; i += 3)
            {
                color_buffer_data[i] = r;
                color_buffer_data[i + 1] = g;
                color_buffer_data[i + 2] = b;
            }
            add_buff_gl(gl_vertex_buffer_data, color_buffer_data, normal_buffer_data, primitiveType);
        }

        float[] computeNormals(float[] gl_vertex_buffer_data)
        {
            var normal_buffer_data = new float[gl_vertex_buffer_data.Length];
            Point3d_GL p1, p2, p3, U, V, Norm1, Norm;
            for (int i = 0; i < normal_buffer_data.Length; i += 9)
            {
                p1 = new Point3d_GL(gl_vertex_buffer_data[i], gl_vertex_buffer_data[i + 1], gl_vertex_buffer_data[i + 2]);
                p2 = new Point3d_GL(gl_vertex_buffer_data[i + 3], gl_vertex_buffer_data[i + 4], gl_vertex_buffer_data[i + 5]);
                p3 = new Point3d_GL(gl_vertex_buffer_data[i + 6], gl_vertex_buffer_data[i + 7], gl_vertex_buffer_data[i + 8]);
                U = p1 - p3;
                V = p1 - p2;
                Norm = new Point3d_GL(
                    U.y * V.z - U.z * V.y,
                    U.z * V.x - U.x * V.z,
                    U.x * V.y - U.y * V.x);
                Norm1 = Norm.normalize();
                normal_buffer_data[i] = (float)Norm1.x;
                normal_buffer_data[i + 1] = (float)Norm1.y;
                normal_buffer_data[i + 2] = (float)Norm1.z;

                normal_buffer_data[i + 3] = (float)Norm1.x;
                normal_buffer_data[i + 4] = (float)Norm1.y;
                normal_buffer_data[i + 5] = (float)Norm1.z;

                normal_buffer_data[i + 6] = (float)Norm1.x;
                normal_buffer_data[i + 7] = (float)Norm1.y;
                normal_buffer_data[i + 8] = (float)Norm1.z;
            }
            return normal_buffer_data;
        }


        #endregion


        #region shader
        string[] assembCode(string[] paths)
        {
            var text = "";
            foreach (var path in paths)
                text += File.ReadAllText(path);
            return new string[] { text };
        }
        void debugShaderComp(uint ShaderName)
        {
            int compiled;

            Gl.GetShader(ShaderName, ShaderParameterName.CompileStatus, out compiled);
            if (compiled != 0)
            {
                Console.WriteLine("SHADER COMPILE");
                return;
            }


            // Throw exception on compilation errors
            const int logMaxLength = 1024;

            StringBuilder infolog = new StringBuilder(logMaxLength);
            int infologLength;

            Gl.GetShaderInfoLog(ShaderName, logMaxLength, out infologLength, infolog);

            throw new InvalidOperationException($"unable to compile shader: {infolog}");
        }
        private uint compileShader(string[] shSource, ShaderType shaderType)
        {
            uint ShaderID = Gl.CreateShader(shaderType);
            Gl.ShaderSource(ShaderID, shSource);
            Gl.CompileShader(ShaderID);
            debugShaderComp(ShaderID);
            return ShaderID;
        }
        private uint createShader(string[] VertexSourceGL, string[] GeometryShaderGL, string[] FragmentSourceGL)
        {
            bool geom = false;
            uint GeometryShaderID = 0;
            if (GeometryShaderGL!=null)
            {
                geom = true;
            }
            var VertexShaderID = compileShader(VertexSourceGL, ShaderType.VertexShader);
            var FragmentShaderID = compileShader(FragmentSourceGL, ShaderType.FragmentShader);
            if (geom)
            {
                GeometryShaderID = compileShader(GeometryShaderGL, ShaderType.GeometryShader);
            }

            uint ProgrammID = Gl.CreateProgram();
            Gl.AttachShader(ProgrammID, VertexShaderID);
            Gl.AttachShader(ProgrammID, FragmentShaderID);
            if(geom)
            {
                Gl.AttachShader(ProgrammID, GeometryShaderID);
            }
            Gl.LinkProgram(ProgrammID);

            int linked;

            Gl.GetProgram(ProgrammID, ProgramProperty.LinkStatus, out linked);

            if (linked == 0)
            {
                const int logMaxLength = 1024;

                StringBuilder infolog = new StringBuilder(logMaxLength);
                int infologLength;

                Gl.GetProgramInfoLog(ProgrammID, 1024, out infologLength, infolog);

                throw new InvalidOperationException($"unable to link program: {infolog}");
            }

            Gl.DeleteShader(VertexShaderID);
            Gl.DeleteShader(FragmentShaderID);
            if(geom)
            {
                Gl.DeleteShader(GeometryShaderID);
            }
            return ProgrammID;
        }
        private uint createShaderCompute(string[] ComputeSourceGL)
        {

            var ComputeShaderID = compileShader(ComputeSourceGL, ShaderType.ComputeShader);

            uint ProgrammID = Gl.CreateProgram();
            Gl.AttachShader(ProgrammID, ComputeShaderID);
            Gl.LinkProgram(ProgrammID);

            int linked;

            Gl.GetProgram(ProgrammID, ProgramProperty.LinkStatus, out linked);

            if (linked == 0)
            {
                const int logMaxLength = 1024;

                StringBuilder infolog = new StringBuilder(logMaxLength);
                int infologLength;

                Gl.GetProgramInfoLog(ProgrammID, 1024, out infologLength, infolog);

                throw new InvalidOperationException($"unable to link program: {infolog}");
            }

            Gl.DeleteShader(ComputeShaderID);
            return ProgrammID;
        }

        #endregion
    }
}
