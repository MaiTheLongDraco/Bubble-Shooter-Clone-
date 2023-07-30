using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class BoardManager : MonoBehaviour
{

    [SerializeField] private RowHolder rowHolder;
    [SerializeField] private List<GameObject> ballTest;
    [SerializeField] private TextAsset data;

    public static BoardManager Instance;

    public RowHolder RowHolder { get => rowHolder; set => rowHolder = value; }

    private void Awake()
    {
        Instance = this;
    }
    private void Reset()
    {
    }
    private void Start()
    {
        ReadDataFromFile(data);
    }

    public void HandleCreateBall(int number, int i, int j)
    {
        var index = RowHolder.rows[i].intItem[j];
        var transform = RowHolder.rows[i].item_row[j].position;
        switch (number == index)
        {
            case true:
                {
                    CreateBall(number - 1, transform);
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
            rowHolder.rows.Add(new Row());
            stringBuilder.Append("|");

            for (int j = 0; j < csvData[i].Count; j++)
            {
                stringBuilder.Append(csvData[i][j] + ", ");
                Debug.Log($" csv data {i} - {j}: {csvData[i][j]}");
                if (string.IsNullOrEmpty(csvData[i][j]))
                {
                    colum++;
                    continue;
                }

                RowHolder.rows[row].intItem.Add(int.Parse(csvData[i][j]));
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
        RowHolder.rows.RemoveAt(row - 1);
        Debug.Log(stringBuilder.ToString());
    }
    private void CreateBall(int ballIndex, Vector3 transform)
    {
        Instantiate(ballTest[ballIndex], transform, Quaternion.identity);
    }
}
