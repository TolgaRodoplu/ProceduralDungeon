using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Generate : MonoBehaviour
{
    int totalRoomNum = 6; // Total number of room pool
    int roomCount = 6;    // Number of rooms to be placed
    int[] roomsPlaced;    // Rooms that are already placed
    int height = 100;
    int width = 100;
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
        SpawnRooms();
    }


    void SpawnRooms()
    {
        int cnt = 0;

        while (cnt < roomCount)
        {
            int selectedRoom = Random.Range(0, totalRoomNum);

            if (IsUsed(selectedRoom))
            {
                continue;
            }

            Debug.Log("NotUsed");

            roomsPlaced[cnt] = selectedRoom;

            cnt++;
        }
    }

    //private bool IsSuitable(GameObject room)
    //{
    //    Room roomData = room.GetComponent<Room>();
    //    int roomHeight = roomData.shape.GetLength(0);
    //    int roomWidht = roomData.shape.GetLength(1);

    //    int z = Random.Range(0, height);
    //    int x = Random.Range(0, width);

    //    for (int i = 0; i < roomHeight; i++)
    //    {
    //        for(int j = 0; j < roomWidht; j++)
    //        {
                
    //        }
    //    }

    //    Debug.Log("suitable");

        
    //}

    bool IsUsed(int room)
    {
        for(int i = 0; i < roomsPlaced.Length; i++) 
        {
            if(room == roomsPlaced[i]) 
            {
                return true;
            }
        }

        return false;
    }

    
}
