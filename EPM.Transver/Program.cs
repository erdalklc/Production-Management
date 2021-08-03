using EPM.Core.Helpers;
using System;
using System.Threading;

namespace EPM.Transver
{
    public class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                if (DateTime.Now.Hour == 23 || DateTime.Now.Hour == 13)
                {

                    Console.WriteLine("Aktarim Başladı !\t" + DateTime.Now.ToString("g"));
                    new Aktarim().AktarKaliteGerceklesen();
                    Console.WriteLine("Aktarim Bitti !\t" + DateTime.Now.ToString("g"));

                    new IcUretimAnaliz().AnalizEt();
                }
                else
                {
                    Console.WriteLine($"Aktarım Bekleniyor {DateTime.Now.ToString("g")}");
                    Thread.Sleep(TimeSpan.FromMinutes(20));
                }
            }
        }
    }
}
