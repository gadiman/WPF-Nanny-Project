using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    internal static class BE_Extensions
    {
        public static T clone<T>(this T p) where T : new()
        {
            T tmp = new T();
           
            foreach (var item in p.GetType().GetProperties())
            {

                item.SetValue(tmp, item.GetValue(p, null), null);
            }
            return tmp;
        }



        public static BE.Mother Clone_mom( this BE.Mother mom)
        {
            BE.Mother tmp = new BE.Mother();
            foreach (var item in mom.GetType().GetProperties())
            {

                item.SetValue(tmp, item.GetValue(mom, null), null);
            }

            tmp.Need_Nanny_Today = mom.Need_Nanny_Today;
            tmp.Needed_Hours = mom.Needed_Hours;

            return tmp;
        }

        public static BE.Nanny Clone_nanny( this BE.Nanny nanny)
        {
            BE.Nanny tmp = new BE.Nanny();
            foreach (var item in nanny.GetType().GetProperties())
            {

                item.SetValue(tmp, item.GetValue(nanny, null), null);
            }

            tmp.Work_Hours = nanny.Work_Hours;
            tmp.Does_Nanny_Work = nanny.Does_Nanny_Work;

            return tmp;
        }

    }
}


