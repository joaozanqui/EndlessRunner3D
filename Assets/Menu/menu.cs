using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menu : MonoBehaviour
{
    public string cena;
    public GameObject instrucoesPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(cena);
    }

    public void QuitGame()
    {
        // Editor Unity
        //UnityEditor.EditorApplication.isPlaying = false;
        // Jogo Compilado
        Application.Quit();
    }

    public void instrucoes()
    {
        instrucoesPanel.SetActive(true);
    }

    public void BackMenu()
    {
        instrucoesPanel.SetActive(false);
    }
}
