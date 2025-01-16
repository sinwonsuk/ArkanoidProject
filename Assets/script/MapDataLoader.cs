using Newtonsoft.Json;
using System.IO;
using UnityEngine;


public class MapDataLoader
{
    public MapData Load(string path)
    {
        if(path.Contains(".json")==false)
        {
            path += ".json";
        }

        path = Path.Combine(Application.streamingAssetsPath, path);
        string dataAsJson = File.ReadAllText(path);
        MapData data = new MapData();
        data = JsonConvert.DeserializeObject<MapData>(dataAsJson);
        return data;
    }    
}
