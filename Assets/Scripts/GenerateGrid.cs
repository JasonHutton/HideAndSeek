using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateGrid : MonoBehaviour
{
    private Vector3 TopLeft;
    private Vector3 BottomRight;
    public float distance;

    // Start is called before the first frame update
    void Start()
    {
        TopLeft = new Vector3(-9, 1, 9);
        BottomRight = new Vector3(9, 1, -9);

        //transform.position = BottomRight;
        Generate(TopLeft, BottomRight, distance);
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Generate(Vector3 topLeft, Vector3 bottomRight, float stepDist)
    {
        List<Node> nodes = new List<Node>();
        
        nodes.Add(new Node(new Vector3(0, 1, 0)));
        for (int k = 0; k < 50; k++)
        {
            for (int i = 0; i < 50; i++)
            {
                Node node;
                node = new Node(new Vector3(-stepDist * i, 1, stepDist * k));

                if (IsOutsideMap(node.Position))
                {
                    break;
                }

                nodes.Add(node);
            }
        }
        for (int k = 0; k < 50; k++)
        {
            for (int i = 0; i < 50; i++)
            {
                Node node;
                node = new Node(new Vector3(stepDist * i, 1, stepDist * k));

                if (IsOutsideMap(node.Position))
                {
                    break;
                }

                nodes.Add(node);
            }
        }
        for (int k = 0; k < 50; k++)
        {
            for (int i = 0; i < 50; i++)
            {
                Node node;
                node = new Node(new Vector3(-stepDist * i, 1, -stepDist * k));

                if (IsOutsideMap(node.Position))
                {
                    break;
                }

                nodes.Add(node);
            }
        }
        for (int k = 0; k < 50; k++)
        {
            for (int i = 0; i < 50; i++)
            {
                Node node;
                node = new Node(new Vector3(stepDist * i, 1, -stepDist * k));

                if (IsOutsideMap(node.Position))
                {
                    break;
                }

                nodes.Add(node);
            }
        }
        /*
        nodes.Add(new Node(new Vector3(0, 1, 0)));
        nodes.Add(new Node(new Vector3(-0.5f, 1, 0)));
        nodes.Add(new Node(new Vector3(-1, 1, 0)));
        nodes.Add(new Node(new Vector3(-1.5f, 1, 0)));
        nodes.Add(new Node(new Vector3(-2, 1, 0)));
        nodes.Add(new Node(new Vector3(-2.5f, 1, 0)));
        nodes.Add(new Node(new Vector3(-3, 1, 0)));
        nodes.Add(new Node(new Vector3(-3.5f, 1, 0)));
        nodes.Add(new Node(new Vector3(-4f, 1, 0)));
        nodes.Add(new Node(new Vector3(-4.5f, 1, 0)));
        nodes.Add(new Node(new Vector3(-5f, 1, 0)));
        */
        //nodes.Add(new Node(topLeft));
        //Debug.DrawLine(nodes[0].Position, nodes[10].Position, Color.white, 30.0f);
        //while()

        //IsInsideBarrier(nodes[5].Position);
        //IsInsideBarrier(nodes[10].Position);
    }

    // Casts a ray down from above the point, and checks if it collides.
    // Ray's range is enough that it won't collide if there isn't a wall, but will if there is.
    static public bool IsInsideBarrier(Vector3 point)
    {
        Vector3 down = new Vector3(0.0f, -2.0f, 0.0f); ;
        Vector3 pointAbove = point;
        pointAbove.y = 2;

        bool hit = Physics.Raycast(pointAbove, down, 1.25f);
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

        return hit;

        //return Physics.Raycast(pointAbove, down, 1.25f);
    }

    static public bool IsOutsideMap(Vector3 point, bool showLine = false)
    {
        Vector3 down = new Vector3(0.0f, -2.0f, 0.0f); ;
        Vector3 pointAbove = point;
        pointAbove.y = 2;

        bool hit = Physics.Raycast(pointAbove, down, 5.0f);
        if (!hit && showLine)
        {
            Debug.DrawLine(pointAbove, point, Color.magenta, 30.0f);
        }

        return !hit;

        return !Physics.Raycast(pointAbove, down, 5.0f);
    }
}

public class Node
{
    public Node North;
    public Node South;
    public Node East;
    public Node West;
    public Vector3 Position;
    public bool Passable;
    /*public Node UpLeft;
    public Node UpRight;
    public Node DownLeft;
    public Node DownRight;*/

    public Node(Vector3 pos)
    {
        Position = pos;
        if(GenerateGrid.IsOutsideMap(pos) || GenerateGrid.IsInsideBarrier(pos))
        {
            Passable = false;
        }
    }
}