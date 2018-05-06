using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupZone : MonoBehaviour {

    public float attentionGain = 1f;
    public EnemyType enemyType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player != null)
        {
            player.SetAttentionModifier(enemyType, attentionGain);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player != null)
        {
            player.SetAttentionModifier(enemyType, 0);
        }
    }
}
