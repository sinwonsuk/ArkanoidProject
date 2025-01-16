using NUnit.Framework.Internal;
using System;
using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;



public class Ball : MonoBehaviour
{
    private void OnEnable()
    {
        GetCollisionInfo();
    }
    private void Awake()
    {
        mDir = new Vector3(0, 5.0f, 0);
        mBallFirstPosition = transform.position;
    }
   void Start()
    {
        collision = new Collision(CollisionType.Ball);
    }

    void FixedUpdate()
    {
        // ��ϰ� ���� �浹 üũ
        CheckBlockBallCollision();
        // �ٱ����� ���� �浹 üũ
        CheckBackGroundCollision();      
        // �� ������
        MoveBall();
    }
    void Update()
    {
        // ���� ������
        if (StopBall() == true)
            return;
        // �ø��� ���� ��������
        GetCollisionInfo();
        // �÷��̾�� ���� �浹 üũ
        CheckPlayerBallCollision();
    }
    public void ReduceBallDegree(float size)
    {
        mBallDegree *= 0.66f;
    }
    public void GetRandomDir()
    {
        float randomInt = UnityEngine.Random.Range(-0.8f, 0.8f);

        mDir = new Vector3(randomInt, 5.0f, 0);
    }
    public void InitBall()
    {
        mBallDegree = 60.0f;
        mBallSpeed = 1.5f;
        mPlayerSpeed = 0.0f;
        mItemCatch = false;
    }
    public void ResetBallPosition()
    {
        mBallStop = true;
        GetRandomDir();
        transform.position = mBallFirstPosition;
        GetCollisionInfo();
    }
    public void SlowItem(float decrease)
    {
        float speed = mBallSpeed;
        speed *= decrease;
        mBallSpeed = speed;
    }
    public void GetPlayerSpeed()
    {
        mPlayerSpeed = mPlayerTransform.gameObject.GetComponent<Player>().mHorizontalSpeed;
    }
    public void GetCollisionInfo()
    {
        Renderer renderer = gameObject.GetComponent<Renderer>();

        if (renderer != null && collision != null)
            collision.Circle(transform, renderer);
    }
    private void RotateBall(ref Vector3 v, float angleDegrees)
    {
        float angleRadians = -angleDegrees * Mathf.Deg2Rad;
        float cos = Mathf.Cos(angleRadians);
        float sin = Mathf.Sin(angleRadians);

        float x = v.x * cos - v.y * sin;
        float y = v.x * sin + v.y * cos;

        v.x = x;
        v.y = y;
    }
    private void CheckBlockBallCollision()
    {
        string collisionWallname = "";
        GameObject collision =null;

        if (CollisionManager.GetInstance().CheckAllCollisions(CollisionType.Ball,CollisionType.Brock, ref collisionWallname, ref collision) ==true)
        {
            Block block = collision.GetComponent<Block>();

            if(block != null)
            {
                block.DownHp();
                CheckEdge(collisionWallname);
            }
        }
    }
    private void CheckEdge(string collisionWallname)
    {     
        // �𼭸� �浹 
        if (collisionWallname == "LeftTopCorner" ||
            collisionWallname == "RightTopCorner" || 
            collisionWallname == "LeftBottomCorner" || 
            collisionWallname == "RightBottomCorner")
        {
            if (mDir.x >= 0 && mDir.y > 0)
                mDir.y *= -1;
            else if (mDir.x <= 0 && mDir.y > 0)
                mDir.y *= -1;
            else if (mDir.y <= 0 && mDir.x > 0)
                mDir.x *= -1;
            else if (mDir.y <= 0 && mDir.x < 0)
                mDir.x *= -1;
        }
        // ���浹
        else if (collisionWallname == "Left")
        {
            if (mDir.x > 0)
                mDir.x *= -1;   
        }
        else if (collisionWallname == "Right")
        {
            if (mDir.x < 0)
                mDir.x *= -1;
        }
        else if (collisionWallname == "Bottom")
        {
            if (mDir.y > 0)
                mDir.y *= -1;
        }
        else if (collisionWallname == "Top")
        {
            if (mDir.y < 0)
                mDir.y *= -1;
        } 
    }

    private void InitDir()
    {
        mDir.x = 0;
        mDir.y = -5.0f;
        mDir.y *= -1;
    }
    private void InitItemCatchDir()
    {
        mDir.y = 0;
        mDir.x = 0;
        mDir.y *= -1;
    }
    private bool CheckPlayerBallCollision()
    {
        string collisionWallname ="";

        Collision playerCollision = mPlayerTransform.gameObject.GetComponent<Player>().mPlayerCollision;

       if (CollisionManager.GetInstance().IsCircleCollisionWithEdge(collision, playerCollision, ref collisionWallname))
       {
            
            float posxWidth = transform.position.x - mPlayerTransform.position.x;

            // ��� �������� ���� �ʾҴٸ� 
            if (mItemCatch == false)
            {
                //�ʱ�ȭ
                InitDir();

                // �� ȸ�� 
                RotateBall(ref mDir, mBallDegree * posxWidth);
            }

            //��� �������� �Ծ��ٸ� 
            else
            {
                // �ʱ�ȭ 
                InitItemCatchDir();
                GetPlayerSpeed();

                // �÷��̾� �ӵ���ŭ ���� ���� �̵� 
                transform.position = new Vector3(transform.position.x + mPlayerSpeed * Time.deltaTime, mBallFirstPosition.y);

                // �ٽ� �߻�
                if (Input.GetKey(KeyCode.Space))
                {
                    InitDir();
                    RotateBall(ref mDir, mBallDegree * posxWidth);
                }
            }           
            return true;
       }
        return false;
    }

    private void CheckBackGroundCollision()
    {
        if (transform.position.y > mUpSideWallLength)
        {
            transform.position = new Vector3(transform.position.x, mUpSideWallLength, transform.position.z); 
            mDir.y *= -1;
        }

        else if (transform.position.x > mRightSideWallLength)
        {
            transform.position = new Vector3(mRightSideWallLength, transform.position.y, transform.position.z); 
            mDir.x *= -1;
        }

        else if (transform.position.x < mLeftSideWallLength)
        {
            transform.position = new Vector3(mLeftSideWallLength, transform.position.y, transform.position.z);
            mDir.x *= -1;
        }
    }

    private void MoveBall()
    {
        transform.position += mBallSpeed * Time.deltaTime * mDir;
    }
    private bool StopBall()
    {
        if (mBallStop == true)
            return true;
        else
            return false;
    }
    public Collision collision { get; set; }
    public float mPlayerSpeed {  get; set; }
    public bool mBallStop { get; set; } = false;
    public float mBallSpeed { get; set; } = 1.5f;
    public Vector3 mBallFirstPosition { get; set; }
    public bool mItemCatch { get; set; } = false;
    public float mBallDegree { get; set; } = 60.0f;

    public Transform mPlayerTransform;

    private Vector3 mDir;

    private const float mLeftSideWallLength = -3.5f;
    private const float mRightSideWallLength = 3.5f;
    private const float mUpSideWallLength = 4.15f;


}
