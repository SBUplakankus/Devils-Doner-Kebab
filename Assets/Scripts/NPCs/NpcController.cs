using System;
using ScriptableObjects;
using UnityEngine;

namespace NPCs
{
    public class NpcController : MonoBehaviour
    {
        public enum NpcState{Entering, Ordering, Waiting, Leaving}
        public bool canTalk;
        
        [Header("NPC Info")]
        public string npcName;
        public NpcState npcState;
        
        [Header("Conversations")]
        public ConvoSO orderConvo, goodbyeConvo;

    }
}
