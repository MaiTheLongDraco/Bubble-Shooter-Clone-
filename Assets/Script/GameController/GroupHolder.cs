using System.Collections.Generic;
using UnityEngine;
using com.soha.bridge;
using System;
[Serializable]
public class GroupHolder : MonoBehaviourSingleton<GroupHolder>
{
    [SerializeField] private List<Group> groups = new List<Group>();
    private int index;
    public Group GetCurrentGroup()
    {
        return groups[index];
    }
    public void MoveNext()
    {
        index++;
    }
    public void RemoveItem(Group group)
    {
        groups.Remove(group);
    }
    public void AddNewGroup(Group group)
    {
        groups.Add(group);
    }
    public void AddNewGroup()
    {
        groups.Add(new Group());
    }
    public Group JoinGroup(List<Group> groups)
    {
        Group joinedGr = new Group();
        foreach (var g in groups)
        {
            joinedGr.AddNewRange(g.fallingBalls);
        }
        return joinedGr;
    }
    public bool IsConnectMoreOneGroup(MatrixBall checkingBall)
    {
        List<Group> toMerge = new List<Group>();
        foreach (var g in groups)
        {
            if (g.fallingBalls.Contains(checkingBall))
            {
                toMerge.Add(g);
            }
        }
        if (toMerge.Count > 1)
        {
            JoinGroup(toMerge);
            return true;
        }
        else
        {
            if (toMerge.Count == 0)
            {
                Group newGroup = new Group();
                newGroup.AddNewBall(checkingBall);
            }
            return false;
        }
    }
}
