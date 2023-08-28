using com.soha.bridge;
using UnityEngine;

public class PredictBallPosToAdd : MonoBehaviourSingleton<PredictBallPosToAdd>
{
    private Vector2 desireBallPos;
    public Vector2 DesireBallPos { get => desireBallPos; set => desireBallPos = value; }
    public Vector2Int TargetID;
    public void HandlePosToAddBall(bool haveLeft, bool haveRight, RaycastHit2D hit2D, MatrixBall matrixBall)
    {
        var ballPos = (Vector2)hit2D.transform.position;
        if (haveLeft && haveRight)
        {
            print("have lefft and right------");
            HandleHaveLeftAndRight(hit2D, matrixBall, ballPos);
        }
        else if (haveLeft && !haveRight)
        {
            print("have left but not have right------");
        }
        else if (!haveLeft && haveRight)
        {
            print("have right but not have left------");
        }
        else
        {
            print("not have left and right------");
        }
    }
    private void HandleHaveLeftAndRight(RaycastHit2D hit2D, MatrixBall matrixBall, Vector2 ballPos)
    {
        bool isDownLeft = hit2D.point.x < ballPos.x ? true : false;
        AssignDesirePos(CalCulateTargetPos(matrixBall, isDownLeft));
        print($" desireBallPos +++++++ {desireBallPos}");
    }
    private Vector2 CalCulateTargetPos(MatrixBall matrixBall, bool isDownLeft)
    {
        if (matrixBall.index.x % 2 == 0)
        {
            if (isDownLeft)
            {
                TargetID = matrixBall.GetDownLeftEvenIndex();
                print($" TargetID {TargetID}");
                print("down left even row +++++++");
                return matrixBall.GetDownLeftEven();
            }
            else
            {
                TargetID = matrixBall.GetDownRightIndexEven();
                print($" TargetID {TargetID}");

                print("down right even row +++++++");
                return matrixBall.GetDownRightEven();
            }
        }
        else
        {
            if (isDownLeft)
            {
                TargetID = matrixBall.GetDownLeftOddIndex();
                print("down left obb row  +++++++");
                print($" TargetID {TargetID}");

                return matrixBall.GetDownLeftOdd();
            }
            else
            {
                TargetID = matrixBall.GetDownRightOddIndex();
                print("down right obb row +++++++");
                print($" TargetID {TargetID}");

                return matrixBall.GetDownRightOdd();
            }
        }
    }

    private void AssignDesirePos(Vector2 targetPos)
    {
        desireBallPos = targetPos;
    }
}
