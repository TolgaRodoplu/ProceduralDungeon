using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Generate : MonoBehaviour
{
    public Intro intro;
    int totalRoomNum = 6; // Total number of room pool
    int roomCount = 6;    // Number of rooms to be placed
    int[] roomsPlaced;    // Rooms that are already placed
    Room[] roomsTree;
    int height = 30;
    int width = 30;
    int[,] map;           // map[-Z][X]
    int hallwayMark = -1;
    Vector2Int startPoint, endPoint;

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
                if(map[i, j] < 0 || map[i, j] == 2)
                {
                    Vector2Int currentPoint = new Vector2Int(i, j);

                    if (IsIsolatedPoint(currentPoint))
                        SceneManager.LoadScene("Test");
                    else
                        PlaceHallway(currentPoint);

                }
            }
        }


        //for (int i = 0; i < height; i++)
        //{
        //    for (int j = 0; j < width; j++)
        //    {
        //        if(map[i, j] < 0) //if regular hallway
        //        {
        //            Instantiate(Resources.Load("Hallway"), new Vector3(j * 5, 0, i * -5), Quaternion.identity);

        //            if (IsValidPoint(new Vector2Int(i - 1, j))) //top 
        //            {
        //                if(map[i - 1, j] == 0 || map[i - 1, j] == 1 || map[i - 1, j] == 4)
        //                {
        //                    Instantiate(Resources.Load("HallwayWall"), new Vector3(j * 5, 0, (i * -5) + 2.5f), Quaternion.Euler(0, 0, 0));
        //                }
                        
        //            }
        //            else
        //                Instantiate(Resources.Load("HallwayWall"), new Vector3(j * 5, 0, (i * -5) + 2.5f), Quaternion.Euler(0, 0, 0));


        //            if (IsValidPoint(new Vector2Int(i + 1, j))) //bottom
        //            {
        //                if (map[i + 1, j] == 0 || map[i + 1, j] == 1 || map[i + 1, j] == 4)
        //                {
        //                    Instantiate(Resources.Load("HallwayWall"), new Vector3(j * 5, 0, (i * -5) - 2.5f), Quaternion.Euler(0, 180, 0));
        //                }
                        
        //            }
        //            else
        //                Instantiate(Resources.Load("HallwayWall"), new Vector3(j * 5, 0, (i * -5) - 2.5f), Quaternion.Euler(0, 180, 0));

        //            if (IsValidPoint(new Vector2Int(i, j + 1)) ) //right
        //            {
        //                if (map[i, j + 1] == 0 || map[i, j + 1] == 1 || map[i, j + 1] == 4)
        //                {
        //                    Instantiate(Resources.Load("HallwayWall"), new Vector3((j * 5) + 2.5f, 0, i * -5), Quaternion.Euler(0, 90, 0));
        //                }
                        
        //            }
        //            else
        //                Instantiate(Resources.Load("HallwayWall"), new Vector3((j * 5) + 2.5f, 0, i * -5), Quaternion.Euler(0, 90, 0));

        //            if (IsValidPoint(new Vector2Int(i, j - 1))) //left 
        //            {
        //                if (map[i, j - 1] == 0 || map[i, j - 1] == 1 || map[i, j - 1] == 4)
        //                {
        //                    Instantiate(Resources.Load("HallwayWall"), new Vector3((j * 5) - 2.5f, 0, i * -5), Quaternion.Euler(0, 270, 0));
        //                }
                        
        //            }
        //            else
        //                Instantiate(Resources.Load("HallwayWall"), new Vector3((j * 5) - 2.5f, 0, i * -5), Quaternion.Euler(0, 270, 0));
        //        }

        //        else if(map[i, j] == 2) //if it is a gate
        //        {
        //            Debug.Log("GateFound");
        //            Instantiate(Resources.Load("Hallway"), new Vector3(j * 5, 0, i * -5), Quaternion.identity);

        //            if (IsValidPoint(new Vector2Int(i - 1, j))) //top 
        //            {
        //                if (map[i - 1, j] == 0 || map[i - 1, j] == 1)
        //                {
        //                    Instantiate(Resources.Load("HallwayWall"), new Vector3(j * 5, 0, (i * -5) + 2.5f), Quaternion.Euler(0, 0, 0));
        //                }
                        
        //            }
        //            else
        //                Instantiate(Resources.Load("HallwayWall"), new Vector3(j * 5, 0, (i * -5) + 2.5f), Quaternion.Euler(0, 0, 0));

        //            if (IsValidPoint(new Vector2Int(i + 1, j))) //bottom
        //            {
        //                if (map[i + 1, j] == 0 || map[i + 1, j] == 1)
        //                {
        //                    Instantiate(Resources.Load("HallwayWall"), new Vector3(j * 5, 0, (i * -5) - 2.5f), Quaternion.Euler(0, 180, 0));
        //                }
                        
        //            }
        //            else
        //                Instantiate(Resources.Load("HallwayWall"), new Vector3(j * 5, 0, (i * -5) - 2.5f), Quaternion.Euler(0, 180, 0));

        //            if (IsValidPoint(new Vector2Int(i, j + 1))) //right
        //            {
        //                if (map[i, j + 1] == 0 || map[i, j + 1] == 1)
        //                {
        //                    Instantiate(Resources.Load("HallwayWall"), new Vector3((j * 5) + 2.5f, 0, i * -5), Quaternion.Euler(0, 90, 0));
        //                }
                        
        //            }
        //            else
        //                Instantiate(Resources.Load("HallwayWall"), new Vector3((j * 5) + 2.5f, 0, i * -5), Quaternion.Euler(0, 90, 0));

        //            if (IsValidPoint(new Vector2Int(i, j - 1))) //left 
        //            {
        //                if (map[i, j - 1] == 0 || map[i, j - 1] == 1)
        //                {
        //                    Instantiate(Resources.Load("HallwayWall"), new Vector3((j * 5) - 2.5f, 0, i * -5), Quaternion.Euler(0, 270, 0));
        //                }
                        
        //            }
        //            else
        //                Instantiate(Resources.Load("HallwayWall"), new Vector3((j * 5) - 2.5f, 0, i * -5), Quaternion.Euler(0, 270, 0));
        //        }
                
                    
        //    }

            
        //}

        intro.StartStartIntro();

        Destroy(gameObject);
    }

    private void PlaceHallway(Vector2Int currentPoint)
    {
        Instantiate(Resources.Load("Hallway"), new Vector3(currentPoint.y * 5, 0, currentPoint.x * -5), Quaternion.identity);

        Vector2Int[] neighbors = {
                     new Vector2Int(-1, 0),  // Top 0
                     new Vector2Int(0, 1),  // Right 1
                     new Vector2Int(1, 0),  // Bottom 2
                     new Vector2Int(0, -1) // Left 3
                     };

        for(int i = 0; i < neighbors.Length; i++)
        {
            Vector2Int neighborPoint = currentPoint + neighbors[i];

            if(IsValidPoint(neighborPoint))
            {
                if(map[neighborPoint.x, neighborPoint.y] == 0 || map[neighborPoint.x, neighborPoint.y ] == 1)
                {
                    Instantiate(Resources.Load("HallwayWall"), Vector3GetPosition(currentPoint, i), QuaternionGetRotation(i));
                    continue;
                }
                else if(map[neighborPoint.x, neighborPoint.y] == 4)
                {
                    if(map[currentPoint.x, currentPoint.y] != 2)
                    {
                        Instantiate(Resources.Load("HallwayWall"), Vector3GetPosition(currentPoint, i), QuaternionGetRotation(i));
                        continue;
                    }
                }
            }
            else
            {
                Instantiate(Resources.Load("HallwayWall"), Vector3GetPosition(currentPoint, i), QuaternionGetRotation(i));
                continue;
            }
        }
    }

    private Vector3 Vector3GetPosition(Vector2Int point, int index) 
    {
        if (index == 0) //top
            return new Vector3(point.y * 5, 0, (point.x * -5) + 2.5f);

        else if (index == 1) //right
            return new Vector3((point.y * 5) + 2.5f, 0, point.x * -5);

        else if (index == 2) //bottom
            return new Vector3(point.y * 5, 0, (point.x * -5) - 2.5f);

        else if (index == 3)
            return new Vector3((point.y * 5) - 2.5f, 0, point.x * -5);

        else
            return Vector3.zero;

    }
     
    private Quaternion QuaternionGetRotation(int index) 
    {
        return Quaternion.Euler(0, index * 90, 0);
    }

    void SpawnRooms()
    {
        int cnt = 0;

        while (cnt < roomCount)
        {
            int selectedRoom = Random.Range(0, totalRoomNum);

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

                if (map[z, x] == 1 || map[z, x] == 4 || map[z, x] == 2) return false;

                

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

        if (map[currentPoint.x, currentPoint.y] != 2)
            map[currentPoint.x, currentPoint.y] = hallwayMark;

        while (currentPoint != endPoint)
        {
            Vector2Int nextPoint = FindNextPoint(currentPoint);
            if (nextPoint == currentPoint)
            {
                Debug.Log("No path found!");
                break;
            }
            if (map[nextPoint.x, nextPoint.y] != 2)
                map[nextPoint.x, nextPoint.y] = hallwayMark;
            else
                Debug.Log("GateFound");

            currentPoint = nextPoint;
        }

        if (map[currentPoint.x, currentPoint.y] != 2)
            map[currentPoint.x, currentPoint.y] = hallwayMark;
        else
            Debug.Log("GateFound");

        hallwayMark--;
    }

    Vector2Int FindNextPoint(Vector2Int currentPoint)
    {
        Vector2Int nextPoint = currentPoint;

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

            if (IsValidPoint(neighborPoint) && map[neighborPoint.x, neighborPoint.y] != 1 && map[neighborPoint.x, neighborPoint.y] != hallwayMark && map[neighborPoint.x, neighborPoint.y] != 4)
            {
                int distance = Mathf.Abs(neighborPoint.x - endPoint.x) + Mathf.Abs(neighborPoint.y - endPoint.y);

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

    bool IsIsolatedPoint(Vector2Int currentPoint)
    {
        Vector2Int[] neighbors = {
                     new Vector2Int(0, 1),  // Right
                     new Vector2Int(0, -1), // Left
                     new Vector2Int(-1, 0),  // Up
                     new Vector2Int(1, 0)  // Down
                     };

        foreach (Vector2Int neighbor in neighbors)
        {
            Vector2Int neighborPoint = currentPoint + neighbor;
            if (!IsValidPoint(neighborPoint))
                continue;

            // Check if the neighbor is within bounds and reachable
            if (map[neighborPoint.x, neighborPoint.y] < 0 || map[neighborPoint.x, neighborPoint.y] == 2)
            {
                return false;
            }
        }

        return true;
    }

}