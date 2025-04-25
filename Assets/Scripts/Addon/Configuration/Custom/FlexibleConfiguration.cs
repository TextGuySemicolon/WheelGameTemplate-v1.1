using UnityEngine;
using System.IO;
using Defective.JSON;
using System;

public class FlexibleConfiguration : ConfigurationBase
{
    public JSONObject json;

    public FlexibleConfiguration()
    {
        json = new JSONObject();
    }

    protected override void Deserialize(StreamReader _reader)
    {
        json = JSONObject.Create(_reader.ReadToEnd());
        _reader.Close();
    }

    protected override void Serialize(StreamWriter writer)
    {
        writer.Write(json.ToString());
        writer.Close();
    }
}

[System.Serializable]
public class FlexibleData
{
    public string id;
    public string value;

    public FlexibleData(string _id, string _value)
    {
        id = _id;
        value = _value;
    }
}

