using System;
using TMPro;
using UnityEngine;
using static UnityEngine.CullingGroup;

public class Title : StageState
{
    private void OnEnable()
    {
        OnOffGameObjects(false);
    }
    public override void Start()
    {
        SoundManager.GetInstance().PlayBgm(SoundManager.bgm.GameStart);
    }
    public override void Update()
    {
        GameStart();
    }
    private void GameStart()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnOffGameObjects(true);
            ChangeStage();

            SoundManager.GetInstance().Bgm_Stop();
            SoundManager.GetInstance().SfxPlay(SoundManager.sfx.NextStage, false);
            gameObject.SetActive(false);
        }
    }
    private void OnOffGameObjects(bool check)
    {
        mplayer.gameObject.SetActive(check);
        mball.gameObject.SetActive(check);
        mstageText.gameObject.SetActive(check);
    }
    [SerializeField]
    private Player mplayer;
    [SerializeField]
    private Ball mball;
    [SerializeField]
    private TextMeshProUGUI mstageText;
}
