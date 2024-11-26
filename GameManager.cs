using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Prefabs")]
    public GameObject enemyPrefab;
    public GameObject bonousPrefab;

    [Header("Timers")]
    public float enemyDistroyTime = 4f;
    public float bonousSpawnInterval = 4f; // Time between bonus spawns
    public float bonousDistroyTime = 4f;

    [Header("Particle Effects")]
    public GameObject explosion;
    public GameObject muzzleFlash;

    [Header("Panels")]
    public GameObject startMenu;
    public GameObject pauseMenu;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        startMenu.SetActive(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 0f;

        // Start spawning enemies and bonuses
        InvokeRepeating("InstantiateEnemy", 1f, 1f); // Spawns an enemy every 1 second
        InvokeRepeating("InstantiateBonus", 2f, bonousSpawnInterval); // Spawns a bonus every 4 seconds
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame(true);
        }
    }

    void InstantiateEnemy()
    {
        // Randomize the position for enemy
        Vector3 enemypos = new Vector3(Random.Range(-3.5f, 4f), 6f);
        GameObject enemy = Instantiate(enemyPrefab, enemypos, Quaternion.identity);
        Destroy(enemy, enemyDistroyTime);
    }

    void InstantiateBonus()
    {
        // Randomize the position for bonus
        Vector3 bonouspos = new Vector3(Random.Range(-3.5f, 4f), 6f);
        GameObject bonous = Instantiate(bonousPrefab, bonouspos, Quaternion.identity);
        Destroy(bonous, bonousDistroyTime);
    }

    public void StartGameButton()
    {
        startMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void PauseGame(bool isPaused)
    {
        if (isPaused == true)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
