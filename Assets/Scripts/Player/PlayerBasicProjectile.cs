using UnityEngine;

public class PlayerBasicProjectile : MonoBehaviour
{
    public float speed = 15f;
    public float lifeTime = 2f;
    public int damage = 1;


    void Start()
    {
        Destroy(gameObject, lifeTime);
    }


    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            GlobalHealth health = other.GetComponent<GlobalHealth>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }


            Destroy(gameObject);
        }


        if (!other.isTrigger)
        {
            Destroy(gameObject);
        }
    }
}