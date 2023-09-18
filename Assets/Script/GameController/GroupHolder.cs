using System.Collections.Generic;
using UnityEngine;
using com.soha.bridge;
using System;
using UnityEngine.UIElements;
using System.Linq;
[Serializable]
public class GroupHolder : MonoBehaviourSingleton<GroupHolder>
{
    [SerializeField] private List<Group> groups = new List<Group>();
    [SerializeField] private GameObject groupBallContainer;

    private BoardManager boardManager => BoardManager.Instance;
    private int index;
    public List<Group> GroupsHolder { get => groups; set => groups = value; }
    public Group JoinGroup(List<Group> groups)
    {
        Group joinedGr = new Group();
        foreach (var g in groups)
        {
            joinedGr.Add(g);
        }
        return joinedGr;
    }
    private List<Group> GetConnectingGroups(MatrixBall checkingBall)
    {
        List<Group> connetingGroups = FindFromExistingGroups(checkingBall);
        if (connetingGroups.Count == 0)
        {
            print($"connnecting count ==0 {checkingBall.index}");
            var newGroup = AddGroup();
            connetingGroups.Add(newGroup);
        }
        return connetingGroups;
    }

    private List<Group> FindFromExistingGroups(MatrixBall checkingBall)
    {
        var connetingGroups = new List<Group>();
        foreach (var g in GroupsHolder)
        {
            if (g.HasConnectWith(checkingBall))
            {
                connetingGroups.Add(g);
            }
        }

        return connetingGroups;
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
        GroupsHolder.Clear();
    }
    public void MakeBallFall()
    {
        List<Group> toRemove = new List<Group>();
        for (int i = 0; i < groups.Count; i++)
        {
            // print($"groups[i].CanDrop() {groups[i].CanDrop()}");
            print($"groups.Count---!!!{groups.Count}");
            print($"lowest index of group---!!!{groups[i].LowestIndex}");
            if (groups[i].LowestIndex <= 0)
                continue;
            print($" group {i} not connect with other group---!!!");
            groups[i].fallingBalls.ForEach(item => ControllWhenFall(item));
            toRemove.Add(groups[i]);
        }
        groups.RemoveAll(gr => toRemove.Any(group => groups.Contains(group)));
    }
    private void ControllWhenFall(MatrixBall matrixBall)
    {
        print("falling");
        boardManager.RemoveFromMatrixList(matrixBall);
        Destroy(matrixBall.gameObject, 1f);
    }
    public void GroupBall(MatrixBall checkingBall)
    {
        List<Group> connectingGroups = GetConnectingGroups(checkingBall);
        AddBall(checkingBall, connectingGroups);
    }
    private void AddBall(MatrixBall checkingBall, List<Group> connectingGroups)
    {
        var connectingGroup = connectingGroups.FirstOrDefault(x => x != null);
        connectingGroup.Add(checkingBall);
        if (connectingGroups.Count >= 2)
        {
            print($"to merge {connectingGroups.Count}-- checking ball {checkingBall.index}");
            Merge(connectingGroups);
        }

    }
    private Group AddGroup()
    {
        Group group = new Group();
        print($"is new group nulll {group == null}");
        GroupsHolder.Add(group);
        return group;
    }


    private void Merge(List<Group> groups)
    {
        Group mergedGroup = JoinGroup(groups);
        GroupsHolder.Add(mergedGroup);
        RemoveFromHolder(groups);
    }

    private void RemoveFromHolder(List<Group> toMerge)
    {
        foreach (var g in toMerge)
        {
            GroupsHolder.Remove(g);
            g.Destroy();
        }
    }
}
