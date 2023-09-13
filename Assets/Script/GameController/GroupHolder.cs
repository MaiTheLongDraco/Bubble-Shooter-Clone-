using System.Collections.Generic;
using UnityEngine;
using com.soha.bridge;
using System;
using System.Linq;
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
    private List<Group> GetListConnect(MatrixBall checkingBall)
    {
        var connetGroups = new List<Group>();
        foreach (var g in groups)
        {
            if (g.HasConnectWith(checkingBall))
            {
                connetGroups.Add(g);
            }
        }
        return connetGroups;
    }

    public void GroupBall(MatrixBall checkingBall)
    {
        HandleAddBallToGroup(checkingBall, GetListConnect(checkingBall));
    }
    private void HandleAddBallToGroup(MatrixBall checkingBall, List<Group> toMerge)
    {
        if (toMerge.Count >= 2)
        {
            MergeGroups(toMerge);
        }
        else if (toMerge.Count <= 0)
        {
            AddNewGroup(checkingBall);
        }
        else
        {
            toMerge[0].AddNewBall(checkingBall);
        }
    }

    private void AddNewGroup(MatrixBall checkingBall)
    {
        Group newGroup = new Group();
        newGroup.AddNewBall(checkingBall);
        groups.Add(newGroup);
    }

    private void MergeGroups(List<Group> toMerge)
    {
        foreach (var g in toMerge)
        {
            groups.Remove(g);
        }
        groups.Add(JoinGroup(toMerge));
    }

    private List<MatrixBall> GetAllAroundItem(MatrixBall checkingBall)
    {
        var listAround = new List<MatrixBall>();
        if (checkingBall.index.x % 2 == 0)
        {
            var UpLeftEven = checkingBall.GetUpLeftEven();
            var UpRightEven = checkingBall.GetUpRightEven();
            var LeftEven = checkingBall.GetLeftBall();
            var RightEven = checkingBall.GetRightBall();
            var downLeft = checkingBall.GetDownLeftEvenBall();
            var downRight = checkingBall.GetDownRightEvenBall();
            listAround.Add(UpLeftEven);
            listAround.Add(UpRightEven);
            listAround.Add(LeftEven);
            listAround.Add(RightEven);
            listAround.Add(downLeft);
            listAround.Add(downRight);
            print($"Even coll =====");
        }
        else
        {
            var UpLeftOdd = checkingBall.GetUpLeftOdd();
            var UpRightOdd = checkingBall.GetUpRightOdd();
            var LeftEven = checkingBall.GetLeftBall();
            var RightEven = checkingBall.GetRightBall();
            var downLeft = checkingBall.GetDownLeftOddBall();
            var downRight = checkingBall.GetDownRightOddBall();
            listAround.Add(UpLeftOdd);
            listAround.Add(UpRightOdd);
            listAround.Add(LeftEven);
            listAround.Add(RightEven);
            listAround.Add(downLeft);
            listAround.Add(downRight);
            print($"odd coll =====");
        }
        return listAround;
    }
}
