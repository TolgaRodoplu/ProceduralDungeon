using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Generate : MonoBehaviour
{
    int RoomCount = 20;
    float spawnArea = 15f;

    // Start is called before the first frame update


    private void Start()
    {
        StartCoroutine(SpawnRooms());        
    }


    IEnumerator SpawnRooms()
    {
        var cnt = RoomCount;

        while (cnt > 0) 
        {
            var rand = Random.Range(2, 9);
            var path = rand.ToString();
            var rotation = Random.Range(0, 2);//0 = 0 // 1 = 90
            Quaternion Rot;
            Vector3 pos = new Vector3(Random.Range(-spawnArea, spawnArea), 0, Random.Range(-spawnArea, spawnArea));

            if (rotation == 0)
            {
                Rot = Quaternion.Euler(Vector3.zero);
            }
            else
                Rot = Quaternion.Euler(0, 90, 0);


            if(!Physics.CheckBox(pos, new Vector3(2.5f, 2.5f, 2.5f)))
            {
                Debug.Log("NotCollided");
                var obj = Instantiate(Resources.Load(path), pos, Rot);
                cnt--;
            }

            

            yield return new WaitForSeconds(.3f);

            
        }
    }
}
