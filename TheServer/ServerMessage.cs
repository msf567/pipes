using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finger
{
    [Serializable]
    public class SVRMSG
    {
        public SVRMSG(string _msg)
        {
            ID = new Random().Next();
            text = _msg;
        }

        public int ID;
        public string text;


    }
}
