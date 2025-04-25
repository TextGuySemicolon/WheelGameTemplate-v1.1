using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Defective.JSON;

public class ConfigurationTest : MonoBehaviour
{

    public List<FlexibleData> data;
    private void OnEnable()
    {
        FlexibleConfiguration _configuration = new FlexibleConfiguration();

        JSONObject _json = new JSONObject();
        foreach(FlexibleData _data in data)
        {
            _json.AddField(_data.id, _data.value);
        }


        Debug.Log(_json);

        _configuration.json = _json;
        _configuration.WriteString();
    }

    private void OnDisable()
    {
        FlexibleConfiguration _configuration = new FlexibleConfiguration();
        _configuration.ReadString();

        data = new List<FlexibleData>();

        foreach (JSONObject _data in _configuration.json)
        {
            data.Add(new FlexibleData(_data.keys[0], _data.stringValue));
        }
    }
}
