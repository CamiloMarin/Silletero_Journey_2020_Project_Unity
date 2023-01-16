using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacionVidaFlor : MonoBehaviour
{
    Animator anim;

    public bool isDead;

    private bool isCountingDown = false;
    private int contadorPetalos;
    private float timeRemaining;

    private VidaFlor _vidaFlor;

    void Start()
    {
        _vidaFlor = this.gameObject.GetComponent<VidaFlor>();
        anim = GetComponent<Animator>();
        contadorPetalos = anim.GetInteger("Flower_Petalos");
        timeRemaining = _vidaFlor.contadorTiempo;

        // si no está contando que empiece a contar:

        if (!isCountingDown)
        {
            isCountingDown = true;

            isDead = false;
        }
    }

    private void Update()
    {
        if (isCountingDown)
        {
            timeRemaining -= Time.deltaTime;
        }
        if (timeRemaining < 0 && contadorPetalos > 0)
        {
            ResetTimeRemeaning();
            contadorPetalos -= 1;
            anim.SetInteger("Flower_Petalos", contadorPetalos);
        }
        else if (timeRemaining < 0 && contadorPetalos == 0)
        {
            isCountingDown = false;
            isDead = true;
        }
    }

    private void ResetTimeRemeaning()
    {
        timeRemaining = _vidaFlor.contadorTiempo;
    }

    public void ResetFlowerLife()
    {
        anim.SetInteger("Flower_Petalos", 6);
        contadorPetalos = 6;
        
    }
}
