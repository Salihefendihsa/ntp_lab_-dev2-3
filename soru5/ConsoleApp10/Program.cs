using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        Console.WriteLine("Polinom hesaplayıcıya hoş geldiniz! 'exit' yazarak çıkabilirsiniz.");

        while (true)
        {
            // Kullanıcıdan iki polinom alın
            Console.Write("Birinci polinomu girin (örn: 2x^2 + 3x - 5): ");
            string polinom1 = Console.ReadLine();
            if (polinom1.ToLower() == "exit") break;

            Console.Write("İkinci polinomu girin (örn: x^2 - 4): ");
            string polinom2 = Console.ReadLine();
            if (polinom2.ToLower() == "exit") break;

            // Polinomları çözümleyip sözlük yapısına dönüştür
            var polinom1Dict = PolinomCozumle(polinom1);
            var polinom2Dict = PolinomCozumle(polinom2);

            // Polinomları topla ve çıkar
            var toplam = PolinomTopla(polinom1Dict, polinom2Dict);
            var fark = PolinomCikar(polinom1Dict, polinom2Dict);

            // Sonuçları ekrana yazdır
            Console.WriteLine("Toplam: " + PolinomYazdir(toplam));
            Console.WriteLine("Fark: " + PolinomYazdir(fark));
        }
    }

    // Polinomu çözümleyip katsayıları derecelerine göre sözlüğe ekler
    static Dictionary<int, double> PolinomCozumle(string polinom)
    {
        var dict = new Dictionary<int, double>();

        // Polinomu terimlerine ayırmak için regex kullan
        string pattern = @"([+-]?\s*\d*\.?\d*)\s*\*?\s*x\^?(\d*)";
        var matches = Regex.Matches(polinom.Replace(" ", ""), pattern);

        foreach (Match match in matches)
        {
            // Katsayıyı belirle (boşsa 1 veya -1 kabul edilir)
            double katsayi = string.IsNullOrEmpty(match.Groups[1].Value) || match.Groups[1].Value == "+" ? 1 :
                             match.Groups[1].Value == "-" ? -1 : double.Parse(match.Groups[1].Value);

            // Dereceyi belirle (boşsa 1, yoksa 0 kabul edilir)
            int derece = string.IsNullOrEmpty(match.Groups[2].Value) ? 1 :
                         int.Parse(match.Groups[2].Value == "" ? "0" : match.Groups[2].Value);

            if (dict.ContainsKey(derece))
                dict[derece] += katsayi; // Aynı dereceli terimleri toplar
            else
                dict[derece] = katsayi;
        }

        return dict;
    }

    // İki polinomu toplama işlemi
    static Dictionary<int, double> PolinomTopla(Dictionary<int, double> p1, Dictionary<int, double> p2)
    {
        var result = new Dictionary<int, double>(p1);

        foreach (var kvp in p2)
        {
            if (result.ContainsKey(kvp.Key))
                result[kvp.Key] += kvp.Value;
            else
                result[kvp.Key] = kvp.Value;
        }

        return result;
    }

    // İki polinomu çıkarma işlemi
    static Dictionary<int, double> PolinomCikar(Dictionary<int, double> p1, Dictionary<int, double> p2)
    {
        var result = new Dictionary<int, double>(p1);

        foreach (var kvp in p2)
        {
            if (result.ContainsKey(kvp.Key))
                result[kvp.Key] -= kvp.Value;
            else
                result[kvp.Key] = -kvp.Value;
        }

        return result;
    }

    // Sözlüğü polinom formatında metne dönüştürme
    static string PolinomYazdir(Dictionary<int, double> polinom)
    {
        var terimler = new List<string>();

        foreach (var kvp in polinom)
        {
            double katsayi = kvp.Value;
            int derece = kvp.Key;

            if (katsayi == 0) continue; // Sıfır katsayılı terimleri atla

            // Katsayı ve dereceye göre terimi oluştur
            string terim = katsayi.ToString();
            if (derece > 0)
                terim += derece == 1 ? "x" : $"x^{derece}";

            // İşaret ve katsayı formatı için
            if (katsayi > 0 && terimler.Count > 0)
                terim = "+" + terim;

            terimler.Add(terim);
        }

        return string.Join(" ", terimler);
    }
}
