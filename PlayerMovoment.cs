using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerMovoment : MonoBehaviour
{
    [SerializeField] float currentSpeed;
    Rigidbody2D rb2D;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform pointToSpawnBullet;
    float nextTimeToShoot;
    public float health;
    public float damage;
    [SerializeField] Slider healthBar;
    [SerializeField] float ammo;
    int ammoShooted;
    [SerializeField] TextMeshProUGUI ammoText;
    private AudioSource audioSourceSoundEffects;
    [SerializeField] AudioClip ak47AudioClip;
    [SerializeField] TextMeshProUGUI plockedInfoText;
    int medKits;
    [SerializeField] TextMeshProUGUI medKitText;
    [SerializeField] float boostedSpeed;
    float orginal_Speed;
    float timeLeftForBoostedSpeed;
    [SerializeField] TextMeshProUGUI timeLeftForBoostedSpeedAmount_Text;
    [SerializeField] TextMeshProUGUI timeLeftForBoostedSpeed_Text;
    bool reloding;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] AudioClip relodAudio;
    float nextTimeToPlayRelodAudio;
    [SerializeField] AudioClip ammoBoxPickedUpAudioClip;
    [SerializeField] AudioClip healingAudioClip;
    [SerializeField] AudioClip errorAudioClip;
    float nextTimeToChangeSpeed;
    bool isBoostedSpeed;
    public int score;
    [SerializeField] TextMeshProUGUI scoreValueTextInGameNotInDieMenu;
    [SerializeField] TextMeshProUGUI scoreValueInDieMenuText;

    [SerializeField] AudioClip pickUpSoulSoundEffect;
    public GameObject pauseMenu;
    public GameObject dieMenu;
    public bool canMove;

    [SerializeField] TextMeshProUGUI highScoreTextIsItHighScoreOrNot;
    public Camera cam;

    Vector2 movement;
    Vector2 mousePos;

    int explosionAmmo;
    [SerializeField] GameObject explosionBullet;
    [SerializeField] TextMeshProUGUI explosionBulletText;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        nextTimeToShoot = Time.time + 0.10f;
        healthBar.maxValue = health;
        healthBar.minValue = 0;
        audioSourceSoundEffects = GetComponent<AudioSource>();
        plockedInfoText.gameObject.SetActive(false);

        orginal_Speed = currentSpeed;

        canMove = true;
        cam = Camera.main;

       
    }

    private void FixedUpdate()
    {
         if(canMove == true)
        {
            Move();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(canMove == true)
        {
            if(transform.position.x < -37.04)
            {
                transform.position = new Vector2(-37f,transform.position.y);
            }else if(transform.position.x > 37)
            {
                transform.position = new Vector2(37f, transform.position.y);

            }
            if(transform.position.y < -23.1)
            {
                transform.position = new Vector2(transform.position.x, -23f);
                
            }else if (transform.position.y > 23.1)
            {
                transform.position = new Vector2(transform.position.x, 23f);

            }
            AlignDirection();

            RotateTowardsMousePosition();

            Shoot();

            DestroyIfHealthZero();

            HealthBar();

            AmmoText();

            MedKitText();

            HealUsingMedKits();

            TimeLeftForBoostedSpeedFunc();

            RelodByPressingR();

            ScoreText();

            Score();

            ExplosionBulletText();
        }
        Pause();
    }
    
    void ExplosionBulletText()
    {
        explosionBulletText.text = explosionAmmo.ToString();
    }
    void AlignDirection()
    {

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }
    void ScoreText()
    {
        scoreValueTextInGameNotInDieMenu.text = score.ToString();
    }
    void Score()
    {
        if(score > PlayerPrefs.GetInt("HighScore"))
        {
            scoreValueInDieMenuText.text = score.ToString();
            PlayerPrefs.SetInt("HighScore", score);
            highScoreTextIsItHighScoreOrNot.text = "New High score!";
        }else if(score < PlayerPrefs.GetInt("HighScore"))
        {
            scoreValueInDieMenuText.text = score.ToString();
            highScoreTextIsItHighScoreOrNot.text = "Score:";

        }
    }
    private void Pause()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            pauseMenu.SetActive(true);
            canMove = false;
        }
    }
    private void Move()
    {
        rb2D.MovePosition(rb2D.position + movement * currentSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - rb2D.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb2D.rotation = angle;
        if (Time.time <=nextTimeToChangeSpeed)
        {
           // print("Current Speed" + currentSpeed);
            //print("Orginal Speed" + orginal_Speed);
            //print("Boosted Speed" + orginal_Speed);

            orginal_Speed = Mathf.Lerp(5, 15, Mathf.Clamp01(Time.timeSinceLevelLoad / 100));
            boostedSpeed = Mathf.Lerp(10, 18, Mathf.Clamp01(Time.timeSinceLevelLoad / 120));
            nextTimeToChangeSpeed = Time.time + 10f;
        }
        if(isBoostedSpeed == false)
        {
            currentSpeed = orginal_Speed;
        }


    }

    void RelodByPressingR()
    {
        if(Input.GetKeyDown(KeyCode.R) && reloding == false && ammo >0)
        {
            if(ammoShooted >0)
            {
                reloding = true;
                plockedInfoText.gameObject.SetActive(false);
                plockedInfoText.gameObject.SetActive(true);
                plockedInfoText.text = "Reloding!";
                StartCoroutine(DissoloveOut());
                Invoke("Relod", 1.5f);
                audioSourceSoundEffects.PlayOneShot(relodAudio);
            }
            else
            {
                plockedInfoText.gameObject.SetActive(false);
                plockedInfoText.gameObject.SetActive(true);
                plockedInfoText.text = "You have Full Ammo!";
                StartCoroutine(DissoloveOut());
            }
        }
    }
    void TimeLeftForBoostedSpeedFunc()
    {
        if(isBoostedSpeed == true)
        {
            timeLeftForBoostedSpeed -= Time.deltaTime;
            timeLeftForBoostedSpeedAmount_Text.text = Mathf.RoundToInt(timeLeftForBoostedSpeed).ToString();
            timeLeftForBoostedSpeedAmount_Text.enabled = true;
            timeLeftForBoostedSpeed_Text.enabled = true;
        }else
        {
            timeLeftForBoostedSpeedAmount_Text.enabled = false;
            timeLeftForBoostedSpeed_Text.enabled = false;
        }
    }
    void HealthBar()
    {
        healthBar.value = health;
    }
    void AmmoText()
    {
        ammoText.text = ammo.ToString();
    }
    void DestroyIfHealthZero()
    {
        if(health <=0)
        {
            dieMenu.SetActive(false);
            dieMenu.SetActive(true);
            Destroy(gameObject);
        }
    }
    private void RotateTowardsMousePosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

        transform.up = direction;
    }
    void MedKitText()
    {
        medKitText.text =  medKits.ToString();
    }
    void HealUsingMedKits()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1) && medKits <= 0)
        {
            StopCoroutine(DissoloveOut());
            audioSourceSoundEffects.PlayOneShot(errorAudioClip);
            plockedInfoText.gameObject.SetActive(false);
            plockedInfoText.gameObject.SetActive(true);
            plockedInfoText.text = "You don't have any medkits";
            StartCoroutine(DissoloveOut());
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && medKits > 0)
        {
            if (health < 100)
            {
                audioSourceSoundEffects.PlayOneShot(healingAudioClip);
                if (health + 20 <= healthBar.maxValue)
                {
                    health += 20;
                }
                else if (health + 20 > healthBar.maxValue)
                {
                    float reMaining = healthBar.maxValue - health;
                    health += reMaining;
                }
                plockedInfoText.gameObject.SetActive(false);
                plockedInfoText.gameObject.SetActive(true);
                plockedInfoText.text = "Healed!";
                StartCoroutine(DissoloveOut());
                medKits--;
            }else
            {
                audioSourceSoundEffects.PlayOneShot(errorAudioClip);
                plockedInfoText.gameObject.SetActive(false);
                plockedInfoText.gameObject.SetActive(true);
                plockedInfoText.text = "Health Is Already Full!";
                StartCoroutine(DissoloveOut());
            }

        }
    }
    void Shoot()
    {
     
        if(Input.GetKeyDown(KeyCode.Z) && explosionAmmo >0)
        {
            if (muzzleFlash.isPlaying)
            {
                muzzleFlash.Stop();
            }
            audioSourceSoundEffects.PlayOneShot(ak47AudioClip);
            explosionAmmo--;
            nextTimeToShoot = Time.time + 0.10f;
            GameObject _bullet = Instantiate(explosionBullet, pointToSpawnBullet.transform.position, pointToSpawnBullet.transform.rotation);
            Rigidbody2D rb = _bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(pointToSpawnBullet.up * 50f, ForceMode2D.Impulse);
            //  _bullet.transform.position = pointToSpawnBullet.transform.position;
            ammo--;
            muzzleFlash.Play();
        }
        if (Input.GetKey(KeyCode.Mouse0) && Time.time >= nextTimeToShoot && ammo>0 && ammoShooted <=20 && reloding ==  false)
        {
            if(muzzleFlash.isPlaying)
            {
                muzzleFlash.Stop();
            }
            audioSourceSoundEffects.PlayOneShot(ak47AudioClip);
            ammoShooted++;
            nextTimeToShoot = Time.time + 0.10f;
             GameObject _bullet = Instantiate(bullet,pointToSpawnBullet.transform.position,pointToSpawnBullet.transform.rotation);
            Rigidbody2D rb = _bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(pointToSpawnBullet.up * 50f, ForceMode2D.Impulse);
          //  _bullet.transform.position = pointToSpawnBullet.transform.position;
            ammo--;
            muzzleFlash.Play();

        }else if (ammoShooted ==20 && reloding == false)
        {
            plockedInfoText.gameObject.SetActive(false);
            plockedInfoText.gameObject.SetActive(true);
            plockedInfoText.text = "Reloding!";
            StartCoroutine(DissoloveOut());
            Invoke("Relod", 1.5f);

            if (Time.time >= nextTimeToPlayRelodAudio)
            {
                audioSourceSoundEffects.PlayOneShot(relodAudio);
                nextTimeToPlayRelodAudio = Time.time + 1f;
            }

            reloding = true;
        }
    }

    void Relod()
    {
        ammoShooted = 0;
        reloding = false;
    }
   
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("AmmoBox"))
        {
            plockedInfoText.gameObject.SetActive(false);

            plockedInfoText.gameObject.SetActive(true);
            Destroy(collision.gameObject);
            ammo += 25;
            plockedInfoText.text = "Ammo box plocked!";
            audioSourceSoundEffects.PlayOneShot(ammoBoxPickedUpAudioClip);
           StartCoroutine(DissoloveOut());
        }else if(collision.CompareTag("Health"))
        {
            medKits++;
           
            plockedInfoText.gameObject.SetActive(false);

            plockedInfoText.gameObject.SetActive(true);
            Destroy(collision.gameObject);
            plockedInfoText.text = "Medkit plocked!";
            StartCoroutine(DissoloveOut());
            Destroy(collision.gameObject);
            audioSourceSoundEffects.PlayOneShot(ammoBoxPickedUpAudioClip);
        }
        else if(collision.CompareTag("SpeedPowerUp"))
         {
            StopCoroutine(SpeedPowerUp());
            StartCoroutine(SpeedPowerUp());

            plockedInfoText.gameObject.SetActive(false);

            plockedInfoText.gameObject.SetActive(true);
            Destroy(collision.gameObject);
            plockedInfoText.text = "Speed Boosted!";
            StartCoroutine(DissoloveOut());
            audioSourceSoundEffects.PlayOneShot(ammoBoxPickedUpAudioClip);

        }else if(collision.CompareTag("Soul"))
        {
            score += 40;
            audioSourceSoundEffects.PlayOneShot(pickUpSoulSoundEffect);
            Destroy(collision.gameObject);
        }else if(collision.CompareTag("BlackHole"))
        {
            health = 0;
        }else if(collision.CompareTag("ExplosionBox"))
        {
            explosionAmmo += 2;

            plockedInfoText.gameObject.SetActive(false);

            plockedInfoText.gameObject.SetActive(true);
            Destroy(collision.gameObject);
            plockedInfoText.text = "Explosion Bullet Plocked!";
            StartCoroutine(DissoloveOut());
            audioSourceSoundEffects.PlayOneShot(ammoBoxPickedUpAudioClip);
            Destroy(collision.gameObject);
        }
    }

  
    IEnumerator DissoloveOut()
    {
        plockedInfoText.GetComponent<Animator>().SetBool("DissoloveOut", false);
        yield return new WaitForSeconds(1.2f);
        plockedInfoText.GetComponent<Animator>().SetBool("DissoloveOut", true);
    }

    IEnumerator SpeedPowerUp()
    {
        isBoostedSpeed = true;
        timeLeftForBoostedSpeed = 10f;
        currentSpeed = boostedSpeed;
        yield return new WaitForSeconds(10f);
        currentSpeed = orginal_Speed;
        isBoostedSpeed = false;
    }
}
