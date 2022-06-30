using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Objects;

namespace AuxComp
{
    static public class AuxInit
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


    }


}
