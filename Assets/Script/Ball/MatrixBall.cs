using UnityEngine;
using System.Linq;
using System.Collections.Generic;
public class MatrixBall : MonoBehaviour
{
    public Vector2Int index;
    public MatrixBallType matrixBallType;
    private BoardManager boardManager => BoardManager.Instance;
    public MatrixBall(Vector2Int index)
    {
        this.index = index;
    }
    public MatrixBall GetAroundItem(int changeX, int changeY)
    {
        var desireIndex = new Vector2Int(index.x + changeX, index.y + changeY);
        return BoardManager.Instance.ListMatrixBall.Find(ball => ball.index == desireIndex);
    }
    #region GetSide
    public Vector2 GetLeft()
    {
        return BoardManager.Instance.RowHolder.GetRowItem(index.x, index.y - 1);
    }
    public MatrixBall GetLeftBall()
    {
        return BoardManager.Instance.GetMatrixBall(GetLeftIndex());
    }
    public Vector2 GetRight()
    {
        return BoardManager.Instance.RowHolder.GetRowItem(index.x, index.y + 1);
    }
    public MatrixBall GetRightBall()
    {
        return BoardManager.Instance.GetMatrixBall(GetRightIndex());
    }
    #endregion
    #region GetDownEven
    public Vector2 GetDownRightEven()
    {

        return BoardManager.Instance.RowHolder.GetRowItem(index.x + 1, index.y);
    }
    public Vector2 GetDownLeftEven()
    {
        // if
        return BoardManager.Instance.RowHolder.GetRowItem(index.x + 1, index.y - 1);
    }
    public MatrixBall GetDownLeftEvenBall()
    {
        return BoardManager.Instance.GetMatrixBall(GetDownLeftEvenIndex());
    }
    public MatrixBall GetDownRightEvenBall()
    {
        return BoardManager.Instance.GetMatrixBall(GetDownRightIndexEven());
    }
    #endregion
    #region GetDownOdd
    public Vector2 GetDownRightOdd()
    {
        return BoardManager.Instance.RowHolder.GetRowItem(index.x + 1, index.y + 1);
    }
    public Vector2 GetDownLeftOdd()
    {
        return BoardManager.Instance.RowHolder.GetRowItem(index.x + 1, index.y);
    }
    public MatrixBall GetDownLeftOddBall()
    {
        return BoardManager.Instance.GetMatrixBall(GetDownLeftOddIndex());
    }
    public MatrixBall GetDownRightOddBall()
    {
        return BoardManager.Instance.GetMatrixBall(GetDownRightOddIndex());
    }
    #endregion
    #region GetAroundIndex
    public Vector2Int GetLeftIndex()
    {
        return new Vector2Int(index.x, index.y - 1);
    }
    public Vector2Int GetRightIndex()
    {
        return new Vector2Int(index.x, index.y + 1);
    }
    public Vector2Int GetDownRightIndexEven()
    {
        return new Vector2Int(index.x + 1, index.y);
    }
    public Vector2Int GetDownRightOddIndex()
    {
        return new Vector2Int(index.x + 1, index.y + 1);
    }
    public Vector2Int GetDownLeftEvenIndex()
    {
        return new Vector2Int(index.x + 1, index.y - 1);
    }
    public Vector2Int GetDownLeftOddIndex()
    {
        return new Vector2Int(index.x + 1, index.y);
    }
    #region GetUpIndex
    public Vector2Int GetUpLeftEvenIndex()
    {
        return new Vector2Int(index.x - 1, index.y - 1);
    }
    public Vector2Int GetUpRighttEvenIndex()
    {
        return new Vector2Int(index.x - 1, index.y);
    }
    public Vector2Int GetUpLeftOddIndex()
    {
        return new Vector2Int(index.x - 1, index.y);
    }
    public Vector2Int GetUpRightOddIndex()
    {
        return new Vector2Int(index.x - 1, index.y + 1);
    }
    #endregion
    #endregion
    #region GetUpEven
    public MatrixBall GetUpLeftEven()
    {
        return BoardManager.Instance.GetMatrixBall(GetUpLeftEvenIndex());
    }
    public MatrixBall GetUpRightEven()
    {
        return BoardManager.Instance.GetMatrixBall(GetUpRighttEvenIndex());
    }
    #endregion
    #region GetUpOdd
    public MatrixBall GetUpLeftOdd()
    {
        return BoardManager.Instance.GetMatrixBall(GetUpLeftOddIndex());
    }
    public MatrixBall GetUpRightOdd()
    {
        return BoardManager.Instance.GetMatrixBall(GetUpRightOddIndex());
    }
    #endregion

    public Vector2 GetSpriteSize()
    {
        return this.GetComponent<SpriteRenderer>().size;
    }
    public List<MatrixBall> GetAllBelowBall()
    {
        return boardManager.GetBelowTargetBall(index);
    }
    public void DestroyBelowBall()
    {
        GetAllBelowBall().ForEach(b => Destroy(b.gameObject));
    }
    public List<MatrixBall> GetAllBesideBall()
    {
        return boardManager.GetBesideTargetBall(index);
    }
    #region Check if have Ball around
    public bool HaveDownLeftOdd()
    {
        return boardManager.IsListMatrixContainItem(GetAroundItem(1, 0));
    }
    public bool HaveDownLeftEven()
    {
        return boardManager.IsListMatrixContainItem(GetAroundItem(1, -1));
    }
    public bool HaveDownRightOdd()
    {
        return boardManager.IsListMatrixContainItem(GetAroundItem(1, 1));
    }
    public bool HaveDownRightEven()
    {
        return boardManager.IsListMatrixContainItem(GetAroundItem(1, 0));
    }

    public bool HaveLeft()
    {
        return boardManager.IsListMatrixContainItem(GetLeftBall());
    }
    public bool HaveRight()
    {
        return boardManager.IsListMatrixContainItem(GetRightBall());
    }
    #endregion
}
public enum MatrixBallType
{
    BALL_01,
    BALL_02,
    BALL_03,
    BALL_04,
    BALL_05
}