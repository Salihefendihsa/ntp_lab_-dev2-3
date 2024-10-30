using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Gelecek tarih için bugünün tarihini al
        DateTime bugun = DateTime.Now;

        // Uygun tarihleri saklamak için bir liste oluştur
        List<string> uygunTarihler = new List<string>();

        // 2000 ile 3000 yılları arasında döngü
        for (int yil = 2000; yil <= 3000; yil++)
        {
            for (int ay = 1; ay <= 12; ay++)
            {
                // Her ayın gün sayısını almak için bir döngü
                int gunSayisi = DateTime.DaysInMonth(yil, ay);
                for (int gun = 1; gun <= gunSayisi; gun++)
                {
                    // Gün, ay ve yıl kontrolü
                    if (AsalMi(gun) && AyBasamakToplamiCiftMi(ay) && YilBasamakToplamiDortteBirindenKucukMu(yil))
                    {
                        DateTime tarih = new DateTime(yil, ay, gun);
                        // Tarih, bugünden büyükse listeye ekle
                        if (tarih > bugun)
                        {
                            uygunTarihler.Add(tarih.ToString("dd/MM/yyyy"));
                        }
                    }
                }
            }
        }

        // Uygun tarihleri ekrana yazdır
        Console.WriteLine("Cihazın kabul ettiği uygun tarihler:");
        foreach (var tarih in uygunTarihler)
        {
            Console.WriteLine(tarih);
        }
    }

    // Asal sayıyı kontrol eden metod
    static bool AsalMi(int sayi)
    {
        if (sayi < 2) return false;
        for (int i = 2; i <= Math.Sqrt(sayi); i++)
        {
            if (sayi % i == 0) return false;
        }
        return true;
    }

    // Ayın basamaklarının toplamının çift olup olmadığını kontrol eden metod
    static bool AyBasamakToplamiCiftMi(int ay)
    {
        int toplam = 0;
        while (ay > 0)
        {
            toplam += ay % 10;
            ay /= 10;
        }
        return toplam % 2 == 0;
    }

    // Yılın basamaklarının toplamının yılın dörtte birinden küçük olup olmadığını kontrol eden metod
    static bool YilBasamakToplamiDortteBirindenKucukMu(int yil)
    {
        int toplam = 0;
        int tmpYil = yil;
        while (tmpYil > 0)
        {
            toplam += tmpYil % 10;
            tmpYil /= 10;
        }
        return toplam < (yil / 4);
    }
}
