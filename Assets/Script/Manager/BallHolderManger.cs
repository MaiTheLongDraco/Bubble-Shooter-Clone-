using System.Collections.Generic;
using UnityEngine;
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

    [SerializeField] private Transform ballParent;
    [SerializeField] private Transform ballAddParent;

    private Vector2 mainPos;
    private PredictBallPosToAdd predictBall;


    public Transform BallParent { get => ballParent; set => ballParent = value; }

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
        ShootBall();
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
            var rand = Random.Range(0, listBall.Count - 1);
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
        ballShootings.GetCurrent().AddListenerFotHitEvent(() => CreateNewMatrixBall());
        ballShootings.GetCurrent().AddListenerFotHitEvent(() => LoadNextBall());
    }
    private void CreateNewMatrixBall()
    {
        DestroyCurrentShootBall();
        var rand = Random.Range(0, listMatrixBall.Count - 1);
        var matrixBall = Instantiate(listMatrixBall[rand], predictBall.DesireBallPos, Quaternion.identity);
        matrixBall.AddComponent<CircleCollider2D>();
        matrixBall.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        matrixBall.GetComponent<MatrixBall>().index = predictBall.TargetID;
        boardManager.ListMatrixBall.Add(matrixBall.GetComponent<MatrixBall>());
        matrixBall.transform.SetParent(ballAddParent);
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

    }
}
public enum BallHolderType
{
    MAINBALL,
    MINORBALL,
    NONE
}

