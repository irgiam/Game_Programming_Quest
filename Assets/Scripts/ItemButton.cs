using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    public Item thisItem;
    public Text itemName;
    Button button;

    private void Awake()
    {
        button = this.GetComponent<Button>();
    }

    private void Start()
    {
        button.onClick.AddListener(HandleClick);
    }

    void HandleClick()
    {
        InventoryManager.instance.itemDescription.text = thisItem.itemName + ": " + thisItem.description;
    }
}
