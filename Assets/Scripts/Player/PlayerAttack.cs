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

    [HideInInspector] public bool isAiming;
    public Quaternion targetRotationAim = Quaternion.identity;

    private float cooldownTimer;
    private PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        isAiming = Input.GetMouseButton(0) || Input.GetKey(KeyCode.LeftControl);

        // ONLY ROTETE THE MOUSE WHEN:
        // SHOOTING
        // NOT MOVING
        if (isAiming || !playerMovement.IsMoving)
        {
            RotateToMouse();
        }

        if (cooldownTimer > 0f)
            cooldownTimer -= Time.deltaTime;

        if (isAiming && cooldownTimer <= 0f)
        {
            Attack();
            cooldownTimer = attackCooldown;
        }
    }

    void RotateToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, transform.position);

        if (groundPlane.Raycast(ray, out float distance))
        {
            Vector3 point = ray.GetPoint(distance);
            Vector3 dir = point - transform.position;
            dir.y = 0f;

            if (dir.sqrMagnitude > 0.01f)
            {
                targetRotationAim = Quaternion.LookRotation(dir);
            }
        }
        else
        {
            targetRotationAim = Quaternion.identity;
        }
    }

    void Attack()
    {
        if (isFanAttack && fanProjectilePrefab != null)
        {
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
            Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        }
    }
}