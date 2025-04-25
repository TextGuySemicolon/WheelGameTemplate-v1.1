using UnityEngine;
using System.IO;
using System;


public class WheelConfiguration : ConfigurationBase
{
    public int airFryer;
    public int astroTiffin;
    public int handheldFan;
    public int lgTv;
    public int powerbank;
    public int astroFibreUmbrella;


    public WheelConfiguration()
    {
        airFryer = 10;
        astroTiffin = 50;
        handheldFan = 20;
        lgTv = 1;
        powerbank = 20;
        astroFibreUmbrella = 100;
    }


    protected override void Deserialize(StreamReader _reader)
    {
        airFryer = _reader.ReadInt(10);
        astroTiffin = _reader.ReadInt(50);
        handheldFan = _reader.ReadInt(20);
        lgTv = _reader.ReadInt(1);
        powerbank = _reader.ReadInt(20);
        astroFibreUmbrella = _reader.ReadInt(100);

        _reader.Close();
    }

    protected override void Serialize(StreamWriter writer)
    {
        writer.WriteLine($"Air Fryer (Qty):{airFryer}");
        writer.WriteLine($"Astro Tiffin (Qty):{astroTiffin}");
        writer.WriteLine($"Handheld Fan (Qty):{handheldFan}");
        writer.WriteLine($"LG 59\" UQ80 Series 4K SMART UHD TV (Qty):{lgTv}");
        writer.WriteLine($"Powerbank (Qty):{powerbank}");
        writer.WriteLine($"Astro Fibre Umbrella (Qty):{astroFibreUmbrella}");
        writer.Close();
    }
}
public class WheelConfigurationData
{
    public string key;
    public int quantity;
}
