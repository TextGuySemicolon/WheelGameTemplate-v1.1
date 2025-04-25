using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

[ExecuteAlways]
public class WheelSector : MonoBehaviour
{
    public int index;
    public WheelDatabaseSO database;
    public WheelItemSO item;

    [SerializeField] private Image baseImg;
    [SerializeField] private Image baseOverlayImg;
    [SerializeField] private TextMeshProUGUI text;

    public float minRotation, maxRotation;
    public float GetRandomRotation() => Random.Range(minRotation, maxRotation);

    private void Update()
    {
        if (!Application.isPlaying) UpdateVisual();
    }

    public void Set(int _index, WheelDatabaseSO _database, WheelItemSO _item)
    {
        index = _index;
        database = _database;
        item = _item;
        UpdateVisual();
    }

    public void UpdateVisual()
    {
        if (baseImg == null) return;
        if (text == null) return;

        text.text = (item) ? item.GetDisplayName() : "-";
        if (!database.useItemColors || !item)
        {
            text.color = database.defaultColor.Length > 0 ? database.defaultColor[index % database.defaultColor.Length] : Color.white;
        }
        else
        {
            text.color = item.textColor;
        }

        baseImg.sprite = database.sectorSprite.Length > 0 ? database.sectorSprite[index % database.sectorSprite.Length] : null;
        baseImg.fillAmount = 1f / database.wheelDataList.Length;
        baseImg.transform.localRotation = Quaternion.Euler(0f, 0f, 180f + database.GetSectorAngle() / 2f);
        if (database.useItemColors) {
            baseImg.color = item.backgroundColor;
        }

        baseOverlayImg.fillAmount = 1f / database.wheelDataList.Length;
        baseOverlayImg.transform.localRotation = Quaternion.Euler(0f, 0f, 180f + database.GetSectorAngle() / 2f);
        if(database.useItemColors)
        {
            baseOverlayImg.color = item.backgroundColor; 
        }
    }

    public void AnimateSector()
    {
        baseOverlayImg.enabled = !baseOverlayImg.enabled;
        LeanTween.delayedCall(gameObject, 0.25f, () =>
        {
            baseOverlayImg.enabled = !baseOverlayImg.enabled;
        }).setRepeat(9);
    }
}
