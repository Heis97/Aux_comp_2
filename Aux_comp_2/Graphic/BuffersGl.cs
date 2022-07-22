using OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geometry;

namespace Graphic
{

    public class BuffersGl
    {
        public int countObj = 0;
        public List<openGlobj> objs;
        public List<openGlobj> objs_dynamic;
        public List<openGlobj> objs_static;
        public BuffersGl()
        {
            objs = new List<openGlobj>();
            objs_dynamic = new List<openGlobj>();
            objs_static = new List<openGlobj>();
        }
        public int add_obj(openGlobj opgl_obj)
        {
            if (opgl_obj.animType == openGlobj.AnimType.Dynamic)
            {
                objs_dynamic.Add(opgl_obj);
                countObj++;
                return countObj - 1;
            }
            else
            {
                objs.Add(opgl_obj);
                return -1;
            }
               
        }

        public void add_obj_id(float[] data_v, int id, bool visible, PrimitiveType primitiveType)
        {
            int ind = 0;
            if (data_v == null)
            {
                return;
            }
            if (objs.Count != 0)
            {
                foreach (var ob in objs_dynamic)
                {
                    if (ob.id == id)
                    {
                        var lam_obj = ob;
                        if (visible)
                        {
                            lam_obj.visible = true;
                            var data_n_ = new float[data_v.Length];
                            var data_c_ = new float[data_v.Length];
                            for (int i = 0; i < data_v.Length; i++)
                            {
                                data_c_[i] = 1f;
                                data_n_[i] = 1f;
                            }
                            lam_obj.vertex_buffer_data = data_v;
                            lam_obj.color_buffer_data = data_c_;
                            lam_obj.normal_buffer_data = data_n_;
                            objs_dynamic[ind] = lam_obj;
                            return;
                        }
                        else
                        {
                            lam_obj.visible = false;
                            objs_dynamic[ind] = lam_obj;
                            return;
                        }

                    }
                    ind++;
                }

            }
            var data_n = new float[data_v.Length];
            var data_c = new float[data_v.Length];
            for (int i = 0; i < data_v.Length; i++)
            {
                data_c[i] = 1f;
                data_n[i] = 1f;
            }
            //Console.WriteLine("new ver " + id+" all "+ind);
            add_obj(new openGlobj(data_v, data_c, data_n,null, primitiveType, id));
        }
        public void sortObj()
        {
            objs_static = new List<openGlobj>();
            foreach (PrimitiveType val_tp in Enum.GetValues(typeof(PrimitiveType)))
            {
                var vertex_buffer_data = new List<float>();
                var color_buffer_data = new List<float>();
                var normal_buffer_data = new List<float>();
                var texture_buffer_data = new List<float>();

                for (int i = 0; i < objs.Count; i++)
                {
                    if (objs[i].tp == val_tp && objs[i].animType == openGlobj.AnimType.Static)
                    {
                        vertex_buffer_data.AddRange(objs[i].vertex_buffer_data);
                        color_buffer_data.AddRange(objs[i].color_buffer_data);
                        normal_buffer_data.AddRange(objs[i].normal_buffer_data);
                        texture_buffer_data.AddRange(objs[i].texture_buffer_data);

                    }
                }

                if (vertex_buffer_data.Count > 2)
                {

                    objs_static.Add(new openGlobj(vertex_buffer_data.ToArray(), color_buffer_data.ToArray(), normal_buffer_data.ToArray(), texture_buffer_data.ToArray(), val_tp));
                }

            }
        }

        public void removeObj(int id)
        {
            objs_dynamic[id] = new openGlobj();
        }

    }


}
