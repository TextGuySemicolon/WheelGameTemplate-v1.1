using System.Collections.Generic;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using System.IO;
using UnityEngine;

public class GoogleSpreadsheetForWheel : MonoBehaviour
{
    private static string[] Scopes = { SheetsService.Scope.Spreadsheets };
    private static string ApplicationName = "Unity Google Sheets Integration";
    private static string SpreadsheetId = "1V3y2D1wD8oY_OEutxWjGGRiTRAekNJycew_QU42IBeo"; // Replace with your spreadsheet ID
    private static string Range = "Sheet1!A2:B9"; // Adjust to match your spreadsheet's data range
    private SheetsService service;

    public TextAsset credentialsJson;

    public static bool isReady;

    void Start()
    {
        if (credentialsJson == null)
        {
            Debug.LogError("Credentials JSON file not assigned!");
            return;
        }

        Authenticate();
    }

    void Authenticate()
    {
        GoogleCredential credential;

        // Use the JSON data from the TextAsset
        using (var stream = new MemoryStream(credentialsJson.bytes))
        {
            credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);
        }

        service = new SheetsService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = ApplicationName,
        });
    }

    public Dictionary<string, int> ReadSpreadsheet()
    {
        Dictionary<string, int> _itemList = new Dictionary<string, int>();

        var request = service.Spreadsheets.Values.Get(SpreadsheetId, Range);
        ValueRange response = request.Execute();
        IList<IList<object>> values = response.Values;

        if (values != null && values.Count > 0)
        {
            foreach (var row in values)
            {
                string id = row[0].ToString().Trim();
                int quantity = int.Parse(row[1].ToString());
                Debug.Log($"ID: {id}, Quantity: {quantity}");
                _itemList.Add(id, quantity);
            }

            isReady = true;
        }
        else
        {
            Debug.Log("No data found.");
        }

        return _itemList;
    }

    public void UpdateQuantity(string itemId, int newQuantity)
    {
        var request = service.Spreadsheets.Values.Get(SpreadsheetId, Range);
        ValueRange response = request.Execute();
        IList<IList<object>> values = response.Values;

        if (values != null && values.Count > 0)
        {
            for (int i = 0; i < values.Count; i++)
            {
                if (values[i][0].ToString().Trim() == itemId.Trim())
                {
                    // Update the quantity in the corresponding row
                    values[i][1] = newQuantity;
                    break;
                }
            }

            // Write the updated values back to the spreadsheet
            ValueRange updateBody = new ValueRange
            {
                Values = values
            };

            var updateRequest = service.Spreadsheets.Values.Update(updateBody, SpreadsheetId, Range);
            updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
            updateRequest.Execute();

            Debug.Log($"Updated {itemId} to quantity {newQuantity}");
        }
    }
}