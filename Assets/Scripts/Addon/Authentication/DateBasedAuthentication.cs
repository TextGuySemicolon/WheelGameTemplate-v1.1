using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using TMPro;


public class DateBasedAuthentication : MonoBehaviour
{
    //BSN
    //Nilai Emilia 
    [SerializeField] private const string gameKey = "Nilai Emilia";

    [SerializeField] private DateTime inputDateTime = new DateTime(2025,4,30);
    [SerializeField] private string outputString;

    [SerializeField] private UnityEvent onSuccessful;

    [SerializeField] private TextMeshProUGUI text1, text2, text3, text4;


    public void Start()
    {

        //Datetime to string
        outputString = EncryptionHandler.EncryptString(inputDateTime.ToString("ddd, dd MMM yyyy HH:mm:ss 'GMT'"), gameKey);
        //



        //string to Datetime
        try
        {
            DatetimeConfiguration _configuration = new DatetimeConfiguration();
            _configuration.ReadString();
            DateTime.TryParseExact(EncryptionHandler.DecryptString(_configuration.encryptedDeadlineDateTime, gameKey),
                                   "ddd, dd MMM yyyy HH:mm:ss 'GMT'",
                                   CultureInfo.InvariantCulture.DateTimeFormat,
                                   DateTimeStyles.AssumeUniversal, out DateTime outputDateTime);

            text1.text = "decrypted datetime: " + outputDateTime;
            text2.text = "online datetime: " + GetNetTime();

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
        catch
        {
            Debug.Log("decryption failed");

            text1.text = "decryption failed";
            text2.text = GetNetTime().ToString();
        }
        //
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
