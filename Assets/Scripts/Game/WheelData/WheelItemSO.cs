using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WheelItemSO", menuName = "ScriptableObject/WheelItem")]
public class WheelItemSO : ScriptableObject
{
    public string displayName = "";
    public string id;
    public int quantity;
    public float percentage;
    public Color backgroundColor = new Color(0.992f, 0.984f, 0.831f);
    public Color textColor = Color.black;

    public string GetDisplayName()
    {
        if (displayName != "") return displayName;
        return id;
    }
}
