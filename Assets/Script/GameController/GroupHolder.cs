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
        Groups.Add(JoinGroup(toMerge));
        foreach (var g in toMerge)
        {
            Groups.Remove(g);
            g.Destroy();
        }
    }
}
