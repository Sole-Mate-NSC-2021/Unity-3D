using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public int gridX, gridY;

    public bool isWall;

    public Vector3 Position;

    public Node Parent;

    public int gCost, hCost;

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }
    public Node(bool a_isWall, Vector3 a_Position, int a_gridX, int a_gridY)
    {
        isWall = a_isWall;
        Position = a_Position;
        gridX = a_gridX;
        gridY = a_gridY;
    }
}
