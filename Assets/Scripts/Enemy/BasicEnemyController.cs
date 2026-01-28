using UnityEngine;

public class BasicEnemyController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public int damage = 1;
    public float damageCooldown = 1f;


    private Transform player;
    private float cooldownTimer;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    void Update()
    {
        if (player == null) return;


        // BLOCK MOEVEMT WHEN STUNNED 
        EnemyHealth health = GetComponent<EnemyHealth>();
        if (health != null && health.IsStunned())
            return;


        // MOVEMENT TO THE PLAYER
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0f;


        transform.position += direction * moveSpeed * Time.deltaTime;


        // LOOK AT PLAYER
        if (direction != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(direction);


        // DMG COOLDOWN
        if (cooldownTimer > 0f)
            cooldownTimer -= Time.deltaTime;
    }


    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && cooldownTimer <= 0f)
        {
            GlobalHealth playerHealth = collision.gameObject.GetComponent<GlobalHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                cooldownTimer = damageCooldown;
            }
        }
    }
}