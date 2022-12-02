using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geometry;
using Emgu.CV;
using OpenGL;
using Graphic;

namespace Geometry
{
    public class Generate_stl
    {
        public static double toRad(double ang)
        {
            return Math.PI * ang / 180;
        }
        public static float[] gen_aucs(double h, double l, double t, double theta)
        {
            var hc45 = h * Math.Cos(Math.PI / 4);
            var ps = new Point3d_GL[51];
            var lSt = l * Math.Sin(toRad(theta));
            var lCt = l * Math.Cos(toRad(theta));
            var h_all = t + lSt;


            ps[6] = new Point3d_GL(0, 0 - h_all, hc45);
            ps[7] = new Point3d_GL(0, 0 - h_all, hc45 + t);
            ps[15] = new Point3d_GL(t, 0 - h_all, hc45 + t);
            ps[18] = new Point3d_GL(hc45 + t, 0 - h_all, t);
            ps[17] = new Point3d_GL(hc45 + t, 0 - h_all, 0);
            ps[26] = new Point3d_GL(hc45, 0 - h_all, 0);
            //--------------------------------------------------------
            ps[4] = new Point3d_GL(0, t / 2 - h_all, hc45);
            ps[8] = new Point3d_GL(0, t / 2 - h_all, hc45 + t);
            ps[0] = new Point3d_GL(t, t / 2 - h_all, hc45 + t);
            ps[1] = new Point3d_GL(hc45 + t, t / 2 - h_all, t);
            ps[34] = new Point3d_GL(hc45 + t, t / 2 - h_all, 0);
            ps[30] = new Point3d_GL(hc45, t / 2 - h_all, 0);

            ps[2] = new Point3d_GL(t, t / 2 - h_all, hc45);
            ps[3] = new Point3d_GL(hc45, t / 2 - h_all, t);
            //---------------------------------------------------


            ps[9] = new Point3d_GL(0, t / 2 + lSt - h_all, hc45 - lCt);
            ps[10] = new Point3d_GL(0, t / 2 + lSt - h_all, hc45 + t - lCt);
            ps[27] = new Point3d_GL(t, t / 2 + lSt - h_all, hc45 + t - lCt);
            ps[32] = new Point3d_GL(hc45 + t - lCt, t / 2 + lSt - h_all, t);
            ps[49] = new Point3d_GL(hc45 + t - lCt, t / 2 + lSt - h_all, 0);
            ps[45] = new Point3d_GL(hc45 - lCt, t / 2 + lSt - h_all, 0);

            ps[28] = new Point3d_GL(t, t / 2 + lSt - h_all, hc45 - lCt);
            ps[29] = new Point3d_GL(hc45 - lCt, t / 2 + lSt - h_all, t);

            var delt45 = new Point3d_GL(hc45 / 2, 0, hc45 / 2);
            ps[37] = ps[10].Clone() + delt45;
            ps[39] = ps[28].Clone() + delt45;
            ps[42] = ps[29].Clone() + delt45;
            ps[41] = ps[49].Clone() + delt45;
            //-------------------------------------------
            var deltz = new Point3d_GL(0, t / 2, 0);
            ps[11] = ps[9].Clone() + deltz;
            ps[12] = ps[10].Clone() + deltz;
            ps[50] = ps[49].Clone() + deltz;
            ps[44] = ps[45].Clone() + deltz;
            ps[35] = ps[28].Clone() + deltz;

            ps[43] = ps[29].Clone() + deltz;
            ps[36] = ps[37].Clone() + deltz;
            ps[38] = ps[39].Clone() + deltz;
            ps[46] = ps[42].Clone() + deltz;
            ps[47] = ps[41].Clone() + deltz;
            //-------------------------------------

            ps[5] = ps[30].Clone();
            ps[13] = ps[15].Clone();
            ps[14] = ps[18].Clone();
            ps[16] = ps[34].Clone();

            ps[19] = ps[44].Clone();
            ps[20] = ps[50].Clone();
            ps[21] = ps[45].Clone();
            ps[22] = ps[49].Clone();

            ps[23] = ps[30].Clone();
            ps[24] = ps[26].Clone();
            ps[25] = ps[30].Clone();

            ps[31] = ps[21].Clone();
            ps[33] = ps[49].Clone();
            ps[40] = ps[49].Clone();
            ps[48] = ps[50].Clone();
            int ind = 0;
            foreach (var p in ps)
            {
                Console.WriteLine(ind + " " + p.ToString());
                ind++;
            }

            var ps_of = new Point3d_GL[52];
            for (int i = 1; i < ps.Length + 1; i++)
            {
                ps_of[i] = ps[i - 1].Clone();
            }

            ps = ps_of;
            var trls = new Polygon3d_GL[]
            {
                new Polygon3d_GL(ps[1],ps[2],ps[3]),
                new Polygon3d_GL(ps[3],ps[2],ps[4]),
                new Polygon3d_GL(ps[3],ps[4],ps[5]),

                new Polygon3d_GL(ps[5],ps[4],ps[6]),
                new Polygon3d_GL(ps[7],ps[8],ps[5]),
                new Polygon3d_GL(ps[5],ps[8],ps[9]),

                new Polygon3d_GL(ps[5],ps[9],ps[10]),
                new Polygon3d_GL(ps[10],ps[9],ps[11]),
                new Polygon3d_GL(ps[10],ps[11],ps[12]),

                new Polygon3d_GL(ps[12],ps[11],ps[13]),
                new Polygon3d_GL(ps[1],ps[9],ps[14]),
                new Polygon3d_GL(ps[14],ps[9],ps[8]),

                new Polygon3d_GL(ps[2],ps[1],ps[15]),
                new Polygon3d_GL(ps[15],ps[1],ps[16]),
                new Polygon3d_GL(ps[17],ps[2],ps[18]),

                new Polygon3d_GL(ps[18],ps[2],ps[19]),
                new Polygon3d_GL(ps[20],ps[21],ps[22]),
                new Polygon3d_GL(ps[22],ps[21],ps[23]),

                new Polygon3d_GL(ps[22],ps[23],ps[24]),
                new Polygon3d_GL(ps[24],ps[23],ps[17]),
                new Polygon3d_GL(ps[24],ps[17],ps[25]),

                new Polygon3d_GL(ps[25],ps[17],ps[18]),
                new Polygon3d_GL(ps[5],ps[26],ps[7]),
                new Polygon3d_GL(ps[7],ps[26],ps[27]),

                new Polygon3d_GL(ps[8],ps[7],ps[14]),
                new Polygon3d_GL(ps[14],ps[7],ps[25]),
                new Polygon3d_GL(ps[14],ps[25],ps[19]),

                new Polygon3d_GL(ps[19],ps[25],ps[18]),
                new Polygon3d_GL(ps[1],ps[28],ps[9]),
                new Polygon3d_GL(ps[9],ps[28],ps[11]),

                new Polygon3d_GL(ps[29],ps[3],ps[10]),
                new Polygon3d_GL(ps[10],ps[3],ps[5]),
                new Polygon3d_GL(ps[3],ps[29],ps[1]),

                new Polygon3d_GL(ps[1],ps[29],ps[28]),
                new Polygon3d_GL(ps[4],ps[30],ps[31]),
                new Polygon3d_GL(ps[31],ps[30],ps[32]),

                new Polygon3d_GL(ps[33],ps[2],ps[34]),
                new Polygon3d_GL(ps[34],ps[2],ps[35]),
                new Polygon3d_GL(ps[4],ps[2],ps[30]),

                new Polygon3d_GL(ps[30],ps[2],ps[33]),
                new Polygon3d_GL(ps[12],ps[36],ps[10]),
                new Polygon3d_GL(ps[10],ps[36],ps[29]),

                new Polygon3d_GL(ps[37],ps[13],ps[38]),
                new Polygon3d_GL(ps[38],ps[13],ps[11]),
                //-------------------------
               /* new Polygon3d_GL(ps[39],ps[37],ps[40]),
               new Polygon3d_GL(ps[48],ps[47],ps[42]),
                new Polygon3d_GL(ps[42],ps[47],ps[43]),
                new Polygon3d_GL(ps[40],ps[37],ps[38]),*/

                //-------------------------
                new Polygon3d_GL(ps[36],ps[39],ps[29]),
                new Polygon3d_GL(ps[29],ps[39],ps[40]),

                new Polygon3d_GL(ps[12],ps[13],ps[36]),
                new Polygon3d_GL(ps[36],ps[13],ps[37]),
                new Polygon3d_GL(ps[36],ps[37],ps[39]),

                new Polygon3d_GL(ps[29],ps[40],ps[28]),
                new Polygon3d_GL(ps[28],ps[40],ps[38]),
                new Polygon3d_GL(ps[28],ps[38],ps[11]),

                new Polygon3d_GL(ps[41],ps[42],ps[33]),
                new Polygon3d_GL(ps[33],ps[42],ps[43]),
                new Polygon3d_GL(ps[33],ps[43],ps[30]),

                new Polygon3d_GL(ps[44],ps[45],ps[30]),
                new Polygon3d_GL(ps[30],ps[45],ps[46]),
                new Polygon3d_GL(ps[47],ps[44],ps[43]),

                new Polygon3d_GL(ps[43],ps[44],ps[30]),
               

                new Polygon3d_GL(ps[49],ps[48],ps[50]),
                new Polygon3d_GL(ps[50],ps[48],ps[42]),
                new Polygon3d_GL(ps[45],ps[44],ps[51]),

                new Polygon3d_GL(ps[51],ps[44],ps[47]),
                new Polygon3d_GL(ps[51],ps[47],ps[48])

            };
            var p_xy_sim = new Point3d_GL(ps[47], ps[39]);
            var trls_sim_xz = Polygon3d_GL.multMatr(trls,
                new Matrix<double>(new double[,]
                {
                    {-1,0,0,2*p_xy_sim.x  },
                    {0,1,0,0 },
                    {0,0,-1,2*p_xy_sim.z },
                    {0,0,0,1 },
                }));
            var trls_l = trls.ToList();
            trls_l.AddRange(trls_sim_xz);
            var trls_arr = trls_l.ToArray();

            var trls_sim_y = Polygon3d_GL.multMatr(trls_arr,
                new Matrix<double>(new double[,]
                {
                    {1,0,0,0 },
                    {0,-1,0,0 },
                    {0,0,1,0 },
                    {0,0,0,1 },
                }));
            trls_sim_y = Polygon3d_GL.invNorm(trls_sim_y);
            var trls_l_2 = trls_sim_y.ToList();
            trls_l_2.AddRange( trls_arr);
            var trls_arr_2 = trls_l_2.ToArray();

            var trls_sim_x = Polygon3d_GL.multMatr(trls_arr_2,
                new Matrix<double>(new double[,]
                {
                    {-1,0,0,0 },
                    {0,1,0,0 },
                    {0,0,1,0 },
                    {0,0,0,1 },
                }));
            trls_sim_x = Polygon3d_GL.invNorm(trls_sim_x);
            var trls_l_3 = trls_sim_x.ToList();
            trls_l_3.AddRange(trls_arr_2);
            var trls_arr_3 = trls_l_3.ToArray();

            var trls_sim_z = Polygon3d_GL.multMatr(trls_arr_3,
                new Matrix<double>(new double[,]
                {
                    {1,0,0,0 },
                    {0,1,0,0 },
                    {0,0,-1,0 },
                    {0,0,0,1 },
                }));
            trls_sim_z = Polygon3d_GL.invNorm(trls_sim_z);
            var trls_l_4 = trls_sim_z.ToList();
            trls_l_4.AddRange(trls_arr_3);
            var trls_arr_4 = trls_l_4.ToArray();
            var k = 1;
            var trls_xyz = Polygon3d_GL.multMatr(trls_arr_4,
                new Matrix<double>(new double[,]
                {
                    {k*1,0,0,0 },
                    {0,0,-k*1,0 },
                    {0,k*1,0,0 },
                    {0,0,0,1 },
                }));
            var trls_l_5 = trls_xyz.ToList();
            trls_l_4.AddRange(trls_arr_4);
            var trls_arr_5 = trls_l_5.ToArray();


            var trls_arr_6 = Polygon3d_GL.del_same_pol(trls_arr_5);
            return Polygon3d_GL.toMesh(trls_arr_6)[0];
        }

