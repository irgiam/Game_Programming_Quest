using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorButton : MonoBehaviour
{
    public Armor thisArmor;
    public Text armorName;
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
        InventoryManager.instance.ResetArmor();
        PlayerController.instance.armor = thisArmor;
        InventoryManager.instance.equipedArmorStat.text = "Equiped: " + armorName.text;
        thisArmor.gameObject.SetActive(true);
    }
}
