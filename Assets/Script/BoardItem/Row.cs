using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Row
{
    public List<Transform> item_row_tranform;
    public List<int> intItem;
}
[Serializable]
public class LevelInfoHolder
{
    public TextAsset levelCSV;
    public int currentLevel;

}
