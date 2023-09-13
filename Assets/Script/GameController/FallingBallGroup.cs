using UnityEngine;
using com.soha.bridge;
using System.Collections.Generic;
using System.Linq;
using System;
public class FallingBallGroup : MonoBehaviourSingleton<FallingBallGroup>
{
    [SerializeField] private GroupHolder groupHolder => GroupHolder.Instance;
    // Start is called before the first frame update
    public void GroupBall(MatrixBall matrixBall)
    {
        groupHolder.GroupBall(matrixBall);
    }
}
[Serializable]
public class Group
{
    public List<MatrixBall> fallingBalls;
    private int lowestIndex;
    public Group()
    {
        fallingBalls = new List<MatrixBall>();
    }
    private bool IsHaveItem(MatrixBall matrixBall)
    {
        return fallingBalls.Contains(matrixBall);
    }
    public bool HaveConnectItem(MatrixBall matrixBall)
    {
        return IsHaveItem(matrixBall);
    }
    public void AddNewBall(MatrixBall newItem)
    {
        if (IsHaveItem(newItem))
            return;
        fallingBalls.Add(newItem);
        Debug.Log($" is new iteem null {newItem == null}");
        if (newItem.index.x < GetLowestIndex())
        {
            lowestIndex = newItem.index.x;
        }
    }
    public void AddNewRange(List<MatrixBall> newItems)
    {
        foreach (var i in newItems)
        {
            if (fallingBalls.Contains(i))
                return;
        }
        fallingBalls.AddRange(newItems);
        foreach (var item in fallingBalls)
        {
            if (item.index.x < GetLowestIndex())
            {
                lowestIndex = item.index.x;
            }
        }
    }
    public int GetLowestIndex()
    {
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
}
