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
            HandleHaveLeftAndRight(hit2D, matrixBall, ballPos);
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
                print("down left even row +++++++");
                return matrixBall.GetDownLeftEven();
            }
            else
            {
                TargetID = matrixBall.GetDownRightIndexEven();
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
                return matrixBall.GetDownLeftOdd();
            }
            else
            {
                TargetID = matrixBall.GetDownRightOddIndex();
                print("down right obb row +++++++");
                return matrixBall.GetDownRightOdd();
            }
        }
    }

    private void AssignDesirePos(Vector2 targetPos)
    {
        desireBallPos = targetPos;
    }
}
