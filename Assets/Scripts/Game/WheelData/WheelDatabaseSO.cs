using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "WheelDatabaseSO", menuName = "ScriptableObject/WheelDatabase")]
public class WheelDatabaseSO : ScriptableObject
{
    public WheelItemSO[] wheelDataList;
    public Sprite[] sectorSprite;
    public Color[] defaultColor;
    public bool useItemColors = false;
    public bool useRamdomPositions = true;

    public SpinType spinType = SpinType.Quantity;

    public float GetSectorAngle() => 360f / wheelDataList.Length;

    public List<WheelItemSO> GetUniqueData()
    {
        // HashSet automatically handles duplicate checking in O(1) time on average
        HashSet<WheelItemSO> uniqueDataSet = new HashSet<WheelItemSO>(wheelDataList);

        // Return the unique data as a List
        return uniqueDataSet.ToList();
    }
}

