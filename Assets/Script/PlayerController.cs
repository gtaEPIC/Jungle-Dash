using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizontal;
    private bool isFacingRight = true;
    private int Life;
    [SerializeField] private GameObject[] hearts;
    private bool heartsVisible;
    [SerializeField] private Sprite heartLost;
    [SerializeField] private float damageCooldown = 2f;
    private bool canTakeDamage = true;
    private bool canDash = true;
    private bool isDashing;
    public Animator playerAnim;
    [SerializeField] private float dashingPower = 24f;
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] private float dashingCooldown = 1f;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpingPower = 16f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private AudioSource jumpSoundEffect;
    [SerializeField] private AudioSource attackSoundEffect;

    private void Start()
    {
        Life = hearts.Length;
        if (!heartLost) Debug.LogError("Heart lost sprite not set");
    }

    private void Update()
    {
        if (isDashing)
        {
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpSoundEffect.Play();
            playerAnim.SetTrigger("jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            playerAnim.SetTrigger("dash");
            StartCoroutine(Dash());
        }

        Flip();

        // poor implementation
        // if (Life<= 0)
        // {
        //     Destroy(gameObject);
        // }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private IEnumerator Dash()
    {
        attackSoundEffect.Play();
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
    
    private IEnumerator ResetDamageCooldown()
    {
        yield return new WaitForSeconds(damageCooldown);
        Debug.Log("Damage cooldown reset");
        canTakeDamage = true;
    }

    private IEnumerator TriggerHearts(GameObject heart)
    {
        Animator heartAnim = heart.GetComponent<Animator>();
        Transform heartTransform = heart.transform;
        heartTransform.localScale = new Vector3(0f,0f, 0f);
        heartTransform.position = new Vector3(0f, 0f, 0f);
        heart.SetActive(true);
        heartAnim.SetTrigger("Triggered");
        yield return new WaitForSeconds(0.5f);
    }
    
    public void TakeDamage(int damage)
    {
        if (!canTakeDamage)
        {
            return;
        }
        canTakeDamage = false;
        Life -= damage;
        Debug.Log("Damage taken: " + damage + " Life: " + Life);
        if (!heartsVisible)
        {
            heartsVisible = true;
            foreach (GameObject heart in hearts)
            {
                StartCoroutine(TriggerHearts(heart));
            }
        }
        if (Life > 0)
        {
            hearts[Life].GetComponent<SpriteRenderer>().sprite = heartLost;
        }
        else
        if (Life <= 0)
        {
            // Reload the scene
            UnityEngine.SceneManagement.SceneManager.LoadScene(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
        StartCoroutine(ResetDamageCooldown());
    }

}