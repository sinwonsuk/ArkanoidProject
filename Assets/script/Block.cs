using UnityEngine;

public enum BlockImage
{
    Empty,
    Basic,
    Red,
    Green,
    Orange,
    Yellow,
    Pink,
    Gray,
    Blue,
    Gold,
}

public class Block : MonoBehaviour
{

    private void OnEnable()
    {
        mTimeCheck = false;
        mTime = 0;
    }
    void Start()
    {
        mBlockCollision = new Collision(CollisionType.Brock, gameObject);
    }
    private void FixedUpdate()
    {
        //  블록 충돌 늦추기
        CooldownBlockCollision();
    }
    // Update is called once per frame
    void Update()
    {
        //mBlockCollision.IsActive();      
        //블록 죽음
        DeathBlock();
        // Empty블록 이미지가 true라면 리턴
        if (IsCheckImageEmpty() == true)
            return;   
        // 충돌체 정보 얻기
        GetCollisionInfo();
    }
    public void DownHp()
    {
        if (mTimeCheck == false)
        {
            mBlockHp -= 1;
            mTimeCheck = true;
        }
    }
    public void CreateRandomItem()
    {
        int check = Random.Range(0, 4);
        int item_Drop = Random.Range(0, 4);

        if (item_Drop == 0)
        {
            mItemPrefab = Instantiate(mItems[check]);
            mItemPrefab.transform.position = transform.position;
        }
    }
    public void SetTileImage(BlockImage blcokImage)
    {
        mBlcokImage = blcokImage;

        if (blcokImage == BlockImage.Empty)
            return;

        if (blcokImage == BlockImage.Gray)
        {
            UpdateBlockImage(blcokImage);
            mBlockHp = 2;
            return;
        }
        if (blcokImage == BlockImage.Gold)
        {
            UpdateBlockImage(blcokImage);
            mBlockHp = -1;
            return;
        }
        else
        {
            UpdateBlockImage(blcokImage);
            mBlockHp = 1;
        }
    }
    private void DeathBlock()
    {
        if (mBlockHp == 0)
        {
            CreateRandomItem();
            gameObject.SetActive(false);
        }
    }
    private bool IsCheckImageEmpty()
    {
        if (mBlcokImage == BlockImage.Empty)
        {
            return true;
        }
        return false;
    }
    private void UpdateBlockImage(BlockImage blcokImage)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = mBlockSprites[(int)blcokImage];
    }
    private void CooldownBlockCollision()
    {
        if (mTimeCheck == true)
        {
            mTime += Time.deltaTime;
            if (mTime > 1)
            {
                mTime = 0;
                mTimeCheck = false;
            }
        }
    }
    private void GetCollisionInfo()
    {
        Renderer renderer = gameObject.GetComponent<Renderer>();

        if (renderer != null && mBlockCollision != null)
            mBlockCollision.Rect(transform, renderer);
    }
    public GameObject mItemPrefab { get; set; }
    public int mFirstHp { get; set; }
    public int mBlockHp { get; set; }= 99;
    public BlockImage GetBlockImage() { return mBlcokImage; }

    [SerializeField]
    private GameObject[] mItems;
    [SerializeField]
    private Sprite[] mBlockSprites;
    [SerializeField]
    private BlockImage mBlcokImage;
    private Collision mBlockCollision;
    private float mTime = 0.0f;
    private bool mTimeCheck = false;   
}
