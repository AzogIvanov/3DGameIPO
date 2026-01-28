using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Proyectiles normales")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float attackCooldown = 0.3f;

    [Header("Fan Attack (Leitmotivs)")]
    public GameObject fanProjectilePrefab;
    public int fanProjectiles = 5;
    public float spreadAngle = 30f;
    public bool isFanAttack = false;

    private float cooldownTimer;

    void Update()
    {
        if (cooldownTimer > 0f)
            cooldownTimer -= Time.deltaTime;

        if ((Input.GetMouseButton(0) || Input.GetKey(KeyCode.LeftControl)) && cooldownTimer <= 0f)
        {
            Attack();
            cooldownTimer = attackCooldown;
        }
    }

    void Attack()
    {
        if (isFanAttack && fanProjectilePrefab != null)
        {
            // FAN SHOT
            float startAngle = -spreadAngle / 2f;
            float angleStep = spreadAngle / (fanProjectiles - 1);

            for (int i = 0; i < fanProjectiles; i++)
            {
                float angle = startAngle + angleStep * i;
                Quaternion rotation = Quaternion.Euler(0, firePoint.eulerAngles.y + angle, 0);
                Instantiate(fanProjectilePrefab, firePoint.position, rotation);
            }
        }
        else
        {
            // NORMAL SHOT
            Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        }
    }
}