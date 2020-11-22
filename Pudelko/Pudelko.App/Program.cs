using PudelkoLibrary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PudelkoApp
{
    class Program
    {
        static void Main(string[] args)
        {
    var pudelkaList = new List<Pudelko>
            {
                new Pudelko(),
                   new Pudelko(3, 7, 10),
                     new Pudelko(10, 0.2),
                          new Pudelko(198, 168, 01, UnitOfMeasure.centimeter),
                            new Pudelko(1.10, 110.542223, 9.1),
                              new Pudelko(0.1, 0.2, 0.3, UnitOfMeasure.centimeter),
                                new Pudelko(1.0, 2.5),
                                  new Pudelko(5.99, 9.999),
                                    new Pudelko(69.1, 6.4, UnitOfMeasure.centimeter),
                                      new Pudelko(0.1, 2599, UnitOfMeasure.milimeter),
                                        new Pudelko(5.0, 2.5),
                                          new Pudelko(99, 9.999),
                                             new Pudelko(1, 2),
                                                new Pudelko(59, 9.999),
                                                    new Pudelko(10220.8, UnitOfMeasure.centimeter),
                                                       new Pudelko(20.77, UnitOfMeasure.milimeter)
             };


           
            Console.WriteLine("Wypisz");
            foreach (var pudelko in pudelkaList)
                Console.WriteLine(pudelko.ToString());

            Console.WriteLine("\nSort");
            var sortedList = pudelkaList
                .OrderBy(x => x.Objetosc)
                .ThenBy(x => x.Pole)
                .ThenBy(x => x.A + x.B + x.C)
                .ToList();

            foreach (var pudelko in sortedList)
                Console.WriteLine(pudelko.ToString());

            Console.WriteLine("\nPorównaj");
            pudelkaList.Sort(PoruwnajPodelka);

            foreach (var pudelko in pudelkaList)
                Console.WriteLine(pudelko.ToString());

        }

        private static int PoruwnajPodelka(Pudelko p1, Pudelko p2)
        {
                    var result = p1.Objetosc.CompareTo(p2.Objetosc);
            if (result == 0)
            {
                result = p1.Pole.CompareTo(p2.Pole);
                if (result == 0)
                result = (p1.A + p1.B + p1.C).CompareTo(p2.A + p2.B + p2.C);
                
            }

            return result;
        }


    }
}
