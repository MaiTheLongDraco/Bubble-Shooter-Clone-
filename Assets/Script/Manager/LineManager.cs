
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LineManager : MonoBehaviour
{
    #region Open for extension
    public DirectionLine DirectionLine { get => directionLine; set => directionLine = value; }
    public static LineManager Instance { get => lineManager; set => lineManager = value; }
    public List<Vector2> LinePoints { get => linePoints; set => linePoints = value; }
    #endregion
    #region  Close For modification
    private static LineManager lineManager;
    [SerializeField] private DirectionLine directionLine;
    private List<Vector2> linePoints = new List<Vector2>();
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        DirectionLine = GetComponentInChildren<DirectionLine>();
        //SetOriginForLine(BallHolderManger.Instance.MainBallShooting.transform.position);
    }
    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        DrawLine();
    }
    private void DrawLine()
    {
        if (GetInput.IsMousePress())
        {
            for (int i = 0; i < LinePoints.Count; i++)
            {
                AddNewPointToLine(LinePoints[i]);
            }
        }
        else
        {
            DirectionLine.ResetLine();
        }
    }

    public void SetOriginForLine(Vector3 originalPos)
    {
        AddNewPointToLine(originalPos);
        DirectionLine.SetPosForFirstPoint(originalPos);
    }
    public void AddNewPointToLine(Vector3 newPoint)
    {
        DirectionLine.AddNewPointToLine(newPoint);
    }
}
