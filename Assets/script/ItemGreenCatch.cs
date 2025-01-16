using Newtonsoft.Json;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using static ItemDatas;



public class ItemGreenCatch : Items
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        base.Start();
        mItemType = (ItemType)mItemDatas.mItemCatch.type;      
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
            mBall.mItemCatch = true;
            Destroy(gameObject);
        }
    }
}
