using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Defective.JSON;

[ExecuteAlways]
public class WheelBoard : MonoBehaviour
{
    public WheelDatabaseSO GetDatabase() => database;


    [SerializeField] private WheelDatabaseSO database;

    [SerializeField] private RectTransform sectorParent;
    [SerializeField] private WheelSector sectorPrefab;


    [SerializeField] private List<WheelSector> sectorList;

    public static bool isReady;
    public float GetRotationZ(WheelItemSO _data)
    {
        List<WheelSector> _sectors = new List<WheelSector>();
        foreach(WheelSector _sector in sectorList)
        {
            if (_sector.item == _data) _sectors.Add(_sector);
        }

        return _sectors[Random.Range(0, _sectors.Count)].GetRandomRotation();
    }

    public void AnimateSector(WheelItemSO _data)
    {
        foreach (WheelSector _sector in sectorList)
        {
            if (_sector.item == _data)
            {
                _sector.AnimateSector();
                //return;
            }
        }
    }

    public List<WheelItemSO> GetUniqueData()
    {
        // HashSet automatically handles duplicate checking in O(1) time on average
        HashSet<WheelItemSO> uniqueDataSet = new HashSet<WheelItemSO>(database.wheelDataList);

        // Return the unique data as a List
        return uniqueDataSet.ToList();
    }


    private void Start()
    {
        ClearWheelSector();
        CreateWheelSectors(database.useRamdomPositions);

        LeanTween.delayedCall(1.5f, ReadData);
    }



    private void ClearWheelSector()
    {
        while (sectorList.Count > 0)
        {
            GameObject _obj = sectorList[0].gameObject;
            sectorList.RemoveAt(0);
            DestroyImmediate(_obj);
        }
    }

    private void CreateWheelSectors(bool randomize)
    {
        if (sectorList.Count == database.wheelDataList.Length) return;

        while(sectorList.Count < database.wheelDataList.Length)
        {
            WheelSector _sector = Instantiate(sectorPrefab, sectorParent);
            sectorList.Add(_sector);
        }

        while (sectorList.Count > database.wheelDataList.Length)
        {
            GameObject _obj = sectorList[0].gameObject;
            sectorList.RemoveAt(0);
            DestroyImmediate(_obj);
        }

        // Instantiate WheelSector based on item list.
        WheelItemSO[] itemList = randomize ? database.wheelDataList.OrderBy(x => Random.value).ToArray() : database.wheelDataList;
        for (int i = 0; i < itemList.Length; i++)
        {
            WheelSector _sector = sectorList[i];
            _sector.Set(i, database, itemList[i]);
            _sector.UpdateVisual();

            RectTransform _sectorRect = _sector.transform as RectTransform;
            _sectorRect.transform.position = sectorParent.transform.position;
            _sectorRect.sizeDelta = sectorParent.sizeDelta;
            _sectorRect.rotation = Quaternion.Euler(0f, 0f, -database.GetSectorAngle() * i);

            // Calculate rotation range for each item.
            float _halfSectorAngle = database.GetSectorAngle() / 2f;
            _sector.minRotation = (database.GetSectorAngle() * i) - _halfSectorAngle;
            _sector.maxRotation = (database.GetSectorAngle() * i) + _halfSectorAngle;
        }
    }

    public void ReadData()
    {

        isReady = true;
        WheelSpreadsheet.spinType = database.spinType;
        if(database.spinType == SpinType.Quantity)
        {
            ReadQuantityData();
        }else if(database.spinType == SpinType.Percentage)
        {
            ReadPercentageData();
        }

        
    }

    public void ReadQuantityData()
    {
        if (!WheelSpreadsheet.HasSpreadsheet()) WheelSpreadsheet.CreateSpreadsheet(database);
        foreach (WheelItemSO _data in database.wheelDataList)
        {
            int quantity = WheelSpreadsheet.GetQuantityById(_data.id);
            if (quantity == -1)
            {
                Debug.Log(_data.id + " not found!");
                isReady = false;
            }

            _data.quantity = quantity;
        }
    }
    public void ReadPercentageData()
    {
        if (!WheelSpreadsheet.HasSpreadsheet()) WheelSpreadsheet.CreateSpreadsheet(database);
        foreach (WheelItemSO _data in database.wheelDataList)
        {
            float percentage = WheelSpreadsheet.GetPercentageById(_data.id);
            if (percentage == 0)
            {
                Debug.Log(_data.id + " not found!");
                isReady = false;
            }
            Debug.Log($"Setting {_data.displayName} {_data.percentage} to {percentage}");
            _data.percentage = percentage;
        }
    }

    public void UpdateQuantity(string _id, int _quantity)
    {
        WheelSpreadsheet.SetQuantityById(_id, _quantity);
    }
    public void UpdatePercentage(string _id, int _percentage)
    {
        WheelSpreadsheet.SetPercentageById(_id, _percentage);
    }
}

