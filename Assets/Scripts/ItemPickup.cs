using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public virtual void Interact()
    {
        //This method is meant to be overwritten
        Debug.Log("Interacting with " + transform.name);
    }
}

public class ItemPickup : Interactable
{
    public Item item;
    bool wasPickedUp;

    public override void Interact()
    {
        base.Interact();

        Pickup();
    }

    void Pickup()
    {
        Debug.Log("Picking up " + item.name);
        wasPickedUp = InventoryManager._instance.AddItem(item);

        if (wasPickedUp)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            InventoryManager._instance.inInteractArea = true;

            if (Input.GetKeyDown(KeyCode.F))
            {
                Interact();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            InventoryManager._instance.inInteractArea = false;
        }
    }
}
