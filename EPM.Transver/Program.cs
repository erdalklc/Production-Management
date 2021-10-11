using EPM.Core.Helpers;
using EPM.Transver.Helpers;
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
                try
                {
                    Console.WriteLine("Aktarim Başladı (Operasyon) !\t" + DateTime.Now.ToString("g"));
                    new OperasyonAnalizHelper().OperasyonAnalizEt();
                    Console.WriteLine("Aktarim Bitti (Operasyon)!\t" + DateTime.Now.ToString("g"));
                    Console.WriteLine("Aktarim Başladı (Kalite Gerceklesen) !\t" + DateTime.Now.ToString("g"));
                    new Aktarim().AktarKaliteGerceklesen();
                    Console.WriteLine("Aktarim Bitti (Kalite Gerceklesen) !\t" + DateTime.Now.ToString("g"));

                    if (DateTime.Now.Hour == 23)
                    {
                        Console.WriteLine("Aktarim Başladı (Analiz) !\t" + DateTime.Now.ToString("g"));
                        new IcUretimAnaliz().AnalizEt();
                        Console.WriteLine("Aktarim Bitti (Analiz) !\t" + DateTime.Now.ToString("g"));
                    }
                    else
                    {
                        Console.WriteLine($"Aktarım Bekleniyor {DateTime.Now.ToString("g")}");
                        Thread.Sleep(TimeSpan.FromMinutes(5));
                    }
                }
                catch (Exception ex )
                {
                    Console.WriteLine(ex.Message);
                }
                
            }
        }
    }
}
