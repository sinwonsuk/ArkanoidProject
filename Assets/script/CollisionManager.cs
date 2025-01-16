using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static UnityEditor.FilePathAttribute;


public enum CollisionType
{
    None,
    Ball,
    Player,
    Brock,
    Item,
}
public class Collision
{
    public Collision(CollisionType _collisionType, GameObject gameObject = null)
    {
        mCollisionType = _collisionType;

        if (CollisionManager.GetInstance() != null)
        {
            CollisionManager.GetInstance().CollisionDictory[mCollisionType].Add(this);
            mCollisionBlock = gameObject;
        }
    }

    public void Rect(Transform transform, Renderer renderer)
    {
        if (renderer != null)
        {
            Vector3 size = renderer.bounds.size; // 오브젝트의 월드 공간 크기
            mLeft = transform.position.x - size.x / 2;
            mRight = transform.position.x + size.x / 2;
            mTop = transform.position.y + size.y / 2;
            mBottom = transform.position.y - size.y / 2;
        }
    }
    public void Circle(Transform transform, Renderer renderer)
    {
        if (renderer != null)
        {
            // 원의 중심
            mCenter = transform.position;
            // 반지름 계산 (bounds.extents.x 또는 y를 사용)
            mRadius = renderer.bounds.extents.x;
        }
    }
    public bool IsActive()
    {
        return mCollisionBlock != null && mCollisionBlock.activeInHierarchy;
    }
    public CollisionType mCollisionType;
    public float mLeft = 0;
    public float mTop = 0;
    public float mRight = 0;
    public float mBottom = 0;
    public float mRadius = 0;
    public Vector3 mCenter;
    public GameObject mCollisionBlock;
}

public class CollisionManager : MonoBehaviour
{
   
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        CollisionDictory.Add(CollisionType.Player, new List<Collision>());
        CollisionDictory.Add(CollisionType.Brock, new List<Collision>());
        CollisionDictory.Add(CollisionType.Ball, new List<Collision>());
        CollisionDictory.Add(CollisionType.Item, new List<Collision>());
    }
    // Update is called once per frame
    public static CollisionManager GetInstance()
    {
        return instance;
    }

    // 전체 충돌 구하기
    public bool CheckAllCollisions(CollisionType circle, CollisionType rect, ref string collisionlocation, ref GameObject collisionbrick)
    {
        List<Collision> circleList = CollisionDictory[circle];
        List<Collision> rectList = CollisionDictory[rect];

        for (int i = 0; i < circleList.Count; i++)
        {
            for (int j = 0; j < rectList.Count; j++)
            {
                if (IsCircleCollisionWithEdge(circleList[i], rectList[j], ref collisionlocation))
                {
                    if(rectList[j].mCollisionBlock.activeSelf ==false)
                        continue;

                    collisionbrick = rectList[j].mCollisionBlock;
                    return true;
                }
            }
        }
        return false;
    }

    public bool IsRectCollision(Collision rect1, Collision rect2)
    {
        // 정상적인 사각형인지 체크 (유효한 크기인지 확인)
        if (rect1.mLeft < rect1.mRight && rect1.mBottom < rect1.mTop && rect2.mLeft < rect2.mRight && rect2.mBottom < rect2.mTop)
        {
            // 두 사각형이 x축에서 겹치는지 확인
            bool isOverlappingX = rect1.mRight > rect2.mLeft && rect1.mLeft < rect2.mRight;

            // 두 사각형이 y축에서 겹치는지 확인
            bool isOverlappingY = rect1.mTop > rect2.mBottom && rect1.mBottom < rect2.mTop;
            // x와 y축 모두 겹치면 충돌
            if (isOverlappingX && isOverlappingY)
            {            
                return true; // 충돌 발생
            }
        }
        return false; // 충돌 없음
    }
    public bool IsCircleCollisionWithEdge(Collision circle, Collision rect, ref string collisionlocation)
    {
        // 정상적인 원, 정상적인 사각형인지 체크
        if (circle.mRadius > 0 && rect.mLeft < rect.mRight && rect.mBottom < rect.mTop)
        {
            // 사각형의 가장 가까운 점 계산
            float closestX = Mathf.Clamp(circle.mCenter.x, rect.mLeft, rect.mRight);
            float closestY = Mathf.Clamp(circle.mCenter.y, rect.mBottom, rect.mTop);

            // 원의 중심과 가장 가까운 점 사이의 거리 계산
            float distanceX = circle.mCenter.x - closestX;
            float distanceY = circle.mCenter.y - closestY;

            // 두 점 사이의 거리 제곱 계산
            float distanceSquared = (distanceX * distanceX) + (distanceY * distanceY);

            // 충돌 여부 확인
            if (distanceSquared <= (circle.mRadius * circle.mRadius))
            {
                // 가장 가까운 사각형의 점을 활용하여 구함

                // 모서리 충돌 판별
                if (closestX == rect.mLeft && closestY == rect.mTop)
                    collisionlocation = "LeftTopCorner";  
                else if (closestX == rect.mRight && closestY == rect.mTop)
                    collisionlocation = "RightTopCorner"; 
                else if (closestX == rect.mLeft && closestY == rect.mBottom)
                    collisionlocation = "LeftBottomCorner"; 
                else if (closestX == rect.mRight && closestY == rect.mBottom)
                    collisionlocation = "RightBottomCorner"; 
                // 벽 충돌 
                else if(closestX == rect.mLeft)
                    collisionlocation = "Left";
                else if (closestX == rect.mRight)
                    collisionlocation = "Right";
                else if (closestY == rect.mTop)
                    collisionlocation = "Top";
                else if (closestY == rect.mBottom)
                    collisionlocation = "Bottom";
                return true; // 충돌 발생
            }
        }
        return false; // 충돌 없음
    }
    
    public Dictionary<CollisionType, List<Collision>> CollisionDictory = new Dictionary<CollisionType, List<Collision>>();

    private static CollisionManager instance;
}
