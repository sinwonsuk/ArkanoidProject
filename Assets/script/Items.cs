using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public class Items : MonoBehaviour
{   
    public enum ItemType
    {
        Empty,
        Lasers,
        Enlarge,
        Catch,
        Slow,
    }
    private void Awake()
    {
        Init();
    }

    public virtual void Start()
    {
        mItemCollision = new Collision(CollisionType.Item);
    }

    // Update is called once per frame
    public virtual void Update()
    {
        GetCollisionInfo();
    }
    public void DeleteItem()
    {
        initItem();
        CollisionManager.GetInstance().CollisionDictory[CollisionType.Brock].Remove(mItemCollision);
        Destroy(gameObject);      
    }
    public void DeleteAlItem()
    {
        if (mStageManager.mItemAlldelete == true)
        {
            CollisionManager.GetInstance().CollisionDictory[CollisionType.Brock].Remove(mItemCollision);
            Destroy(gameObject);
        }
    }
    public ItemDatas LoadJson(string path)
    {
        string fullPath = Path.Combine(Application.dataPath, "ItemData", path);
        if (File.Exists(fullPath))  // 파일이 존재하는지 확인
        {
            string dataAsJson = File.ReadAllText(fullPath);
            ItemDatas data = JsonConvert.DeserializeObject<ItemDatas>(dataAsJson);
            return data;
        }
        else
        {
            Debug.LogError("File not found: " + fullPath);
            return null;
        }
    }
    protected void CheckDieTime()
    {
        mTime += Time.deltaTime;

        if (mTime > 10.0f)
        {
            mTime = 0;
            Destroy(gameObject);
        }
    }
    protected void MoveItem()
    {
        transform.position += new Vector3(0, mSpeed * Time.deltaTime, 0);
    }   
    protected bool CollideWithPlayer()
    {
        if (CollisionManager.GetInstance().IsRectCollision(mItemCollision, mPlayer.mPlayerCollision)==true)
        {
            return true;
        }           
        else 
            return false;
    }   
    protected void initItem()
    {
        mPlayer.InitPlayer();
        mBall.InitBall();
    }
    private void GetCollisionInfo()
    {
        Renderer renderer = gameObject.GetComponent<Renderer>();

        if (renderer != null && mItemCollision != null)
            mItemCollision.Rect(transform, renderer);
    }

    private void Init()
    {
        mItemDatas = LoadJson("ItemJson.json");

        GameObject _player = GameObject.Find("Player");

        mPlayer = _player.GetComponent<Player>();

        GameObject _ball = GameObject.Find("Ball");

        mBall = _ball.GetComponent<Ball>();

        GameObject _mapManager = GameObject.Find("StageManager");

        mStageManager = _mapManager.GetComponent<StageManager>();
    }

    protected ItemDatas mItemDatas;
    protected Player mPlayer;
    protected Ball mBall;
    protected StageManager mStageManager;
    protected float mCollisionWidth = 1f;
    protected ItemType mItemType;
    protected Collision mItemCollision;

    private float mTime = 0.0f;
    private float mSpeed = -1.0f;

}
