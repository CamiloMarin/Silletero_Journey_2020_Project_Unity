using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTronco : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            anim.SetTrigger("pisado");
        }
    }


}
