using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFire : MonoBehaviour
{
    public float fireForce;
    public int initAmmoValue;
    public int maxAmmo;

    public Transform firePoint;
    public GameObject bulletPrefab;

    public Color activeColor;
    public Dictionary<Color, int> colorAmmos = new Dictionary<Color, int>();
    public AmmoBarsCircular ammoBarsUi;
    public GameObject meleeWeapon;

    void OnEnable()
    {
        ColorManager.OnColorChanged += changeActiveColor;
    }

    void Start()
    {
        // For standard ammoBars
        // Loop on all available color
        // set all color ammos amount in map, and set this color to this corresponding ammoBar
        for (int i = 0; i < ColorManager.colors.Length; i++) {
            colorAmmos.Add(ColorManager.colors[i], initAmmoValue);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) {
            if (colorAmmos.TryGetValue(activeColor, out int ammo) && ammo > 0) {
                shoot();
                useAmmo(1);
            } else {
                Debug.Log("NO AMMO");
            }
        }

        if(Input.GetButtonDown("Fire2")) {
            meleeWeapon.GetComponent<Animator>().SetTrigger("meleeAttackTrigger");
        }
        
    }

    void LateUpdate()
    {   
        //meleeWeapon.GetComponent<Animator>().ResetTrigger("meleeAttackTrigger");
    }

    void OnDisable()
    {
        ColorManager.OnColorChanged -= changeActiveColor;
    }

    void shoot() {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Renderer>().material.color = activeColor;
        bullet.GetComponent<Rigidbody>().AddForce(firePoint.forward * fireForce, ForceMode.Impulse);
        bullet.GetComponent<BulletBehavior>().damage = 1;
        Destroy(bullet, 5f);
    }

    void useAmmo(int amount) {
        if (colorAmmos.TryGetValue(activeColor, out int ammo)) {
            int newAmmoAmount = ammo - amount;
            colorAmmos[activeColor] = newAmmoAmount;
            ammoBarsUi.setColorAmmoBarValue(activeColor, newAmmoAmount);
        }
    }

    public bool isColorAmmoFull(Color color) {
        if (colorAmmos.TryGetValue(color, out int ammo)) {
            return ammo >= maxAmmo;
        }
        return true;
    }

    public int reloadColorAmmo(Color color, int amount) {
        if (colorAmmos.TryGetValue(color, out int ammo) && !isColorAmmoFull(color)) {
            int newAmmoAmount = ammo + amount;
            colorAmmos[color] = newAmmoAmount;
            ammoBarsUi.setColorAmmoBarValue(color, newAmmoAmount);
        }
        return ammo;
    }

    void changeActiveColor(Color color) {
        activeColor = color;
    }
}
