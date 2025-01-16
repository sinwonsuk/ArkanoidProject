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
        // �״� �ð�
        CheckDieTime();
        // ���� ���� ���� 
        DeleteAlItem();
        // ������ �̵� 
        MoveItem();
        // �÷��̾�� �浹�� ó�� ����
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
