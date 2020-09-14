using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public List<Weapon> weaponPrefabs;
    public WeaponButton weaponButtonPrefab;

    public Armor armorPrefab;
    public ArmorButton armorButtonPrefab;

    public Item itemPrefab;
    public ItemButton itemButtonPrefab;


    private void Awake()
    {
        instance = this;
    }

    public void UniversalClosePanel(GameObject thisPanel)
    {
        thisPanel.gameObject.SetActive(false);
    }

    public void UniversalOpenPanel(GameObject thisPanel)
    {
        thisPanel.gameObject.SetActive(true);
    }
}
