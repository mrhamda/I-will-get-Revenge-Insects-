using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    PlayerMovoment player;
    [SerializeField] Vector2 diffuclityMinMax;
    float nextTimeToSpawnWeakEnemies;
    float nextTimeToSpawnHelpItems;

    [SerializeField] GameObject[] helpItemsPrefabs;
    [SerializeField] Vector2 minMaxPositionsXHelpItems;
    [SerializeField] Vector2 minMaxPositionsYHelpItems;
    [SerializeField] GameObject[] insectPrefabs;
    [SerializeField] GameObject weakInsectPrefabs;
    [SerializeField] GameObject strongInsectPrefabs;
    [SerializeField] GameObject explosionInsect;
    float nextTimeToSpawnStrongEnemies;
    float nextTimeToSpawnExplosionEnemies;
    [SerializeField] GameObject explosionBulletBoxPrefab;
    [SerializeField] GameObject teleportInsectPrefab;

    float nextTimeToSpawnExplosionBulletBox;
    float nextTimeToSpawnTeleportInsects;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerMovoment>();
        nextTimeToSpawnStrongEnemies = Time.time + 10f;
        nextTimeToSpawnExplosionEnemies = Time.time + 30f;
        nextTimeToSpawnExplosionBulletBox = Time.time + 60f;
        nextTimeToSpawnTeleportInsects = Time.time + 90f;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.canMove == true)
        {
            SpawnEnemies();
            SpawnHelpItems();
        }
       
    }
   
   void SpawnEnemies()
    {
        int amountOfkEnemies = GameObject.FindObjectsOfType<Enemy>().Length;
        if (Time.time >= nextTimeToSpawnStrongEnemies)
        {
            float secondsBetweenSpawn = Mathf.Lerp(13, 5, Mathf.Clamp01(Time.timeSinceLevelLoad / 150));
            nextTimeToSpawnStrongEnemies = Time.time + secondsBetweenSpawn + secondsBetweenSpawn;
           Instantiate(strongInsectPrefabs);

        }


        if (Time.time >= nextTimeToSpawnWeakEnemies && amountOfkEnemies <= 12)
         {
            float secondsBetweenSpawn = Mathf.Lerp(diffuclityMinMax.x, diffuclityMinMax.y, Mathf.Clamp01(Time.timeSinceLevelLoad / 150));
         
             nextTimeToSpawnWeakEnemies = Time.time + secondsBetweenSpawn;
              Instantiate(weakInsectPrefabs);
        }

        if (Time.time >= nextTimeToSpawnExplosionEnemies && amountOfkEnemies <= 12)
        {
            float secondsBetweenSpawn = Mathf.Lerp(30, 10, Mathf.Clamp01(Time.timeSinceLevelLoad / 200));
            nextTimeToSpawnExplosionEnemies = Time.time + secondsBetweenSpawn + secondsBetweenSpawn + secondsBetweenSpawn;
            Instantiate(explosionInsect);
        }
        if (Time.time >= nextTimeToSpawnTeleportInsects && amountOfkEnemies <= 12)
        {
            float secondsBetweenSpawn = Mathf.Lerp(13, 5, Mathf.Clamp01(Time.timeSinceLevelLoad / 200));
            nextTimeToSpawnTeleportInsects = Time.time + secondsBetweenSpawn + secondsBetweenSpawn + secondsBetweenSpawn;
            Instantiate(teleportInsectPrefab);
        }


    }

    void SpawnHelpItems()
    {
        if(Time.time >=nextTimeToSpawnExplosionBulletBox)
        {
            float secondsBetweenSpawn = Mathf.Lerp(60, 20, Mathf.Clamp01(Time.time / 180));
            nextTimeToSpawnExplosionBulletBox = Time.time + secondsBetweenSpawn;
            int number = Random.Range(0, helpItemsPrefabs.Length);
            GameObject help_Item = Instantiate(explosionBulletBoxPrefab);
            Vector2 randomPosition = new Vector2(Random.Range(minMaxPositionsXHelpItems.x, minMaxPositionsXHelpItems.y), Random.Range(minMaxPositionsYHelpItems.x, minMaxPositionsYHelpItems.y));
            help_Item.transform.position = randomPosition;
        }
        int amountOfHelpItems = GameObject.FindGameObjectsWithTag("AmmoBox").Length;
        if (Time.time >= nextTimeToSpawnHelpItems && amountOfHelpItems <=5)
        {
            float secondsBetweenSpawn = Mathf.Lerp(8, 3, Mathf.Clamp01(Time.time / 180));
            nextTimeToSpawnHelpItems = Time.time + secondsBetweenSpawn;
            int number = Random.Range(0, helpItemsPrefabs.Length);
            GameObject help_Item = Instantiate(helpItemsPrefabs[number]);
            Vector2 randomPosition = new Vector2(Random.Range(minMaxPositionsXHelpItems.x, minMaxPositionsXHelpItems.y), Random.Range(minMaxPositionsYHelpItems.x, minMaxPositionsYHelpItems.y));
            help_Item.transform.position = randomPosition;
        }
    }
}

