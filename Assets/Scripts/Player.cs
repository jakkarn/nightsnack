using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    public float passiveAttentionGain = 0.01f;
    public RectTransform attentionMeterPrefab;
    public RectTransform meterRoot;
    private Dictionary<EnemyType, EnemyAttention> attentions;

    private void Start()
    {
        attentions = new Dictionary<EnemyType, EnemyAttention>();

        foreach (EnemyType type in Enum.GetValues(typeof(EnemyType)))
        {
            var meter = Instantiate<RectTransform>(attentionMeterPrefab);
            meter.SetParent(meterRoot);
            var texts = meter.GetComponentsInChildren<Text>();

            texts[0].text = type.ToString();
            var enemyAttention = new EnemyAttention()
            {
                Text = texts[1],
                Attention = 0,
                Modifier = 0
            };
            attentions.Add(type, enemyAttention);
        }
    }

    void Update () {

        /* encounter battle mechanics */
        foreach (EnemyType type in Enum.GetValues(typeof(EnemyType)))
        {
            /* restore player position */
            if (Global.hasBattled)
            {
                this.transform.position = Global.playerPos;
                Global.hasBattled = false;
            }

            var attention = attentions[type];
            attention.Attention += passiveAttentionGain + attention.Modifier;
            attention.Text.text = ((int)attention.Attention).ToString();

            if (attention.Attention >= 100)
            {
                Global.encounter = type;
                Global.hasBattled = true;
                Global.playerPos = new Vector3(this.transform.position.x, this.transform.position.y);
                SceneManager.LoadScene ("SimpleBattle");

                /* reset all attentions on battle */
                foreach (EnemyType type2 in Enum.GetValues(typeof(EnemyType)))
                    attentions[type2].Attention = 0;
                break; //only last encounter type works otherwise
            }
        }

        
	}

    public void SetAttentionModifier(EnemyType type, float value)
    {
        attentions[type].Modifier = value;
    }
}
