using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class BallHolderManger : MonoBehaviour
{
    public static BallHolderManger Instance;
    [SerializeField] private List<GameObject> listBall;
    [SerializeField] private List<GameObject> listMatrixBall;
    [SerializeField] private List<Transform> listShootingPos;
    [SerializeField] private CircularList<BallShooting> ballShootings;
    [SerializeField] private float targetVelocity;
    [SerializeField] private LineReflection lineReflection;
    [SerializeField] private BoardManager boardManager;
    [SerializeField] private bool canShoot;
    [SerializeField] private Transform ballParent;
    [SerializeField] private Transform ballAddParent;
    [SerializeField] private Transform loadBallThreshHold;
    [SerializeField]
    private GameCOntroller gameCOntroller => GameCOntroller.Instance;
    private CheckSameType checkSameType => CheckSameType.Instance;

    private Vector2 mainPos;
    private PredictBallPosToAdd predictBall;
    public Transform BallParent { get => ballParent; set => ballParent = value; }
    public bool CanShoot { get => canShoot; set => canShoot = value; }
    [SerializeField] private UnityEvent onWinGame;

    private void Start()
    {
        boardManager = FindObjectOfType<BoardManager>();
        mainPos = listShootingPos[0].position;
        predictBall = PredictBallPosToAdd.Instance;
        lineReflection = FindObjectOfType<LineReflection>();
        ballShootings = new CircularList<BallShooting>();
        CreateFirstTurnBall();
    }
    private void Update()
    {
        HandleShoot();
        LoadNextBallIfOverThreshold();
    }
    private void HandleShoot()
    {
        if (canShoot)
        {
            ShootBall();
        }
    }
    public void SetCanShoot(bool set)
    {
        canShoot = set;
        print($"canShoot {canShoot}");
    }
    private void CreateFirstTurnBall()
    {
        for (int i = 0; i < 2; i++)
        {
            var rand = Random.Range(0, listBall.Count - 1);
            var ball = Instantiate(listBall[rand], listShootingPos[i].position, Quaternion.identity);
            ballShootings.Add(ball.GetComponent<BallShooting>());
        }
    }
    private void AddNewShootingBall()
    {
        if (ballShootings.Count <= 1)
        {
            var rand = Random.Range(0, listBall.Count);
            var ball = Instantiate(listBall[rand], listShootingPos[1].position, Quaternion.identity);
            ballShootings.Add(ball.GetComponent<BallShooting>());
        }
    }
    private void ShootBall()
    {
        if (!Input.GetMouseButtonUp(0)) return;
        ballShootings.GetCurrent().SetBallVelocity(targetVelocity);
        ballShootings.GetCurrent().ShowShootDirection(lineReflection.PassDir);
        print($" ballShootings.GetCurrent() {ballShootings.GetCurrent().name}");
        gameCOntroller.SetInteractSwapBtn(false);
        ballShootings.GetCurrent().AddListenerFotHitEvent(() => CreateNewMatrixBall());
        ballShootings.GetCurrent().AddListenerFotHitEvent(() => LoadNextBall());
    }
    private void HandleAddBallToSameType(MatrixBall toAdd)
    {
        {
            CheckSameType.Instance.AddNewBall(toAdd);
        }
        MakeBallExplode(CheckSameType.Instance.SameTypeBalls);
    }
    private void MakeBallExplode(List<MatrixBall> listExplode)
    {
        // GroupHolder.Instance.TestList.Clear();
        var sameTypeCount = listExplode.Count;
        if (sameTypeCount >= 3)
        {
            boardManager.ListMatrixBall.RemoveAll(item => listExplode.Contains(item));
            fallingBallGroup.GroupAfterShoot();
            fallingBallGroup.MakeBallFall();
            listExplode.ForEach(b => Destroy(b.gameObject, 0.5f));
            listExplode.Clear();
            // boardManager.ListMatrixBall.Clear();
            if (boardManager.IsBoardEmpty())
            {
                onWinGame?.Invoke();
                print("win game");
            }
        }

    }
    private FallingBallGroup fallingBallGroup => FallingBallGroup.Instance;
    private void CreateNewMatrixBall()
    {
        DestroyCurrentShootBall();
        GameObject matrixBall = AddRequireComponent();
        boardManager.ListMatrixBall.Add(matrixBall.GetComponent<MatrixBall>());
        checkSameType.CheckSameTypeAround(matrixBall.GetComponent<MatrixBall>());
        HandleAddBallToSameType(matrixBall.GetComponent<MatrixBall>());
        gameCOntroller.DecreaseMovesLeft();
        gameCOntroller.SetInteractSwapBtn(true);
        matrixBall.transform.SetParent(ballAddParent);
    }
    private void LoadNextBallIfOverThreshold()
    {
        var isOverThreshold = ballShootings.GetCurrent().transform.position.y <= loadBallThreshHold.position.y;
        if (isOverThreshold)
        {
            LoadNextBall();
            gameCOntroller.SetInteractSwapBtn(true);
        }
    }
    private GameObject AddRequireComponent()
    {
        var matrixBall = HandleCreateType(ballShootings.GetCurrent().GetMatrixBall());
        matrixBall.AddComponent<CircleCollider2D>();
        matrixBall.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        matrixBall.GetComponent<MatrixBall>().index = predictBall.TargetID;
        return matrixBall;
    }

    private GameObject HandleCreateType(MatrixBall matrixBall)
    {
        GameObject newBall = new GameObject();
        switch (matrixBall.matrixBallType)
        {
            case MatrixBallType.BALL_01:
                {
                    newBall = Instantiate(listMatrixBall[0], predictBall.DesireBallPos, Quaternion.identity);
                }
                break;
            case MatrixBallType.BALL_02:
                {
                    newBall = Instantiate(listMatrixBall[1], predictBall.DesireBallPos, Quaternion.identity);
                }
                break;
            case MatrixBallType.BALL_03:
                {
                    newBall = Instantiate(listMatrixBall[2], predictBall.DesireBallPos, Quaternion.identity);
                }
                break;
            case MatrixBallType.BALL_04:
                {
                    newBall = Instantiate(listMatrixBall[3], predictBall.DesireBallPos, Quaternion.identity);
                }
                break;
            case MatrixBallType.BALL_05:
                {
                    newBall = Instantiate(listMatrixBall[4], predictBall.DesireBallPos, Quaternion.identity);
                }
                break;
        }
        return newBall;
    }
    private void LoadNextBall()
    {
        print("ball come to tarrget poss");
        ballShootings.Remove(ballShootings.GetCurrent());
        ballShootings.MoveNext();
        ballShootings.GetCurrent().transform.position = mainPos;
        AddNewShootingBall();
    }
    private void DestroyCurrentShootBall()
    {
        Destroy(ballShootings.GetCurrent().gameObject);
    }
    public void SwapBall()
    {
        var i = ballShootings.Index;
        print($"index of shooting ball {i}");
        ballShootings.MoveNext();
        var temp = ballShootings.GetCurrent().transform.position;
        ballShootings.GetCurrent().transform.position = ballShootings.GetNext().transform.position;
        ballShootings.GetNext().transform.position = temp;
    }
}
public enum BallHolderType
{
    MAINBALL,
    MINORBALL,
    NONE
}

