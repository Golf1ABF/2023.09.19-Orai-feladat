using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;








namespace pekaruk20230908
{
    class Balaton
    {
        public string Nev { get; set; }
        public string Rajtszam { get; set; }
        public bool Kategoria { get; set; }
        public TimeSpan VersenyIdo { get; set; }
        public int Tavszazalek { get; set; }
        public double IdoOraban => VersenyIdo.TotalHours;

        public Balaton(string sor)
        {
            string[] v = sor.Split(";");
            this.Nev = v[0];
            this.Rajtszam = v[1];
            this.Kategoria = v[2] == "Ferfi";
            var ido = v[3].Split(":");
            this.VersenyIdo = new TimeSpan(
                hours: int.Parse(ido[0]),
                minutes: int.Parse(ido[1]),
                seconds: int.Parse(ido[2]));
            Tavszazalek = int.Parse(v[4]);   
        }
    }




    class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("pekaruk.txt");
            _ = sr.ReadLine();
            var Versenyzok = new List<Balaton>();

            while (!sr.EndOfStream)
            {
                Versenyzok.Add(new Balaton(sr.ReadLine()));
            }

            Console.WriteLine($"3.Feladat: {Versenyzok.Count} Fő");

            Console.WriteLine($"4.Feladat: {Versenyzok.Where(noi => !noi.Kategoria && noi.Tavszazalek == 100).Count()} Fő");

            Console.Write("Kérek egy nevet");
            var bekert = Console.ReadLine().ToLower();
            var f5 = Versenyzok.SingleOrDefault(v => v.Nev.ToLower() == bekert);

            if (f5 == null) Console.WriteLine("\tNincs ilyen nevű versenyző");
            else
            {
                Console.WriteLine("Van ilyen versenyző jipijehurakrunkimanipad");
                Console.WriteLine($"\t teljes tav teljesitve? {(f5.Tavszazalek == 100 ? "Igen" : "Nem")}");
            }
            var f7 = Versenyzok
                .Where(v => v.Kategoria && v.Tavszazalek == 100)
                .Average(v => v.IdoOraban);
            Console.WriteLine($"7.Feladat: Átlag: {f7:0.00} Óra");

            var f8n = Versenyzok
                .Where(v => !v.Kategoria && v.Tavszazalek == 100)
                .OrderByDescending(v => v.VersenyIdo)
                .First();
            var f8f = Versenyzok
                .Where(v => v.Kategoria && v.Tavszazalek == 100)
                .OrderByDescending(v => v.VersenyIdo)
                .First();
            Console.WriteLine("8f. verseny győztesei:");
            Console.WriteLine($"\tNok: {f8n.Nev} ({f8n.Rajtszam}.) - {f8n.VersenyIdo}");
            Console.WriteLine($"\tFerfiak: {f8f.Nev} ({f8f.Rajtszam}.) - {f8f.VersenyIdo}");
        }

        
    }
}


