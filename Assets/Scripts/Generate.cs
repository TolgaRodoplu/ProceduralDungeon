using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Generate : MonoBehaviour
{
    int totalRoomNum = 6; // Total number of room pool
    int roomCount = 6;    // Number of rooms to be placed
    int[] roomsPlaced;    // Rooms that are already placed
    int height = 30;
    int width = 30;
    int[,] map;           // map[-Z][X]

    // Start is called before the first frame update


    private void Start()
    {
        map = new int[height, width];

        for(int i = 0; i < height; i++)
        {
            for(int j = 0; j < width; j++)
            {
                map[i, j] = 0;
            }
        }

        roomsPlaced = new int[roomCount];

        for(int i = 0;i < roomCount; i++)
            roomsPlaced[i] = -1;

        SpawnRooms();
    }


    void SpawnRooms()
    {
        int cnt = 0;

        while (cnt < roomCount)
        {
            int selectedRoom = Random.Range(0, roomCount);

            if (IsUsed(selectedRoom))
                continue;

            roomsPlaced[cnt] = selectedRoom;
            cnt++;

            GameObject room = Instantiate(Resources.Load("Rooms/Room" + selectedRoom)) as GameObject;

            while(!IsSuitable(room)) { }

            Debug.Log(room.name);
        }
    }

    private bool IsSuitable(GameObject room)
    {
        Room roomData = room.GetComponent<Room>();
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

                if(z >= map.GetLength(0)) return false;
                if(x >= map.GetLength(1)) return false;

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
            }
        }

        room.transform.position = new Vector3(widhtPlace * 5, 0, heightPlace * -5);

        Debug.Log("inserted");

        return true;


    }

    bool IsUsed(int room)
    {
        for(int i = 0; i < roomCount; i++) 
        {
            if(room == roomsPlaced[i]) 
            {
                return true;
            }
        }

        return false;
    }

    
}
