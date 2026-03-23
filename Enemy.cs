using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.Threading.Tasks;
[RequireComponent(typeof(AudioSource))]
public  class Enemy : MonoBehaviour
{
    PlayerMovoment player;
    [SerializeField] Vector2 minMaxYInsect;
    [SerializeField] Vector2 minMaxXInsect;
    [SerializeField] Vector2 speedMinMax;
    float speed;
    public float health;
    public float damage;
    [SerializeField] Vector2[] postisions;
    bool moveBack;
    int index;
    public string name;
    [SerializeField] ParticleSystem playerBloodEffectPrefab;
    private AudioSource audioSource;
    [SerializeField] AudioClip insectLaugh;
    Collider2D powerUp;
    public GameObject circle;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerMovoment>();
        RandomGeneratePositions();
        DiffuclityOfSpeed();
        audioSource = GetComponent<AudioSource>();

        moveBack = false;
     

    }

    // Update is called once per frame
    void Update()
    {
        if(player !=null)
        {
            if (player.canMove == true)
            {
                SetVolume();

                MoveForward();

                IfHealthIsZeroDestroy();

                RotateToWardPlayer();

                BackToMissionIfReachedPosition();
                if (player != null)
                {
                    transform.GetComponent<AIDestinationSetter>().target = player.transform;

                }
                else
                {
                    transform.GetComponent<AIDestinationSetter>().target = null;

                }

            }
            else
            {
                transform.GetComponent<AIDestinationSetter>().target = transform;
            }
        }
       
       

    }
    void DestroyIfNotExplosiv()
    {
        if(name != "Explosive")
        {
            Destroy(gameObject);
        }
    }
    void InstiateExplosionEffect()
    {
        if(name == "Explosive")
        {
            GameObject ci = Instantiate(circle, transform.position, Quaternion.identity);
        }
        
    }
   void SetVolume()
    {
        audioSource.volume = PlayerPrefs.GetFloat("SoundEffectVolume");

    }
    void MoveForward()
    {
        if(player !=null)
        { 
            if(moveBack == false)
            {
                // transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed / 100);
                transform.GetComponent<AIDestinationSetter>().target = player.transform;
                transform.GetComponent<AIPath>().maxSpeed = this.speed;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, postisions[index], 15 * Time.deltaTime);
                transform.GetComponent<AIDestinationSetter>().target = null;

            }
        }
    }

    void BackToMissionIfReachedPosition()
    {
        if(moveBack == true)
        {
            if(transform.position.x >= postisions[index].x && transform.position.y >= postisions[index].y)
            {
                if(powerUp !=null)
                {
                    Destroy(powerUp.gameObject);
                }
                moveBack = false;
                health += 50;
                transform.localScale = new Vector3(transform.localScale.x + 0.1f, transform.localScale.y + 0.1f, transform.localScale.z + 0.1f);
            }
        }
    }
    void RandomGeneratePositions()
    {
         index = Random.Range(0, postisions.Length);
        transform.position = postisions[index];
    }
    void DiffuclityOfSpeed()
    {
        speed = Mathf.Lerp(speedMinMax.x, speedMinMax.y, Mathf.Clamp01(Time.time / 200));

    }
    void IfHealthIsZeroDestroy()
    {
        if (health <= 0)
        {
            InstiateExplosionEffect();
            
            Destroy(gameObject);
        }
    }

    void RotateToWardPlayer()
    {
        if(player !=null)
        {
            //   transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, player.transform.position.x + -180);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<BlackHoleIfTouched>() !=null)
        {
            
        }
        if(collision.GetComponent<PlayerMovoment>() !=null && player.tag == "Untagged")
        {
            if(name != "Explosive")
            {
                player.health -= damage;

            }
            if (name == "Explosive")
            {
                GameObject ci = Instantiate(circle, transform.position, Quaternion.identity);
            }
            ParticleSystem _blood = Instantiate(playerBloodEffectPrefab);
            _blood.transform.position = new Vector3(collision.GetComponent<PlayerMovoment>().transform.position.x, collision.GetComponent<PlayerMovoment>().transform.position.y, -1f);
            _blood.Play();
           StartCoroutine(Damage());
        }
        if (collision.CompareTag("Health")|| collision.CompareTag("AmmoBox") || collision.CompareTag("SpeedPowerUp") || collision.CompareTag("Soul") || collision.CompareTag("ExplosionBox"))
        {
            powerUp = collision;
            audioSource.PlayOneShot(insectLaugh);
            moveBack = true;

        }
        if(collision.CompareTag("BlackHole"))
        {
            DestroyIfNotExplosiv();
        }
        if(collision.CompareTag("ExplosionBullet"))
        {
            GameObject ci = Instantiate(circle, transform.position, Quaternion.identity);
            Destroy(collision);
        }
       


    }
   

    IEnumerator Damage()
    {
        if(player !=null)
        {
            player.tag = "CannotBeHitten";
            yield return new WaitForSeconds(0.8f);
            player.tag = "Untagged";
        }
       

    }
   
  

  

}
