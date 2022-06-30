using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geometry;
using OpenGL;

namespace Objects
{
    public struct ObjectMassGL
    {
        float mass;
        public float size;
        public float true_size;
        public Vertex3f pos;
        Vertex3f vel;
        public Vertex3f posrot;
        Vertex3f velrot;
        public int mesh_number;
        static int datalen = 32;
        public ObjectMassGL(int _mesh_number,float _mass, float _size, float _true_size, Vertex3f _pos, Vertex3f _vel, Vertex3f _posrot , Vertex3f _velrot)
        {
            mesh_number = _mesh_number;
            mass = _mass;
            size = _size;
            true_size = _true_size;
            pos = _pos;
            vel = _vel;
            posrot = _posrot;
            velrot = _velrot;
        }

        public float[] getData()
        {
            return new float[] { 
                pos.x, pos.y, pos.z, mass,
                vel.x, vel.y, vel.z, size,
                posrot.x, posrot.y, posrot.z, true_size, //поворот
                velrot.x, velrot.y, velrot.z, 0, //поворот скорость
                0, 0, 0, 0,//матрица
                0, 0, 0, 0,//4
                0, 0, 0, 0,//х
                0, 0, 0, 0,//4
                 };
        }
        public ObjectMassGL Clone()
        {
            return new ObjectMassGL(mesh_number, mass, size, true_size, pos, vel, posrot, velrot);
        }
        public ObjectMassGL setData(float[] data)
        {
            pos.x = data[0];
            pos.y = data[1];
            pos.z = data[2];
            mass = data[3];
            vel.x = data[4];
            vel.y = data[5];
            vel.z = data[6];
            size = data[7];

            posrot.x = data[8];
            posrot.y = data[9];
            posrot.z = data[10];
            true_size = data[11];
            velrot.x = data[12];
            velrot.y = data[13];
            velrot.z = data[14];
           
            return this;
        }
        static public int getLength()
        {
            return datalen;
        }

        public override string ToString()
        {
            return pos.ToString();
        }
    }

    public struct ObjectAuxGL
    {
        public float h;
        public float l;
        public float t;
        public float thetta;

        public float porosity;
        public float pore_size_A_A;
        public float pore_size_B_B;

        static int datalen = 8;
        public ObjectAuxGL(float _h, float _l, float _t, float _thetta)
        {
            h = _h; l = _l; t = _t; thetta = _thetta;
            porosity = 1; pore_size_A_A= 1; pore_size_B_B = 1;
        }

        public float[] getData()
        {
            return new float[] {
                h, l, t, thetta,//inp
                0, 0, 0, 0,//out  porosity ; pore_size_A_A; pore_size_B_B ;
                 };
        }
        public ObjectAuxGL Clone()
        {
            return new ObjectAuxGL(h,l,t,thetta);
        }
        public ObjectAuxGL setData(float[] data)
        {
            h = data[0];
            l = data[1];
            t = data[2];
            thetta = data[3];

            porosity = data[4];
            pore_size_A_A = data[5];
            pore_size_B_B = data[6];


            return this;
        }
        static public int getLength()
        {
            return datalen;
        }

        public override string ToString()
        {
            return "h = " + h + "\nl = " + l + "\nt = " + t + "\nthetta = " + thetta + "\nporosity = " + porosity + "\npore_size_A_A = " + pore_size_A_A + "\npore_size_B_B = " + pore_size_B_B + "\n";
        }

        public static float[] getDataFromObjs(ObjectAuxGL[] objects)
        {
            var len = getLength();
            var data = new float[objects.Length * len];
            for (int i = 0; i < objects.Length; i++)
            {
                var obData = objects[i].getData();
                for (int j = 0; j < len; j++)
                {
                    data[len * i + j] = obData[j];
                }
            }
            return data;
        }
    }

}