        public static float[] gen_aucs_v2(double h, double l, double t, double theta)
        {
            var hc45 = h * Math.Cos(Math.PI / 4);
            var ps = new Point3d_GL[51];
            var lSt = l * Math.Sin(toRad(theta));
            var lCt = l * Math.Cos(toRad(theta));
            var h_all = t + lSt;
            #region ps

            ps[6] = new Point3d_GL(0, 0 - h_all, hc45);
            ps[7] = new Point3d_GL(0, 0 - h_all, hc45 + t);
            ps[15] = new Point3d_GL(t, 0 - h_all, hc45 + t);
            ps[18] = new Point3d_GL(hc45 + t, 0 - h_all, t);
            ps[17] = new Point3d_GL(hc45 + t, 0 - h_all, 0);
            ps[26] = new Point3d_GL(hc45, 0 - h_all, 0);
            //--------------------------------------------------------
            ps[4] = new Point3d_GL(0, t / 2 - h_all, hc45);
            ps[8] = new Point3d_GL(0, t / 2 - h_all, hc45 + t);
            ps[0] = new Point3d_GL(t, t / 2 - h_all, hc45 + t);
            ps[1] = new Point3d_GL(hc45 + t, t / 2 - h_all, t);
            ps[34] = new Point3d_GL(hc45 + t, t / 2 - h_all, 0);
            ps[30] = new Point3d_GL(hc45, t / 2 - h_all, 0);

            ps[2] = new Point3d_GL(t, t / 2 - h_all, hc45);
            ps[3] = new Point3d_GL(hc45, t / 2 - h_all, t);
            //---------------------------------------------------


            ps[9] = new Point3d_GL(0, t / 2 + lSt - h_all, hc45 - lCt);
            ps[10] = new Point3d_GL(0, t / 2 + lSt - h_all, hc45 + t - lCt);
            ps[27] = new Point3d_GL(t, t / 2 + lSt - h_all, hc45 + t - lCt);
            ps[32] = new Point3d_GL(hc45 + t - lCt, t / 2 + lSt - h_all, t);
            ps[49] = new Point3d_GL(hc45 + t - lCt, t / 2 + lSt - h_all, 0);
            ps[45] = new Point3d_GL(hc45 - lCt, t / 2 + lSt - h_all, 0);

            ps[28] = new Point3d_GL(t, t / 2 + lSt - h_all, hc45 - lCt);
            ps[29] = new Point3d_GL(hc45 - lCt, t / 2 + lSt - h_all, t);

            var delt45 = new Point3d_GL(hc45 / 2, 0, hc45 / 2);
            ps[37] = ps[10].Clone() + delt45;
            ps[39] = ps[28].Clone() + delt45;
            ps[42] = ps[29].Clone() + delt45;
            ps[41] = ps[49].Clone() + delt45;
            //-------------------------------------------
            var deltz = new Point3d_GL(0, t / 2, 0);
            ps[11] = ps[9].Clone() + deltz;
            ps[12] = ps[10].Clone() + deltz;
            ps[50] = ps[49].Clone() + deltz;
            ps[44] = ps[45].Clone() + deltz;
            ps[35] = ps[28].Clone() + deltz;

            ps[43] = ps[29].Clone() + deltz;
            ps[36] = ps[37].Clone() + deltz;
            ps[38] = ps[39].Clone() + deltz;
            ps[46] = ps[42].Clone() + deltz;
            ps[47] = ps[41].Clone() + deltz;
            //-------------------------------------

            ps[5] = ps[30].Clone();
            ps[13] = ps[15].Clone();
            ps[14] = ps[18].Clone();
            ps[16] = ps[34].Clone();

            ps[19] = ps[44].Clone();
            ps[20] = ps[50].Clone();
            ps[21] = ps[45].Clone();
            ps[22] = ps[49].Clone();

            ps[23] = ps[30].Clone();
            ps[24] = ps[26].Clone();
            ps[25] = ps[30].Clone();

            ps[31] = ps[21].Clone();
            ps[33] = ps[49].Clone();
            ps[40] = ps[49].Clone();
            ps[48] = ps[50].Clone();
            #endregion
            int ind = 0;
            foreach (var p in ps)
            {
                Console.WriteLine(ind + " " + p.ToString());
                ind++;
            }

            var ps_of = new Point3d_GL[52];
            for (int i = 1; i < ps.Length + 1; i++)
            {
                ps_of[i] = ps[i - 1].Clone();
            }

            ps = ps_of;
            var trls = new Polygon3d_GL[]
            {
                new Polygon3d_GL(ps[1],ps[2],ps[3]),
                new Polygon3d_GL(ps[3],ps[2],ps[4]),
                new Polygon3d_GL(ps[3],ps[4],ps[5]),

                new Polygon3d_GL(ps[5],ps[4],ps[6]),
                new Polygon3d_GL(ps[7],ps[8],ps[5]),
                new Polygon3d_GL(ps[5],ps[8],ps[9]),

                new Polygon3d_GL(ps[5],ps[9],ps[10]),
                new Polygon3d_GL(ps[10],ps[9],ps[11]),
                new Polygon3d_GL(ps[10],ps[11],ps[12]),

                new Polygon3d_GL(ps[12],ps[11],ps[13]),
                new Polygon3d_GL(ps[1],ps[9],ps[14]),
                new Polygon3d_GL(ps[14],ps[9],ps[8]),

                new Polygon3d_GL(ps[2],ps[1],ps[15]),
                new Polygon3d_GL(ps[15],ps[1],ps[16]),
                new Polygon3d_GL(ps[17],ps[2],ps[18]),

                new Polygon3d_GL(ps[18],ps[2],ps[19]),
                new Polygon3d_GL(ps[20],ps[21],ps[22]),
                new Polygon3d_GL(ps[22],ps[21],ps[23]),

                new Polygon3d_GL(ps[22],ps[23],ps[24]),
                new Polygon3d_GL(ps[24],ps[23],ps[17]),
                new Polygon3d_GL(ps[24],ps[17],ps[25]),

                new Polygon3d_GL(ps[25],ps[17],ps[18]),
                new Polygon3d_GL(ps[5],ps[26],ps[7]),
                new Polygon3d_GL(ps[7],ps[26],ps[27]),

                new Polygon3d_GL(ps[8],ps[7],ps[14]),
                new Polygon3d_GL(ps[14],ps[7],ps[25]),
                new Polygon3d_GL(ps[14],ps[25],ps[19]),

                new Polygon3d_GL(ps[19],ps[25],ps[18]),
                new Polygon3d_GL(ps[1],ps[28],ps[9]),
                new Polygon3d_GL(ps[9],ps[28],ps[11]),

                new Polygon3d_GL(ps[29],ps[3],ps[10]),
                new Polygon3d_GL(ps[10],ps[3],ps[5]),
                new Polygon3d_GL(ps[3],ps[29],ps[1]),

                new Polygon3d_GL(ps[1],ps[29],ps[28]),
                new Polygon3d_GL(ps[4],ps[30],ps[31]),
                new Polygon3d_GL(ps[31],ps[30],ps[32]),

                new Polygon3d_GL(ps[33],ps[2],ps[34]),
                new Polygon3d_GL(ps[34],ps[2],ps[35]),
                new Polygon3d_GL(ps[4],ps[2],ps[30]),

                new Polygon3d_GL(ps[30],ps[2],ps[33]),
                new Polygon3d_GL(ps[12],ps[36],ps[10]),
                new Polygon3d_GL(ps[10],ps[36],ps[29]),

                new Polygon3d_GL(ps[37],ps[13],ps[38]),
                new Polygon3d_GL(ps[38],ps[13],ps[11]),
                //-------------------------
               /* new Polygon3d_GL(ps[39],ps[37],ps[40]),
               new Polygon3d_GL(ps[48],ps[47],ps[42]),
                new Polygon3d_GL(ps[42],ps[47],ps[43]),
                new Polygon3d_GL(ps[40],ps[37],ps[38]),*/

                //-------------------------
                new Polygon3d_GL(ps[36],ps[39],ps[29]),
                new Polygon3d_GL(ps[29],ps[39],ps[40]),

                new Polygon3d_GL(ps[12],ps[13],ps[36]),
                new Polygon3d_GL(ps[36],ps[13],ps[37]),
                new Polygon3d_GL(ps[36],ps[37],ps[39]),

                new Polygon3d_GL(ps[29],ps[40],ps[28]),
                new Polygon3d_GL(ps[28],ps[40],ps[38]),
                new Polygon3d_GL(ps[28],ps[38],ps[11]),

                new Polygon3d_GL(ps[41],ps[42],ps[33]),
                new Polygon3d_GL(ps[33],ps[42],ps[43]),
                new Polygon3d_GL(ps[33],ps[43],ps[30]),

                new Polygon3d_GL(ps[44],ps[45],ps[30]),
                new Polygon3d_GL(ps[30],ps[45],ps[46]),
                new Polygon3d_GL(ps[47],ps[44],ps[43]),

                new Polygon3d_GL(ps[43],ps[44],ps[30]),


                new Polygon3d_GL(ps[49],ps[48],ps[50]),
                new Polygon3d_GL(ps[50],ps[48],ps[42]),
                new Polygon3d_GL(ps[45],ps[44],ps[51]),

                new Polygon3d_GL(ps[51],ps[44],ps[47]),
                new Polygon3d_GL(ps[51],ps[47],ps[48])

            };
            var p_xy_sim = new Point3d_GL(ps[47], ps[39]);
            var trls_sim_xz = Polygon3d_GL.multMatr(trls,
                new Matrix<double>(new double[,]
                {
                    {-1,0,0,p_xy_sim.x  },
                    {0,1,0,0 },
                    {0,0,-1,p_xy_sim.z },
                    {0,0,0,1 },
                }));

            var trls_transl_xz = Polygon3d_GL.multMatr(trls,
                new Matrix<double>(new double[,]
                {
                    {1,0,0,-p_xy_sim.x  },
                    {0,1,0,0 },
                    {0,0,1,-p_xy_sim.z },
                    {0,0,0,1 },
                }));
            var trls_l = trls_sim_xz.ToList();

            trls_l.AddRange(trls_sim_xz);
            trls_l.AddRange(trls_transl_xz);

            var trls_transl_x = Polygon3d_GL.multMatr(trls_l.ToArray(),
                new Matrix<double>(new double[,]
                {
                    {1,0,0,2*p_xy_sim.x  },
                    {0,1,0,0 },
                    {0,0,-1,0  },
                    {0,0,0,1 },
                }));


            
            trls_l.AddRange(trls_transl_x);
            var trls_arr = trls_l.ToArray();


            

            /*var trls_sim_xz = Polygon3d_GL.multMatr(trls,
                new Matrix<double>(new double[,]
                {
                    {-1,0,0,2*p_xy_sim.x  },
                    {0,1,0,0 },
                    {0,0,-1,2*p_xy_sim.z },
                    {0,0,0,1 },
                }));
            var trls_l = trls.ToList();
            trls_l.AddRange(trls_sim_xz);
            var trls_arr = trls_l.ToArray();

            var trls_sim_y = Polygon3d_GL.multMatr(trls_arr,
                new Matrix<double>(new double[,]
                {
                    {1,0,0,0 },
                    {0,-1,0,0 },
                    {0,0,1,0 },
                    {0,0,0,1 },
                }));
            trls_sim_y = Polygon3d_GL.invNorm(trls_sim_y);
            var trls_l_2 = trls_sim_y.ToList();
            trls_l_2.AddRange(trls_arr);
            var trls_arr_2 = trls_l_2.ToArray();

            var trls_sim_x = Polygon3d_GL.multMatr(trls_arr_2,
                new Matrix<double>(new double[,]
                {
                    {-1,0,0,0 },
                    {0,1,0,0 },
                    {0,0,1,0 },
                    {0,0,0,1 },
                }));
            trls_sim_x = Polygon3d_GL.invNorm(trls_sim_x);
            var trls_l_3 = trls_sim_x.ToList();
            trls_l_3.AddRange(trls_arr_2);
            var trls_arr_3 = trls_l_3.ToArray();

            var trls_sim_z = Polygon3d_GL.multMatr(trls_arr_3,
                new Matrix<double>(new double[,]
                {
                    {1,0,0,0 },
                    {0,1,0,0 },
                    {0,0,-1,0 },
                    {0,0,0,1 },
                }));
            trls_sim_z = Polygon3d_GL.invNorm(trls_sim_z);
            var trls_l_4 = trls_sim_z.ToList();
            trls_l_4.AddRange(trls_arr_3);
            var trls_arr_4 = trls_l_4.ToArray();
            var k = 1;
            var trls_xyz = Polygon3d_GL.multMatr(trls_arr_4,
                new Matrix<double>(new double[,]
                {
                    {k*1,0,0,0 },
                    {0,0,-k*1,0 },
                    {0,k*1,0,0 },
                    {0,0,0,1 },
                }));
            var trls_l_5 = trls_xyz.ToList();
            trls_l_4.AddRange(trls_arr_4);
            var trls_arr_5 = trls_l_5.ToArray();*/


            return Polygon3d_GL.toMesh(trls_arr)[0];
            //return Polygon3d_GL.toMesh(trls_arr_5)[0];
        }
    }
}
