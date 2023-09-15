using System.Collections.Generic;
using UnityEngine;
using com.soha.bridge;
using System;
using System.Linq;
using Unity.VisualScripting;
[Serializable]
public class GroupHolder : MonoBehaviourSingleton<GroupHolder>
{
    [SerializeField] private List<Group> groups = new List<Group>();
    private BoardManager boardManager => BoardManager.Instance;
    private int index;
    public List<Group> Groups { get => groups; set => groups = value; }

    public Group GetCurrentGroup()
    {
        return Groups[index];
    }
    public void MoveNext()
    {
        index++;
    }
    public void RemoveItem(Group group)
    {
        Groups.Remove(group);
    }
    public void AddNewGroup(Group group)
    {
        Groups.Add(group);
    }
    public void AddNewGroup()
    {
        Groups.Add(new Group());
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
        foreach (var g in Groups)
        {
            if (g.HasConnectWith(checkingBall))
            {
                connetGroups.Add(g);
            }
        }
        return connetGroups;
    }
    public void ClearGroup()
    {
        Groups.Clear();
    }
    public void MakeBallFall()
    {
        for (int i = 0; i < groups.Count; i++)
        {
            // print($"groups[i].CanDrop() {groups[i].CanDrop()}");
            print($"groups.Count---!!!{groups.Count}");
            print($"lowest index of group---!!!{groups[i].LowestIndex}");
            if (groups[i].LowestIndex <= 0)
                continue;
            print($" group {i} not connect with other group---!!!");
            groups[i].fallingBalls.ForEach(item => ControllWhenFall(item));
            groups.Remove(groups[i]);

        }
    }
    private void ControllWhenFall(MatrixBall matrixBall)
    {
        boardManager.RemoveFromMatrixList(matrixBall);
        Destroy(matrixBall.gameObject, 1f);
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
        Groups.Add(newGroup);
    }

    private void MergeGroups(List<Group> toMerge)
    {
        foreach (var g in toMerge)
        {
            Groups.Remove(g);
        }
        Groups.Add(JoinGroup(toMerge));
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
