using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 18f;
    public float jumpForce = 7f;
    private Rigidbody rig;
    private CapsuleCollider col;
    public Animator animator;
    public GameObject swordJoint;
    private int currentPlayerHealth;
    public bool isAlive = true;

    public LayerMask groundLayer;
    Vector3 targetRotation;

    float rotationSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        //animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        currentPlayerHealth = GetComponent<PlayerHealth>().playerCurrentHealth;
        if (currentPlayerHealth <= 0 && isAlive)
        {
            isAlive = false;
            swordJoint.GetComponent<BoxCollider>().enabled = false;
            animator.SetTrigger("playerDeath");
        }

        if (isAlive)
        {
            if (Input.GetButtonDown("Fire1"))
                animator.SetBool("isAttacking", true);
            else
                animator.SetBool("isAttacking", false);

            //----------------------- Jump System --------------------------------

            //if (IsGrounded() && Input.GetButtonDown("Jump"))
            //{
            //    //rig.constraints &= ~RigidbodyConstraints.FreezePositionY;
            //    //rig.velocity = new Vector3(0, jumpForce, 0);
            //    rig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            //    animator.SetBool("isJumping", true);
            //}
            //else
            //{
            //    animator.SetBool("isJumping", false);
            //}

            //----------------------- End of Jump System --------------------------------
        }
    }

    void FixedUpdate()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        float hAxisRaw = Input.GetAxisRaw("Horizontal");
        float vAxisRaw = Input.GetAxisRaw("Vertical");

        Vector3 input = new Vector3(hAxis, 0, vAxis);
        Vector3 inputRaw = new Vector3(hAxisRaw, 0, vAxisRaw);

        if (input.sqrMagnitude > 1f)
        {
            input.Normalize();
        }

        if (inputRaw.sqrMagnitude > 1f)
        {
            inputRaw.Normalize();
        }

        if (inputRaw != Vector3.zero)
        {
            targetRotation = Quaternion.LookRotation(input).eulerAngles;
        }

        if (isAlive)
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Sword Attack") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Cast Spell") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Ultimate"))
            {
                rig.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(targetRotation.x, Mathf.Round(targetRotation.y / 45) * 45, targetRotation.z), Time.deltaTime * rotationSpeed);
                //Vector3 vel = input * speed * Time.deltaTime;
                //rig.velocity = vel;

                rig.AddForce(input * speed);

                swordJoint.GetComponent<HurtEnemy>().isAttackTriggered = false;
                swordJoint.GetComponent<BoxCollider>().enabled = false;
            }
            else
            {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Sword Attack"))
                {
                    swordJoint.GetComponent<BoxCollider>().enabled = true;
                }
            }
        }



        animator.SetFloat("moveX", hAxis);
        animator.SetFloat("moveZ", vAxis);

        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            //Enables player animations when moving
            animator.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
            animator.SetFloat("lastMoveZ", Input.GetAxisRaw("Vertical"));
        }
    }

    private bool IsGrounded()
    {
        return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x, 
            col.bounds.min.y, col.bounds.center.z), col.radius * .9f, groundLayer);
    }

}
