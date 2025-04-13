using System;
class Program
{
    static void Main(string[] args)
    {
        CovidConfig config = CovidConfig.LoadFromFile("/Users/rakhafatihathallah/Projects/tpmodul8_103022300130/tpmodul8_103022300130/covid_config.json");

        Console.WriteLine($"Suhu saat ini: {config.SatuanSuhu}");

        Console.Write("\nApakah ingin mengubah satuan suhu? (y/n): ");
        string ubah = Console.ReadLine();
        if (ubah.ToLower() == "y")
        {
            config.UbahSatuan();
        }

        Console.WriteLine($"Satuan suhu sekarang: {config.SatuanSuhu}");

        Console.Write($"Berapa suhu badan anda saat ini dalam {config.SatuanSuhu}? ");
        if (!double.TryParse(Console.ReadLine(), out double suhu))
        {
            Console.WriteLine("Input suhu tidak valid.");
            return;
        }

        Console.Write("Berapa hari yang lalu anda terakhir kali terkena gejala demam? ");
        if (!int.TryParse(Console.ReadLine(), out int hari))
        {
            Console.WriteLine("Input hari tidak valid.");
            return;
        }

        bool diterima = config.ApakahDiterima(suhu, hari);

        Console.WriteLine(diterima ? config.PesanDiterima : config.PesanDitolak);

    }
}