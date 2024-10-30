using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        Console.WriteLine("Bir matematiksel ifade girin (örn: 3 + 4 * 2 / (1 - 5) ^ 2 ^ 3):");
        string ifade = Console.ReadLine();

        // İfade çözümleme ve sonucu hesaplama
        try
        {
            double sonuc = Hesapla(ifade);
            Console.WriteLine($"Sonuç: {sonuc}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Geçersiz ifade: " + ex.Message);
        }
    }

    // İfade çözümleme ve hesaplama
    static double Hesapla(string ifade)
    {
        // Parantezleri çözerek işlemleri adım adım çözümle
        return ParantezleriCoz(ifade);
    }

    // Parantez içeren ifadeleri çöz
    static double ParantezleriCoz(string ifade)
    {
        // Parantezleri içten dışa doğru çözmek için
        while (ifade.Contains("("))
        {
            // En içteki parantezi bul
            int baslangic = ifade.LastIndexOf('(');
            int bitis = ifade.IndexOf(')', baslangic);
            string parantezIci = ifade.Substring(baslangic + 1, bitis - baslangic - 1);

            // Parantez içindeki ifadeyi çöz
            double parantezSonucu = IslemCoz(parantezIci);

            // Adımı ekrana yazdır
            Console.WriteLine($"{ifade.Substring(0, baslangic)}({parantezIci}){ifade.Substring(bitis + 1)} = {parantezSonucu}");

            // Parantezi sonucu ile değiştir
            ifade = ifade.Substring(0, baslangic) + parantezSonucu + ifade.Substring(bitis + 1);
        }

        // Parantezsiz ifadeyi çöz
        return IslemCoz(ifade);
    }

    // İşlem çözme (üs alma, çarpma/bölme, toplama/çıkarma sırasına göre)
    static double IslemCoz(string ifade)
    {
        // Üs alma işlemi
        ifade = IslemAdimi(ifade, @"(\-?\d+(\.\d+)?)\s*\^\s*(\-?\d+(\.\d+)?)", Math.Pow);

        // Çarpma ve bölme işlemi
        ifade = IslemAdimi(ifade, @"(\-?\d+(\.\d+)?)\s*([*/])\s*(\-?\d+(\.\d+)?)", (a, b, op) => op == "*" ? a * b : a / b);

        // Toplama ve çıkarma işlemi
        ifade = IslemAdimi(ifade, @"(\-?\d+(\.\d+)?)\s*([+-])\s*(\-?\d+(\.\d+)?)", (a, b, op) => op == "+" ? a + b : a - b);

        return double.Parse(ifade);
    }

    // İşlem adımlarını çözümleyen metod
    static string IslemAdimi(string ifade, string pattern, Func<double, double, double> islem)
    {
        var regex = new Regex(pattern);
        while (regex.IsMatch(ifade))
        {
            Match match = regex.Match(ifade);

            // İlk sayıyı, işlemi ve ikinci sayıyı al
            double sayi1 = double.Parse(match.Groups[1].Value);
            double sayi2 = double.Parse(match.Groups[4].Value);
            double sonuc = islem(sayi1, sayi2);

            // Adımı ekrana yazdır
            Console.WriteLine($"{match.Value} = {sonuc}");

            // İşlemi ifadede güncelle
            ifade = ifade.Substring(0, match.Index) + sonuc + ifade.Substring(match.Index + match.Length);
        }
        return ifade;
    }

    // İşlem adımları için aşırı yüklenmiş versiyon (operatörleri dikkate alarak)
    static string IslemAdimi(string ifade, string pattern, Func<double, double, string, double> islem)
    {
        var regex = new Regex(pattern);
        while (regex.IsMatch(ifade))
        {
            Match match = regex.Match(ifade);

            // İlk sayıyı, operatörü ve ikinci sayıyı al
            double sayi1 = double.Parse(match.Groups[1].Value);
            string op = match.Groups[3].Value;
            double sayi2 = double.Parse(match.Groups[4].Value);
            double sonuc = islem(sayi1, sayi2, op);

            // Adımı ekrana yazdır
            Console.WriteLine($"{match.Value} = {sonuc}");

            // İşlemi ifadede güncelle
            ifade = ifade.Substring(0, match.Index) + sonuc + ifade.Substring(match.Index + match.Length);
        }
        return ifade;
    }
}
