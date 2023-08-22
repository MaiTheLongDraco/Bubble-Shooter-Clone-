using UnityEngine;
using System.Linq;
public class MatrixBall : MonoBehaviour
{
    public Vector2Int index;
    public MatrixBall(Vector2Int index)
    {
        this.index = index;
    }
    public MatrixBall GetAroundItem(int changeX, int changeY)
    {
        var desireIndex = new Vector2Int(index.x + changeX, index.y + changeY);
        return BoardManager.Instance.ListMatrixBall.Find(ball => ball.index == desireIndex);
    }
    public Vector2 GetAroundPos(int changeX, int changeY)
    {
        Debug.Log($"row {index.x + changeX} -- column {index.y + changeY}");
        return BoardManager.Instance.RowHolder.GetRowItem(index.x + changeX, index.y + changeY);
    }
}
