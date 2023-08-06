using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    private int numberOfRows = 20;
    [SerializeField]
    private int numberOfColumns = 20;
    [SerializeField]
    private float gridCellSize = 2.0f;
    [SerializeField]
    private bool showGrid = true;
    [SerializeField]
    private bool showObstacleBlocks = true;

    private Vector3 origin = new Vector3();
    private GameObject[] obstacleList;
    private Node[,] nodes{ get; set; }
    /* [,] means it's a 2D array
     {get; set;} is called auto property.
    get & set are accessors & since Node has both these properties we can get & set info into it even though Node is a private variable/field*/

    public Vector3 Origin { get { return origin; } }

    public void Awake()
    {
        obstacleList = GameObject.FindGameObjectsWithTag("Obstacle");
        InitializeNodes();
        CalculateObstacles();
    }

    private void InitializeNodes()
    {
        nodes = new Node[numberOfRows, numberOfColumns];
        int index = 0;
        for(int i=0;i<numberOfRows;i++)
        {
            for(int j=0;j<numberOfColumns;j++)
            {
                Vector3 cellPosition = GetGridCellCenter(index);
                Node node = new Node(cellPosition);
                nodes[i, j] = node;

                index++;
            }
        }
    }

    public Vector3 GetGridCellCenter(int index)
    {
        Vector3 cellPosition = GetGridCellPositionAtIndex(index);
        cellPosition.x += (gridCellSize / 2.0f);
        cellPosition.y += (gridCellSize / 2.0f);

        return cellPosition;
    }

    private Vector3 GetGridCellPositionAtIndex(int index)
    {
        int row = GetRowOfIndex(index);
        int col = GetColumnOfIndex(index);
        float xPositionInGrid = row * gridCellSize;
        float zPositionInGrid = col * gridCellSize;

        return Origin + new Vector3(xPositionInGrid, 0.0f, zPositionInGrid);
    }

    private int GetRowOfIndex(int index)
    {
        int row = index / numberOfColumns;
        return row;
    }

    private int GetColumnOfIndex(int index)
    {
        int col = index % numberOfColumns;
        return col;
    }

    private bool IsInBounds(Vector3 position)
    {
        float width = numberOfColumns * gridCellSize;
        float height = numberOfRows * gridCellSize;

        return (position.x >= Origin.x && position.x <= Origin.x + width
            && position.z >= Origin.z && position.z <= Origin.z + height);
    }

    public int GetGridIndex(Vector3 position)
    {
        if (!IsInBounds(position)) { return -1; }

        position -= Origin;
        int row = (int)(position.x / gridCellSize);
        int column = (int)(position.z / gridCellSize);

        return (row * numberOfColumns + column);
    }

    private void CalculateObstacles()
    {
        if(obstacleList!=null && obstacleList.Length>0)
        {
            foreach(GameObject data in obstacleList)
            {
                int indexCell = GetGridIndex(data.transform.position);
                int column = GetColumnOfIndex(indexCell);
                int row = GetRowOfIndex(indexCell);

                nodes[row, column].MarkAsObstacle();
            }
        }
    }

    public void GetNeighbors(Node node, ArrayList neighbors)
    {
        Vector3 neighborPosition = node.position;
        int neighborIndex = GetGridIndex(neighborPosition);

        int row = GetRowOfIndex(neighborIndex);
        int column = GetColumnOfIndex(neighborIndex);

        // Top
        int nextNodeRow = row - 1;
        int nextNodeColumn = column;
        AssignNeighbor(nextNodeRow, nextNodeColumn, neighbors);
        // Bottom
        nextNodeRow = row + 1;
        nextNodeColumn = column;
        AssignNeighbor(nextNodeRow, nextNodeColumn, neighbors);
        // Left
        nextNodeRow = row;
        nextNodeColumn = column - 1;
        AssignNeighbor(nextNodeRow, nextNodeColumn, neighbors);
        // Right
        nextNodeRow = row;
        nextNodeColumn = column + 1;
        AssignNeighbor(nextNodeRow, nextNodeColumn, neighbors);
    }

    // Check the neighbor. If it's not an obstacle assign as neighbor
    private void AssignNeighbor(int row, int column, ArrayList neighbors)
    {
        if(row!=-1 && column!=-1 && row<numberOfRows && column<numberOfColumns)
        {
            Node nodeToAdd = nodes[row, column];
            if(!nodeToAdd.bObstacle)
            {
                neighbors.Add(nodeToAdd);
            }
        }
    }

    // Show Debug Grids & obstacles inside the editor
    private void OnDrawGizmos()
    {
        if(showGrid)
        {
            DebugDrawGrid();
        }

        Gizmos.DrawSphere(transform.position, 0.5f);
    }

    private void DebugDrawGrid()
    {
        Color drawColor;

        if (nodes == null) { return; }

        drawColor = Color.blue;
        foreach (Node y in nodes)
        {
            //drawColor = (y.bObstacle && showObstacleBlocks) ? Color.red : Color.blue; 

            Vector3 position = y.position;
            Vector3 topLeftCorner = position + new Vector3(-gridCellSize * 0.5f, 0f, -gridCellSize * 0.5f);
            Vector3 topRightCorner = position + new Vector3(-gridCellSize * 0.5f, 0f, +gridCellSize * 0.5f);
            Vector3 bottomLeftCorner = position + new Vector3(+gridCellSize * 0.5f, 0f, -gridCellSize * 0.5f);
            Vector3 bottomRightCorner = position + new Vector3(+gridCellSize * 0.5f, 0f, +gridCellSize * 0.5f);

            Debug.DrawLine(topLeftCorner, topRightCorner, drawColor);
            Debug.DrawLine(topLeftCorner, bottomLeftCorner, drawColor);
            Debug.DrawLine(topRightCorner, bottomRightCorner, drawColor);
            Debug.DrawLine(bottomRightCorner, bottomLeftCorner, drawColor);
        }

        // Draw obstacle cubes at last
        if (!showObstacleBlocks) { return; }

        drawColor = Color.red;
        foreach (Node y in nodes)
        {
            if (!y.bObstacle) { continue; }

            Vector3 position = y.position;
            Vector3 topLeftCorner = position + new Vector3(-gridCellSize * 0.5f, 0f, -gridCellSize * 0.5f);
            Vector3 topRightCorner = position + new Vector3(-gridCellSize * 0.5f, 0f, +gridCellSize * 0.5f);
            Vector3 bottomLeftCorner = position + new Vector3(+gridCellSize * 0.5f, 0f, -gridCellSize * 0.5f);
            Vector3 bottomRightCorner = position + new Vector3(+gridCellSize * 0.5f, 0f, +gridCellSize * 0.5f);

            Debug.DrawLine(topLeftCorner, topRightCorner, drawColor);
            Debug.DrawLine(topLeftCorner, bottomLeftCorner, drawColor);
            Debug.DrawLine(topRightCorner, bottomRightCorner, drawColor);
            Debug.DrawLine(bottomRightCorner, bottomLeftCorner, drawColor);
        }
    }
}
