using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destructoresMetro : MonoBehaviour
{


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        Destroy(col.gameObject);

    }



}
