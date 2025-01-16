using Unity.VisualScripting;
using UnityEngine;

public class Laser : MonoBehaviour
{
   
    

    void Start()
    {
        Init();
    }
   
    void Update()
    {
        // 레이져 전체 삭제
        IsDeleteAllLaser();
        // 죽음 체크
        CheckDeath();
        //블록 데미지 체크
        CheckAndDamageBlock();
    }
    private void IsDeleteAllLaser()
    {
        if (mStageManager.mItemAlldelete == true)
        {
            Destroy(gameObject);
        }
    }
    private void CheckDeath()
    {
        if (transform.position.y > mLaserDeadLine)
        {
            Destroy(gameObject);
        }
    }
    private void CheckAndDamageBlock()
    {
        transform.Translate(Vector2.up * mLaserSpeed * Time.deltaTime);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, mRayDistance);

        if (hit.collider != null)
        {
            Block block = hit.transform.GetComponent<Block>();

            if (block != null)
            {
                block.mBlockHp -= 1;
                Destroy(gameObject);
            }
        }
    }
    private void Init()
    {
        GameObject _stageManager = GameObject.Find("StageManager");
        mStageManager = _stageManager.GetComponent<StageManager>();
    }
    private const float mRayDistance = 0.2f;
    private const float mLaserSpeed = 6.0f;
    private const float mLaserDeadLine = 4.1f;
    private StageManager mStageManager;
}
