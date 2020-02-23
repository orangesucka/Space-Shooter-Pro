using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefabs;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] _powerups;


    private bool _stopEnemySpawning = false;
    private bool _stopPowerUpSpawning = false;

    // Start is called before the first frame update
    void Start()
    {

    }
    public void StartSpawning()
    {
        StartCoroutine(SpawnPowerUpRoutine());
        StartCoroutine(SpawnEnemyRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(2.0f);
        while (_stopEnemySpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-7.5f, 7.5f), 6f, 0f);
            GameObject newEnemy = Instantiate(_enemyPrefabs, transform.position + posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(Random.Range(1.0f, 3f));
            //GameObject newLaser = Instantiate(_laserPrefab,transform.position,Quaternion.identity);
            //newLaser.transform.parent = _laserPrefab.transform;
        }
    }
    /*IEnumerator EnemySpawnLazer()
    {
        while (_stopEnemySpawning == false)
        {
            GameObject newLaser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
            newLaser.transform.parent = _laserPrefab.transform;
        }
    }*/
    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        while(_stopPowerUpSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-7.5f, 7.5f), 6f, 0f);
            int randomPowerUp = Random.Range(0, 3);
            Instantiate(_powerups[randomPowerUp], transform.position + posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(5f, 10f));
           
        }
    }

    public void OnPlayerDeath()
    {
        _stopEnemySpawning = true;
        _stopPowerUpSpawning = true;
    }
}