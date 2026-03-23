using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleExplosionScript : MonoBehaviour
{
    public float delay;
    private void Update()
    {
        delay -= Time.deltaTime;
        if(delay <=0)
        {
            gameObject.GetComponent<Animator>().SetBool("Disspear", true);
        }
    }
    public void destoy()
    {
        Destroy(gameObject);
    }
}
