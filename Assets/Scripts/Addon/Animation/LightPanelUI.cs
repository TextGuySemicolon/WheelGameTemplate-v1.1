using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightPanelUI : MonoBehaviour
{
    [SerializeField] private GameObject[] lightBase;
    private int interval;
    private float intervalTime;

    public void PlayPattern(int _index)
    {
        CancelInvoke("LightPattern1");

        if (_index == -1)
        {
            LightPattern00();
        }
        else
        if (_index == 0)
        {
            LightPattern0();
        }
        else
        if (_index == 1)
        {
            interval = 0;
            intervalTime = 0.2f;
            LightPattern1();
        }
    }
    private void LightPattern00()
    {
        for (int i = 0; i < lightBase.Length; i++)
        {
            lightBase[i].gameObject.SetActive(false);
        }
    }
    private void LightPattern0()
    {
        for (int i = 0; i < lightBase.Length; i++)
        {
            lightBase[i].gameObject.SetActive(true);
        }
    }
    private void LightPattern1()
    {
        for (int i = 0; i < lightBase.Length; i++)
        {
            lightBase[i].gameObject.SetActive(interval % 2 == 0);
        }
        interval++;

        if (interval < 12) Invoke("LightPattern1", intervalTime);
    }

}
