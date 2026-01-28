using UnityEngine;
using System.Collections;

public class EnemyHealth : GlobalHealth
{
    private bool stunned = false;

    [Header("Stun Visual")]
    public GameObject stunIconPrefab;
    private GameObject currentStunIcon;

    public void Stun(float duration)
    {
        if (!stunned)
            StartCoroutine(StunCoroutine(duration));
    }

    IEnumerator StunCoroutine(float duration)
    {
        stunned = true;

        // CREATE FLOATING ICON
        if (stunIconPrefab != null)
        {
            currentStunIcon = Instantiate(stunIconPrefab, transform.position + Vector3.up * 2f, Quaternion.identity);
            currentStunIcon.transform.SetParent(transform);

            // START ICON ANIMATION
            StartCoroutine(FloatAndRotateIcon(currentStunIcon));
        }

        yield return new WaitForSeconds(duration);

        stunned = false;

        // DESTROY WHEN STUN ENDS
        if (currentStunIcon != null)
            Destroy(currentStunIcon);
    }

    IEnumerator FloatAndRotateIcon(GameObject icon)
    {
        float amplitude = 0.3f;
        float speed = 2f;
        float rotationSpeed = 180f;
        Vector3 startPos = icon.transform.localPosition;

        while (icon != null && stunned)
        {
            // floating movement
            icon.transform.localPosition = startPos + Vector3.up * Mathf.Sin(Time.time * speed) * amplitude;

            // rotation
            icon.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);

            yield return null;
        }
    }

    public bool IsStunned()
    {
        return stunned;
    }

    protected override void Die()
    {
        // DESTROY THE ICON IF THE ENEMY DIES
        if (currentStunIcon != null)
            Destroy(currentStunIcon);

        base.Die();
    }
}