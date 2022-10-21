using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.GetComponent<DestructionBulletOnGround>())
        {
            Destroy(gameObject);
            Destroy(collision.gameObject); //разрушаем пулю
        }
    }
}
