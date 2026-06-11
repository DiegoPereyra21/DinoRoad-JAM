using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 22f;

    [Range(0f, 1f)]
    public float jumpCut = 0.5f; // cuanto se le baja al salto si suelta antes

    [Header("Sensacion de salto")]
    public float fallMultiplier = 2.5f;  // que tan pesado cae (mas alto = cae mas rapido)
    public float riseMultiplier = 2f;    // baja mas rapido si ya solto la tecla subiendo

    public Vector2 groundCheckSize = new Vector2(0.5f, 0.1f);
    public LayerMask groundLayer;

    [Header("Agacharse")]
    public bool slowWhileCrouching = true;
    public float crouchSpeed = 2f;
    public bool cantJumpCrouched = true;
    public Collider2D standingCollider;
    public Collider2D crouchingCollider;

    [Header("Caida rapida")]
    public float fastFallForce = 80f;     // agacharse en el aire empuja para abajo
    public float maxFallSpeed = 25f;

    Rigidbody2D rb;
    Animator anim;

    bool crouching;
    float inputX;
    bool grounded;
    bool wantsToJump;
    bool releasedJump;
    bool holdingJump;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            wantsToJump = true;

        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
            releasedJump = true;

        // se mantiene apretada alguna tecla de salto?
        holdingJump = Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        // el chequeo de piso sale del borde de abajo del collider activo,
        // asi no se desajusta si cambio el sprite o la escala
        Collider2D currentCollider = crouching ? crouchingCollider : standingCollider;
        Vector2 groundCheckPos = new Vector2(currentCollider.bounds.center.x, currentCollider.bounds.min.y);
        grounded = Physics2D.OverlapBox(groundCheckPos, groundCheckSize, 0f, groundLayer);

        // para mantenerme agachado solo miro la tecla (sino parpadea entre las dos formas)
        bool downPressed = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);

        if (downPressed && !crouching)
            Crouch();
        else if (!downPressed && crouching)
            StandUp();

        if (anim != null)
        {
            anim.SetBool("grounded", grounded);
            anim.SetFloat("speed", Mathf.Abs(inputX));
            anim.SetBool("crouching", crouching);
        }

        if (inputX > 0)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
        else if (inputX < 0)
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
    }

    void FixedUpdate()
    {
        // si estoy agachado me muevo mas lento
        float currentSpeed = (crouching && slowWhileCrouching) ? crouchSpeed : moveSpeed;
        rb.linearVelocity = new Vector2(inputX * currentSpeed, rb.linearVelocity.y);

        bool canJump = grounded && !(crouching && cantJumpCrouched);

        if (wantsToJump && canJump)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
        wantsToJump = false;

        if (releasedJump)
        {
            if (rb.linearVelocity.y > 0f)
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * jumpCut);

            releasedJump = false;
        }

        // agacharse en el aire = caida rapida
        if (crouching && !grounded && rb.linearVelocity.y > -maxFallSpeed)
        {
            rb.linearVelocity += Vector2.down * fastFallForce * Time.fixedDeltaTime;
        }

        ApplyExtraGravity();
    }

    void Crouch()
    {
        crouching = true;
        if (standingCollider != null) standingCollider.enabled = false;
        if (crouchingCollider != null) crouchingCollider.enabled = true;
    }

    void StandUp()
    {
        crouching = false;
        if (standingCollider != null) standingCollider.enabled = true;
        if (crouchingCollider != null) crouchingCollider.enabled = false;
    }

    void ApplyExtraGravity()
    {
        // si esta cayendo, le sumo gravedad extra para que caiga mas pesado
        if (rb.linearVelocity.y < 0f)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1f) * Time.fixedDeltaTime;
        }
        // si esta subiendo pero ya solto la tecla, tambien lo hago bajar mas rapido
        else if (rb.linearVelocity.y > 0f && !holdingJump)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (riseMultiplier - 1f) * Time.fixedDeltaTime;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (standingCollider == null) return;
        Gizmos.color = Color.red;
        Vector2 pos = new Vector2(standingCollider.bounds.center.x, standingCollider.bounds.min.y);
        Gizmos.DrawWireCube(pos, groundCheckSize);
    }
}