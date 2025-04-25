using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DatetimeConfiguration : ConfigurationBase
{
    public string encryptedDeadlineDateTime;

    public override string GetFilename() => "key";
    public DatetimeConfiguration()
    {
        encryptedDeadlineDateTime = "";
    }
    protected override void Deserialize(StreamReader _reader)
    {
        encryptedDeadlineDateTime = _reader.ReadString("");
    }

    protected override void Serialize(StreamWriter writer)
    {
        writer.WriteLine($"{encryptedDeadlineDateTime}");
        writer.Close();
    }
}
