using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    GenGrid grid;

    public GameObject TargetObject;

    public GameObject NowObject;

    public int startI, startJ, TargetI = 0, TargetJ = 3;

    private void Awake()
    {
        grid = GetComponent<GenGrid>();
        
    }

    private void Update()
    {
        NowObject = GameObject.Find("astronaustBody").GetComponent<PlayerControl>().dotPointed;
        //Debug.Log(NowObject.name);
        startI = NowObject.name[3] - '0';
        startJ = NowObject.name[4] - '0';
        TargetObject = GameObject.Find("astronaustBody").GetComponent<PlayerControl>().dotHovered;
        if (TargetObject == null)
        {
            TargetI = TargetJ = 0;
        }
        else
        {
            TargetI = TargetObject.name[3] - '0';
            TargetJ = TargetObject.name[4] - '0';
        }
        //Debug.Log(startI + " " + startJ + " " + TargetI + " " + TargetJ);
        FindPath(startI, startJ, TargetI, TargetJ);
    }

    void FindPath(int a_StartI, int a_StartJ, int a_TargetI, int a_TargetJ)
    {
        GenNode StartNode = grid.AccessNode(a_StartI, a_StartJ);
        GenNode TargetNode = grid.AccessNode(a_TargetI, a_TargetJ);

        //Debug.Log(TargetNode.Position.ToString("F4"));

        List<GenNode> OpenList = new List<GenNode>();
        HashSet<GenNode> ClosedList = new HashSet<GenNode>();

        OpenList.Add(StartNode);

        while (OpenList.Count > 0) {
            GenNode CurrentNode = OpenList[0];
            for (int i = 1; i < OpenList.Count; i++)
            {
                if (OpenList[i].fCost < CurrentNode.fCost || OpenList[i].fCost == CurrentNode.fCost && OpenList[i].hCost < CurrentNode.hCost)
                {
                    CurrentNode = OpenList[i];
                }
            }
            //Debug.Log("HAHA " + CurrentNode.gridI + " " + CurrentNode.gridJ);
            OpenList.Remove(CurrentNode);
            ClosedList.Add(CurrentNode);
        
            if(CurrentNode == TargetNode)
            {
                GetFinalPath(StartNode, TargetNode);
            }

            foreach (GenNode NeighborNode in grid.GetNeighborNodes(CurrentNode))
            {
                //Debug.Log("Neigh " + NeighborNode.gridI + " " + NeighborNode.gridJ);
                if (ClosedList.Contains(NeighborNode))
                {
                    continue;
                }
                int MoveCost = CurrentNode.gCost + GetManhattenDistance(CurrentNode, NeighborNode);

                //Debug.Log("Cost : " + MoveCost + " " + NeighborNode.gCost);

                if(MoveCost < NeighborNode.gCost || !OpenList.Contains(NeighborNode))
                {
                    NeighborNode.gCost = MoveCost;
                    NeighborNode.hCost = GetManhattenDistance(NeighborNode, TargetNode);
                    NeighborNode.Parent = CurrentNode;

                    if (!OpenList.Contains(NeighborNode))
                    {
                        OpenList.Add(NeighborNode);
                    }

                }
            }
        }

        void GetFinalPath(GenNode a_StartNode, GenNode a_EndNode)
        {
            List<GenNode> FinalPath = new List<GenNode>();
            GenNode CurrentNode = a_EndNode;

            while(CurrentNode != a_StartNode)
            {
                FinalPath.Add(CurrentNode);
                CurrentNode = CurrentNode.Parent;
            }
            FinalPath.Add(a_StartNode);
            FinalPath.Reverse();

            grid.FinalPath = FinalPath;
        }

        int GetManhattenDistance(GenNode a_NodeA, GenNode a_NodeB)
        {
            return Mathf.Abs(a_NodeA.gridI - a_NodeB.gridI) + Mathf.Abs(a_NodeA.gridJ - a_NodeB.gridJ);
        }

    }

}
