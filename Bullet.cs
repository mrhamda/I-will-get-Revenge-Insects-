using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float delay;
    Vector2 direction;
    PlayerMovoment player;
    [SerializeField] ParticleSystem bloodEffectEnemy;
    [SerializeField] Color weakInsectColor;
    [SerializeField] Color strongInsectColor;
    [SerializeField] Color explosivInsectColor;

    [SerializeField] GameObject soulPrefab;
    [SerializeField] ParticleSystem trailParticleSystemEffectBullet;
    ParticleSystem effect;
    float nextTimeToSpawn;
    [SerializeField] GameObject circle;
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
            StartCoroutine(DestroyAfterCertainTime());
           
            Summon();
            
        }
       

    }
    void Summon()
    {
        if(Time.time >= nextTimeToSpawn)
        {
            if(gameObject.tag != "ExplosionBullet")
            {
                effect = Instantiate(trailParticleSystemEffectBullet, transform.position, Quaternion.identity);
                nextTimeToSpawn = delay + Time.time + 1;
                Rigidbody2D rb = effect.GetComponent<Rigidbody2D>();
                rb.AddForce(transform.up * 50f, ForceMode2D.Impulse);
            }
           

        }

    }
    IEnumerator DestroyAfterCertainTime()
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
    
    

    public void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (collision.GetComponent<Enemy>() !=null)
        {
            if (collision.GetComponent<Enemy>().health - player.damage <= 0)
            {
                Instantiate(soulPrefab, transform.position, Quaternion.identity);
                player.score += 10;
                Destroy(gameObject);
            }
            if (player.tag == "CannotBeHitten" && collision.GetComponent<Enemy>().health - player.damage <=0)
            {
                player.tag = "Untagged";
            }
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.health -= player.damage;
            ParticleSystem _blood = Instantiate(bloodEffectEnemy);
            
                if (collision.GetComponent<Enemy>().name == "Strong")
                {
                    ParticleSystem.MainModule settings = _blood.main;
                    settings.startColor = strongInsectColor;
                    settings.startSize = 0.7f;
                }
                else if (collision.GetComponent<Enemy>().name == "Weak")
                {
                    ParticleSystem.MainModule settings = _blood.main;
                    settings.startColor = weakInsectColor;

                }
                else if (collision.GetComponent<Enemy>().name == "Explosive")
                {
                    ParticleSystem.MainModule settings = _blood.main;
                    settings.startColor = explosivInsectColor;
                }
            
            


             _blood.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            
            
                Destroy(gameObject);
            
        }
        else if(collision.GetComponent<PlayerMovoment>() == null && collision.GetComponent<PowerUp>() == null)
        {
            Destroy(gameObject);
    
        }
    }
}
