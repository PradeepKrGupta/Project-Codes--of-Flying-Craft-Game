using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;

    [Header("Missile")]
    public GameObject missile;
    public Transform missileSpawnPosition;
    public Transform muzzleSpawnPosition;
    public float destroyTime = 3f;

    [Header("Audio")]
    public AudioClip fireSound;
    public AudioClip collisionSound;
    private AudioSource audioSource;

    [Header("Score")]
    public Text scoreText;           // UI Text to display score
    private int score = 0;           // Player's score

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        UpdateScoreUI();             // Initialize score UI
    }

    private void Update()
    {
        PlayerMovement();
        PlayerShoot();
    }

    void PlayerMovement()
    {
        float xPos = Input.GetAxis("Horizontal");
        float yPos = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(xPos, yPos, 0) * speed * Time.deltaTime;
        transform.Translate(movement);
    }

    void PlayerShoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnMissile();
            SpawnMuzzleFlash();
            PlaySound(fireSound);
        }
    }

    void SpawnMuzzleFlash()
    {
        GameObject muzzle = Instantiate(GameManager.Instance.muzzleFlash, muzzleSpawnPosition);
        muzzle.transform.SetParent(null);
        Destroy(muzzle, destroyTime);
    }

    void SpawnMissile()
    {
        GameObject gm = Instantiate(missile, missileSpawnPosition);
        gm.transform.SetParent(null);
        Destroy(gm, destroyTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            IncreaseScore(1);        // Increase score by 1 when hit by enemy
            PlaySound(collisionSound);
        }
        else if (collision.gameObject.tag == "Bonous")
        {
            IncreaseScore(2);        // Increase score by 2 when hit by bonus
            PlaySound(collisionSound);
        }

        // Destroy the player after collision
        Destroy(this.gameObject);
        ShowFinalScore();           // Display final score
    }

    public void IncreaseScore(int increment)
    {
        score += increment;         // Add the increment to the current score
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score;
    }

    void ShowFinalScore()
    {
        Debug.Log("Game Over! Final Score: " + score);
        // Display final score in UI or Game Over screen
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
