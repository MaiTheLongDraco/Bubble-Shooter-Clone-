using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHolderManger : MonoBehaviour
{
    public static BallHolderManger Instance;
    [SerializeField] private List<GameObject> listBall;
    private BallShooting mainBallShooting;
    private BallShooting minorBallShooting;
    [SerializeField] private Transform mainTranform;
    [SerializeField] private Transform minorTranform;
    [SerializeField] private float swapTime;

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
        var mainBall = Instantiate(listBall[random], mainTranform.position, Quaternion.identity);
        SetParent(mainBall);
        var random1 = Random.Range(0, listBall.Count);
        var minorBall = Instantiate(listBall[random1], minorTranform.position, Quaternion.identity);
        SetParent(minorBall);
        mainBall.AddComponent<BallShooting>();
        minorBall.AddComponent<BallShooting>();
        MainBallShooting = mainBall.GetComponent<BallShooting>();
        MinorBallShooting = minorBall.GetComponent<BallShooting>();
    }
    private void SetParent(GameObject gameObject)
    {
        gameObject.transform.SetParent(this.transform);
    }
    public void SwapBall()
    {
        var temp = MainBallShooting.transform.position;
        MainBallShooting.transform.position = MinorBallShooting.transform.position;
        MinorBallShooting.transform.position = temp;
    }
    private void Log(string message)
    {
        Utilities.Log(message);
    }

}
