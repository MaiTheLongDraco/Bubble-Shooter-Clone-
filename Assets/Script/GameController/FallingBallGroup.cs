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
    private bool IsHaveItem(MatrixBall matrixBall)
    {
        return fallingBalls.Contains(matrixBall);
    }
    public void AddNewBall(MatrixBall newItem)
    {
        if (IsHaveItem(newItem))
            return;
        fallingBalls.Add(newItem);
        if (newItem.index.x <= LowestIndex)
        {
            LowestIndex = newItem.index.x;
        }
        newItem.transform.parent = root;

    }
    public void AddNewRange(List<MatrixBall> newItems)
    {
        foreach (var item in newItems)
        {
            AddNewBall(item);
        }

    }
    public int GetLowestIndex()
    {
        Debug.Log($"fallingBalls.Min(b => b.index.x){fallingBalls.Min(b => b.index.x)}");
        return fallingBalls.Min(b => b.index.x);
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
    internal void Add(MatrixBall checkingBall)
    {
        throw new NotImplementedException();
    }
    internal bool IsFallingBallContainItem(List<MatrixBall> matrixBalls)
    {
        if (matrixBalls.Count == 0) return false;
        Debug.Log($"matrixBalls.Count {matrixBalls.Count}");
        if (matrixBalls.Any(item => fallingBalls.Contains(item)))
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
