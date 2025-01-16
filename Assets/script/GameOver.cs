using UnityEngine;

public class GameOver : StageState
{  
    public void Awake()
    {
        Initialize();
    }
    private void OnDisable()
    {
        InitGame();
    }
    public override void Update()
    {
        ReturnTitle();
    }
    private void InitGame()
    {
        mPlayer.mLife = 3;
        mPlayer.InitPlayer();
        mBall.InitBall();

        mStageText.gameObject.SetActive(false);
        mPlayer.gameObject.SetActive(false);
        mBall.gameObject.SetActive(false);
    }
    private void ReturnTitle()
    {
        mOverTime += Time.deltaTime;

        if (mOverTime > 2)
        {
            SoundManager.GetInstance().PlayBgm(SoundManager.bgm.GameStart);
            m_Title.gameObject.SetActive(true);
            mOverTime = 0;
            gameObject.SetActive(false);
        }
    }
    [SerializeField]
    private Title m_Title;

    private float mOverTime = 0;

}
