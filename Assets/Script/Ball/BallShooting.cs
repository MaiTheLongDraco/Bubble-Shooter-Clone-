using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShooting : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 direction;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ShowShootDirection();
    }
    private void ShowShootDirection()
    {
        var firstPos = GetInput.GetMouseInput();
        if (!IsPosValid(firstPos)) return;
        direction = firstPos - transform.position;
        direction.z = 0;
        Debug.DrawRay(transform.position, direction, Color.green);
        GetInput.CheckHit(transform.position, direction);
    }

    private bool IsPosValid(Vector3 firstPos)
    {
        if (Vector3.Distance(firstPos, transform.position) < 0.5f)
        {
            return false;
        }
        else return true;
    }
}
