using UnityEngine;

public class Utilities : MonoBehaviour
{
    public static void Log(string message, object info)
    {
        Debug.Log($"{message}: {info}");
    }
    public static void Log(string message)
    {
        Debug.Log($"{message}");
    }
    public static void SetTypeForBall(BallShooting ballShoot, GameObject ballObj, BallHolderType type)
    {
        ballShoot = ballObj.GetComponent<BallShooting>();
        ballShoot.transform.position = ballObj.transform.position;
        Log($"ballShoot {ballShoot.ballHolderType.ToString()}");
        ballShoot.ballHolderType = type;
    }
    public static void ChangeLayerBall(GameObject ball)
    {
        ball.layer = LayerMask.NameToLayer("BallShooting");
    }
    public static void AddRigigBodyForBall(GameObject ball)
    {
        ChangeLayerBall(ball);
        ball.AddComponent<Rigidbody2D>();
    }
    public static void SetUpComponent(BallShooting ballShoot, GameObject ballObj, BallHolderType type)
    {
        ballObj.AddComponent<BallShooting>();
        AddRigigBodyForBall(ballObj);
        SetTypeForBall(ballShoot, ballObj, type);
        // Log($"ballShoot tranform {ballShoot.transform.position}");
    }
}
