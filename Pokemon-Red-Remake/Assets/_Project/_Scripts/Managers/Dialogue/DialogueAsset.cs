using System;
using System.Collections.Generic;
using Game.Systems;
using UnityEngine;

namespace Game.Managers.Dialogue
{
    [CreateAssetMenu(fileName = "dialogue_NewDialogue", menuName = "Game/Data/Dialogue")]
    public class DialogueAsset : ScriptableObject
    {
        [SerializeField] private List<string> _dialogueNodes;

        public IReadOnlyList<string> Dialogues => _dialogueNodes;
    }
}
