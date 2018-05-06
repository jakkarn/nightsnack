using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupZone : MonoBehaviour {

    public float attentionGain = 1f;
    public EnemyType enemyType;
    private Vector2 spotlightDir;
    private float time;
    private float movetime;
    private float x, y;

    private void Start()
    {
        x = Random.Range(-0.1f, 0.1f);
        y = Random.Range(-0.1f, 0.1f);
        movetime = Random.Range(0, 2);
        spotlightDir = new Vector2(x, y);
        time = Time.time;
    }

    private void Update()
    {
        if(Time.time - time > movetime)
        {
            spotlightDir = -spotlightDir;
            time = Time.time;
        }

        this.GetComponent<Transform>().position += (Vector3)spotlightDir; 
    }

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
