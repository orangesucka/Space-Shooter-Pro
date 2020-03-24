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

    private UIManager _uiManager;

    // Start is called before the first frame update
    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null)
        {
            Debug.Log("The UI Manager in SpawnManager is NULL");
        }
    }
    public void StartSpawning()
    {
        StartCoroutine(SpawnPowerUpRoutine());
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnBGLPowerUpRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(2.0f);
        while (_stopEnemySpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-7.5f, 7.5f), 6f, 0f);
            GameObject newEnemy = Instantiate(_enemyPrefabs, transform.position + posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(Random.Range(1f, 4f));          
        }
    }
    
    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        while(_stopPowerUpSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-7.5f, 7.5f), 6f, 0f);
            int randomPowerUp = Random.Range(0, 5);
            Instantiate(_powerups[randomPowerUp], transform.position + posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(5f, 10f));
        }
    }

    IEnumerator SpawnBGLPowerUpRoutine()
    {
        yield return new WaitForSeconds(5f);
        while(_stopPowerUpSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-7.5f, 7.5f), 6f, 0f);
            Instantiate(_powerups[5], transform.position + posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(15f, 20f));
        }
    }

    public void OnPlayerDeath()
    {
        _stopEnemySpawning = true;
        _stopPowerUpSpawning = true;
    }
}