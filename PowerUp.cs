using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    bool moveBack = false;
    private Collider2D target;
    PlayerMovoment player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerMovoment>();
    }

    // Update is called once per frame
    void Update()
    {
        if(moveBack == true && target !=null && player.canMove == true)
        {
            transform.position = target.transform.position;
            transform.GetComponent<DestroyAfterCertainTime>().StopAllCoroutines();
            transform.GetComponent<Animator>().SetBool("WillBeGone", false);
            
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Enemy>() !=null)
        {
            
            moveBack = true;
            target = collision;
        }
    }
}
