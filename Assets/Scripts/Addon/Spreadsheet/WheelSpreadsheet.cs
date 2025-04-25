using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WheelSpreadsheet
{
    
    public static SpinType spinType = SpinType.Percentage;
    public static string SheetPath
    {
        get => $"../../spreadsheet{spinType.ToString()}.csv";
    }

    public static bool HasSpreadsheet()
    {
        return ES3.FileExists(SheetPath);
    }

    public static void CreateSpreadsheet(WheelDatabaseSO database)
    {
        var sheet = new ES3Spreadsheet();

        // Header row (row 0)
        sheet.SetCell(0, 0, "id");
        sheet.SetCell(1, 0, spinType.ToString());

        // Populate data starting from row 1
        List<WheelItemSO> uniqueList = database.GetUniqueData();
        for (int i = 0; i < uniqueList.Count; i++)
        {
            WheelItemSO data = uniqueList[i];
            int row = i + 1;

            sheet.SetCell(0, row, data.id);       // Column 0 = id
            sheet.SetCell(1, row, spinType == SpinType.Quantity ? data.quantity : data.percentage); // Column 1 = quantity or percentage
        }

        sheet.Save(SheetPath);
        Debug.Log("Spreadsheet created with data from WheelDatabaseSO " + database.name);
    }


    public static ES3Spreadsheet ReadSpreadsheet()
    {
        // Create a blank ES3Spreadsheet.
        var sheet = new ES3Spreadsheet();
        if (!ES3.FileExists(SheetPath))
        {
            // Add headers
            sheet.SetCell(0, 0, "id");
            sheet.SetCell(0, 1, spinType.ToString());
            sheet.Save(SheetPath);
        }
        sheet.Load(SheetPath);

        return sheet;
    }

    public static int GetQuantityById(string id)
    {
        ES3Spreadsheet sheet = ReadSpreadsheet();

        for (int row = 1; row < sheet.GetColumnLength(0); row++) // scan column 0
        {
            string currentId = sheet.GetCell<string>(0, row);
            if (currentId == id)
            {
                return sheet.GetCell<int>(1, row); // Column 1 is quantity or percentage
            }
        }

        return -1; // Not found
    }


    public static void SetQuantityById(string id, int newQuantity)
    {
        ES3Spreadsheet sheet = ReadSpreadsheet();
        bool found = false;

        for (int row = 1; row < sheet.GetColumnLength(0); row++)
        {
            string currentId = sheet.GetCell<string>(0, row);
            if (currentId == id)
            {
                sheet.SetCell(1, row, newQuantity); // update quantity
                found = true;
                break;
            }
        }

        if (!found)
        {
            int newRow = sheet.GetColumnLength(0); // get next empty row
            sheet.SetCell(0, newRow, id);
            sheet.SetCell(1, newRow, newQuantity);
        }

        sheet.Save(SheetPath);
    }

    public static float GetPercentageById(string id)
    {
        ES3Spreadsheet sheet = ReadSpreadsheet();
        for(int row = 1; row < sheet.GetColumnLength(0); row++)
        {
            string currentId = sheet.GetCell<string>(0, row);
            if (currentId == id)
            {
                return sheet.GetCell<float>(1, row); // Column 1 is quantity or percentage
            }
        }

        return 0;//if not found
    }

    public static void SetPercentageById(string id, float newPercentage)
    {
        ES3Spreadsheet sheet = ReadSpreadsheet();
        bool found = false;

        for (int row = 1; row < sheet.GetColumnLength(0); row++)
        {
            string currentId = sheet.GetCell<string>(0, row);
            if (currentId == id)
            {
                sheet.SetCell(1, row, newPercentage); // update quantity
                found = true;
                break;
            }
        }

        if (!found)
        {
            int newRow = sheet.GetColumnLength(0); // get next empty row
            sheet.SetCell(0, newRow, id);
            sheet.SetCell(1, newRow, newPercentage);
        }

        sheet.Save(SheetPath);
    }

}
