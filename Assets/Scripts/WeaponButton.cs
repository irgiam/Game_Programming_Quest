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
        Weapon equiped = (Weapon)Instantiate(thisWeapon);
        PlayerController.instance.weapon = equiped;
        equiped.transform.SetParent(PlayerController.instance.weaponParent, false);
        equiped.transform.localPosition = new Vector3 (-0.1f, -0.1f, 0.45f);
        equiped.gameObject.SetActive(true);
    }
}
