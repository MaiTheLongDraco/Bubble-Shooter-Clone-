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
    }
}
