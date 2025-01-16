using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using TMPro;
using UnityEngine.Tilemaps;

public class MapDataSave : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputField.text = "NoName";
    }

    public void save()
    {
        MapData mapData = mapTool.GetMapData();
        string fileName = inputField.text;

        if (fileName.Contains(".json") == false)
            fileName += ".json";

        fileName = Path.Combine(Application.streamingAssetsPath, fileName);
        string toJson = JsonConvert.SerializeObject(mapData,Formatting.Indented);
        File.WriteAllText(fileName, toJson);
    }

    [SerializeField]
    private TMP_InputField inputField;
    [SerializeField]
    private MapTool mapTool;

}
