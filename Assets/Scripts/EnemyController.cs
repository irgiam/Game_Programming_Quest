using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public float health;
    public Slider healthbar;

    private void Update()
    {
        float distance = Vector3.Distance(this.transform.position, PlayerController.instance.transform.position);
        if (distance < 10f)
        {
            this.transform.LookAt(PlayerController.instance.transform);
        }
    }
}
