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
        // �״� �ð�
        CheckDieTime();
        // ���� ���� ���� 
        DeleteAlItem();
        // ������ �̵� 
        MoveItem();
        // �÷��̾�� �浹�� ó�� ���� 
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
