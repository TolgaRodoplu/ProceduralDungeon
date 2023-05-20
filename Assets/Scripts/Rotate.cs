using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private int rotationSpeed = 15;

    private float randX;
    private float randY;
    private float randZ;

    private void Start()
    {
        randX = Random.Range(-1f, 1f) * rotationSpeed;
        randY = Random.Range(-1f, 1f) * rotationSpeed;
        randZ = Random.Range(-1f, 1f) * rotationSpeed;

    }


    void Update()
    { 

        transform.Rotate(randX * Time.deltaTime, randY * Time.deltaTime, randZ * Time.deltaTime);
    }

    

}
