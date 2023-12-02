using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int LifeTotal = 1;
    public Rigidbody2D rigidbody;
    public int EnemySpeed = 5;
    public Animator anime;

    private float timeBtwAttack;
    public float startTimeBtwAttack;
    public Transform enemyAttackPos;
    public LayerMask whatIsPlayer;
    public float attackRange;
    public int damage = 1;
    private bool attackingPlayer = false;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.velocity = transform.right * EnemySpeed;
        anime.SetTrigger("attackt");
        attackingPlayer = true;
        if(timeBtwAttack <= 0)
        {
            if(attackingPlayer== true)
            {
                Collider2D[] playerToDamage = Physics2D.OverlapCircleAll(enemyAttackPos.position, attackRange, whatIsPlayer);
                for (int i = 0; i < playerToDamage.Length; i++)
                {
                    playerToDamage[i].GetComponent<PlayerController>().TakeDamage(damage);
                }
            }
            timeBtwAttack = startTimeBtwAttack;

        } else
        {
            timeBtwAttack -= Time.deltaTime;
        }
        if (LifeTotal <= 0)
        {
            Destroy(gameObject);
        }
    }
}
