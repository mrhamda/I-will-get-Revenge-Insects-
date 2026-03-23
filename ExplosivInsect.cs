using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosivInsect : MonoBehaviour
{
    Enemy enemy;
    PlayerMovoment player;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
        if(player !=null)
        {
            player = GameObject.FindObjectOfType<PlayerMovoment>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(player !=null)
        {
            
        }
       
    }
}
