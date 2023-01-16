using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformRotation : MonoBehaviour
{
    
     private Player player;
     bool canControl = true;

     void Start()
     {
         player = GetComponent<Player>(); 
     }

    //   Update is called once per frame
     void Update()
     {
        

             if (Input.GetKeyDown(KeyCode.W))
             {

                 transform.forward = new Vector3(-90, 0, 0);

             }

             if (Input.GetKeyDown(KeyCode.S))
             {
                 transform.forward = new Vector3(90, 0, 0);

             }


             if (Input.GetKeyDown(KeyCode.A))
             {

                 transform.forward = new Vector3(0, 0, -90);

             }

             if (Input.GetKeyDown(KeyCode.D))
             {
                 transform.forward = new Vector3(-1, 0, 90);
             }

         } 

        


       
}

