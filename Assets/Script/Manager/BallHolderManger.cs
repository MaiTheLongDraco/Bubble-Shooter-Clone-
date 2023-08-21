using System.Collections.Generic;
using UnityEngine;
public class BallHolderManger : MonoBehaviour
{
    public static BallHolderManger Instance;
    [SerializeField] private List<GameObject> listBall;
    private BallShooting mainBallShooting;
    private BallShooting minorBallShooting;
    [SerializeField] private List<BallHolder> listBallHolder;
    private GameObject ball1;
    private GameObject ball2;
    [SerializeField] private Transform mainTranform;
    [SerializeField] private Transform minorTranform;
    [SerializeField] private float distance;
    [SerializeField] private float ballSpeed;

    public BallShooting MainBallShooting { get => mainBallShooting; set => mainBallShooting = value; }
    public BallShooting MinorBallShooting { get => minorBallShooting; set => minorBallShooting = value; }
    public Transform MainTranform { get => mainTranform; set => mainTranform = value; }

    private Vector3 direction;


    // Start is called before the first frame update
    void Start()
    {
        LoadComponent();
    }
    private void Awake()
    {
        Instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        ShowShootDirection();
    }
    private void LoadComponent()
    {
        LoadBallPosBall1();
        LoadBallPosBall2();
    }
    private void ShowShootDirection()
    {
        if (MainBallShooting == null)
            return;
        MainBallShooting.ShowShootDirection();
        HandleMainBall(MainBallShooting);
    }
    private void LoadBallPosBall1()
    {
        var random = Random.Range(0, listBall.Count);
        ball1 = Instantiate(listBall[random], mainTranform.position, Quaternion.identity);
        SetParent(ball1);

        ball1.AddComponent<BallShooting>();
        BallShooting ballShooting = ball1.GetComponent<BallShooting>();
        ballShooting.SetBallVelocity(ballSpeed);

        MainBallShooting = ball1.GetComponent<BallShooting>();
        MainBallShooting.ballHolderType = BallHolderType.MAINBALL;
        Utilities.AddRigigBodyForBall(ball1);
    }
    private void LoadBallPosBall2()
    {
        var random1 = Random.Range(0, listBall.Count);
        ball2 = Instantiate(listBall[random1], minorTranform.position, Quaternion.identity);
        SetParent(ball2);
        ball2.AddComponent<BallShooting>();
        MinorBallShooting = ball2.GetComponent<BallShooting>();
        MinorBallShooting.ballHolderType = BallHolderType.MINORBALL;
        Utilities.AddRigigBodyForBall(ball2);
    }
    private void HandleMainBall(BallShooting ballShooting)
    {
        if (MainBallShooting == null)
        {
            MainBallShooting = MinorBallShooting;
            MinorBallShooting = null;
            if (MinorBallShooting == null)
            {
                LoadBallPosBall2();
            }
        }
    }
    private void SetParent(GameObject gameObject)
    {
        gameObject.transform.SetParent(this.transform);
    }
    public void SwapBall()
    {
        if (Input.GetMouseButtonUp(0))
            return;
        var temp = ball1.transform.position;
        ball1.transform.position = ball2.transform.position;
        ball2.transform.position = temp;
        SwapBallType();
    }
    private void SwapBallType()
    {
        var temp = MainBallShooting.ballHolderType;
        MainBallShooting.ballHolderType = MinorBallShooting.ballHolderType;
        MinorBallShooting.ballHolderType = temp;
    }
    private void Log(string message)
    {
        Utilities.Log(message);
    }

}
public enum BallHolderType
{
    MAINBALL,
    MINORBALL,
    NONE
}

