using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public PlayerHealth playerHealth;

    private Animator anim;

    public void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Update()
    {
        if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("GameOver");
        }
    }
}
