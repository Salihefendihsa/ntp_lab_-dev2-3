using System;

class Program
{
    static void Main()
    {
        // Kullanıcıdan tam sayı dizisini al
        Console.WriteLine("Dizideki eleman sayısını girin:");
        int n = int.Parse(Console.ReadLine());

        // Diziyi oluştur ve kullanıcıdan değerleri al
        int[] dizi = new int[n];
        Console.WriteLine("Dizi elemanlarını girin:");
        for (int i = 0; i < n; i++)
        {
            dizi[i] = int.Parse(Console.ReadLine());
        }

        // Diziyi sıralama
        Array.Sort(dizi);

        // Sıralanmış diziyi ekrana yazdır
        Console.WriteLine("Sıralanmış dizi:");
        foreach (int sayi in dizi)
        {
            Console.Write(sayi + " ");
        }
        Console.WriteLine();

        // Kullanıcıdan aramak istediği sayıyı al
        Console.WriteLine("Aramak istediğiniz sayıyı girin:");
        int arananSayi = int.Parse(Console.ReadLine());

        // İkili arama algoritması kullanarak sayıyı kontrol et
        int sonuc = BinarySearch(dizi, arananSayi);

        // Sonucu ekrana yazdır
        if (sonuc != -1)
        {
            Console.WriteLine($"Sayı dizide bulundu. İndex: {sonuc}");
        }
        else
        {
            Console.WriteLine("Sayı dizide bulunamadı.");
        }
    }

    // İkili arama metodu
    static int BinarySearch(int[] dizi, int arananSayi)
    {
        int baslangic = 0;
        int bitis = dizi.Length - 1;

        while (baslangic <= bitis)
        {
            int orta = (baslangic + bitis) / 2;

            if (dizi[orta] == arananSayi)
            {
                return orta; // Aranan sayı bulundu, index'i geri döndür
            }
            else if (dizi[orta] < arananSayi)
            {
                baslangic = orta + 1; // Aranan sayı sağ tarafta
            }
            else
            {
                bitis = orta - 1; // Aranan sayı sol tarafta
            }
        }

        return -1; // Aranan sayı bulunamadı
    } 
}
