using UnityEngine;
using System.Collections.Generic;

public class SlimeForm : PlayerBase
{
    [SerializeField] LayerMask wallLayer;
    [SerializeField] float wallCheckDistance = 0.8f;
    bool isTouchingWall;
    bool isStuckToWall;

    // RASTRO
    [SerializeField] GameObject slimeMarkPrefab; // 🔥 de volta
    List<GameObject> slimeMarks = new List<GameObject>();
    [SerializeField] float markInterval = 0.05f;
    float markTimer;
    [SerializeField] float slimeMarkScale = 0.7f;

    // EFEITO GEL
    [SerializeField] float squashX = 1.1f;
    [SerializeField] float squashY = 0.9f;
    [SerializeField] float stretchX = 0.9f;
    [SerializeField] float stretchY = 1.1f;
    [SerializeField] float returnSpeed = 6f;
    Vector3 originalScale;

    protected override void ApplyFormStats()
    {
        speed = 3f;
        jumpForce = 7f;
        gravityScale = 1f;
        rb.gravityScale = gravityScale;
        sr.color = new Color(0.2f, 1f, 0.3f);
        //OUTLINE DA FORMA
        float outlineStrength = 0.4f;
        Color c = sr.color;
        outlineSR.color = new Color(
        c.r * outlineStrength,
        c.g * outlineStrength,
        c.b * outlineStrength,
        1f);

        //TRAIL
        SetTrailFromSprite(0.4f, 0.5f, 0f);
        baseScale = new Vector3(1.2f, 0.8f, 1f);
        originalScale = Vector3.one;
    }

    void CheckWall()
    {
        Vector2 origin = (Vector2)transform.position + Vector2.up * 0.2f;
        RaycastHit2D hitRight = Physics2D.Raycast(origin, Vector2.right, wallCheckDistance, wallLayer);
        RaycastHit2D hitLeft = Physics2D.Raycast(origin, Vector2.left, wallCheckDistance, wallLayer);
        isTouchingWall = hitRight.collider != null || hitLeft.collider != null;
        Debug.DrawRay(origin, Vector2.right * wallCheckDistance, Color.red);
        Debug.DrawRay(origin, Vector2.left * wallCheckDistance, Color.blue);
    }

    protected override void Update()
    {
        base.Update();
        CheckWall();
        LeaveSlimeTrail();

        if (Input.GetKeyDown(KeyCode.E) && isTouchingWall)
            isStuckToWall = !isStuckToWall;

        if (isStuckToWall && isTouchingWall)
        {
            rb.gravityScale = 0;
            float moveY = Input.GetAxis("Vertical");
            rb.linearVelocity = new Vector2(0, moveY * speed);
        }
        else
        {
            rb.gravityScale = gravityScale;
            isStuckToWall = false;
        }

        ApplyGelEffect();
    }

    void LeaveSlimeTrail()
    {
        if (!isGrounded && !isStuckToWall) return;
        markTimer += Time.deltaTime;
        if (markTimer < markInterval) return;

        Vector3 spawnPos = transform.position;
        Quaternion rotation = Quaternion.identity;

        if (isGrounded)
        {
            spawnPos += Vector3.down * 0.5f;
        }
        else if (isStuckToWall)
        {
            float direction = transform.localScale.x;
            spawnPos += new Vector3(direction * 0.5f, 0f, 0f);
            rotation = direction > 0
                ? Quaternion.Euler(0, 0, 90)
                : Quaternion.Euler(0, 0, -90);
        }

        if (slimeMarkPrefab != null)
        {
            GameObject mark = Instantiate(slimeMarkPrefab, spawnPos, rotation);
            mark.transform.localScale = Vector3.one * slimeMarkScale;
            slimeMarks.Add(mark);
        }

        markTimer = 0f;
    }

    void ApplyGelEffect()
    {
        Vector3 targetScale = originalScale;

        if (rb.linearVelocity.y < -0.2f)
            targetScale = new Vector3(squashX, squashY, 1f);
        else if (rb.linearVelocity.y > 0.2f)
            targetScale = new Vector3(stretchX, stretchY, 1f);

        SetVisualScale(Vector3.Lerp(currentVisualScale, targetScale, returnSpeed * Time.deltaTime));
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        isStuckToWall = false;
        rb.gravityScale = gravityScale;

        foreach (var mark in slimeMarks)
            if (mark != null) Destroy(mark);

        slimeMarks.Clear();
    }

    public override void SpecialAbility() { }
}