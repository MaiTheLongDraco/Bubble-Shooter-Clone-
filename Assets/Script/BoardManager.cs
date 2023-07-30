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
    private void Start()
    {
        ReadDataFromFile(data);
    }

    public void HandleCreateBall(int number, int i, int j)
    {
        var transform = RowHolder.rows[i].item_row[j].position;
        switch (number)
        {
            case 1:
                {
                    CreateBall(0, transform);
                }
                break;
            case 2:
                {
                    CreateBall(1, transform);
                }
                break;
            case 3:
                {
                    CreateBall(2, transform);
                }
                break;
            case 4:
                {
                    CreateBall(3, transform);
                }
                break;
            case 5:
                {
                    CreateBall(4, transform);
                }
                break;

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
                rowHolder.rows[i].intItem[j] = int.Parse(csvData[i][j]);
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
    private void CreateBall(int index, Vector3 transform)
    {
        Instantiate(ballTest[index], transform, Quaternion.identity);
    }
}
