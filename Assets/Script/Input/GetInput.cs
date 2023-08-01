using UnityEngine;

public class GetInput : MonoBehaviour
{
    public static Vector3 firstPos;
    public static Vector3 GetMouseInput()
    {
        if (IsMousePress())
        {
            var mousePos = Input.mousePosition;
            mousePos.z = 0;
            firstPos = Camera.main.ScreenToWorldPoint(mousePos);
        }
        return firstPos;
    }
    public static void CheckHit(Vector3 ballPos, Vector3 direction)
    {
        if (IsMousePress())
        {
            RaycastHit2D hit2D = Physics2D.Raycast(ballPos, direction, Mathf.Infinity);
            if (Physics2D.Raycast(ballPos, direction, Mathf.Infinity))
            {
                if (hit2D.collider.CompareTag("Ball"))
                {
                    Utilities.Log($"hit ball {hit2D.collider.name}");
                }
                else
                {
                    Utilities.Log($"hit other {hit2D.collider.name}");
                }
            }
            else Utilities.Log("doesnt hit");
        }
    }
    public static bool IsMousePress()
    {
        if (Input.GetMouseButton(0)) return true;
        else return false;
    }

}
