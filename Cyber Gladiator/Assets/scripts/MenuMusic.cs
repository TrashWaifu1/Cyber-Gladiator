using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMusic : MonoBehaviour
{
    public AudioSource Source;
    public int FinalMenuScene = 3;

    void Awake()
    {
        if (FindObjectsOfType<MenuMusic>().Length > 1)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        Source.volume = SceneManager.GetActiveScene().buildIndex <= FinalMenuScene ? 1 : 0;
    }
}