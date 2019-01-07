using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class FactorySingletonBL
    {
        static IBL bl;

        public static IBL GetBL()
        {
            if (bl == null)
                bl = new MyBL();
            return bl;
        }


    }
}