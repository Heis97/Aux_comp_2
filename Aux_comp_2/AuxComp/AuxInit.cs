using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Objects;

namespace AuxComp
{
    static public class AuxProc
    {
        static public ObjectAuxGL[] loadObjs(int len)
        {
            var objs = new ObjectAuxGL[len];
            for (int i = 0; i < len; i++)
            {
                objs[i] = new ObjectAuxGL(i,1,2,3);
            }
            return objs;
        }

        static public string data_to_str(float[][] data)
        {
            StringBuilder txt = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < data[i].Length; j++)
                {
                    if(j == 4)
                    {
                        txt.Append(" | ");
                    }
                    txt.Append(data[i][j]+", ");
                }
                txt.Append(" \n");
            }
            return txt.ToString();    
        }

    }


}
