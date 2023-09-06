using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class RowHolder
{
    [SerializeField] public List<Row> rows;

    public Vector2 GetRowItem(int i, int j)
    {
        return rows[i].item_row_tranform[j].position;
        //ahihi
    }
    public bool IsLegal(int i, int j)
    {
        return i >= 0 && i <= rows.Count - 1 && j >= 0 && j <= rows[i].item_row_tranform.Count - 1;
    }
}
