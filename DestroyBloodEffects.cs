using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBloodEffects : MonoBehaviour
{
    [SerializeField] float delay;
    PlayerMovoment player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerMovoment>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.canMove == true)
        {
            Invoke("destory", delay);
        }
        
    }

    void destory()
    {
        Destroy(gameObject);
    }
}
