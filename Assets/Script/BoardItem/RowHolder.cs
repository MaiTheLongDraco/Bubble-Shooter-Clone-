using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class RowHolder
{
    [SerializeField] public List<Row> rows;

    public Vector2 GetRowItem(int i, int j)
    {
        i = CalculatePos(i, rows.Count - 1);
        j = CalculatePos(j, rows[i].item_row_tranform.Count - 1);
        Debug.Log($"i {i} ---- j {j}");
        return rows[i].item_row_tranform[j].position;
        //ahihi
    }
    public bool IsLegal(int i, int j)
    {
        return i >= 0 && i <= rows.Count - 1 && j >= 0 && j <= rows[i].item_row_tranform.Count - 1;
    }
    private int CalculatePos(int i, int maxLimit)
    {
        if (i < 0)
        {
            i = 0;
            // return 0;
        }
        else if (i > maxLimit)
        {
            i = maxLimit;
            // return maxLimit;
        }

        return i;

    }
}
