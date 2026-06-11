using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float jumpForce = 22f;

    [Range(0f, 1f)]
    public float jumpCut = 0.5f; // cuanto se le baja al salto si suelta antes

    [Header("Jump Feeling")]
    public float fallMultiplier = 2.5f;  // que tan pesado cae (mas alto = cae mas rapido)
    public float riseMultiplier = 2f;    // baja mas rapido si ya suelto la tecla subiendo

    public Vector2 groundCheckSize = new Vector2(0.5f, 0.1f);
    public LayerMask groundLayer;

    [Header("Crouch Movement")]
    public bool cantJumpCrouched = true;
    public Collider2D standingCollider;
    public Collider2D crouchingCollider;

    [Header("Heavy Fall")]
    public float fastFallForce = 80f;     // agacharse en el aire empuja para abajo
    public float maxFallSpeed = 25f;

    Rigidbody2D rb;
    Animator anim;

    bool crouching;
    bool grounded;
    bool wantsToJump;
    bool releasedJump;
    bool holdingJump;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        rb.freezeRotation = true; // que no gire por el Rigidbody2D dynamic 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            wantsToJump = true;

        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
            releasedJump = true;

        // y si mantienen apretada alguna tecla de salto?
        holdingJump = Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        // el chequeo de piso sale del borde de abajo del collider activo, asi no se desajusta si cambio el sprite o la escala porque se rompe todo
        Collider2D currentCollider = crouching ? crouchingCollider : standingCollider;
        Vector2 groundCheckPos = new Vector2(currentCollider.bounds.center.x, currentCollider.bounds.min.y);
        grounded = Physics2D.OverlapBox(groundCheckPos, groundCheckSize, 0f, groundLayer);

        // para estar agachado solo veo si estan apretando la tecla, sino parpadea entre las dos formas y se bugea 
        bool downPressed = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);

        if (downPressed && !crouching)
            Crouch();
        else if (!downPressed && crouching)
            StandUp();

        if (anim != null)
        {
            anim.SetBool("grounded", grounded);
            anim.SetBool("crouching", crouching);
        }
    }

    void FixedUpdate()
    {
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
        // si esta subiendo pero ya suelto la tecla, tambien lo hago bajar mas rapido
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