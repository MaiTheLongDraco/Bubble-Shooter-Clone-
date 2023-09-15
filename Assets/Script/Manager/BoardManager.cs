using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class BoardManager : MonoBehaviour
{

    [SerializeField] private RowHolder rowHolder;
    [SerializeField] private List<GameObject> ballTest;
    [SerializeField] private TextAsset data;
    [SerializeField] private GameObject ballParent;
    [SerializeField] private List<MatrixBall> listMatrix = new List<MatrixBall>();
    public static BoardManager Instance;
    public List<MatrixBall> ListMatrixBall { get => listMatrix; set => listMatrix = value; }
    public RowHolder RowHolder { get => rowHolder; set => rowHolder = value; }

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        ReadDataFromFile(data);
        GroupBall();
    }
    public MatrixBall GetMatrixBall(Vector2Int target)
    {
        return listMatrix.Find(b => b.index == target);
    }
    public bool IsBoardEmpty()
    {
        if (listMatrix.Count <= 0)
        {
            return true;
        }
        return false;
    }
    public void RemoveFromMatrixList(MatrixBall toRemove)
    {
        listMatrix.Remove(toRemove);
    }

    public void HandleCreateBall(int number, int i, int j)
    {
        var index = RowHolder.rows[i].intItem[j];
        var transform = RowHolder.rows[i].item_row_tranform[j].position;
        MatrixBall matrixBall = new MatrixBall(new Vector2Int(i, j));
        switch (number == index)
        {
            case true:
                {
                    CreateBall(number - 1, transform, matrixBall);
                    break;
                }
            case false: { break; }
        }
    }
    public void ReadDataFromFile(TextAsset csv)
    {
        CsvReader csvReader = new CsvReader();
        List<List<string>> csvData = csvReader.ReadCsv(csv.text);
        int row = 0;
        int colum = 0;
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < csvData.Count; i++)
        {
            colum = 0;
            // rowHolder.rows.Add(new Row());
            stringBuilder.Append("|");

            for (int j = 0; j < csvData[i].Count; j++)
            {
                stringBuilder.Append(csvData[i][j] + ", ");
                //Debug.Log($" csv data {i} - {j}: {csvData[i][j]}");
                if (string.IsNullOrEmpty(csvData[i][j]))
                {
                    colum++;
                    continue;
                }

                // RowHolder.rows[row].intItem.Add(int.Parse(csvData[i][j]));
                if (int.Parse(csvData[i][j]) != 0)
                {
                    HandleCreateBall(int.Parse(csvData[i][j]), i, j);
                }
                colum++;
            }
            stringBuilder.Remove(stringBuilder.Length - 2, 2);
            stringBuilder.Append("|\n");
            row++;
        }
        // RowHolder.rows.RemoveAt(row - 1);
        Debug.Log(stringBuilder.ToString());
    }
    private void CreateBall(int ballIndex, Vector3 transform, MatrixBall matrixBall)
    {
        var ball = Instantiate(ballTest[ballIndex], transform, Quaternion.identity);
        ball.AddComponent<CircleCollider2D>();
        ball.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        ball.GetComponent<MatrixBall>().index = matrixBall.index;
        listMatrix.Add(ball.GetComponent<MatrixBall>());
        ball.gameObject.tag = "Ball";
        ball.transform.SetParent(ballParent.transform);
    }
    public bool IsListMatrixContainItem(MatrixBall ball)
    {
        var isContain = ListMatrixBall.Contains(ball);
        return isContain;
    }
    public List<MatrixBall> GetBelowTargetBall(Vector2Int index)
    {
        return listMatrix.FindAll(ball => ball.index.x > index.x);
    }
    public List<MatrixBall> GetBesideTargetBall(Vector2Int index)
    {
        return listMatrix.FindAll(ball => ball.index.x == index.x);
    }
    public void GroupBall()
    {
        foreach (var b in listMatrix)
        {
            print($"ball index {b.index}");
            FallingBallGroup.Instance.GroupBall(b);
        }
    }
}
