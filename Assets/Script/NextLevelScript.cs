using UnityEngine;

public class NextLevelScript : MonoBehaviour
{
    [SerializeField] private AudioSource finishSoundEffect;

    private void Start()
    {
        finishSoundEffect = GetComponent<AudioSource>();
    }
    
    private IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(1f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            finishSoundEffect.Play();
            // Load the next scene
            GameObject player = other.gameObject;
            player.GetComponent<PlayerController>().enabled = false;
            player.GetComponent<Animator>().SetTrigger("Fade");
            StartCoroutine(LoadNextScene());
        }
    }
}
