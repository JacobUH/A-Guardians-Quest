using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

public class Quest : ScriptableObject
{
    public struct Info {
        public string Name;
        public Sprite Icon;
        public string Description;
    }

    [Header("Info")] public Info Information;

    [System.Serializable]

    public struct Stat {
        public int Currency;
        public int EXP;
    }

    [Header("Rewards")] public Stat Rewards = new Stat{Currency = 10, EXP = 50};

    public bool Completed { get; protected set; }
    public QuestCompletedEvent QuestCompleted;

    public abstract class QuestGoal : ScriptableObject {
        protected string Description;
        public int CurrentAmount { get; protected set; }
        public int RequiredAmount { get; protected set; }
        public bool Completed { get; protected set; }
        [HideInInspector] public UnityEvent GoalCompleted;
        public virtual string GetDescription() {
            return Description;
        }
        public virtual void Initialize() {
            Completed = false;
            GoalCompleted = new UnityEvent();
        }

        protected void Evaluate() {
            if (CurrentAmount >= RequiredAmount) {
                Complete();
            }
        }

        private void Complete() {
            Completed = true;
            GoalCompleted.Invoke();
            GoalCompleted.RemoveAllListeners();
        }
    }

    public List<QuestGoal> Goals;

    public void Initialize() {
        Completed = false;
        QuestCompleted = new QuestCompletedEvent();
        foreach (var goal in Goals) {
            goal.Initialize();
            goal.GoalCompleted.AddListener(delegate { CheckGoals(); });
        }
    }

    private void CheckGoals() {
        Completed = Goals.All(g => g.Completed);
        if (Completed) {
            QuestCompleted.Invoke(this);
            QuestCompleted.RemoveAllListeners();
        }
    }
}

public class QuestCompletedEvent : UnityEvent<Quest> {

}

# if UNITY_EDITOR
[CustomEditor(typeof(Quest))]
public class QuestEditor : Editor {
    SerializedProperty m_QuestInfoProperty;
    SerializedProperty m_QuestStatProperty;
    List<string> m_QuestGoalType;
    SerializedProperty m_QuestGoalListProperty;

    [MenuItem("Assets/Quest", priority = 0)]
    public static void CreateQuest() {
        var newQuest = CreateInstance<Quest>();
        ProjectWindowUtil.CreateAsset(newQuest, "quest.asset");
    }

    void onEnable() {
        m_QuestInfoProperty = serializedObject.FindProperty(nameof(Quest.Information));
        m_QuestStatProperty = serializedObject.FindProperty(nameof(Quest.Rewards));
        m_QuestGoalListProperty = serializedObject.FindProperty(nameof(Quest.Goals));

        var lookup = typeof(Quest.QuestGoal);
        m_QuestGoalType = System.AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(lookup))
            .Select(type => type.Name)
            .ToList();
    }

    public override void OnInspectorGUI() {
        var child = m_QuestInfoProperty.Copy();
        var depth = child.depth;
        child.NextVisible(true);

        EditorGUILayout.LabelField("Quest info", EditorStyles.boldLabel);
        while (child.depth > depth) {
            EditorGUILayout.PropertyField(child, true);
            child.NextVisible(false);
        }

        child = m_QuestStatProperty.Copy();
        depth = child.depth;
        child.NextVisible(true);

        EditorGUILayout.LabelField("Quest rewards", EditorStyles.boldLabel);
        while (child.depth > depth) {
            EditorGUILayout.PropertyField(child, true);
            child.NextVisible(false);
        }

        int choice = EditorGUILayout.Popup("Add new Quest Goal", -1, m_QuestGoalType.ToArray());

        if (choice != -1) {
            var newInstance = ScriptableObject.CreateInstance(m_QuestGoalType[choice]);

            AssetDatabase.AddObjectToAsset(newInstance, target);
            m_QuestGoalListProperty.InsertArrayElementAtIndex(m_QuestGoalListProperty.arraySize);
            m_QuestGoalListProperty.GetArrayElementAtIndex(m_QuestGoalListProperty.arraySize - 1)
                .objectReferenceValue = newInstance;
        }

        Editor ed = null;
        int toDelete = -1;
        for (int i = 0; i < m_QuestGoalListProperty.arraySize; ++i) {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical();
            var item = m_QuestGoalListProperty.GetArrayElementAtIndex(i);
            SerializedObject obj = new SerializedObject(item.objectReferenceValue);

            Editor.CreateCachedEditor(item.objectReferenceValue, null, ref ed);

            ed.OnInspectorGUI();
            EditorGUILayout.EndVertical();

            if (GUILayout.Button("-", GUILayout.Width(32))) {
                toDelete = i;
            }
            EditorGUILayout.EndHorizontal();
        }

        if (toDelete != -1) {
            var item = m_QuestGoalListProperty.GetArrayElementAtIndex(toDelete);
            DestroyImmediate(item.objectReferenceValue, true);
            m_QuestGoalListProperty.DeleteArrayElementAtIndex(toDelete);
            m_QuestGoalListProperty.DeleteArrayElementAtIndex(toDelete);
        }
    }
}
# endif
