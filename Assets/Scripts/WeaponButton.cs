using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponButton : MonoBehaviour
{
    public Weapon thisWeapon;
    public Text weaponName;
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
        InventoryManager.instance.ResetWeapon();
        PlayerController.instance.weapon = thisWeapon;
        InventoryManager.instance.equipedWeaponStat.text = "Equiped: " + thisWeapon.weaponName;
        thisWeapon.transform.localPosition = new Vector3 (-0.1f, -0.1f, 0.45f);
        thisWeapon.gameObject.SetActive(true);
    }
}
