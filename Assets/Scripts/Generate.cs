using System.Net;
using UnityEngine;

public class Generate : MonoBehaviour
{
    int totalRoomNum = 6; // Total number of room pool
    int roomCount = 6;    // Number of rooms to be placed
    int[] roomsPlaced;    // Rooms that are already placed
    Room[] roomsTree;
    int height = 50;
    int width = 50;
    int[,] map;           // map[-Z][X]
    int hallwayMark = -1;
    Vector2Int startPoint, endPoint;

    // Start is called before the first frame update


    private void Start()
    {
        map = new int[height, width];

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                map[i, j] = 0;
            }
        }

        roomsPlaced = new int[roomCount];

        for (int i = 0; i < roomsPlaced.Length; i++)
            roomsPlaced[i] = -1;

        roomsTree = new Room[roomCount];

        for (int i = 0; i < roomsTree.Length; i++)
            roomsTree[i] = null;

        SpawnRooms();


        StartPathFind();


        

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (map[i, j] < 0)
                {
                    map[i, j] = 3;
                    Instantiate(Resources.Load("Hallway"), new Vector3(j * 5, 0, i * -5), Quaternion.identity);
                }
                    
            }
        }

        for (int i = 0; i < height; i++)
        {
            string row = null;
            for (int j = 0; j < width; j++)
            {
                row = row + " " + map[i, j].ToString();
            }
            Debug.Log(row);
        }

        Debug.Log("pathFound!!!!!!!!!!!!!!!!!!!!!!!!!");
    }


    void SpawnRooms()
    {
        int cnt = 0;

        while (cnt < roomCount)
        {
            int selectedRoom = Random.Range(0, roomCount);

            if (IsUsed(selectedRoom))
                continue;

            

            GameObject room = Instantiate(Resources.Load("Rooms/Room" + selectedRoom)) as GameObject;

            Room roomData = room.GetComponent<Room>();

            while (!IsSuitable(room, roomData));

            roomsTree[cnt] = roomData;
            roomsPlaced[cnt] = selectedRoom;
            cnt++;

            Debug.Log(room.name);
        }
    }

    private bool IsSuitable(GameObject room, Room roomData)
    {
        int roomHeight = roomData.shape.GetLength(0);
        int roomWidht = roomData.shape.GetLength(1);

        int heightPlace = Random.Range(0, height);
        int widhtPlace = Random.Range(0, width);

        for (int i = 0; i < roomHeight; i++)
        {
            for (int j = 0; j < roomWidht; j++)
            {
                int z = heightPlace + i;
                int x = widhtPlace + j;

                if (z >= map.GetLength(0)) return false;
                if (x >= map.GetLength(1)) return false;

                if (map[z, x] == 1) return false;
            }
        }

        Debug.Log("suitable");

        for (int i = 0; i < roomHeight; i++)
        {
            for (int j = 0; j < roomWidht; j++)
            {
                int z = heightPlace + i;
                int x = widhtPlace + j;


                map[z, x] = roomData.shape[i, j];

                if(map[z, x] == 2)
                {
                    roomData.doorZ = z;
                    roomData.doorX = x;
                }
            }
        }

        room.transform.position = new Vector3(widhtPlace * 5, 0, heightPlace * -5);

        Debug.Log("inserted");

        return true;


    }



    bool IsUsed(int room)
    {
        for (int i = 0; i < roomCount; i++)
        {
            if (room == roomsPlaced[i])
            {
                return true;
            }
        }

        return false;
    }



    void StartPathFind()
    {
        for (int i = 0; i < roomsTree.Length; i++)
        {
            Vector2Int parent = new Vector2Int(roomsTree[i].doorZ, roomsTree[i].doorX);


            if (HasLeftChild(i))
            {
                Vector2Int leftChild = new Vector2Int(roomsTree[GetLeftChildIndex(i)].doorZ, roomsTree[GetLeftChildIndex(i)].doorX);
                startPoint = parent;
                endPoint = leftChild;
                FindShortestPath();
            }

            if (HasRightChild(i))
            {
                Vector2Int rightChild = new Vector2Int(roomsTree[GetRightChildIndex(i)].doorZ, roomsTree[GetRightChildIndex(i)].doorX);
                startPoint = parent;
                endPoint = rightChild;
                FindShortestPath();
            }


        }
    }

    int GetLeftChildIndex(int room)
    {
        return (2 * room) + 1;
    }

    int GetRightChildIndex(int room)
    {
        return (2 * room) + 2;
    }

    bool HasLeftChild(int room)
    {
        if ((2 * room) + 1 >= roomsTree.Length)
            return false;
        else
            return true;
    }

    bool HasRightChild(int room)
    {
        if ((2 * room) + 2 >= roomsTree.Length)
            return false;
        else
            return true;
    }


    void FindShortestPath()
    {
        Vector2Int currentPoint = startPoint;
        map[currentPoint.x, currentPoint.y] = hallwayMark;
        while (currentPoint != endPoint)
        {
            Vector2Int nextPoint = FindNextPoint(currentPoint);
            if (nextPoint == currentPoint)
            {
                Debug.Log("No path found!");
                break;
            }
            map[nextPoint.x, nextPoint.y] = hallwayMark;
            currentPoint = nextPoint;
        }
        map[currentPoint.x, currentPoint.y] = hallwayMark;
        hallwayMark--;
    }

    // Finds the next point in the path
    Vector2Int FindNextPoint(Vector2Int currentPoint)
    {
        Vector2Int nextPoint = currentPoint;

        // Check all neighboring points
        Vector2Int[] neighbors = {
            new Vector2Int(0, 1),  // Right
            new Vector2Int(0, -1), // Left
            new Vector2Int(1, 0),  // Up
            new Vector2Int(-1, 0)  // Down
        };

        int shortestDistance = int.MaxValue;
        foreach (Vector2Int neighbor in neighbors)
        {
            Vector2Int neighborPoint = currentPoint + neighbor;

            // Check if the neighbor is within bounds and reachable
            if (IsValidPoint(neighborPoint) && map[neighborPoint.x, neighborPoint.y] != 1 && map[neighborPoint.x, neighborPoint.y] != hallwayMark)
            {
                // Calculate the distance to the end point
                int distance = Mathf.Abs(neighborPoint.x - endPoint.x) + Mathf.Abs(neighborPoint.y - endPoint.y);

                // Update the next point if it has a shorter distance
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    nextPoint = neighborPoint;
                }
            }
        }

        return nextPoint;
    }

    // Checks if a point is within the matrix bounds
    bool IsValidPoint(Vector2Int point)
    {
        return point.x >= 0 && point.x < map.GetLength(0) && point.y >= 0 && point.y < map.GetLength(1);
    }


}