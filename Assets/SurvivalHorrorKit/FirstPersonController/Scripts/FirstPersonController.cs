using UnityEngine;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine.UI;
using System;

interface Interactable
{
    public void Interact();
    public string InteractionText();
}

[RequireComponent(typeof(Rigidbody))]
public class FirstPersonController : MonoBehaviour
{
    public static FirstPersonController Instance { get; private set; }


    [Header("References")]
    public Transform playerCamera; //Player Camera

    [Header("Movement Settings")]
    public float walkSpeed = 4f; //Player Walk Speed
    public float sprintSpeed = 6f; //Player Sprint Speed
    public float crouchSpeed = 2f; //Player Crouch Speed
    public float jumpForce = 4f; //Player Jump Power
    public float crouchHeightMultiplier = 0.6f; //How low the character may crouch
    public float groundCheckDistance = 0.1f; //Do not touch

    [Header("Mouse Settings")]
    public float mouseSensitivity = 2f; //Mouse Sensitivity
    public float verticalLookLimit = 80f; //Player Vertical Look Limit (Degrees)

    [Header("FOV Settings")]
    public float normalFOV = 60f; //Camera FOV
    public float sprintFOV = 75f; //Camera Sprint FOV
    public float fovStepTime = 4f; //Do not touch

    [Header("Crouch Settings")]
    public float crouchCooldown = 0.5f; //Time in seconds between crouch toggles
    private float lastCrouchTime = -Mathf.Infinity; //Do not touch

    [Header("Sprint Stamina Settings")]
    public float maxStamina = 100f; //Player Max Stamina
    public float staminaDrainRate = 20f; //Stamina Drain Rate
    public float staminaRegenRate = 1f; //Stamina Regeneration Rate
    public float sprintCooldownThreshold = 5f; //Minimum stamina to sprint
    public float currentStamina; //Player's current Stamina
    public bool canSprint = true; //Toggle Sprint ON / OFF

    [Header("UI Elements")]
    public Image sprintBar; //Stamina Bar UI
    public Image healthBar; //Health Bar UI
    public Image hungerBar; //Hunger Bar UI
    public Image thirstBar; //Hunger Bar UI
    public Image hidingIcon;
    public Image bleedingIcon;
    private UserInterfaceManager userInterfaceManager;


    private Rigidbody rb;
    private CapsuleCollider col;
    private float cameraPitch = 0f;
    private Vector3 originalScale;
    [HideInInspector] public bool isCrouching = false; //Check if player is Crouching
    private bool isGrounded = false; //Check if player is Grounded

    [Header("Health")]
    public float maxHealth = 100f; //Player's max Health
    public float currentHealth = 100; //Player's current Health
    [HideInInspector] public bool isBleeding = false; //Player's bleeding boolean
    private float bleedingDamage = 0.5f;

    [Header("Hunger")]
    public float maxHunger = 100;
    public float currentHunger = 100;
    public float hungerReduction = 1f;
    public float hungerDamage = 1f;

    [Header("Thirst")]
    public float maxThirst = 100;
    public float currentThirst = 100;
    public float thirstReduction = 1f;
    public float thirstDamage = 1f;

    [Header("Hiding")]
    public bool isHiding = false;

    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // prevent duplicates
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // keep FPC across scenes
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>(); //Get player RigidBody
        col = GetComponent<CapsuleCollider>(); //Get Player Collider
        userInterfaceManager = GetComponent<UserInterfaceManager>();

        currentHealth = maxHealth;
        currentStamina = maxStamina;

        originalScale = transform.localScale; //Player's Original Scale

        Cursor.lockState = CursorLockMode.Locked; //Lock Cursor
        Cursor.visible = false; //Disable Cursor

        rb.freezeRotation = true; //Freeze rigidbody rotation

