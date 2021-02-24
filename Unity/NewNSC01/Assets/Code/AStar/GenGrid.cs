using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenGrid : MonoBehaviour
{
    GenNode[,] grid;
    public List<GenNode> FinalPath;
    public int gridSizeI = 2, gridSizeJ = 6;
    public GameObject currentNode;

    public int[] di = { 1, -1, 0, 0 }, dj = {0, 0, 1, -1};

    void Start()
    {
        gridSizeI = 3;
        gridSizeJ = 6;
        CreateGrid();
    }

    private void Update()
    {
        //printNode();
    }

    void CreateGrid()
    {
        grid = new GenNode[gridSizeI, gridSizeJ];
        for (int i = 0; i < gridSizeI; i++)
        {
            for (int j = 0; j < gridSizeJ; j++)
            {
                currentNode = findNode("Dot", i, j);
                if (currentNode == null)
                {
                    continue;
                }
                grid[i, j] = new GenNode(currentNode.transform.position, i, j);
            }
        }
    }
    GameObject findNode(string nodeName, int i, int j)
    {
        nodeName += i;
        nodeName += j;
        return GameObject.Find(nodeName);
    }
    public GenNode AccessNode(int i, int j) {
        return grid[i, j];
    }

    public List<GenNode> GetNeighborNodes(GenNode a_Node)
    {
        List<GenNode> NeighborNodes = new List<GenNode>();
        for(int k = 0; k < 4; k++)
        {
            int ii = a_Node.gridI + di[k], jj = a_Node.gridJ + dj[k];
            if (ii < 0 || jj < 0 || ii >= gridSizeI || jj >= gridSizeJ)
                continue;
            if (grid[ii, jj] == null)
                continue;
            NeighborNodes.Add(grid[ii, jj]);
        }
        return NeighborNodes;
    }

    void printNode()
    {
        if(FinalPath.Count < 1)
        {
            return;
        }
        foreach (GenNode x in FinalPath)
        {
            Debug.Log(x.gridI + " " + x.gridJ);
        }
    }
}
