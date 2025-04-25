using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightController : MonoBehaviour
{
    [SerializeField] private GameObject[] lightBase;
    [SerializeField] private GameObject[] lightOverlay1;
    [SerializeField] private GameObject[] lightOverlay2;
    [SerializeField] private GameObject[] lightOverlay3;
    private int interval;
    private float intervalTime;

    public void PlayPattern(int _index)
    {
        CancelInvoke("LightPattern1");
        CancelInvoke("LightPattern2");
        if (_index == 0)
        {
            LightPattern0();
        }
        else
        if (_index == 1)
        {
            interval = 0;
            intervalTime = 0.5f;
            LightPattern1();
        }
        else
        if (_index == 2)
        {
            interval = 0;
            intervalTime = 0.2f;
            LightPattern2();
        }

    }
    private void LightPattern0()
    {
        for (int i = 0; i < lightBase.Length; i++)
        {
            if(lightBase[i]) lightBase[i].gameObject.SetActive(true);
            if (lightOverlay1[i]) lightOverlay1[i].gameObject.SetActive(true);
            if (lightOverlay2[i]) lightOverlay2[i].gameObject.SetActive(true);
            if (lightOverlay3[i]) lightOverlay3[i].gameObject.SetActive(true);
        }
    }
    private void LightPattern1()
    {
        for (int i = 0; i < lightBase.Length; i++)
        {
            if (lightBase[i]) lightBase[i].gameObject.SetActive((i + interval) % 2 == 0);
            if (lightOverlay1.Length == lightBase.Length) lightOverlay1[i].gameObject.SetActive((i + interval) % 2 == 0);
            if (lightOverlay2.Length == lightBase.Length) lightOverlay2[i].gameObject.SetActive((i + interval) % 2 == 0);
            if (lightOverlay3.Length == lightBase.Length) lightOverlay3[i].gameObject.SetActive((i + interval) % 2 == 0);
        }
        interval++;

        Invoke("LightPattern1", intervalTime);
    }
    private void LightPattern2()
    {
        for (int i = 0; i < lightBase.Length; i++)
        {
            if (lightBase[i]) lightBase[i].gameObject.SetActive(interval % 2 == 0);
            if (lightOverlay1.Length == lightBase.Length) lightOverlay1[i].gameObject.SetActive(interval % 2 == 0);
            if (lightOverlay2.Length == lightBase.Length) lightOverlay2[i].gameObject.SetActive(interval % 2 == 0);
            if (lightOverlay3.Length == lightBase.Length) lightOverlay3[i].gameObject.SetActive(interval % 2 == 0);
        }
        interval++;

        if(interval < 12) Invoke("LightPattern2", intervalTime);
    }

}
