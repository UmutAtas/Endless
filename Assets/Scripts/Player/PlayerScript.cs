using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public CharacterController controller;
    public Animator anim;
    private Vector3 direction;
    public float forwardSpeed;
    public float maxSpeed;
    
    private int desiredLane = 1;
    public float laneDistance;

    public float jumpForce;
    public float gravity;

    private bool isSliding = false;
        
    void Start()
    {
        
    }

    
    void Update()
    {
        HandleInput();
    }

    void FixedUpdate()
    {
        if (!PlayerManager.isStart)
            return;
        HandleMovement();
    }

    private void HandleMovement()
    {
        anim.SetBool("isGameStarted", true);
        anim.SetBool("isGrounded", true);
        if (forwardSpeed < maxSpeed)
        {
            forwardSpeed += 0.3f * Time.fixedDeltaTime;
        }
        direction.z = forwardSpeed;
        controller.Move(direction * Time.fixedDeltaTime);

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (desiredLane == 0 )
        {
            targetPosition += Vector3.left * laneDistance;
        }
        else if (desiredLane == 2 )
        {
            targetPosition += Vector3.right * laneDistance;
        }
        transform.position = Vector3.Lerp(transform.position, targetPosition, 10f * Time.fixedDeltaTime);

        if (controller.isGrounded == false)
        {
            direction.y += gravity*Time.fixedDeltaTime;
        }
    }

    private void HandleInput()
    {
        if (SwipeManager.swipeLeft)
        {
            desiredLane--;
            if (desiredLane == -1 )
            {
                desiredLane = 0;
            }
        }
        if (SwipeManager.swipeRight)
        {
            desiredLane++;
            if (desiredLane == 3)
            {
                desiredLane = 2;
            }
        }
        if (SwipeManager.swipeUp)
        {
            Jump();
            FindObjectOfType<AudioManager>().PlaySound("Jump");
        }
        if (SwipeManager.swipeDown && !isSliding)
        {
            StartCoroutine(Slide());
            FindObjectOfType<AudioManager>().PlaySound("Slide");
        }
    }

    private void Jump()
    {
        if (controller.isGrounded)
        {
            direction.y = jumpForce;
            anim.SetBool("isGrounded", false);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Obstacle")
        {
            PlayerManager.gameOver = true;
            FindObjectOfType<AudioManager>().PlaySound("GameOver");
        }
    }

    private IEnumerator Slide()
    {
        isSliding = true;
        controller.center = new Vector3(0, -0.5f, 0);
        controller.height = 1;
        anim.SetBool("isSlide", true);

        yield return new WaitForSeconds(1f);

        controller.center = new Vector3(0, -0, 0);
        controller.height = 2;
        anim.SetBool("isSlide", false);
        isSliding = false;
    }
}
