
using UnityEngine;
using com.soha.bridge;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine.Playables;

public class GruopFallingBall : MonoBehaviourSingleton<GruopFallingBall>
{
    [SerializeField] private GroupHolder groupHolder => GroupHolder.Instance;
    // Start is called before the first frame update
    public void Test(MatrixBall matrixBall)
    {
        groupHolder.AddNewGroup(GroupLinkBall(matrixBall));
        groupHolder.MoveNext();
    }
    public Group GroupLinkBall(MatrixBall checkingBall)
    {
        print($"explore ball index {checkingBall}");
        if (checkingBall.index.x % 2 == 0)
        {
            var UpLeftEven = checkingBall.GetUpLeftEven();
            var UpRightEven = checkingBall.GetUpRightEven();
            var LeftEven = checkingBall.GetLeftBall();
            var RightEven = checkingBall.GetRightBall();
            var downLeft = checkingBall.GetDownLeftEvenBall();
            var downRight = checkingBall.GetDownRightEvenBall();
            return CheckAround(checkingBall, UpLeftEven, UpRightEven, LeftEven, RightEven, downLeft, downRight);
            print($"Even coll =====");
        }
        else
        {
            var UpLeftOdd = checkingBall.GetUpLeftOdd();
            var UpRightOdd = checkingBall.GetUpRightOdd();
            var LeftEven = checkingBall.GetLeftBall();
            var RightEven = checkingBall.GetRightBall();
            var downLeft = checkingBall.GetDownLeftOddBall();
            var downRight = checkingBall.GetDownRightOddBall();
            return CheckAround(checkingBall, UpLeftOdd, UpRightOdd, LeftEven, RightEven, downLeft, downRight);
            print($"odd coll =====");
        }
    }

    private Group CheckAround(MatrixBall sameTypeBall, MatrixBall UpLeftEven, MatrixBall UpRightEven, MatrixBall LeftEven, MatrixBall RightEven, MatrixBall downLeft, MatrixBall downRight)
    {
        var up1 = ReCursionCheck(UpLeftEven, sameTypeBall);
        var up2 = ReCursionCheck(UpRightEven, sameTypeBall);
        var left = ReCursionCheck(LeftEven, sameTypeBall);
        var right = ReCursionCheck(RightEven, sameTypeBall);
        var down1 = ReCursionCheck(downLeft, sameTypeBall);
        var down2 = ReCursionCheck(downRight, sameTypeBall);
        Group newGr = new Group();
        newGr.AddNewBall(up1);
        newGr.AddNewBall(up2);
        newGr.AddNewBall(left);
        newGr.AddNewBall(right);
        newGr.AddNewBall(down1);
        newGr.AddNewBall(down2);
        return newGr;
    }

    private MatrixBall ReCursionCheck(MatrixBall otherBall, MatrixBall curentBall)
    {
        var notMatch = otherBall == null || otherBall.index.x <= curentBall.index.x || otherBall.index.x <= 0;
        if (notMatch)
            return curentBall;
        GroupLinkBall(otherBall);
        return otherBall;
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

}
