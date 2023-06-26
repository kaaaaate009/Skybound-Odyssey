using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rgBdy;

    [SerializeField] private AudioSource deathSound;
    void Start()
    {
        anim = GetComponent<Animator>();
        rgBdy = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Die();
        }
    }

    private void Die()
    {
        deathSound.Play();
        rgBdy.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
    }

    private void ResetartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
