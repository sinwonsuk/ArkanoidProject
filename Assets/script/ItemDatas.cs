using Newtonsoft.Json;
using System.IO;
using UnityEngine;

[System.Serializable]
public class ItemDatas 
{
    [System.Serializable]
    public class ItemGreenCatchData
    {
        public int type = 0;
    }
    [System.Serializable]
    public class ItemRedLasersData
    {
        public int type = 0;
    }
    [System.Serializable]
    public class ItemBlueEnlargeData
    {
        public float size = 0;
        public int type = 0;
    }
    [System.Serializable]
    public class ItemOrangeSlowData
    {
        public float decrease = 0;
        public int type = 0;
    }
    public ItemDatas()
    {
        mItemCatch = new ItemGreenCatchData();
        mItemRedLasers = new ItemRedLasersData();
        mItemBlueEnlarge = new ItemBlueEnlargeData();
        mItemOrangeSlow = new ItemOrangeSlowData();
    }

    public void Save()
    {
        string fileName = "ItemGreenCatchData.json";
        fileName = Path.Combine(Application.dataPath, "ItemData", fileName);
        string toJson = JsonConvert.SerializeObject(this, Formatting.Indented);
        File.WriteAllText(fileName, toJson);
    }

    public ItemGreenCatchData mItemCatch;
    public ItemRedLasersData mItemRedLasers;
    public ItemBlueEnlargeData mItemBlueEnlarge;
    public ItemOrangeSlowData mItemOrangeSlow;
}
