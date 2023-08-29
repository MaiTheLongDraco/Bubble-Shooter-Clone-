using UnityEngine;
using System.Linq;
public class MatrixBall : MonoBehaviour
{
    public Vector2Int index;
    public MatrixBallType matrixBallType;
    public MatrixBall(Vector2Int index)
    {
        this.index = index;
    }
    public MatrixBall GetAroundItem(int changeX, int changeY)
    {
        var desireIndex = new Vector2Int(index.x + changeX, index.y + changeY);
        return BoardManager.Instance.ListMatrixBall.Find(ball => ball.index == desireIndex);
    }
    public Vector2 GetLeft()
    {
        return BoardManager.Instance.RowHolder.GetRowItem(index.x, index.y - 1);
    }
    public Vector2 GetRight()
    {
        return BoardManager.Instance.RowHolder.GetRowItem(index.x, index.y + 1);
    }
    public Vector2 GetDownRightEven()
    {
        return BoardManager.Instance.RowHolder.GetRowItem(index.x + 1, index.y);
    }
    public Vector2 GetDownRightOdd()
    {
        return BoardManager.Instance.RowHolder.GetRowItem(index.x + 1, index.y + 1);
    }
    public Vector2 GetDownLeftEven()
    {
        return BoardManager.Instance.RowHolder.GetRowItem(index.x + 1, index.y - 1);
    }
    public Vector2 GetDownLeftOdd()
    {
        return BoardManager.Instance.RowHolder.GetRowItem(index.x + 1, index.y);
    }
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
    public Vector2 GetSpriteSize()
    {
        return this.GetComponent<SpriteRenderer>().size;
    }
}
public enum MatrixBallType
{
    BALL_01,
    BALL_02,
    BALL_03,
    BALL_04,
    BALL_05
}