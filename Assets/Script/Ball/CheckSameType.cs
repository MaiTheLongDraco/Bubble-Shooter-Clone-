using System.Collections.Generic;
using com.soha.bridge;
using UnityEngine;
using System.Linq;

public class CheckSameType : MonoBehaviourSingleton<CheckSameType>
{
    [SerializeField] private List<MatrixBall> sameTypeBalls = new List<MatrixBall>();
    public List<MatrixBall> SameTypeBalls { get => sameTypeBalls; set => sameTypeBalls = value; }
    public void CheckSameTypeAround(MatrixBall curentBall)
    {
        if (curentBall.index.x % 2 == 0)
        {
            var UpLeftEven = curentBall.GetUpLeftEven();
            var UpRightEven = curentBall.GetUpRightEven();
            var LeftEven = curentBall.GetLeftBall();
            var RightEven = curentBall.GetRightBall();
            ReCursionCheck(UpLeftEven, curentBall);
            ReCursionCheck(UpRightEven, curentBall);
            ReCursionCheck(LeftEven, curentBall);
            ReCursionCheck(RightEven, curentBall);
            print($"Even coll =====");
        }
        else
        {
            var UpLeftOdd = curentBall.GetUpLeftOdd();
            var UpRightOdd = curentBall.GetUpRightOdd();
            var LeftEven = curentBall.GetLeftBall();
            var RightEven = curentBall.GetRightBall();
            ReCursionCheck(UpLeftOdd, curentBall);
            ReCursionCheck(UpRightOdd, curentBall);
            ReCursionCheck(LeftEven, curentBall);
            ReCursionCheck(RightEven, curentBall);
            print($"odd coll =====");
        }
    }
    private void ReCursionCheck(MatrixBall otherBall, MatrixBall curentBall)
    {
        var notMatch = otherBall == null || otherBall.matrixBallType != curentBall.matrixBallType;
        if (notMatch)
            return;
        if (!sameTypeBalls.Contains(otherBall))
        {
            AddNewBall(otherBall);
            CheckSameTypeAround(otherBall);
        }
        print("CheckSameTypeAround");
    }
    public void AddNewBall(MatrixBall newBall)
    {
        if (sameTypeBalls.Contains(newBall))
            return;
        sameTypeBalls.Add(newBall);
    }

    public void ClearListSameType(MatrixBall toCompareBall)
    {
        if (sameTypeBalls.Any(b => b.matrixBallType != toCompareBall.matrixBallType))
        {
            sameTypeBalls.Clear();
        }
    }
    public void ClearListSameType()
    {
        sameTypeBalls.Clear();
    }


}
