using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<Weapon> weaponPrefabs;
    public WeaponButton weaponButtonPrefab;

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
