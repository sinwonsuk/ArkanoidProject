using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class Map : StageState
{
    private void OnEnable()
    {
        mStageWaitingTime = 0;
        InitBlock(true);
    }
    private void OnDisable()
    {
        InitBlock(false);
    }
    private void Awake()
    {
        Initialize();
        InitializeMapData();
        CreateMap();      
    }
    public override void Start()
    {
       
    }
    public override void Update()
    {
        //스테이지 초기화
        InitStage();
        // 블록체크
        BlockCheck();
    }
    public void CreateMap()
    {
        // map툴로 만든거 가져오기
        MapData data = mMapLoader.Load(mMapName);

        //  맵생성 
        for (int y = 0; y < mScaleY; y++)
        {
            for (int x = 0; x < mScaleX; x++)
            {
                int blocktype = data.mapData[(y * mScaleX) + x];

                if (blocktype == 0)
                {
                    continue;
                }

                GameObject blockObject = InstantiateBlock(x, y);

                Block tempblock = blockObject.GetComponent<Block>();

                if (tempblock != null)
                {
                    tempblock.SetTileImage((BlockImage)blocktype);
                    tempblock.mFirstHp = tempblock.mBlockHp;
                    mBlockList.Add(tempblock);
                }
            }

        }
    }

    private void GoNextStage()
    {
        SoundManager.GetInstance().SfxPlay(SoundManager.sfx.NextStage, false);
        DeleteAllItem();
        mPlayer.DeleteAllLaser();
        mBall.ResetBallPosition();
        mStageText.gameObject.SetActive(true);
        ChangeStage();
        gameObject.SetActive(false);
       
    }
    private bool CheckNullBlock()
    {
        bool allNull = true;

        for (int i = 0; i < mBlockList.Count; i++)
        {
            if (mBlockList[i].mBlockHp > 0)
            {
                allNull = false;
            }
        }
        if(allNull == true)
            return true;
        else
            return false;
    }

    private void BlockCheck()
    {
        bool allNull = CheckNullBlock();
   
        if (allNull == true)
        {
            GoNextStage();
        }
    }
    private void InitBlock(bool check)
    {
        for (int i = 0; i < mBlockList.Count; i++)
        {
            mBlockList[i].gameObject.SetActive(check);
            mBlockList[i].mBlockHp = mBlockList[i].mFirstHp;
        }
    }
    private GameObject InstantiateBlock(int x, int y)
    {
        Vector3 position = new Vector3(mBlcokMove * x, -y * mBlcokMove, 0);
        GameObject instantiate = Instantiate(mBlock, transform);
        instantiate.transform.localScale = Vector3.one;
        instantiate.transform.localPosition = position;

        return instantiate;
    }
    private void InitializeMapData()
    {
        mMapLoader = new MapDataLoader();
        mBlockList = new List<Block>();
    }
    public List<Block> mBlockList { get; set; }

    [SerializeField]
    private GameObject mBlock;
    [SerializeField]
    private string mMapName;
    private MapDataLoader mMapLoader;
    private const int mScaleX = 9;
    private const int mScaleY = 15;
    private const float mBlcokMove = 2.86f;
   
}
