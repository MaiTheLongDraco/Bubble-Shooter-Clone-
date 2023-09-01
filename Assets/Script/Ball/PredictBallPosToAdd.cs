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
            HandleRemainCase(hit2D, matrixBall, ballPos);
        }
        else if (!haveLeft && haveRight)
        {
            print("have right but not have left------");
            HandleRemainCase(hit2D, matrixBall, ballPos);
        }
        else
        {
            print("not have left and right------");
            HandleRemainCase(hit2D, matrixBall, ballPos);
        }
    }
    private void HandleHaveLeftAndRight(RaycastHit2D hit2D, MatrixBall matrixBall, Vector2 ballPos)
    {
        bool isDownLeft = hit2D.point.x < ballPos.x ? true : false;
        AssignDesirePos(CalCulateTargetPos(matrixBall, isDownLeft));
        print($" desireBallPos +++++++ {desireBallPos}");
    }
    private void HandleRemainCase(RaycastHit2D hit2D, MatrixBall matrixBall, Vector2 ballPos)
    {
        var upLimit = ballPos.y + matrixBall.GetSpriteSize().y / 18;
        var downLimit = ballPos.y - matrixBall.GetSpriteSize().y / 18;
        var isSide = hit2D.point.y < upLimit && hit2D.point.y > downLimit;
        print($"upLimit ++{upLimit}");
        print($"downLimit ++{downLimit}");
        print($"isSide ++{isSide}");
        if (isSide)
        {
            bool isLeft = isSide && hit2D.point.x < ballPos.x ? true : false;
            print($"isLeft ++{isLeft}");
            AssignDesirePos(CalculateSidePos(matrixBall, isLeft));
        }
        else
        {
            HandleHaveLeftAndRight(hit2D, matrixBall, ballPos);
        }
    }
    private Vector2 CalculateSidePos(MatrixBall matrixBall, bool isLeft)
    {
        if (isLeft)
        {
            TargetID = matrixBall.GetLeftIndex();
            print($" TargetID {TargetID}");
            print("left  +++++++");
            return matrixBall.GetLeft();
        }
        else
        {
            TargetID = matrixBall.GetRightIndex();
            print($" TargetID {TargetID}");
            print("right+++++++");
            return matrixBall.GetRight();
        }
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
