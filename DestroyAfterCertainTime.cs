using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterCertainTime : MonoBehaviour
{
    public float delay;
    float nextTime;
    PlayerMovoment player;
    // Start is called before the first frame update
    void Start()
    {
        nextTime = Time.time + delay;
        player = GameObject.FindObjectOfType<PlayerMovoment>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player !=null)
        {
            if (player.canMove == true)
            {
                if (nextTime - Time.time <= 3 && nextTime - Time.time >= 1)
                {
                    transform.GetComponent<Animator>().SetBool("WillBeGone", true);
                }
                else if (nextTime - Time.time <= 1)
                {
                    transform.GetComponent<Animator>().speed = 5;

                }
                StartCoroutine(DestroyAfterCertainTimeFunction());
            }
        }
       
      
    }

    public IEnumerator DestroyAfterCertainTimeFunction()
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
