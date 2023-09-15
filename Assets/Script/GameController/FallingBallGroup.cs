using UnityEngine;
using com.soha.bridge;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEditor.Experimental.GraphView;
public class FallingBallGroup : MonoBehaviourSingleton<FallingBallGroup>
{
    [SerializeField] private GroupHolder groupHolder => GroupHolder.Instance;
    [SerializeField] private BoardManager boardManager => BoardManager.Instance;

    // Start is called before the first frame update
    public void GroupBall(MatrixBall matrixBall)
    {
        groupHolder.GroupBall(matrixBall);
    }
    public void GroupAfterShoot()
    {
        groupHolder.ClearGroup();
        boardManager.GroupBall();
    }
    public void MakeBallFall()
    {
        groupHolder.MakeBallFall();
    }

}
[Serializable]
public class Group
{
    [SerializeField] private GroupHolder groupHolder => GroupHolder.Instance;

    public int LowestIndex { get => lowestIndex; private set => lowestIndex = value; }

    public List<MatrixBall> fallingBalls;
    [SerializeField] private int lowestIndex = 15;
    private Transform root;
    public Group()
    {
        fallingBalls = new List<MatrixBall>();
        root = new GameObject("group____").transform;
    }
    public Group(MatrixBall matrixBall)
    {
        fallingBalls = new List<MatrixBall>();
        root = new GameObject("group____").transform;
        fallingBalls.Add(matrixBall);
    }
    private bool HasItem(MatrixBall matrixBall)
    {
        return fallingBalls.Contains(matrixBall);
    }
    public void Add(MatrixBall ball)
    {
        Debug.Log($" newitem index {ball.index}");
        if (HasItem(ball))
            return;
        fallingBalls.Add(ball);
        if (ball.index.x <= LowestIndex)
        {
            LowestIndex = ball.index.x;
        }
        ball.transform.parent = root;
    }
    public void Add(List<MatrixBall> newItems)
    {
        foreach (var item in newItems)
        {
            Add(item);
        }
    }
    public void Add(Group otherGroup)
    {
        foreach (var item in otherGroup.fallingBalls)
        {
            Add(item);
        }
    }
    internal bool HasConnectWith(MatrixBall checkingBall)
    {
        var around = checkingBall.GetAllAroundItem();
        if (around.Any(b => fallingBalls.Contains(b)))
        {
            return true;
        }
        else return false;
    }
    private bool IsAroundItemNull()
    {
        foreach (var obj in fallingBalls)
        {
            if (obj.GetAllAroundItem().Count == 0)
                return true;
            else return false;
        }
        return false;
    }
    public void Destroy()
    {
        GameObject.Destroy(root.gameObject);
    }
}
