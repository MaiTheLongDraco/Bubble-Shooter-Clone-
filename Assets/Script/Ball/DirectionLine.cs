using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionLine : MonoBehaviour
{
    [SerializeField] private LineRenderer line;

    public LineRenderer Line { get => line; set => line = value; }

    // Start is called before the first frame update
    void Start()
    {
        Line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ResetLine()
    {
        line.positionCount = 0;
    }
    public void AddNewPointToLine(Vector3 newPoint)
    {
        Line.positionCount++;
        Line.SetPosition(Line.positionCount - 1, newPoint);
    }
    public void RemovPointFromLine(int num)
    {
        Line.positionCount -= num;
    }
    public void SetPosForFirstPoint(Vector3 pos)
    {
        Line.SetPosition(0, pos);
    }
    public void SetPosForLastPoint(Vector3 pos)
    {
        Line.SetPosition(Line.positionCount - 1, pos);
    }

}
