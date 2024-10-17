using UnityEngine;

namespace ScriptableObjects
{   
    [CreateAssetMenu(menuName = "Conversation", fileName = "New Convo", order = 0)]
    public class ConvoSO : ScriptableObject
    {
        [TextArea(3,10)]
        public string[] dialogueTree;
        public int[] dialogueSpeaker;
        public AudioClip[] dialogueAudio;
    }
}
