using System.Collections.Generic;
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
public class CircularList<T> : List<T>
{
    public List<T> values = new List<T>();
    private int i = 0;
    public int Index => i;
    public T currentItem;
    public new void Add(T item)
    {
        values.Add(item);
    }
    public new void Remove(T item)
    {
        values.Remove(item);
    }
    public void MoveNext()
    {
        if (i > values.Count - 1)
        {
            i = 0;
        }
        i = (i + 1) % values.Count;
        Debug.Log($" enter move nexxt -- valuesCOunt {values.Count}");
        currentItem = values[i];
    }
    public T GetCurrent()
    {
        currentItem = values[i];
        return currentItem;
    }
    public void MovePrevious()
    {
        i = (i - 1 + values.Count) % values.Count;
    }

}
