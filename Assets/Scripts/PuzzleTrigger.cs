using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTrigger : MonoBehaviour
{
    private bool isTriggered = false;
    [SerializeField] private int keyID;
    [SerializeField] private AnimateObject objectToTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (!isTriggered)
        {
            var checkKey = other.transform.GetComponent<Key>();
            var checkInteract = other.transform.GetComponent<IInteractable>();

            if (checkKey != null)
            {
                if (checkKey.keyID == keyID)
                {
                    objectToTrigger.Animate();

                    isTriggered = true;

                    if (checkInteract != null)
                    {
                        other.gameObject.layer = LayerMask.NameToLayer("Default");
                        Destroy((Object)checkInteract);
                    }

                    Destroy(this);
                    Destroy(objectToTrigger);
                }
            }
        }
    }

}
