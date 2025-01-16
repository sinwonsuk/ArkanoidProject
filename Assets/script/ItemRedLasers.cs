using UnityEngine;

public class ItemRedLasers : Items
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        base.Start();
        mItemType = (ItemType)mItemDatas.mItemRedLasers.type;
    }

    // Update is called once per frame
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

    private void ProcessPlayerCollision()
    {
        if (CollideWithPlayer() == true)
        {
            CollisionManager.GetInstance().CollisionDictory[CollisionType.Brock].Remove(mItemCollision);
            initItem();
            mPlayer.mlaserCheck = true;
            Destroy(gameObject);
        }
    }
}
