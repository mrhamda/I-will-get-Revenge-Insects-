using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportInsect : MonoBehaviour
{
    public float radius;
    [SerializeField] Vector2 minMaxX;
    [SerializeField] Vector2 minMaxY;
    [SerializeField] GameObject circleTelePort;
    bool canTelePort = true;
    int length;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] collisionPlayer = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (Collider2D coll in collisionPlayer)
        {
            if(coll.GetComponent<PlayerMovoment>() !=null && canTelePort == true)
            {

                EnemeyDetection[] enemys = GameObject.FindObjectsOfType<EnemeyDetection>();
                

                GameObject[] blackHoles = GameObject.FindGameObjectsWithTag("BlackHole");
                if(blackHoles.Length >0)
                {
                    int number = Random.Range(0, blackHoles.Length);
                    coll.transform.position = blackHoles[number].transform.position;
                }else if(enemys.Length > 0 )
                {
                    GameObject circleTelePort0 = Instantiate(circleTelePort,transform.position,Quaternion.identity);
                    int number = Random.Range(0, enemys.Length);
                    coll.transform.position = enemys[number].transform.position;
                    GameObject circleTelePort1 = Instantiate(circleTelePort,coll.transform.position, Quaternion.identity);
                   StartCoroutine(SetBoolTrue(circleTelePort1, circleTelePort0));
                    canTelePort = false;
                    StartCoroutine(Wait());
                 
                }
            }
        }

        IEnumerator SetBoolTrue(GameObject circleTelePort1, GameObject circleTelePort0)
        {
            yield return new WaitForSeconds(0.5f);
            circleTelePort1.GetComponent<Animator>().SetBool("Disspear", true);
            circleTelePort0.GetComponent<Animator>().SetBool("Disspear", true);

        }

        IEnumerator Wait()
        {
            yield return new WaitForSeconds(0.5f);
            canTelePort = true;

        }
    }
  
}
