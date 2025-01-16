using System.Drawing;
using UnityEngine;

public class ItemOrangeSlow : Items
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        base.Start();
        mItemType = (ItemType)mItemDatas.mItemOrangeSlow.type;
         mDecrease = mItemDatas.mItemOrangeSlow.decrease;
    }

    // Update is called once per frame
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
            mBall.SlowItem(mDecrease);
            Destroy(gameObject);
        }
    }

    private float mDecrease;
}
