using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SlayGoal: Quest.QuestGoal {
    public string EnemyName;
    public int EnemyAmount;

    public override string GetDescription() {
        return "Slay " + EnemyAmount + " " + EnemyName;
    }

    public override void Initialize() {
        base.Initialize();
        EventManager.Instance.AddListener<SlayingGameEvent>(EnemyKilled);
    }

    private void EnemyKilled(SlayingGameEvent eventInfo) {
        if (eventInfo.EnemyName == EnemyName) {
            CurrentAmount++;
            Evaluate();
        }
    }
}