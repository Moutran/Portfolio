using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PistolScript : MonoBehaviour
{
    public bool canShoot = true;

    [Header("References")]
    public ParticleSystem MuzzleFlash;
    public TMP_Text bulletsUI;
    private Animator animator;
    public AudioSource soundShoot;
    public AudioSource soundReload;
    private PlayerInventoryScript playerInventory;

    [Header("Weapon Settings")]
    public int ammo = 0; //Total Pistol Ammo
    public int clip = 0; //Ammo Inside Clip
    public int clipSize = 8; //How much ammo the clip can hold
    public float range = 15f; //Pistol's Range
    public float shootCooldown = 1f; //Pistol Shoot Cooldown
    public float reloadTime = 2; //Pistol Reload Time
    public float damage = 30f; //Pistol's bullet damage

    [Header("Bullet Shell Injection Settings")]
    public bool bulletShellEjection;
    public float bulletShellRemoveTimer;
    public GameObject BulletShell;
    public Transform BulletShellEjectionTransform;

    [Header("Jam Settings")]
    public bool canJam;
    public float jamChance;
    private bool isJammed = false;

    [Header("Key Binds")]
    public KeyCode Shoot_Key;
    public KeyCode Reload_Key;

    private void Awake()
    {
        playerInventory = FindObjectOfType<PlayerInventoryScript>();
    }

    private void OnEnable() //Called when the weapon is equipped
    {
        animator = GetComponent<Animator>();
        canShoot = false;
        ammo = playerInventory.lightAmmo;
    }

    private void OnDisable()
    {
        bulletsUI.enabled = false;
    }

    void Update()
    {
        HandleInput();
        HandleUI();
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(Shoot_Key))
        {
            Shoot();
        }
        else if (Input.GetKeyDown(Reload_Key))
        {
            Reload();
        }
    }
    void Shoot()
    {
        if (canShoot && !isJammed)
        {
            if (clip > 0)
            {
                soundShoot.Play();
                clip -= 1;
                MuzzleFlash.Play();
                canShoot = false;
                animator.SetTrigger("Shoot");
                BulletEjection();
                StartCoroutine(ShootCooldownHandler());
                JamHandler();

                Ray ray = new Ray();
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo, range))
                {
                    if (hitInfo.collider.gameObject.CompareTag("Enemy"))
                    {
                        Enemy_AI target = hitInfo.collider.gameObject.GetComponentInParent<Enemy_AI>();  
                        target.TakeDamage(damage);
                        Debug.Log(target.health);
                    }
                }
            }
            else
            {
                Reload();
            }
        }
    }

    void Reload()
    {
        if (clip != clipSize && canShoot && ammo > 0) //Check if the clip is already full and gun is not shooting
        {
            int ammoNeeded = clipSize - clip; //Ammo needed to fill the clip
            animator.SetBool("Reload", true);
            if (ammo > ammoNeeded) //Check if ammo is more that the ammo needed
            {
                canShoot = false;
                StartCoroutine(ReloadTimeHandler(ammoNeeded));
            }
            else //If ammo is less or equal than the ammo needed 
            {
                canShoot = false;
                StartCoroutine(ReloadTimeHandler(ammo));
            }
        }
    }
    IEnumerator ShootCooldownHandler() //Called after shooting to ensure shoting cooldown
    {
        yield return new WaitForSeconds(shootCooldown);
        canShoot = true;
    }

    IEnumerator ReloadTimeHandler(int ammoValue) //Called when reloading to ensure reload time
    {
        soundReload.Play();
        yield return new WaitForSeconds(reloadTime);
        canShoot = true;
        isJammed = false;
        animator.SetBool("Reload", false);
        animator.SetBool("Jam", isJammed);
        clip += ammoValue;
        ammo -= ammoValue;
    }
    void BulletEjection() //Function that handles bullet shell injection
    {
        if (bulletShellEjection) 
        { 
            GameObject shell = Instantiate(BulletShell, BulletShellEjectionTransform.position, Quaternion.LookRotation(Vector3.up)); //Create Bullet Shell
            Rigidbody rb = shell.GetComponent<Rigidbody>(); 
            rb.AddForce(transform.right * 500); //Add force to the shell
            Destroy(shell, bulletShellRemoveTimer); //Destroy to ensure lag free gameplay
        }
    }

    void EquipTimer() //Called through a animation event when the Equip Animation finishes
    {
        canShoot = true;
        bulletsUI.enabled = true;
    }

    void JamHandler()
    {
        if (canJam)
        {
            int randomNumberGenerator = UnityEngine.Random.Range(0, 100);
            if (randomNumberGenerator < jamChance)
            {
                isJammed = true;
                canShoot = false;
                animator.SetBool("Jam", isJammed);
            }
        }
    }

    void HandleUI() 
    {
        bulletsUI.text = clip + " / " + ammo;
    }


}
