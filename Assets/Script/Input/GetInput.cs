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

    public static bool IsMousePress()
    {
        if (Input.GetMouseButton(0)) return true;
        else return false;
    }

}
