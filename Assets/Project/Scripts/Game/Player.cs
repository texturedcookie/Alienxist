﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Visuals") ]
    public Camera playerCamera;

    [Header("Gameplay")]
    public int initialHealth = 100;
    public int initialAmmo = 12;
    public float knockbackForce = 10;
    public float hurtDuration = 0.5f;

  
    private int health;
    public int Health { get { return health; } }

    private int ammo;
    public int Ammo {get { return ammo; } }

    private bool isHurt;

    void Start()
    {
        health = initialHealth;
        ammo = initialAmmo;


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (ammo > 0) {
                ammo--;

            GameObject bulletObject = PoolingManager.Instance.GetBullet(true);
            bulletObject.transform.position = playerCamera.transform.position + playerCamera.transform.forward;
            bulletObject.transform.forward = playerCamera.transform.forward;
        }
        }
    }

    //Check for collisions
    void OnTriggerEnter (Collider otherCollider) {
        if (otherCollider.GetComponent<AmmoCrate>() != null)
        {
            //Collect with ammo crate
            AmmoCrate ammoCrate = otherCollider.GetComponent<AmmoCrate>();
            ammo += ammoCrate.ammo;

            Destroy(ammoCrate.gameObject);
        }


        if (isHurt == false) {
            GameObject hazard = null;


             if (otherCollider.GetComponent<Enemy> () != null)  {
                 Enemy enemy = otherCollider.GetComponent<Enemy> ();
                 hazard = enemy.gameObject;
                 health -= enemy.damage;

            } else if (otherCollider.GetComponent <Bullet>() != null)    {
                Bullet bullet =otherCollider.GetComponent<Bullet>  ();
                if (bullet.ShotByPlayer == false)  {
                    hazard = bullet.gameObject;
                    health -= bullet.damage;
                    bullet.gameObject.SetActive(false);
                }
            }
             if (hazard != null)
            {
                isHurt = true;

                //Perfom knockbak effect.
                Vector3 hurtDirection = (transform.position - hazard.transform.position).normalized;
                Vector3 knockbackDirection = (hurtDirection + Vector3.up).normalized;
                GetComponent<ForceReciever>().AddForce(knockbackDirection, knockbackForce);

                StartCoroutine(HurtRoutine());
            }
        }
    }
    IEnumerator HurtRoutine ()
    {
        yield return new WaitForSeconds (hurtDuration);

        isHurt = false;
    }
}
