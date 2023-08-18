using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineHandle : MonoBehaviour
{
    [SerializeField] private LineRenderer line;

    public LineRenderer Line { get => line; set => line = value; }

    private void Start()
    {
        ResetLine();

    }
    public void ResetLine()
    {
        line.positionCount = 0;
    }
    public void AddNewPoint(Vector2 origin, Vector2 newPoint)
    {
        if (IsEmpty())
            AddNewPoint(origin);
        AddNewPoint(newPoint);
    }
    private void AddNewPoint(Vector2 next)
    {
        line.positionCount++;
        line.SetPosition(line.positionCount - 1, next);
    }

    private bool IsEmpty()
    {
        return line.positionCount == 0;
    }

}
