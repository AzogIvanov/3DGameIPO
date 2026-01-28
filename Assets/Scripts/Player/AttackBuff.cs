using System.Collections;
using UnityEngine;

public class AttackBuff : MonoBehaviour
{
    public PlayerAttack playerAttack;
    public float duration = 5f;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ActivateBuff();
        }
    }

    public void ActivateBuff()
    {
        StartCoroutine(DoBuff());
    }


    IEnumerator DoBuff()
    {
        playerAttack.isFanAttack = true;


        yield return new WaitForSeconds(duration);


        playerAttack.isFanAttack = false;
    }
}