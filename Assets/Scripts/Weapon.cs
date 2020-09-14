using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string weaponName;
    public float damagePoint;
    public int bulletSize;
    public float reloadTime;
    public float shootPower;
    public float shootRange;

    public void ShootBullet()
    {
        Bullet bullet = (Bullet)Instantiate(GameManager.instance.bulletPrefab);
        bullet.damage = damagePoint;
        bullet.transform.localPosition = this.transform.position;
        bullet.gameObject.SetActive(true);
        bullet.GetComponent<Rigidbody>().AddRelativeForce(PlayerController.instance.transform.forward * shootPower);
    }
}
