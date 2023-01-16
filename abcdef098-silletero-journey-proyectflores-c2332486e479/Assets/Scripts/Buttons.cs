using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public GameObject Panel1;
   
     void Start()
    {
        Panel1.SetActive(false);
        Time.timeScale = 1;
    }


     public void Dibujo()
    {
        SceneManager.LoadScene("Dibujo", LoadSceneMode.Single);
    }

     public void Crossy()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

     public void Menu()
    {
        SceneManager.LoadScene("MenuPpal", LoadSceneMode.Single);
    }

      public void AbrirPanel()
     {
         Panel1.SetActive(true);
     }

     public void Cerrarpaneles()
     {
         Panel1.SetActive(false);
     }

      
}
