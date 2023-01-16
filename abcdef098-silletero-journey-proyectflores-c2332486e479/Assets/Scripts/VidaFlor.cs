using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaFlor : MonoBehaviour
{
    private Player _player;

    private AnimacionVidaFlor _florAnim;

    [SerializeField]
    public float contadorTiempo = 5.0f; // el tiempo que se demora cada petalo en desaparecer

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _florAnim = this.gameObject.GetComponent<AnimacionVidaFlor>();
    }

    void Update()
    {
        if (_florAnim.isDead == true)
        {
            _player.dead();
        }
    }

    public void ColisionFlor() {
        _florAnim.ResetFlowerLife();
     }
}
