using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleIfTouched : MonoBehaviour
{
    public GameObject circlePrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("ExplosionBullet"))
        {
            GameObject ci = Instantiate(circlePrefab, collision.transform.position, Quaternion.identity);

        }
    }
}
