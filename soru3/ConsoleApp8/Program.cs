using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Kullanıcıdan tam sayıları alacağız, 0 girildiğinde duracak
        List<int> sayilar = new List<int>();
        int sayi;

        Console.WriteLine("Pozitif tam sayıları girin (0 girildiğinde program duracak):");

        while (true)
        {
            try
            {
                // Kullanıcıdan giriş al ve tam sayıya dönüştür
                sayi = int.Parse(Console.ReadLine());

                if (sayi == 0) // 0 girildiğinde döngüden çık
                    break;

                sayilar.Add(sayi); // Sayıyı listeye ekle
            }
            catch (FormatException)
            {
                Console.WriteLine("Lütfen geçerli bir tam sayı girin.");
            }
        }

        // Dizide ardışık grupları bul ve ekrana yazdır
        List<string> ardısikGruplar = ArdısikGruplariBul(sayilar);

        // Sonuçları ekrana yazdır
        if (ardısikGruplar.Count > 0)
        {
            Console.WriteLine("Ardışık sayılar grupları:");
            foreach (string grup in ardısikGruplar)
            {
                Console.WriteLine(grup);
            }
        }
        else
        {
            Console.WriteLine("Ardışık sayı grubu bulunamadı.");
        }
    }

    // Ardışık grupları bulan metod
    static List<string> ArdısikGruplariBul(List<int> sayilar)
    {
        List<string> gruplar = new List<string>();
        sayilar.Sort(); // Sayıları küçükten büyüğe sıralıyoruz

        int baslangic = sayilar[0];
        int onceki = baslangic;

        for (int i = 1; i < sayilar.Count; i++)
        {
            // Eğer sayı ardışık değilse grubu sonlandır
            if (sayilar[i] != onceki + 1)
            {
                // Grup oluştur
                if (baslangic == onceki)
                {
                    gruplar.Add(baslangic.ToString());
                }
                else
                {
                    gruplar.Add($"{baslangic}-{onceki}");
                }
                // Yeni grup başlat
                baslangic = sayilar[i];
            }
            // Önceki sayıyı güncelle
            onceki = sayilar[i];
        }

        // Son grubu ekle
        if (baslangic == onceki)
        {
            gruplar.Add(baslangic.ToString());
        }
        else
        {
            gruplar.Add($"{baslangic}-{onceki}");
        }

        return gruplar;
    }
}

