using UnityEngine;
using System.IO;
using System;

public static class ConfigurationSetter
{
    #region Read
    public static bool ReadBool(this StreamReader _reader, bool _defaultValue)
    {
        string _line = _reader.ReadLine();
        string _lineValue = _line.Split(":")[_line.Split(":").Length - 1];
        if (_lineValue.ToLower() == "true")
        {
            return true;
        }
        else
        if (_lineValue.ToLower() == "false")
        {
            return false;
        }
        else
        {
            return _defaultValue;
        }
    }
    public static float ReadFloat(this StreamReader _reader, float _defaultValue)
    {
        string _line = _reader.ReadLine();
        string _lineValue = _line.Split(":")[_line.Split(":").Length - 1];
        if (float.TryParse(_lineValue, out float _lineValueFloat))
        {
            return _lineValueFloat;
        }

        return _defaultValue;
    }
    public static int ReadInt(this StreamReader _reader, int _defaultValue)
    {
        string _line = _reader.ReadLine();
        string _lineValue = _line.Split(":")[_line.Split(":").Length - 1];
        if (int.TryParse(_lineValue, out int _lineValueFloat))
        {
            return _lineValueFloat;
        }

        return _defaultValue;
    }
    public static string ReadString(this StreamReader _reader, string _defaultValue)
    {
        string _line = _reader.ReadLine();
        string _lineValue = _line.Split(":")[_line.Split(":").Length - 1];
        if(_lineValue != string.Empty) return _lineValue;
        return _defaultValue;
    }
    #endregion
}

