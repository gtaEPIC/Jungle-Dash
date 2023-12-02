using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelScript : MonoBehaviour
{
    [SerializeField] private AudioSource finishSoundEffect;

    private void Start()
    {
        finishSoundEffect = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            finishSoundEffect.Play();
            // Load the next scene
            UnityEngine.SceneManagement.SceneManager.LoadScene(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
