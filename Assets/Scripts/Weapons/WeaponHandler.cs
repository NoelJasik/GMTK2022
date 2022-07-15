using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI ammoCounter;
    [SerializeField]
    Weapon[] weapons;

    [SerializeField]
    SpriteRenderer weaponSpriteRenderer;
    [SerializeField]
    Transform barrel;

    public bool canShoot;

    public Weapon currentWeapon;


    public int currentAmmo;

    public float currentReloadCooldown;

    public float currentShootCooldown;

    bool reloading = false;


    // Start is called before the first frame update
    void Start()
    {
     RollGun();
    }
    void ApplyWeapon(Weapon _weaponToApply)
    {
       currentWeapon = _weaponToApply;
       weaponSpriteRenderer.sprite = currentWeapon.weaponSprite;
       currentAmmo = currentWeapon.maxAmmo;
       currentReloadCooldown = currentWeapon.reloadCooldown;
       currentShootCooldown = currentWeapon.shootCoolDown;
       barrel.localPosition = currentWeapon.barrelPos;
    }

    // Update is called once per frame
    void Update()
    {
        if(reloading)
        {
             currentReloadCooldown -= Time.deltaTime;
        } if(currentReloadCooldown <= 0 && reloading)
        {
            currentAmmo = currentWeapon.maxAmmo;
            currentReloadCooldown = currentWeapon.reloadCooldown;
            reloading = false;
        }
        currentShootCooldown -= Time.deltaTime;
        if(canShoot)
        {
            weaponSpriteRenderer.enabled = true;
            ammoCounter.text = currentAmmo.ToString() + "/" + currentWeapon.maxAmmo.ToString();
            if(Input.GetButton("Fire1") && currentShootCooldown <= 0 && currentAmmo > 0)
        {
            Shoot();
        } else 
        if((Input.GetButton("Fire1") && currentAmmo <= 0 && !reloading) || (Input.GetButton("Reload") && !reloading))
        {
            Reload();
        } 
        } else {
            weaponSpriteRenderer.enabled = false;
        }
        
       
    }
    public void RollGun()
    {
        ApplyWeapon(weapons[Random.Range(0, weapons.Length)]);
    }

    void Shoot()
    { 
       Debug.Log("Bam!");
       currentShootCooldown = currentWeapon.shootCoolDown;
       for (int i = 0; i < currentWeapon.bulletAmount; i++)
       {
        Invoke("Fire", i * currentWeapon.bulletCooldown);
       }
       if(!currentWeapon.countMultipleShoots)
       {
           currentAmmo--; 
       }
       
    }
    void Fire()
    {
       barrel.localRotation = Quaternion.Euler(barrel.rotation.x, barrel.rotation.z, Random.Range(-currentWeapon.bulletSpread, currentWeapon.bulletSpread));
       GameObject bullet = Instantiate(currentWeapon.bullet, barrel.position, barrel.rotation);
       bullet.GetComponent<Rigidbody2D>().AddForce(currentWeapon.bulletSpeed * barrel.right);
       Destroy(bullet, 3f);   
        if(currentWeapon.countMultipleShoots)
       {
           currentAmmo--; 
       }
    }
    void Reload()
    { 
       reloading = true;
       Debug.Log("Reloading");
    }
}
