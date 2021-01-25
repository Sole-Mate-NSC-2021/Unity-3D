using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenNode
{
    public int gridI, gridJ;

    public Vector3 Position;

    public GenNode Parent;

    public int gCost, hCost;

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }
    public GenNode(Vector3 a_Position, int a_gridI, int a_gridJ)
    {
        Position = a_Position;
        gridI = a_gridI;
        gridJ = a_gridJ;
    }
}
