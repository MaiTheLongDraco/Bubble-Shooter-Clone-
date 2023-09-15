using System.Collections.Generic;
using UnityEngine;
using com.soha.bridge;
using System;
[Serializable]
public class GroupHolder : MonoBehaviourSingleton<GroupHolder>
{
    [SerializeField] private List<Group> groups = new List<Group>();
    [SerializeField] private GameObject groupBallContainer;

    private BoardManager boardManager => BoardManager.Instance;
    private int index;
    public List<Group> Groups { get => groups; set => groups = value; }
    public Group JoinGroup(List<Group> groups, MatrixBall checkingBall)
    {
        Group joinedGr = new Group();
        joinedGr.Add(checkingBall);
        foreach (var g in groups)
        {
            joinedGr.Add(g);
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
    public void ChangeBallParent()
    {
        for (int i = 0; i < groups.Count; i++)
        {
            var parent = Instantiate(groupBallContainer);
            parent.name = $"Group {i}";
            groups[i].fallingBalls.ForEach(ball => ball.transform.SetParent(parent.transform));
        }
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
        var ball1 = new Vector2Int(2, 8);
        var ball2 = new Vector2Int(12, 2);

        if (toMerge.Count >= 2)
        {
            print($"to merge {toMerge.Count}-- checking ball {checkingBall.index}");
            MergeGroups(toMerge, checkingBall);
        }
        else if (toMerge.Count <= 0)
        {
            print($"to merge {toMerge.Count}-- checking ball {checkingBall.index}");
            AddNewGroup(checkingBall);
        }
        else
        {
            print($"to merge {toMerge.Count}");
            toMerge[0].Add(checkingBall);
        }

    }
    private void AddNewGroup(MatrixBall checkingBall)
    {
        Group newGroup = new Group(checkingBall);
        print($"is new group nulll {newGroup == null}");
        newGroup.Add(checkingBall);
        Groups.Add(newGroup);
    }
    private void MergeGroups(List<Group> toMerge, MatrixBall checkingBall)
    {
        Groups.Add(JoinGroup(toMerge, checkingBall));
        foreach (var g in toMerge)
        {
            Groups.Remove(g);
            g.Destroy();
        }
    }
}
