using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonController : MonoBehaviour
{
    Vector3 playerMovment;

    //private const float SLOPESPEED = 2.5f;
    private const float AIRSPEED = 3.25f;

    //private Vector3 StartGravity = new Vector3(0.0f, -9.8f, 0.0f);
    //private Vector3 SlopeGravity = new Vector3(0.0f, -100f, 0.0f);

    public float speed;
    private float moveSpeed;
    public float rotateSpeed;
    public float groundCheckLength;

    public float jumpTimeSpan = 0.3f;
    private float jumptime;

    public float jumpForce;
    public float fallForce;

    private float deathDelay;

    bool canDoubleJump;

    //float maxSlopeHeight = 70f;
    //float minSlopeHeight = 90f;

    public LayerMask groundLayer;

    //public GameObject SlopeDetect;
    public GameObject camPivot;

    Animator ani;
    Rigidbody rb;
    CapsuleCollider col;
  
    public bool dead;

    void Start()
    {
        col = GetComponent<CapsuleCollider>();
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        deathDelay = 2;
        
    }

    void FixedUpdate()
    {
        //RaycastHit hitSlopeUp;

        //RaycastHit hitSlopeDown;

        ////Finding upwards slopes 
        //if (Physics.Raycast(gameObject.transform.position, Vector3.forward, out hitSlopeUp, rayLength))
        //{
        //    if (hitSlopeUp.collider.gameObject.layer == 0)
        //    {
        //        float slopeAngleUp = Vector3.Angle(hitSlopeUp.normal, Vector3.up);

        //        if(slopeAngleUp >= maxSlopeHeight)
        //        {
        //            speed = SLOPESPEED;
        //            Physics.gravity = SlopeGravity;
        //        }
        //        else
        //        {
        //            speed = STARTSPEED;
        //            Physics.gravity = StartGravity;
        //        }

        //    }
        //}

        //Finding downwards slopes
        //if (Physics.Raycast(SlopeDetect.transform.position, Vector3.forward, out hitSlopeDown, rayLength))
        //{
        //    if (hitSlopeDown.collider.gameObject.layer == 0)
        //    {
        //        float slopeAngleDown = Vector3.Angle(hitSlopeDown.normal, Vector3.down);

        //        if (slopeAngleDown <= minSlopeHeight)
        //        {
        //            speed = SLOPESPEED;
        //            Physics.gravity = SlopeGravity;
        //        }
        //        else
        //        {
        //            speed = STARTSPEED;
        //            Physics.gravity = StartGravity;
        //        }
        //    }
        //}

        if (!dead)
        {
            movementManager();
            //AirManager();
        }
        else
        {
            ani.SetBool("Dead", false);
            deathDelay -= Time.deltaTime;
        }
    }

    void Update()
    {
        if (!dead)
        {
            AirManager();
        }
    }

    void movementManager()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        //MOVEMENT HAPPENING  
        playerMovment = (camPivot.transform.forward * ver * speed * Time.deltaTime) + (camPivot.transform.right * hor * speed * Time.deltaTime);
        Vector3.Normalize(playerMovment * Time.deltaTime);
        transform.Translate(playerMovment, Space.World);

        //Rotate player when moving 
        if (hor != 0 || ver != 0)
        {
            transform.rotation = Quaternion.Euler(0.0f, gameObject.transform.rotation.eulerAngles.y, 0.0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(playerMovment.x, 0f, playerMovment.z));
            gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
            moveSpeed = 1;

        }
        //Not Moving, for the animation trigger
        else
        {
            moveSpeed = 0;
        }        

        if (IsGround())
        {
            //---------------------------------------------------Animation sets
            ani.SetBool("isGrounded", true);

            ani.SetFloat("moveSpeed", moveSpeed);
        }
        else
        {
            //  IN AIR
            ani.SetBool("isGrounded", false);
            //speed = AIRSPEED;
        }
    }

    void AirManager()
    {

        if (IsGround())
        {
            canDoubleJump = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGround())
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
            else
            {
                if (canDoubleJump)
                {
                    rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                    canDoubleJump = false;
                }
            }
        }
   
    }

    //public void TakeDamage(float damage)
    //{
    //    playerHealth = playerHealth - damage;
    //    if(playerHealth <= 0)
    //    {
    //        Dead();
    //    }
    //}

    //void Dead()
    //{
    //    ani.SetBool("Dead", true);
    //    dead = true;
    //}

    private bool IsGround()
    {
        RaycastHit hit;
        return Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, groundCheckLength, groundLayer);
        
    }
   
}

   
 





