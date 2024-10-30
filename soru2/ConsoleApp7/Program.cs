using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Kullanıcıdan pozitif tam sayılar alacağız, 0 girildiğinde duracak
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

                if (sayi > 0)
                {
                    sayilar.Add(sayi);
                }
                else
                {
                    Console.WriteLine("Lütfen pozitif bir tam sayı girin.");
                }
            }
            catch (FormatException)
            {
                // Geçersiz giriş olduğunda kullanıcıya uyarı ver
                Console.WriteLine("Lütfen geçerli bir tam sayı girin.");
            }
        }

        // Sayı listesi boş değilse ortalama ve medyanı hesapla
        if (sayilar.Count > 0)
        {
            // Ortalamayı hesapla
            double ortalama = OrtalamaHesapla(sayilar);

            // Medyanı hesapla
            double medyan = MedyanHesapla(sayilar);

            // Sonuçları ekrana yazdır
            Console.WriteLine($"Ortalama: {ortalama}");
            Console.WriteLine($"Medyan: {medyan}");
        }
        else
        {
            Console.WriteLine("Hiç pozitif tam sayı girilmedi.");
        }
    }

    // Ortalama hesaplayan metod
    static double OrtalamaHesapla(List<int> sayilar)
    {
        double toplam = 0;
        foreach (int sayi in sayilar)
        {
            toplam += sayi;
        }
        return toplam / sayilar.Count;
    }

    // Medyan hesaplayan metod
    static double MedyanHesapla(List<int> sayilar)
    {
        sayilar.Sort(); // Sayıları sıralıyoruz

        int n = sayilar.Count;
        if (n % 2 == 1)
        {
            return sayilar[n / 2]; // Tek eleman varsa ortadaki eleman medyan
        }
        else
        {
            // Çift eleman varsa ortadaki iki elemanın ortalaması medyan
            return (sayilar[(n / 2) - 1] + sayilar[n / 2]) / 2.0;
        }
    }
}
