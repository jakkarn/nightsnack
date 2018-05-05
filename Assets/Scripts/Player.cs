using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        foreach (EnemyType type in Enum.GetValues(typeof(EnemyType)))
        {
            var attention = attentions[type];
            attention.Attention += passiveAttentionGain + attention.Modifier;
            attention.Text.text = attention.Attention.ToString();

            if (attention.Attention >= 100)
            {
                Debug.Log(string.Format("ENCOUNTER - {0}", type));
                Application.LoadLevel("SimpleBattle");
                attention.Attention = 0;
            }
        }
	}

    public void SetAttentionModifier(EnemyType type, float value)
    {
        attentions[type].Modifier = value;
    }
}
