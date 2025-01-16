using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class ToolInputController : MonoBehaviour
{
    void Update()
    {
        RayCastUI();
    }
    public void SetBlockImage(int check)
    {
        blockImage = (BlockImage)check;
    }
    void RayCastUI()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                Block block = hit.transform.GetComponent<Block>();

                if (block != null)
                {
                    block.SetTileImage(blockImage);
                }
            }
        }
    }
    public Camera cam;
    private BlockImage blockImage = BlockImage.Basic;
}
