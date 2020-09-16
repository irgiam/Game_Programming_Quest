using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    //player inventory
    public List<Weapon> weapons = new List<Weapon>();
    public List<Armor> armors = new List<Armor>();
    public List<Item> items = new List<Item>();

    public Transform weaponPanel;
    public Transform armorPanel;
    public Transform itemPanel;

    public Text equipedWeaponStat;
    public Text equipedArmorStat;
    public Text itemDescription;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //sample sementara
        for (int i=0; i<GameManager.instance.weaponPrefabs.Count; i++)
        {
            GenerateWeapon(i);
        }

        //sample sementara
        GenerateArmor("Vest 1", 10);
        GenerateArmor("Vest 2", 7);

        //sample sementara
        GenerateItem("Steel", "for crafting");
        GenerateItem("Food", "for restoring health");
    }

    public void ResetWeapon()
    {
        //if (PlayerController.instance.weapon != null)
        //{
        //    Destroy(PlayerController.instance.weapon.gameObject);
        //}
        PlayerController.instance.weapon = null;
        foreach (Weapon weapon in weapons)
        {
            weapon.gameObject.SetActive(false);
        }
    }

    public void ResetEquipedWeaponStat()
    {
        equipedWeaponStat.text = "Equiped: None";
    }

    void GenerateWeapon(int weaponType)
    {
        Weapon newWeapon = (Weapon)Instantiate(GameManager.instance.weaponPrefabs[weaponType]);
        weapons.Add(newWeapon);
        newWeapon.transform.SetParent(PlayerController.instance.weaponParent, false);
        newWeapon.gameObject.SetActive(false);

        WeaponButton button = (WeaponButton)Instantiate(GameManager.instance.weaponButtonPrefab);
        button.thisWeapon = newWeapon;
        button.weaponName.text = newWeapon.weaponName;
        button.transform.SetParent(weaponPanel, false);
        button.gameObject.SetActive(true);
    }


    void GenerateArmor(string name, float _maxResistance)
    {
        Armor newArmor = (Armor)Instantiate(GameManager.instance.armorPrefab);
        newArmor.name = name; //game object name
        newArmor.armorName = name;
        newArmor.maxResistenace = _maxResistance;
        newArmor.resistance = Mathf.Clamp(_maxResistance, 0, _maxResistance);
        newArmor.gameObject.SetActive(false);
        newArmor.transform.SetParent(PlayerController.instance.transform, false);
        armors.Add(newArmor);

        ArmorButton button = (ArmorButton)Instantiate(GameManager.instance.armorButtonPrefab);
        button.thisArmor = newArmor;
        button.armorName.text = newArmor.armorName;
        button.transform.SetParent(armorPanel, false);
        button.gameObject.SetActive(true);
    }

    public void SetArmor(Armor _armor)
    {
        PlayerController.instance.armorBar.maxValue = _armor.maxResistenace;
        PlayerController.instance.armorBar.value = _armor.resistance;
        PlayerController.instance.armorStat.text = _armor.resistance.ToString();
    }

    public void ResetArmor()
    {
        PlayerController.instance.armor = null;
        PlayerController.instance.armorBar.value = 0;
        PlayerController.instance.armorStat.text = "0";
        foreach (Armor armor in armors)
        {
            armor.gameObject.SetActive(false);
        }
        equipedArmorStat.text = "Equiped: None";
    }

    void GenerateItem(string _name, string _description)
    {
        Item newItem = (Item)Instantiate(GameManager.instance.itemPrefab);
        newItem.itemName = _name;
        newItem.description = _description;
        newItem.transform.SetParent(this.transform, false);
        newItem.gameObject.SetActive(true);

        ItemButton button = (ItemButton)Instantiate(GameManager.instance.itemButtonPrefab);
        button.thisItem = newItem;
        button.itemName.text = newItem.itemName;
        button.transform.SetParent(itemPanel, false);
        button.gameObject.SetActive(true);
    }
}
