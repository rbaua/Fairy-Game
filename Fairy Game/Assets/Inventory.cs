using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    private bool inventoryEnabled;
    public GameObject inventory;

    private int allSlots;
    private int enabledSlots;
    private GameObject[] slots;

    public GameObject slotHolder;

    void Start()
    {
        allSlots = 42;
        slots = new GameObject[allSlots];

        for(int i=0; i<allSlots; i++)
        {
            slots[i] = slotHolder.transform.GetChild(i).gameObject;
            if(slots[i].GetComponent<Slot>().item == null)
            {
                slots[i].GetComponent<Slot>().empty = true;
            }
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            inventoryEnabled = !inventoryEnabled;
        }

        if(inventoryEnabled)
        {
            inventory.SetActive(true);
        }
        else
        {
            inventory.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Item")
        {
            GameObject itemTouched = other.gameObject;
            Item item = itemTouched.GetComponent<Item>();
            AddItem(itemTouched, item.id, item.type, item.description, item.icon);
        }
    }

    void AddItem(GameObject itemObject, int itemID, string itemType, string itemDescription, Sprite itemIcon)
    {
        for(int i =0; i<allSlots; i++)
        {
            if(slots[i].GetComponent<Slot>().empty)
            {
                itemObject.GetComponent<Item>().pickedUp = true;

                slots[i].GetComponent<Slot>().icon = itemIcon;
                slots[i].GetComponent<Slot>().type = itemType;
                slots[i].GetComponent<Slot>().id = itemID;
                slots[i].GetComponent<Slot>().description = itemDescription;
                slots[i].GetComponent<Slot>().item = itemObject;

                itemObject.transform.parent = slots[i].transform;
                itemObject.SetActive(false);

                slots[i].GetComponent<Slot>().UpdateSlot();
                slots[i].GetComponent<Slot>().empty = false;
                return;
            }
            
        }
    }
}
