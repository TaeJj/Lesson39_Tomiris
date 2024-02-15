using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationView : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;

    private Animator animator;
    private Rigidbody2D rb;
    private Camera mainCamera;
    private float screenHalfWidth;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        screenHalfWidth = mainCamera.aspect * mainCamera.orthographicSize;
    }

    void Update()
    {
        float moveDirection = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);

        if (moveDirection < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (moveDirection > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        float clampedXPosition = Mathf.Clamp(transform.position.x, -screenHalfWidth, screenHalfWidth);
        transform.position = new Vector3(clampedXPosition, transform.position.y, transform.position.z);

        if (Mathf.Abs(moveDirection) > 0)
        {
            if (Input.GetKey(KeyCode.R))
            {
                animator.SetBool("isRunning", true);
                animator.SetBool("isWalking", false);
            }
            else
            {
                animator.SetBool("isWalking", true);
                animator.SetBool("isRunning", false);
            }
            animator.SetBool("isIdle", false);
        }
        else
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
            animator.SetBool("isIdle", true);
        }
    }
}
