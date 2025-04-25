using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public abstract class ConfigurationBase
{
    public virtual string GetFilename() => "setting";
    public virtual string GetPath()
    {
        string path;
        #if UNITY_STANDALONE_WIN
                            path = Application.dataPath + $"/../../{GetFilename()}.txt";
        #endif

        #if UNITY_EDITOR_WIN
                path = Application.dataPath + $"/../{GetFilename()}.txt";
#endif

#if UNITY_ANDROID
                path = Application.persistentDataPath + $"/{GetFilename()}.txt";
#endif

        return path;
    }

    public ConfigurationBase()
    {

    }

    protected abstract void Deserialize(StreamReader _reader);

    protected abstract void Serialize(StreamWriter writer);

    public void WriteString()
    {
        Debug.Log("save configuration at " + GetPath());

        File.WriteAllText(GetPath(), string.Empty);
        StreamWriter writer = new StreamWriter(GetPath(), true);
        Serialize(writer);
        writer.Close();
    }

    public void ReadString()
    {
        try
        {
            StreamReader reader = new StreamReader(GetPath());
            Deserialize(reader);
            reader.Close();
        }
        catch
        {

        }
    }


}