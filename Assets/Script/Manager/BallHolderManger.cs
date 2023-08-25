using System.Collections.Generic;
using UnityEngine;
public class BallHolderManger : MonoBehaviour
{
    public static BallHolderManger Instance;
    [SerializeField] private List<GameObject> listBall;
    [SerializeField] private List<Transform> listShootingPos;
    [SerializeField] private CircularList<BallShooting> ballShootings;
    [SerializeField] private float targetVelocity;
    [SerializeField] private LineReflection lineReflection;
    [SerializeField] private Transform ballParent;
    private Vector2 mainPos;
    private PredictBallPosToAdd predictBallPosToAdd;

    public Transform BallParent { get => ballParent; set => ballParent = value; }

    private void Start()
    {
        mainPos = listShootingPos[0].position;
        predictBallPosToAdd = PredictBallPosToAdd.Instance;
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
        ballShootings.GetCurrent().AddListenerFotHitEvent(() => LoadNextBall());
    }

    private void LoadNextBall()
    {
        print("ball come to tarrget poss");
        ballShootings.Remove(ballShootings.GetCurrent());
        ballShootings.MoveNext();
        ballShootings.GetCurrent().transform.position = mainPos;
        AddNewShootingBall();
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

