using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Reload the scene
            UnityEngine.SceneManagement.SceneManager.LoadScene(
                            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
    }
}
