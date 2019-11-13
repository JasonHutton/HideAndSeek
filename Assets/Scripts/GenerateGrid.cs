using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateGrid : MonoBehaviour
{
    private Vector3 TopLeft;
    private Vector3 BottomRight;
    public float distance;

    private List<Node> nodes;

    // Start is called before the first frame update
    void Start()
    {
        TopLeft = new Vector3(-9, 1, 9);
        BottomRight = new Vector3(9, 1, -9);

        nodes = new List<Node>();

        Generate(TopLeft, BottomRight, distance);

        ConnectNodes(nodes, distance);// * 1.5f);
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConnectNodes(List<Node> nodes, float radius)
    {
        for(int i = 0;i < nodes.Count;i++)
        { 
            for(int k = 0;k < nodes.Count;k++)
            {
                float distance = Vector3.Distance(nodes[i].Position, nodes[k].Position);
                if (distance <= 0.000f)
                    continue;

                if (distance <= radius)
                {
                    nodes[i].ConnectNode(nodes[k]);
                }
            }
        }
    }

    // This is kind of a mess, but it works.
    public void Generate(Vector3 topLeft, Vector3 bottomRight, float stepDist)
    {
        int maxNodes = 5000;
        nodes.Add(new Node(new Vector3(0, 1, 0)));
        for (int k = 0; k < maxNodes; k++)
        {
            for (int i = 0; i < maxNodes; i++)
            {
                Node node;
                node = new Node(new Vector3(-stepDist * i, 1, stepDist * k));

                if (IsOutsideMap(node.Position))
                {
                    break;
                }

                if (Vector3.Distance(nodes[k].Position, node.Position) <= 0.000f)
                    continue;
                    
                nodes.Add(node);
            }
        }
        for (int k = 0; k < maxNodes; k++)
        {
            for (int i = 0; i < maxNodes; i++)
            {
                Node node;
                node = new Node(new Vector3(stepDist * i, 1, stepDist * k));

                if (IsOutsideMap(node.Position))
                {
                    break;
                }

                if (Vector3.Distance(nodes[k].Position, node.Position) <= 0.000f)
                    continue;

                nodes.Add(node);
            }
        }
        for (int k = 0; k < maxNodes; k++)
        {
            for (int i = 0; i < maxNodes; i++)
            {
                Node node;
                node = new Node(new Vector3(-stepDist * i, 1, -stepDist * k));

                if (IsOutsideMap(node.Position))
                {
                    break;
                }

                if (Vector3.Distance(nodes[k].Position, node.Position) <= 0.000f)
                    continue;

                nodes.Add(node);
            }
        }
        for (int k = 0; k < maxNodes; k++)
        {
            for (int i = 0; i < maxNodes; i++)
            {
                Node node;
                node = new Node(new Vector3(stepDist * i, 1, -stepDist * k));

                if (IsOutsideMap(node.Position))
                {
                    break;
                }

                if (Vector3.Distance(nodes[k].Position, node.Position) <= 0.000f)
                    continue;

                nodes.Add(node);
            }
        }
    }

    // Casts a ray down from above the point, and checks if it collides.
    // Ray's range is enough that it won't collide if there isn't a wall, but will if there is.
    static public bool IsInsideBarrier(Vector3 point)
    {
        Vector3 down = new Vector3(0.0f, -2.0f, 0.0f); ;
        Vector3 pointAbove = point;
        pointAbove.y = 2;

        /*bool hit = Physics.Raycast(pointAbove, down, 1.25f);
        if (hit)
        {
            Debug.DrawLine(pointAbove, point, Color.red, 30.0f);
        }
        else
        {
            if (!IsOutsideMap(point, false))
            {
                Debug.DrawLine(pointAbove, point, Color.green, 30.0f);
            }
        }

        return hit;*/

        return Physics.Raycast(pointAbove, down, 1.25f);
    }

    static public bool IsOutsideMap(Vector3 point, bool showLine = false)
    {
        Vector3 down = new Vector3(0.0f, -2.0f, 0.0f); ;
        Vector3 pointAbove = point;
        pointAbove.y = 2;

        /*bool hit = Physics.Raycast(pointAbove, down, 5.0f);
        if (!hit && showLine)
        {
            Debug.DrawLine(pointAbove, point, Color.magenta, 30.0f);
        }

        return !hit;*/

        return !Physics.Raycast(pointAbove, down, 5.0f);
    }
}

public class Node
{
    private List<Node> connectedNodes;
    public Vector3 Position;
    public bool Passable;

    public Node(Vector3 pos)
    {
        Passable = true;
        connectedNodes = new List<Node>();

        Position = pos;
        if(GenerateGrid.IsOutsideMap(pos) || GenerateGrid.IsInsideBarrier(pos))
        {
            Passable = false;
        }
    }

    public void ConnectNode(Node node)
    {
        if (!Passable)
            return;

        foreach (Node connectedNode in connectedNodes)
        {
            if (!connectedNode.Passable)
                return;

            if (Vector3.Distance(connectedNode.Position, node.Position) <= 0.000f)
                return;
        }

        connectedNodes.Add(node);
    }
}