using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Weapon", order = 1)]
public class Weapon : ScriptableObject
{

    public GameObject bullet;

    public Vector2 barrelPos;

    public float shootCoolDown;

    public float reloadCooldown;

    public Sprite weaponSprite;

    public int maxAmmo;

    public float bulletSpeed;

    public float bulletSpread = 0;

    public int bulletAmount = 1;
    public float bulletCooldown = 0;

    public bool countMultipleShoots;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}