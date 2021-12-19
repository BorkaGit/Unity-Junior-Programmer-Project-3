using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float jumpForce=10.0f;
    public float gravityModifier;
    public bool isOnGround;
    public bool gameOver;
    public bool doubleJumped;
    public bool doubleSpeed = false;
    public bool walkEnter = true;
    private float currentTime1=0.0f;
    private float currentTime=0.0f;
    private float freezeTime1=0.2f;
    private float freezeTime=1.5f;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    private Animator playerAnim; 
    public AudioClip jumpSound;
    public AudioClip crashSound;
    private AudioSource playerAudio;
    private float topBound = 7.0f;
    private float score;
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier; 
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        if (walkEnter)
        {
            playerAnim.SetFloat("Speed_Multiplier", 0.5f);
        }
        
        if (Input.GetKeyDown(KeyCode.S) && walkEnter)
        {
            walkEnter = false;

        }

        if (Input.GetKey(KeyCode.LeftShift) && isOnGround && !walkEnter )
            {
                doubleSpeed = true;
                playerAnim.SetFloat("Speed_Multiplier", 2.0f);
            }
            else if (doubleSpeed && !walkEnter)
            {
                doubleSpeed = false;
                playerAnim.SetFloat("Speed_Multiplier", 1.0f);
            }
        




        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver && !walkEnter)
        {
            playerRb.AddForce(Vector3.up * jumpForce,ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            dirtParticle.Stop();
        }else if (Input.GetKeyDown(KeyCode.Space) && !isOnGround && !doubleJumped && !gameOver && !walkEnter)
        {
            playerRb.AddForce(Vector3.up * jumpForce,ForceMode.Impulse);
            playerAnim.SetTrigger("Jump_trig");
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            dirtParticle.Stop();
            doubleJumped = true;
            currentTime = Time.deltaTime;

        }

        if (doubleJumped && currentTime+freezeTime<=Time.deltaTime && !walkEnter)
        {
            doubleJumped = false;
        }
        if (transform.position.y > topBound && !walkEnter)
        {
            transform.position = new Vector3(transform.position.x, topBound, transform.position.z);
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
        } else if(collision.gameObject.CompareTag("Obstacle"))

        {
            gameOver = true;
            Debug.Log("Game Over!");
            playerAnim.SetBool("Death_b",true);
            playerAnim.SetInteger("DeathType_int",1);
            explosionParticle.Play();
            playerAudio.PlayOneShot(crashSound, 1.0f);
            dirtParticle.Stop();
        }
        
    }


 
}
