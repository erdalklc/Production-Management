using EPM.Core.Helpers;
using System;

namespace EPM.Transver
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Aktarim Başladı !\t" + DateTime.Now.ToString("g"));
            new Aktarim().AktarKaliteGerceklesen();
            Console.WriteLine("Aktarim Bitti !\t" + DateTime.Now.ToString("g"));

            new IcUretimAnaliz().AnalizEt();
        }
    }
}
