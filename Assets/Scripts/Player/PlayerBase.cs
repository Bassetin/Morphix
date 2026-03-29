using UnityEngine;

public abstract class PlayerBase : MonoBehaviour
{

    [Header("Movimento")]
    public float speed;
    public float jumpForce;
    public float gravityScale;

    [Header("Forma Visual")]
    public Vector3 baseScale = Vector3.one;
    protected Vector3 currentVisualScale = Vector3.one;

    protected TrailRenderer trail;
    protected SpriteRenderer sr;
    protected SpriteRenderer outlineSR;
    protected Rigidbody2D rb;
    protected bool isGrounded;

    protected virtual void Awake(){
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        trail = GetComponent<TrailRenderer>();
        outlineSR = transform.Find("Outline").GetComponent<SpriteRenderer>();   
    }


    protected virtual void OnEnable(){
        ApplyFormStats();

        currentVisualScale = baseScale;
        transform.localScale = baseScale; // aplica forma da forma

           if(trail != null)
            trail.Clear(); //  limpa rastro ao trocar forma
    }

    protected virtual void OnDisable()
{
    // base para as formas usarem
}

    protected virtual void Update(){
        HandleMovement();
        HandleJump();

    }

    protected abstract void ApplyFormStats();

    public abstract void SpecialAbility();


    public void SetVisualScale(Vector3 scale)
{
    currentVisualScale = scale;
}


    protected void SetTrailFromSprite(float time, float startWidth, float endWidth)
{
    Color c = sr.color;

    trail.startColor = c;
    trail.endColor = new Color(c.r, c.g, c.b, 0f);

    trail.time = time;
    trail.startWidth = startWidth;
    trail.endWidth = endWidth;

    trail.Clear();
}

    protected void HandleMovement(){
        
    float moveX = Input.GetAxis("Horizontal");
    rb.linearVelocity = new Vector2(moveX * speed, rb.linearVelocity.y);

    float dirX = moveX != 0 ? Mathf.Sign(moveX) : Mathf.Sign(transform.localScale.x);

    transform.localScale = new Vector3(
        dirX * Mathf.Abs(currentVisualScale.x),
        currentVisualScale.y,
        currentVisualScale.z
    );
}

    protected void HandleJump(){
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded){
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Ground")){
            isGrounded = true;
        }
    }

    protected virtual void OnCollisionExit2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Ground")){
            isGrounded = false;
        }
    }
}


