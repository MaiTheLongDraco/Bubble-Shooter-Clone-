using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class LineReflection : MonoBehaviour
{
    [SerializeField] private Transform mainPos;
    [SerializeField] private Vector2 distance;
    private string[] startLayerMask = { "UpLimit", "RightLimit", "LeftLimit", "DownLimit" };
    [SerializeField] private LineHandle lineHandle;
    [SerializeField] private bool isLineOn;
    public static LineReflection Instance;
    private Vector2 passDir;
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        SwitchOnOffLine();
        End();
    }
    private void SwitchOnOffLine()
    {
        if (isLineOn)
        {
            FollowMouseDir();
        }
        else
        {
            lineHandle.ResetLine();
        }
    }

    public void SetActiveLine(bool set)
    {
        isLineOn = set;
    }
    private void FollowMouseDir()
    {
        if (!Input.GetMouseButton(0)) return;
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        var direction = mousePos - mainPos.position;
        PassDir = direction;
        lineHandle.ResetLine();
        HitHandlle(mainPos.position, direction, LayerMask.GetMask(startLayerMask));
    }
    private void HitHandlle(Vector2 orrigin, Vector2 dirrection, LayerMask layerMask)
    {
        RaycastHit2D hit2D = Physics2D.Raycast(orrigin, dirrection);
        if (hit2D)
        {
            DrawLine(orrigin, hit2D.point);
            var newDir = Vector2.Reflect(dirrection, hit2D.normal);
            if (!IsMeetCondition(hit2D.collider.tag, hit2D.collider.gameObject.GetComponent<MatrixBall>(), hit2D)) return;
            HitHandlle(hit2D.point - distance / hit2D.point, newDir, layerMask);
        }
        else
        {
            DrawLine(orrigin, dirrection * 100);
        }
    }

    private BoardManager BoardManager => BoardManager.Instance;
    private PredictBallPosToAdd PredicPos => PredictBallPosToAdd.Instance;
    public Vector2 PassDir { get => passDir; set => passDir = value; }

    private bool IsMeetCondition(string tag, MatrixBall matrixBall, RaycastHit2D hit2D)
    {
        if (tag == "Ball")
        {
            print($" index {matrixBall.index}");
            var haveLeft = BoardManager.IsListMatrixContainItem(matrixBall.GetAroundItem(0, -1));
            var haveRight = BoardManager.IsListMatrixContainItem(matrixBall.GetAroundItem(0, 1));
            CheckSameType.Instance.CheckSameTypeAround(matrixBall);

            print($"haveLeft __ {haveLeft}");
            print($"haveRight __ {haveRight}");
            print($"ball {matrixBall.index} __ hit2d.point {hit2D.point}__ ball tranform {hit2D.transform.position}");
            PredicPos.HandlePosToAddBall(haveLeft, haveRight, hit2D, matrixBall);
            return false;
        }

        else return true;
    }
    private void DrawLine(Vector2 origin, Vector2 next)
    {
        // Debug.DrawLine(orrigin, next);
        lineHandle.AddNewPoint(origin, next);
    }
    private void End()
    {
        if (Input.GetMouseButtonUp(0))
        {
            lineHandle.AddPointToList();
            lineHandle.ResetLine();
        }
    }
}
