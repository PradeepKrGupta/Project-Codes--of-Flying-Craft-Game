using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 0.1f;          // Initial speed of the enemy
    public float speedIncrement = 0.2f; // Speed increase after 30 seconds
    public float speedIncreaseInterval = 30f; // Time interval for speed increase

    private void Start()
    {
        // Start the coroutine to increase enemy speed
        StartCoroutine(IncreaseSpeedOverTime());
    }

    void Update()
    {
        // Move the enemy downward
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with a missile
        if (collision.gameObject.tag == "Missile")
        {
            // Find the PlayerController script and increase the score
            PlayerController player = FindObjectOfType<PlayerController>();
            if (player != null)
            {
                player.IncreaseScore(1); // Call the method to increase the score
            }

            // Destroy both the missile and the enemy
            Destroy(collision.gameObject); // Destroy the missile
            Destroy(this.gameObject);     // Destroy the enemy
        }
    }

    // Coroutine to gradually increase enemy speed
    private IEnumerator IncreaseSpeedOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(speedIncreaseInterval); // Wait for 30 seconds
            speed += speedIncrement; // Increase the speed
            Debug.Log("Enemy speed increased to: " + speed);
        }
    }
}

