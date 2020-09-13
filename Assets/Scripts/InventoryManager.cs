using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public List<Weapon> weapons = new List<Weapon>();
    public Transform weaponPanel;

    public List<Armor> armors = new List<Armor>();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        for (int i=0; i<GameManager.instance.weaponPrefabs.Count; i++)
        {
            GenerateWeapon(i);
        }
    }

    public void ResetWeapon()
    {
        if (PlayerController.instance.weapon != null)
        {
            Destroy(PlayerController.instance.weapon.gameObject);
        }
        PlayerController.instance.weapon = null;
    }

    public void GenerateWeapon(int weaponType)
    {
        Weapon newWeapon = (Weapon)Instantiate(GameManager.instance.weaponPrefabs[weaponType]);
        weapons.Add(newWeapon);
        newWeapon.gameObject.SetActive(false);

        WeaponButton button = (WeaponButton)Instantiate(GameManager.instance.weaponButtonPrefab);
        button.thisWeapon = newWeapon;
        button.name = newWeapon.weaponName;
        button.transform.SetParent(weaponPanel, false);
        button.gameObject.SetActive(true);
    }
}
