using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHolderManger : MonoBehaviour
{
    public static BallHolderManger Instance;
    [SerializeField] private List<GameObject> listBall;
    private BallShooting mainBallShooting;
    private BallShooting minorBallShooting;
    private GameObject ball1;
    private GameObject ball2;
    [SerializeField] private Transform mainTranform;
    [SerializeField] private Transform minorTranform;

    public BallShooting MainBallShooting { get => mainBallShooting; set => mainBallShooting = value; }
    public BallShooting MinorBallShooting { get => minorBallShooting; set => minorBallShooting = value; }

    // Start is called before the first frame update
    void Start()
    {
        LoadBallPos();
    }
    private void Awake()
    {
        Instance = this;
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void LoadComponent()
    {

    }
    private void LoadBallPos()
    {
        var random = Random.Range(0, listBall.Count);
        ball1 = Instantiate(listBall[random], mainTranform.position, Quaternion.identity);
        SetParent(ball1);
        var random1 = Random.Range(0, listBall.Count);
        ball2 = Instantiate(listBall[random1], minorTranform.position, Quaternion.identity);
        SetParent(ball2);
        ball1.AddComponent<BallShooting>();
        ball2.AddComponent<BallShooting>();

        MainBallShooting = ball1.GetComponent<BallShooting>();
        MainBallShooting.ballHolderType = BallHolderType.MAINBALL;
        MinorBallShooting = ball2.GetComponent<BallShooting>();
        MinorBallShooting.ballHolderType = BallHolderType.MINORBALL;

    }
    private void HandleMainBall(BallShooting ballShooting)
    {
        if (ballShooting.ballHolderType == BallHolderType.MAINBALL)
        {
            mainBallShooting = ballShooting;
        }
    }
    private void SetParent(GameObject gameObject)
    {
        gameObject.transform.SetParent(this.transform);
    }
    public void SwapBall()
    {
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
    MINORBALL
}