        currentStamina = maxStamina; //Set Stamina
    }

    void Update()
    {
        Look();
        CheckGround();
        Jump();
        Crouch();
        HandleFOV();
        UpdatePlayerUI();
        InteractionHandler();
        UpdateStats();
    }

    private void InteractionHandler() //Player Interaction raycast
    {
        Ray ray = new Ray();
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 2.5f))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out Interactable interactable))
            {
                string interactable_InteractionText = interactable.InteractionText();
                userInterfaceManager.EnableInteractionText(interactable_InteractionText);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.Interact();
                }
            }
            else
            {
                userInterfaceManager.DisableInteractionText();
            }
        }
        else
        {
            userInterfaceManager.DisableInteractionText();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        cameraPitch -= mouseY;
        cameraPitch = Mathf.Clamp(cameraPitch, -verticalLookLimit, verticalLookLimit);

        if (playerCamera != null)
        {
            playerCamera.localEulerAngles = Vector3.right * cameraPitch;
        }
    }

    void Move()
    {
        bool isSprinting = Input.GetKey(KeyCode.LeftShift) && !isCrouching && canSprint && currentStamina > sprintCooldownThreshold;

        float speed = walkSpeed;

        if (isSprinting)
        {
            speed = sprintSpeed;
            currentStamina -= staminaDrainRate * Time.fixedDeltaTime; // Drain stamina while sprinting
            currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
        }
        else
        {
            currentStamina += staminaRegenRate * Time.fixedDeltaTime; // Regenerate stamina
            currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
        }

        if (currentStamina <= sprintCooldownThreshold)
        {
            canSprint = false;
        }

        if (currentStamina >= 20f)
        {
            canSprint = true;
        }

        Vector3 move = transform.forward * Input.GetAxis("Vertical") +
                       transform.right * Input.GetAxis("Horizontal");

        Vector3 velocity = move.normalized * speed;
        velocity.y = rb.velocity.y; // Preserve Y velocity (gravity/jumping)
        rb.velocity = velocity;
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isCrouching && currentStamina >= 20f)
        {
            currentStamina -= 20f;
            currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);

            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void Crouch()
    {
        if (Time.time < lastCrouchTime + crouchCooldown)
            return;

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = !isCrouching;
            lastCrouchTime = Time.time;

            float targetScaleY = isCrouching ? crouchHeightMultiplier : 1f;
            Vector3 newScale = originalScale;
            newScale.y *= isCrouching ? crouchHeightMultiplier : 1f;

            float inverseScaleY = 1f / targetScaleY;
            foreach (Transform child in transform)
            {
                Vector3 localScale = child.localScale;
                child.localScale = new Vector3(localScale.x, inverseScaleY, localScale.z);
            }

            transform.localScale = newScale;
        }
    }

    void CheckGround()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down,
                                     (col.bounds.extents.y + groundCheckDistance));
    }

    void HandleFOV()
    {
        if (playerCamera == null) return;

        Camera cam = playerCamera.GetComponent<Camera>();
        if (cam == null) return;

        float targetFOV = Input.GetKey(KeyCode.LeftShift) && !isCrouching ? sprintFOV : normalFOV;
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, fovStepTime * Time.deltaTime);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }

    void UpdatePlayerUI()
    {
        if (sprintBar != null) //Update Stamina Bar
        {
            sprintBar.fillAmount = currentStamina / maxStamina;
        }

        if (healthBar != null) //Update Health Bar
        {
            healthBar.fillAmount = currentHealth / maxHealth;
        }

        if (thirstBar != null) //Update Health Bar
        {
            thirstBar.fillAmount = currentThirst / maxThirst;
        }

        if (hungerBar != null) //Update Hunger Bar
        {
            hungerBar.fillAmount = currentHunger / maxHunger;
        }

        if (isHiding) //Update Hiding Icon
        {
            hidingIcon.enabled = true;
        }
        else
        {
            hidingIcon.enabled = false;
        }
        
        if (isBleeding)
        {
            bleedingIcon.enabled = true;
        }
        else
        {
            bleedingIcon.enabled = false;
        }
    }

    void UpdateStats()
    {
        HungerHandler();
        ThirstHandler();
        BleedingHandler();
    }

    void HungerHandler()
    {
        currentHunger = Mathf.Clamp(currentHunger, 0, maxHunger);
        if (currentHunger > 0)
        {
            currentHunger -= hungerReduction * Time.deltaTime;
        }
        else
        {
            currentHealth -= hungerDamage * Time.deltaTime;
        }
    }

    void ThirstHandler()
    {
        currentThirst = Mathf.Clamp(currentThirst, 0, maxThirst);
        if (currentThirst > 0)
        {
            currentThirst -= thirstReduction * Time.deltaTime;
        }
        else
        {
            currentHealth -= thirstDamage * Time.deltaTime;
        }
    }

    public void StartBleeding()
    {
        isBleeding = true;
    }

    public void StopBleeding()
    {
        isBleeding = false;
    }

    void BleedingHandler()
    {
        if (isBleeding)
        {
            currentHealth -= bleedingDamage * Time.deltaTime;
        }
    }

    public void Heal(float healing)
    {
        if (currentHealth + healing > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += healing;
        }
    }
}