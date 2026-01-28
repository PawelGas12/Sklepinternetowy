using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklepinternetowy
{
    using System.Collections.Generic;

    namespace Sklepinternetowy
    {
        public class WgWyplatyComparer : IComparer<Pracownik>
        {
            public int Compare(Pracownik? x, Pracownik? y)
            {
                if (x == null || y == null) return 0;

                decimal wyplataX = x.ObliczWyplate();
                decimal wyplataY = y.ObliczWyplate();


                if (wyplataX < wyplataY) return 1;
                if (wyplataX > wyplataY) return -1;
                return 0;
            }
        }
    }

}
