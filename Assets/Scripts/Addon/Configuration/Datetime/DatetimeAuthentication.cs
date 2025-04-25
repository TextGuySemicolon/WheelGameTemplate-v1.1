using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using TMPro;

public class DatetimeAuthentication : MonoBehaviour
{
    [SerializeField] private string outputString;
    [SerializeField] private UnityEvent onSuccessful;

    public void Start()
    {
        DateTime outputDateTime = new DateTime(2025, 1, 1);

        if (outputDateTime > GetNetTime())
        {
            Debug.Log("sucessful!");
            onSuccessful?.Invoke();
        }
        else
        {
            Debug.Log("unsucessful!");

        }
    }
    public static DateTime GetNetTime()
    {
        var myHttpWebRequest = (HttpWebRequest)WebRequest.Create("http://www.microsoft.com");
        var response = myHttpWebRequest.GetResponse();
        string todaysDates = response.Headers["date"];
        return DateTime.ParseExact(todaysDates,
                                   "ddd, dd MMM yyyy HH:mm:ss 'GMT'",
                                   CultureInfo.InvariantCulture.DateTimeFormat,
                                   DateTimeStyles.AssumeUniversal);
    }
}
