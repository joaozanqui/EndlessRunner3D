using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMoviment : MonoBehaviour
{
    public string menu;
    public GameObject GameOver;
    public bool alive = true;

    public float speed = 20;
    public float inicialSpeed = 20f;
    [SerializeField] Rigidbody rb;

    float horizontalInput;
    [SerializeField] float horizontalMultiplier = 0.75f;

    public float speedIncreasePerPoint = 0.1f;

    [SerializeField] float jumpForce = 400f;
    [SerializeField] LayerMask groundMask;

    private Animator  anim;
    
    public int runType = 1;
    private bool pulando = false;
    private bool caindo = false;
    private bool salto = false;

    //Sons
    [SerializeField] private AudioSource jumpSoundEffect;
    [SerializeField] private AudioSource dieSound;
    [SerializeField] private AudioSource runSound;
    [SerializeField] private AudioSource die2Sound;
    [SerializeField] private AudioSource musicSound;

    private void FixedUpdate() 
    {
        if(!alive)
            return;
        
        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.fixedDeltaTime * horizontalMultiplier;
        rb.MovePosition(rb.position + forwardMove + horizontalMove);
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
    }


    // Update is called once per frame
    private void Update()
    {
        Debug.Log("Vel: " + speed);
        anim.SetInteger("transition", runType);

        
        //if(alive && !salto)

        if(!salto)
            pulando = false;
        if(rb.velocity.y == 0 && pulando)
        {
            caindo = true;
            anim.SetInteger("caindo", 1);
            pulando = false;    
            anim.SetInteger("pulando", 0);
        }

        if(rb.velocity.y != 0 && salto)
        {
            anim.SetInteger("transition", 0);
            if(rb.velocity.y > 0) {
                anim.SetInteger("pulando", 1);
                pulando = true;
            }    
            else if(rb.velocity.y < 0) {
                anim.SetInteger("pulando", 0);
                pulando = false;
                anim.SetInteger("caindo", 1);
                caindo = true;
            }
        }
        else if(rb.velocity.y == 0 && caindo) {
                runSound.Play();
                anim.SetInteger("caindo", 0);
                caindo = false;
                salto = false;
        }
        
        horizontalInput = Input.GetAxis("Horizontal");

        if(Input.GetKeyDown(KeyCode.Space)) {
            Jump();
        }


        if(transform.position.y < -5)
            Die();
    }

    public void Die()
    {
        dieSound.Play();
        runSound.Stop();
        musicSound.Stop();
        runType = 0;
        alive = false;
        Invoke("Gameover", 0);
        die2Sound.Play();
    }

    public void Gameover(){
        GameOver.SetActive(true);
    }

    public void BackMenu(){
        SceneManager.LoadScene(menu);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    private void Jump()
    {
        //ver se ta no chao
        float height = GetComponent<Collider>().bounds.size.y;
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, (height/2) + 0.1f, groundMask);
        //Se estiver pulando
        if(isGrounded && alive) {
            runSound.Stop();
            jumpSoundEffect.Play();
            salto = true;
            rb.AddForce(Vector3.up * jumpForce);
        }
    }
}
