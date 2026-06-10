using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float velocidad = 5f;
    public float fuerzaSalto = 7f;

    [Range(0f, 1f)]
    public float corteSalto = 0.5f; // cuanto se le baja al salto si suelta antes

    [Header("Sensacion de salto")]
    public float multiplicadorCaida = 2.5f;  // que tan pesado cae (mas alto = cae mas rapido)
    public float multiplicadorSubida = 2f;    // que tan rapido sube cuando NO mantiene la tecla

    public Transform piesCheck;
    public Vector2 volumenCaja = new Vector2(0.5f, 0.1f);
    public LayerMask capaSuelo;

    Rigidbody2D rb;
    Animator anim;

    float inputX;
    bool enSuelo;
    bool quiereSaltar;
    bool soltoSalto;
    bool manteniendoSalto;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            quiereSaltar = true;

        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
            soltoSalto = true;

        // se mantiene apretada alguna tecla de salto?
        manteniendoSalto = Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        enSuelo = Physics2D.OverlapBox(piesCheck.position, volumenCaja, 0f, capaSuelo);

        if (anim != null)
        {
            anim.SetBool("enSuelo", enSuelo);
            anim.SetFloat("velocidad", Mathf.Abs(inputX));
        }

        if (inputX > 0)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
        else if (inputX < 0)
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(inputX * velocidad, rb.linearVelocity.y);

        if (quiereSaltar && enSuelo)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerzaSalto);
        }
        quiereSaltar = false;

        if (soltoSalto)
        {
            if (rb.linearVelocity.y > 0f)
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * corteSalto);

            soltoSalto = false;
        }

        AplicarGravedadExtra();
    }

    void AplicarGravedadExtra()
    {
        // si esta cayendo, le sumo gravedad extra para que caiga mas pesado
        if (rb.linearVelocity.y < 0f)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (multiplicadorCaida - 1f) * Time.fixedDeltaTime;
        }
        // si esta subiendo pero ya solto la tecla, tambien lo hago bajar mas rapido
        else if (rb.linearVelocity.y > 0f && !manteniendoSalto)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (multiplicadorSubida - 1f) * Time.fixedDeltaTime;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (piesCheck == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(piesCheck.position, volumenCaja);
    }
}