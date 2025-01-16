using UnityEngine;

public class Ending : StageState
{  
    private void OnEnable()
    {
        InitGame();
    }
    public void Awake()
    {
        Initialize();
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

        mPlayer.gameObject.SetActive(false);
        mBall.gameObject.SetActive(false);
        mStageText.gameObject.SetActive(false);

        SoundManager.GetInstance().All_Sfx_Stop();
        SoundManager.GetInstance().PlayBgm(SoundManager.bgm.Ending);
    }
    private void ReturnTitle()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SoundManager.GetInstance().PlayBgm(SoundManager.bgm.GameStart);
            mTitle.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
    [SerializeField]
    private Title mTitle;
}
