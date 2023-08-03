
using UnityEngine;

public class LineManager : MonoBehaviour
{
    private static LineManager lineManager;
    public static LineManager Instance { get => lineManager; set => lineManager = value; }

    [SerializeField] private DirectionLine directionLine;
    public DirectionLine DirectionLine { get => directionLine; set => directionLine = value; }

    // Start is called before the first frame update
    void Start()
    {
        DirectionLine = GetComponentInChildren<DirectionLine>();
        //        SetOriginForLine(BallHolderManger.Instance.MainBallShooting.transform.position);
    }
    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {

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
