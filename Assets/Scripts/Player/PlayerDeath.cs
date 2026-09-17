using UnityEngine;
using System.Collections;

public class PlayerDeath : MonoBehaviour
{
    public float deathDelay = 1.5f; //tiempo para que se vea la animacion antes de cambiar de escena(calcular a mano)

    Animator anim;
    PlayerMovement movement;
    Rigidbody2D rb;
    bool dead;

    void Start()
    {
        anim = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Die()
    {
        if (dead) return; //por si choca dos cosas en el mismo frame
        dead = true;

        anim.SetTrigger("dead");
        movement.enabled = false;
        rb.linearVelocity = Vector2.zero;

        //congeloo todo el juego pero dejo al animator en tiempo real
        //para que la animacion de muerte se reproduzca igual
        anim.updateMode = AnimatorUpdateMode.UnscaledTime;
        Time.timeScale = 0f;

        StartCoroutine(LoadDeathScene());
    }

    IEnumerator LoadDeathScene()
    {
        //tiene que ser Realtime porque con timeScale en 0 el tiempo normal no avanza
        yield return new WaitForSecondsRealtime(deathDelay);

        Time.timeScale = 1f; //si no lo vuelvo a 1 la otra escena arranca congelada
        GameManager.Instance.Perder();
    }
}