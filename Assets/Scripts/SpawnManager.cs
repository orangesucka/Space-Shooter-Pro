using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _enemys;
    [SerializeField]
    private GameObject[] _powerups;
    [SerializeField]
    private int _spawnTime;
    //[SerializeField]
    //private int _waveTwo, _waveThree;

    private bool _stopEnemySpawning = false;
    private bool _stopPowerUpSpawning = false;
    private bool _waveTwo = false;
    private bool _waveThree = false;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void Update()
    {
    }
    public void StartSpawning()
    {
        StartCoroutine(SpawnPowerUpRoutine());
        StartCoroutine(MorePowerUpRoutine());

        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnEnemy5Routine());
        StartCoroutine(SpawnEnemy6Routine());
        StartCoroutine(SpawnEnemy7Routine());
        //StartCoroutine(SpawnEnemy1Routine());
        //StartCoroutine(SpawnEnemy2Routine());
        //StartCoroutine(SpawnEnemy3Routine());

        //StartCoroutine(SpawnEnemy4Routine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(2.0f);
        while (_stopEnemySpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-7.5f, 7.5f), 6f, 0f);
            Instantiate(_enemys[0], transform.position + posToSpawn, Quaternion.identity);
            _spawnTime = Random.Range(5, 5);
            yield return new WaitForSeconds(_spawnTime);
        }
    }
    IEnumerator SpawnEnemy1Routine()
    {
        yield return new WaitForSeconds(2.0f);
        while (_stopEnemySpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(0f, 7f), 6f, 0f);
            Instantiate(_enemys[1], transform.position + posToSpawn, Quaternion.identity);
            _spawnTime = Random.Range(5, 5);
            yield return new WaitForSeconds(10);
        }
    }
    IEnumerator SpawnEnemy2Routine()
    {
        yield return new WaitForSeconds(2.0f);
        while (_stopEnemySpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-7f, 0f), 6f, 0f);
            Instantiate(_enemys[2], transform.position + posToSpawn, Quaternion.identity);
            _spawnTime = Random.Range(5, 5);
            yield return new WaitForSeconds(10);
        }
    }

    IEnumerator SpawnEnemy3Routine()
    {
        yield return new WaitForSeconds(2.0f);
        while (_stopEnemySpawning == false)
        {
            Vector3 posToSpawn = new Vector3(-8.5f, Random.Range(1f, 5f), 0f);
            Instantiate(_enemys[3], transform.position + posToSpawn, Quaternion.identity);
            _spawnTime = Random.Range(5, 5);
            yield return new WaitForSeconds(10);
        }
    }

    IEnumerator SpawnEnemy4Routine()
    {
        yield return new WaitForSeconds(2.0f);
        while (_stopEnemySpawning == false)
        {
            Vector3 posToSpawn = new Vector3(8.5f, Random.Range(1f, 5f), 0f);
            Instantiate(_enemys[4], transform.position + posToSpawn, Quaternion.identity);
            _spawnTime = Random.Range(5, 5);
            yield return new WaitForSeconds(_spawnTime);
        }
    }

    IEnumerator SpawnEnemy5Routine()
    {
        yield return new WaitForSeconds(2.0f);
        while (_stopEnemySpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-7.5f, 7.5f), 6, 0f);
            Instantiate(_enemys[5], transform.position + posToSpawn, Quaternion.identity);
            _spawnTime = Random.Range(5, 5);
            yield return new WaitForSeconds(_spawnTime);
        }
    }
    IEnumerator SpawnEnemy6Routine()
    {
        yield return new WaitForSeconds(2.0f);
        while (_stopEnemySpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-7.5f, 7.5f), 6.5f, 0f);
            Instantiate(_enemys[6], transform.position + posToSpawn, Quaternion.identity);
            _spawnTime = Random.Range(5, 5);
            yield return new WaitForSeconds(_spawnTime);
        }
    }
    IEnumerator SpawnEnemy7Routine()
    {
        yield return new WaitForSeconds(2.0f);
        while (_stopEnemySpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-7.5f, 7.5f), 6.5f, 0f);
            Instantiate(_enemys[7], transform.position + posToSpawn, Quaternion.identity);
            _spawnTime = Random.Range(5, 5);
            yield return new WaitForSeconds(_spawnTime);
        }
    }
    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        while(_stopPowerUpSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-7.5f, 7.5f), 6f, 0f);
            int randomPowerUp = Random.Range(0, 7);
            Instantiate(_powerups[randomPowerUp], transform.position + posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(5f, 10f));
        }
    }

    IEnumerator MorePowerUpRoutine()
    {
        yield return new WaitForSeconds(5f);
        while(_stopPowerUpSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-7.5f, 7.5f), 6f, 0f);
            Instantiate(_powerups[6], transform.position + posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(5f, 10f));
        }
    }

    public void Wavetwo()
    {
        Debug.Log("WaveTwo has been activated");
        StartCoroutine(SpawnEnemy3Routine());
        StartCoroutine(SpawnEnemy4Routine());

    }

    public void WaveThree()
    {
        StartCoroutine(SpawnEnemy1Routine());
        StartCoroutine(SpawnEnemy2Routine());
    }

    public void OnPlayerDeath()
    {
        _stopEnemySpawning = true;
        _stopPowerUpSpawning = true;
    }
}