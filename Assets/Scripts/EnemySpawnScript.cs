using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnScript : MonoBehaviour
{
    public GameObject playerObject;
    public GameObject enemySpawnArea;
    private int enemyNumber = 1;
    private int summonedEnemies = 0;
    public GameObject enemyPrefab;

    private float minX;
    private float maxX;
    private float minY;
    private float maxY;
    private float summonCooldown = 0f;
    private int level = 0;

    private List<GameObject> enemyTrack = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        float posX = enemySpawnArea.transform.position.x;
        float posY = enemySpawnArea.transform.position.y;
        float scaleX = enemySpawnArea.transform.localScale.x;
        float scaleY = enemySpawnArea.transform.localScale.y;

        minX = posX - (scaleX/2);
        minY = posY - (scaleY/2);
        maxX = posX + (scaleX/2);
        maxY = posY + (scaleY/2);

        summonEnemy();
    }

    // Update is called once per frame
    void Update()
    {   

        if (summonedEnemies < enemyNumber && summonCooldown < 0) {
            summonEnemy();
        }

        if (summonCooldown > 0) {
            summonCooldown -= Time.deltaTime;
        }

        enemyTrack.RemoveAll(item => item == null);
        if (enemyTrack.Count == 0) {
            level += 1;
            int increaseFactor = (int) Mathf.Floor(level/5);
            enemyNumber = 1 + increaseFactor;

            summonedEnemies = 0;
            summonEnemy();
        }
    }

    private void summonEnemy() {
        float enemyXPosition = NextFloat(minX, maxX);
        float enemyYPosition = NextFloat(minY, maxY);

        GameObject newEnemy = Instantiate(enemyPrefab, new Vector3(enemyXPosition, enemyYPosition, 0f), transform.rotation);
        enemyTrack.Add(newEnemy);
        Enemy e = newEnemy.GetComponent<Enemy>();
        e.target = playerObject;
        e.speed = 0.2f + Mathf.Floor(level/10)/10;
        summonedEnemies += 1;
        summonCooldown = 0.5f;
    }

    private float NextFloat(float min, float max){
        System.Random random = new System.Random();
        double val = (random.NextDouble() * (max - min) + min);
        return (float) val;
    }

    public void Restart() {
        level = 0;
        foreach (GameObject a in enemyTrack) {
            Destroy(a);
        }
        enemyNumber = 1;
        summonedEnemies = 0;
    }
}
