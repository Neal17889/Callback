using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;
using System.IO;

using Common.Data;

using Newtonsoft.Json;

public class DataManager : Singleton<DataManager>
{
    public string DataPath;
    public Dictionary<int, ShopDefine> Shops = null;


    public DataManager()
    {
        this.DataPath = "Data/";
    }


    public IEnumerator LoadData()
    {
        
        string json = File.ReadAllText(this.DataPath + "ShopDefine.txt");
        this.Shops = JsonConvert.DeserializeObject<Dictionary<int, ShopDefine>>(json);

        yield return null;
    }
}
