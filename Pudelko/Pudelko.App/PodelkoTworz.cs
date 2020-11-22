using PudelkoLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace PudelkoApp
{
    public static class PodelkoTworz
    {
        public static Pudelko Kompresuj(this Pudelko p)
        {
            var objetosc = p.Objetosc;
                var strona = Math.Cbrt(objetosc);
                   return new Pudelko(strona, strona, strona);
        }
    }
}
