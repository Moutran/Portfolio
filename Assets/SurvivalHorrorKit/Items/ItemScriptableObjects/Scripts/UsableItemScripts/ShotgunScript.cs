using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ShotgunScript : MonoBehaviour
{
    private bool canShoot = true;

    [Header("References")]
    public ParticleSystem MuzzleFlash;
    public TMP_Text bulletsUI;
    private Animator animator;
    private AudioSource audioSource;
    public Transform shootOrigin;
    private PlayerInventoryScript playerInventory;

    [Header("Weapon Settings")]
    public int ammo = 0;
    public int clip = 0; //Ammo Inside Clip
    public int clipSize = 8; //How much ammo the clip can hold
    public float range = 15f; 
    public float attackRadius = 0.5f;
    public float shootCooldown = 0.6f; 
    public float reloadTime = 2; 

    public int pelletCount = 10;
    public float spreadAngle = 5f;
    public int damagePerPellet = 5;


    [Header("Bullet Shell Injection Settings")]
    public bool bulletShellEjection;
    public float bulletShellRemoveTimer;
    public GameObject BulletShell;
    public Transform BulletShellEjectionTransform;

    [Header("Key Binds")]
    public KeyCode Shoot_Key;
    public KeyCode Reload_Key;

    private void Awake()
    {
        playerInventory = FindObjectOfType<PlayerInventoryScript>();
    }

    private void OnDisable()
    { 
        bulletsUI.enabled = false;
    }

    private void OnEnable() //Called when the weapon is equipped
    {
        animator = GetComponent<Animator>();
        canShoot = false;
        ammo = playerInventory.shotgunShells;
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
        if (canShoot)
        {
            if (clip > 0)
            {
                clip -= 1;
                MuzzleFlash.Play();
                canShoot = false;
                animator.SetTrigger("Shoot");
                StartCoroutine(ShootCooldownHandler());

                Dictionary<GameObject, int> damageMap = new Dictionary<GameObject, int>();

                for (int i = 0; i < pelletCount; i++)
                {
                    Vector2 spread = UnityEngine.Random.insideUnitCircle * spreadAngle;
                    Quaternion rotation = Quaternion.Euler(spread.x, spread.y, 0);
                    Vector3 direction = rotation * shootOrigin.forward;

                    if (Physics.Raycast(shootOrigin.position, direction, out RaycastHit hit, range))
                    {
                        Debug.DrawLine(shootOrigin.position, hit.point, Color.red, 1f);

                        GameObject target = hit.collider.gameObject;

                        if (!damageMap.ContainsKey(target))
                            damageMap[target] = 0;

                        damageMap[target] += damagePerPellet;
                    }
                }

                foreach (var kvp in damageMap)
                {
                    Enemy_AI health = kvp.Key.GetComponentInParent<Enemy_AI>();
                    if (health != null)
                    {
                        health.TakeDamage(kvp.Value);
                        Debug.Log(kvp.Value.ToString());
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
        yield return new WaitForSeconds(reloadTime);
        canShoot = true;
        animator.SetBool("Reload", false);
        clip += ammoValue;
        ammo -= ammoValue;
    }
    public void BulletEjection() //Function that handles bullet shell injection
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

    void HandleUI()
    {
        bulletsUI.text = clip + " / " + ammo;
    }
}
