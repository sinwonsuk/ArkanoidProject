using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    void InitPlayerData()
    {
        mFirstPosition = transform.position;
        mFirstScale = transform.localScale;
        mDeathLine = transform.position.y - mDeadLineGab;
        mStop = false;
        mPlayerCollision = new Collision(CollisionType.Player);
        mLasers = new List<GameObject>();
    }
    void Start()
    {
        InitPlayerData();      
    }

    void Update()
    {
        // �浹ü ���� 
        GetCollisionInfo();
        // ���� ����
        GameOver();
        // ���߱�
        if (Stop() ==true)
        {
            return;
        }
        // ���� 
        DiePlayer();
        // ������ ������
        InputLaser();
        // �̵� 
        MovePlayer();
    }
    void InputLaser()
    {
        if (Input.GetKeyDown(KeyCode.Space) && mlaserCheck == true)
        {
            SoundManager.GetInstance().SfxPlay(SoundManager.sfx.LaserHit, false);
            CreateLaser();
        }
    }
    bool Stop()
    {
        if (mStop == true)
            return true;
        else 
            return false;
    }
    public void IncreasePlayer(float size)
    {
        Vector3 scale = mFirstScale;
        scale.x *= size;   
        transform.localScale = scale;
    }
    public void DeleteAllLaser()
    {
        for (int i = 0; i < mLasers.Count; i++)
        {
            if (mLasers[i] != null)
            {
                Destroy(mLasers[i]);  // ������ ����
            }
        }

        mLasers.Clear();
    }
    public void CreateLaser()
    {
        // ������ ������ ����
        GameObject rightLaser = Instantiate(mLaser);
        Vector3 rightLaserPos = transform.position;
        rightLaserPos.x += mRightLaserPos.x;
        rightLaserPos.y += mRightLaserPos.y;
        rightLaser.transform.position = rightLaserPos;
        mLasers.Add(rightLaser);

        // ���� ������ ����
        GameObject leftLaser = Instantiate(mLaser);
        Vector3 leftLaserPos = transform.position;
        leftLaserPos.x += mLeftLaserPos.x;
        leftLaserPos.y += mLeftLaserPos.y;
        leftLaser.transform.position = leftLaserPos;
        mLasers.Add(leftLaser);
    }
    public void InitPlayer()
    {      
        transform.localScale = mFirstScale;
        mlaserCheck = false;
    }
    private void DiePlayer()
    {
        if (mDeathLine > mBall.transform.position.y)
        {
            SoundManager.GetInstance().SfxPlay(SoundManager.sfx.ballDeath, false);
            mLife -= 1;
            mEventOnDeath?.Invoke();
        }
    }
    private bool CheckBackGroundCollision()
    {
        // ���������� ���̰� Ŀ���� ���� changeScale�� �÷���  
        float changeScale = transform.localScale.x - mFirstScale.x;

        changeScale /= 2;

        float minX = mLeftSideWallLength + changeScale;
        float maxX = mRightSideWallLength - changeScale;

        if (transform.position.x < minX)
        {
            transform.position = new Vector3(minX, transform.position.y, transform.position.z);
            mHorizontalSpeed = 0;
            return false;
        }

        if (transform.position.x > maxX)
        {
            transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
            mHorizontalSpeed = 0;
            return false;
        }

        return true;
    }
    private void MovePlayer()
    {
        mHorizontalSpeed = mPlayerSpeed * Input.GetAxis("Horizontal");
        transform.position = new Vector3(transform.position.x + mHorizontalSpeed * Time.deltaTime, transform.position.y, 0);
        CheckBackGroundCollision();
    }

    private void GameOver()
    {
        if (mLife == 0)
        {
            SoundManager.GetInstance().All_Sfx_Stop();
            SoundManager.GetInstance().SfxPlay(SoundManager.sfx.GameOver, false);
            mEventGameOver?.Invoke();
        }
    }
    private void GetCollisionInfo()
    {
        Renderer renderer = gameObject.GetComponent<Renderer>();

        if (renderer != null && mPlayerCollision != null)
            mPlayerCollision.Rect(transform, renderer);
    }

    public event Action mEventOnDeath;
    public event Action mEventGameOver;
    public Collision mPlayerCollision { get; set; }
    public int mLife { get; set; } = 3;
    public bool mlaserCheck { get; set; } = false;
    public float mHorizontalSpeed { get; set; } = 0.0f;
    public bool mStop { get; set; }
    public float mPlayerSpeed { get; set; } = 5.0f;
    public Vector3 mFirstPosition { get; set; }

    [SerializeField]
    private GameObject mLaser;
    [SerializeField]
    private Ball mBall;

    private const float mLeftSideWallLength = -2.71f;
    private const float mRightSideWallLength = 2.77f;
    private Vector2 mLeftLaserPos = new Vector2(-0.8f, 0.5f);
    private Vector2 mRightLaserPos = new Vector2(0.8f, 0.5f);
    private Vector3 mFirstScale;
    private float mDeathLine = 0.0f;
    private float mDeadLineGab = 0.3f;
    private List<GameObject> mLasers;
}
