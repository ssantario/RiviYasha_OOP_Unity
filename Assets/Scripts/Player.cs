using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public PlayerMovement playerMovement;
    public Animator animator;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Retrieve PlayerMovement component
        playerMovement = GetComponent<PlayerMovement>();
        if (playerMovement == null)
        {
            Debug.LogError("PlayerMovement component not found on Player.");
        }

        // Find EngineEffect object and retrieve its Animator component
        GameObject engineEffect = GameObject.Find("EngineEffect");
        if (engineEffect != null)
        {
            animator = engineEffect.GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogError("Animator component not found on EngineEffect.");
            }
        }
        else
        {
            Debug.LogError("EngineEffect GameObject not found.");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // FixedUpdate is called at a fixed interval
    void FixedUpdate()
    {
        // Call Move method from PlayerMovement
        if (playerMovement != null)
        {
            playerMovement.Move();
        }
    }

    // LateUpdate is called after all Update functions have been called
    void LateUpdate()
    {
        // Set the IsMoving parameter of the Animator
        if (animator != null && playerMovement != null)
        {
            bool isMoving = playerMovement.IsMoving();
            Debug.Log("IsMoving: " + isMoving);
            animator.SetBool("IsMoving", isMoving);
        }
    }
}