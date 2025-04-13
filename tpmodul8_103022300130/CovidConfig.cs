using Microsoft.Extensions.Configuration;
using System;
using System.IO;

public class CovidConfig
{
    public string SatuanSuhu { get; set; } = "celcius";
    public int BatasHariDemam { get; set; } = 14; 
    public string PesanDitolak { get; set; } = "Anda tidak diperbolehkan masuk ke gedung ini";
    public string PesanDiterima { get; set; } = "Anda diperbolehkan masuk ke gedung ini";

    public static CovidConfig LoadFromFile(string filePath)
    {
        var config = new CovidConfig();

        var builder = new ConfigurationBuilder()
            .AddJsonFile(filePath, optional: false);

        IConfiguration configuration = builder.Build();

        config.SatuanSuhu = configuration["satuan_suhu"] == "CONFIG1" ? "celcius" : "fahrenheit";

        int.TryParse(configuration["batas_hari_demam"], out int batasHariDemam);
        config.BatasHariDemam = batasHariDemam == 0 ? 14 : batasHariDemam; 

        config.PesanDitolak = configuration["pesan_ditolak"] == "CONFIG3" ? "Anda tidak diperbolehkan masuk ke gedung ini" : configuration["pesan_ditolak"];
        config.PesanDiterima = configuration["pesan_diterima"] == "CONFIG4" ? "Anda diperbolehkan masuk ke gedung ini" : configuration["pesan_diterima"];

        return config;
    }

    public void UbahSatuan() => SatuanSuhu = SatuanSuhu == "celcius" ? "fahrenheit" : "celcius";

    public bool ApakahDiterima(double suhuInput, int hariDemam)
    {
        bool suhuValid = SatuanSuhu == "celcius" ? suhuInput >= 36.5 && suhuInput <= 37.5 : suhuInput >= 97.7 && suhuInput <= 99.5;
        bool hariValid = hariDemam < BatasHariDemam; 
        return suhuValid && hariValid;
    }
}