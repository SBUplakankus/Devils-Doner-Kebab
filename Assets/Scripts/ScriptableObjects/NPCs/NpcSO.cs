using UnityEngine;

namespace ScriptableObjects.NPCs
{
    [CreateAssetMenu(menuName = "NPC", fileName = "New NPC")]
    public class NpcSO : ScriptableObject
    {
        public enum NpcState{Entering, Ordering, Waiting, Leaving}

        [Header("NPC Info")] 
        public string npcName;
        public NpcState currentState;

        [Header("Conversations")] 
        public ConvoSO convo;
    }
}
