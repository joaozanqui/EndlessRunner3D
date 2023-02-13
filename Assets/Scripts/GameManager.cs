using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int score;
    public static GameManager inst;
    
    [SerializeField] Text scoreText;

    [SerializeField] PlayerMoviment playerMovement;

    public int metrosPercorridos = 0;
    public Text metrosText;

    //sons
    [SerializeField] private AudioSource collectSound;


    public void IncrementScore()
    {
        collectSound.Play();
        score++;
        scoreText.text = ": " + score;
        if(score >= 20)
            playerMovement.runType = 2;

        //Aumentar a velocidade
        playerMovement.speed += playerMovement.speedIncreasePerPoint;

    }

    public void DistanciaPercorrida()
    {
        if(playerMovement.alive)
            metrosPercorridos++;
        metrosText.text = metrosPercorridos + "m";
    }

    private void Awake()
    {
        inst = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("DistanciaPercorrida", 0f, 0.2f * (playerMovement.inicialSpeed / playerMovement.speed));
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Vivo: " + playerMovement.alive);
    }
}
