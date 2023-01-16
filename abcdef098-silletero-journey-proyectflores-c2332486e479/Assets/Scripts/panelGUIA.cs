using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class panelGUIA : MonoBehaviour
{

    public GameObject Panel;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waiter());
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    IEnumerator waiter()
{
     Panel.SetActive(true);

    //Wait for 5 seconds
    yield return new WaitForSeconds(5);

    Panel.SetActive(false);

    
}
}
