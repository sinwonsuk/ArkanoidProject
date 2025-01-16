using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public enum eStage
{
    Title,
    Stage1,
    Stage2,
    Stage3,
    Stage4,
    Stage5,
    Ending,
    GameOver,
}
public class StageManager : MonoBehaviour
{
    void Start()
    {
        GetStageInfo();
    }
    private void GetStageInfo()
    {
        for (int i = 0; i < mStages.Count; i++)
        {
            mStages[i].mStateChange += StageManager_StateChange;
            mStages[i].mItemManager += StageManager_ItemManager;
        }
        mPlayer.mEventGameOver += HandleGameOver;
    }  
    private void StageManager_ItemManager(eStage estage)
    {
        mItemAlldelete = mStages[(int)estage].mItemDelete;
    }
    private void StageManager_StateChange(eStage estage)
    {
        mItemAlldelete = true;
        mStage = estage + mNextIndex;
        mStages[(int)estage + mNextIndex].gameObject.SetActive(true);
        mStages[(int)estage + mNextIndex].mStage = (int)estage + mNextIndex; 
    } 
    private void HandleGameOver()
    {
        for (int i = 0; i < mStages.Count; i++)
        {
            mStages[i].gameObject.SetActive(false);

            if((eStage)i == eStage.GameOver)
            {
                mStages[i].gameObject.SetActive(true);
            }
        }
    }
    public bool mItemAlldelete { get; set; } = false;
    public eStage mStage { get; set; }
    [SerializeField]
    private List<StageState> mStages = new List<StageState>();
    [SerializeField]
    private Player mPlayer; 
    private const int mNextIndex = 1;
}
