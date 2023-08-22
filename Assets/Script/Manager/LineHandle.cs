using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LineHandle : MonoBehaviour
{
    [SerializeField] private LineRenderer line;

    public LineRenderer Line { get => line; set => line = value; }
    private Vector3[] positions;
    [SerializeField] public List<Vector3> ListPoint = new List<Vector3>();
    public static LineHandle Instance;
    private void Start()
    {
        Instance = this;
        // ResetLine();
    }
    public void ResetLine()
    {
        line.positionCount = 0;
    }
    public void AddPointToList()
    {
        positions = new Vector3[Line.positionCount];
        line.GetPositions(positions);
        ListPoint.AddRange(positions.ToList());
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
    public void ResetListPoint()
    {
        ListPoint.Clear();
    }

}
