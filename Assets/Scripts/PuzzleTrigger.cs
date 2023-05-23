using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTrigger : MonoBehaviour
{
    public int puzzleID;
    private bool isTriggered = false;
    [SerializeField] public float keyID;

    private void OnTriggerEnter(Collider other)
    {
        if (!isTriggered)
        {
            var checkKey = other.transform.GetComponent<Key>();

            if (checkKey != null)
            {
                if (checkKey.keyID == keyID)
                {
                    EventSystem.instance.TriggerPuzzle(puzzleID);

                    isTriggered = true;

                    Destroy(this);
                }
            }
        }
    }

}
