using UnityEngine;

public class ItemBlueEnlarge : Items
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        base.Start();
        mItemType = (ItemType)mItemDatas.mItemBlueEnlarge.type;
        mSize = mItemDatas.mItemBlueEnlarge.size;
    }
    public override void Update() 
    {
        base.Update();
        // 죽는 시간
        CheckDieTime();
        // 강제 전부 삭제 
        DeleteAlItem();
        // 아이템 이동 
        MoveItem();
        // 플레이어와 충돌후 처리 과정 
        ProcessPlayerCollision();
    }
    void ProcessPlayerCollision()
    {
        if (CollideWithPlayer() == true)
        {
            CollisionManager.GetInstance().CollisionDictory[CollisionType.Brock].Remove(mItemCollision);
            initItem();
            mPlayer.IncreasePlayer(mSize);
            mBall.ReduceBallDegree(mSize);
            Destroy(gameObject);
        }
    }

    private float mSize;
}
