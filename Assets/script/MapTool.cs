using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static Unity.Collections.AllocatorManager;

public class MapTool : MonoBehaviour
{
    private void Awake()
    {
        InitializeMapToolDate();
    }
    void Start()
    {
        CreateMap();
    } 
    public MapData GetMapData()
    {
        for (int i = 0; i < mBlockList.Count; i++)
        {        
            mMapData.mapData[i] = (int)mBlockList[i].GetBlockImage();
        }
        return mMapData;
    }
    private void CreateMap()
    {
        for (int y = 0; y < mScaleY; y++)
        {
            for (int x = 0; x < mScaleX; x++)
            {
                Vector3 position = new Vector3(mBlcokMove * x, -y * mBlcokMove, 0);

                GameObject instantiate = Instantiate(mBlock, transform);
                instantiate.transform.localScale = Vector3.one;
                instantiate.transform.localPosition = position;

                Block tempblock = instantiate.GetComponent<Block>();
                if (tempblock != null)
                    mBlockList.Add(tempblock);
            }
        }
        mMapData.mapData = new int[mBlockList.Count];
    }
    private void InitializeMapToolDate()
    {
        mMapData = new MapData();
        mBlockList = new List<Block>();
    }
    public List<Block> mBlockList { private set; get; }

    [SerializeField]
    private GameObject mBlock;
    private MapData mMapData;
    private const int mScaleX = 9;
    private const int mScaleY = 15;
    private const float mBlcokMove = 2.86f;
}
