using com.soha.bridge;
using UnityEngine;

public class PredictBallPosToAdd : MonoBehaviourSingleton<PredictBallPosToAdd>
{
    private Vector2 desireBallPos;
    public Vector2 DesireBallPos { get => desireBallPos; set => desireBallPos = value; }
    public Vector2Int TargetID;
    public void HandlePosToAddBall(bool haveLeft, bool haveRight, RaycastHit2D hit2D, MatrixBall matrixBall)
    {
        if (IsNoNeedToCalculate(matrixBall))
            return;
        var ballPos = (Vector2)hit2D.transform.position;
        if (haveLeft && haveRight)
        {
            print("have lefft and right------");
            HandleHaveLeftAndRight(hit2D, matrixBall, ballPos);
            return;
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
            if (matrixBall.HaveLeft())
            {
                TargetID = matrixBall.GetRightIndex();
                return matrixBall.GetRight();
            }
            TargetID = matrixBall.GetLeftIndex();
            print($" TargetID {TargetID}");
            print("left  +++++++");
            return matrixBall.GetLeft();
        }
        else
        {
            if (matrixBall.HaveRight())
            {
                TargetID = matrixBall.GetLeftIndex();
                return matrixBall.GetLeft();
            }
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
            return PosForEvenRow(matrixBall, isDownLeft);
        }
        else
            return PosForOddRow(matrixBall, isDownLeft);
    }

    private Vector2 PosForOddRow(MatrixBall matrixBall, bool isDownLeft)
    {
        if (isDownLeft)
        {
            if (matrixBall.HaveDownLeftOdd())
            {
                TargetID = matrixBall.GetDownRightOddIndex();
                return matrixBall.GetDownRightOdd();
            }
            TargetID = matrixBall.GetDownLeftOddIndex();
            print("down left obb row  +++++++");
            print($" TargetID {TargetID}");

            return matrixBall.GetDownLeftOdd();
        }
        else
        {
            if (matrixBall.HaveDownRightOdd())
            {
                TargetID = matrixBall.GetDownLeftOddIndex();
                return matrixBall.GetDownLeftOdd();
            }
            TargetID = matrixBall.GetDownRightOddIndex();
            print("down right obb row +++++++");
            print($" TargetID {TargetID}");
            return matrixBall.GetDownRightOdd();
        }
    }

    private Vector2 PosForEvenRow(MatrixBall matrixBall, bool isDownLeft)
    {
        if (isDownLeft)
        {
            if (matrixBall.HaveDownLeftEven())
            {
                TargetID = matrixBall.GetDownRightIndexEven();
                return matrixBall.GetDownRightEven();
            }
            TargetID = matrixBall.GetDownLeftEvenIndex();
            print($" TargetID {TargetID}");
            print("down left even row +++++++");
            return matrixBall.GetDownLeftEven();
        }
        else
        {
            if (matrixBall.HaveDownRightEven())
            {
                TargetID = matrixBall.GetDownLeftEvenIndex();
                return matrixBall.GetDownLeftEven();
            }
            TargetID = matrixBall.GetDownRightIndexEven();
            print($" TargetID {TargetID}");
            print("down right even row +++++++");
            return matrixBall.GetDownRightEven();
        }
    }

    private void AssignDesirePos(Vector2 targetPos)
    {
        desireBallPos = targetPos;
    }
    private bool IsNoNeedToCalculate(MatrixBall matrixBall)
    {
        if (matrixBall.index.x % 2 == 0)
        {
            var isHaveLeft = matrixBall.HaveLeft();
            var isHaveDownLeft = matrixBall.HaveDownLeftEven();
            var isHaveRight = matrixBall.HaveRight();
            var isHaveDownRight = matrixBall.HaveDownRightEven();
            if (isHaveLeft && isHaveDownLeft && isHaveRight && isHaveDownRight)
            {
                return true;
            }
            return false;

        }
        else
        {
            var isHaveLeft = matrixBall.HaveLeft();
            var isHaveDownLeft = matrixBall.HaveDownLeftOdd();
            var isHaveRight = matrixBall.HaveRight();
            var isHaveDownRight = matrixBall.HaveDownRightOdd();
            if (isHaveLeft && isHaveDownLeft && isHaveRight && isHaveDownRight)
            {
                return true;
            }
            return false;
        }
    }
}
