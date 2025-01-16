using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class StageState : MonoBehaviour
{
    public virtual void Start()
    {

    }
    public virtual void Update()
    {

    }
    public void Initialize()
    {
        GameObject PlayerObject = GameObject.Find("Player");
        mPlayer = PlayerObject.GetComponent<Player>();
        mPlayer.mEventOnDeath += Player_OnDeath;

        GameObject BallObject = GameObject.Find("Ball");
        mBall = BallObject.GetComponent<Ball>();
        GameObject StageTextObject = GameObject.Find("StageText");
     
        if(StageTextObject == null)
        {
            return;
        }

        mStageText = StageTextObject.GetComponent<TextMeshProUGUI>();
    }
    protected void ChangeStage()
    {
        mStateChange.Invoke((eStage)mStage);
    }
    protected void DeleteAllItem()
    {
        mItemDelete = true;
        mItemManager.Invoke((eStage)mStage);
    }
    protected void OnableItem()
    {
        mItemDelete = false;
        mItemManager.Invoke((eStage)mStage);
    }
    protected bool InitStage()
    {
        if (mStageWaitingTime > 3)
        {
            OnableItem();
            mPlayer.mStop = false;
            mBall.mBallStop = false;
            mStageText.gameObject.SetActive(false);
            return true;
        }
        // 플레이어 공 초기화
        mPlayer.InitPlayer();
        mBall.InitBall();

        // 시간진행 
        mStageWaitingTime += Time.deltaTime;

        // 스테이지 글자 
        mStageText.gameObject.SetActive(true);
        mStageText.text = "stage" + (int)mStage;

        // 플레이어 잠깐 멈추고 처음 포지션으로 
        mPlayer.mStop = true;
        mPlayer.transform.position = mPlayer.mFirstPosition;

        // 볼 잠깐 멈추고 랜덤 방향 처음 포지션으로 
        mBall.ResetBallPosition();

        return false;
    }

    private void Player_OnDeath()
    {
        mStageWaitingTime = 0;
        DeleteAllItem();
    }

    public event Action<eStage> mStateChange;
    public event Action<eStage> mItemManager;
    public int mStage { get; set; } = 0;
    public bool mItemDelete { get; set; }

    protected Player mPlayer;
    protected Ball mBall;
    protected TextMeshProUGUI mStageText;
    protected float mStageWaitingTime = 0.0f; 
}
