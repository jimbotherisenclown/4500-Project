using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sorter : MonoBehaviour
{
    public int itemsCreated = 0;

    // Start is called before the first frame update
    void Start()
    {
        clearNumberOfItems();
    }

    public void setNumberOfItems() {
        itemsCreated++;
    }

    public int getNumberOfItems() {
        return itemsCreated;
    }

    public void clearNumberOfItems() {
        itemsCreated = 0;
    }

}
