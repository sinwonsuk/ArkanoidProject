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
            mBall.mItemCatch = true;
            Destroy(gameObject);
        }
    }
}
