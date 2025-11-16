using Game.Interaction;
using UnityEngine;

namespace Game.Managers.Dialogue
{
    public class DialogueRunner : MonoBehaviour, IInteractable
    {
        [Header("Dialogue Lines")]
        [SerializeField] private DialogueAsset OnUpDialogueAsset;
        [SerializeField] private DialogueAsset OnDownDialogueAsset;
        [SerializeField] private DialogueAsset OnLeftDialogueAsset;
        [SerializeField] private DialogueAsset OnRightDialogueAsset;

        private DialogueManager _dialogueManager;

        private void Start()
        {
            _dialogueManager = DialogueManager.Instance;
        }

        public void OnUpInteract(PlayerManager manager)
        {
            if (OnUpDialogueAsset == null || OnUpDialogueAsset.Dialogues.Count <= 0) return;
            
            _dialogueManager.StartDialogue(manager, OnUpDialogueAsset);
        }

        public void OnDownInteract(PlayerManager manager)
        {
            if (OnDownDialogueAsset == null || OnDownDialogueAsset.Dialogues.Count <= 0) return;

            _dialogueManager.StartDialogue(manager, OnDownDialogueAsset);
        }

        public void OnLeftInteract(PlayerManager manager)
        {
            if (OnLeftDialogueAsset == null || OnLeftDialogueAsset.Dialogues.Count <= 0) return;
            
            _dialogueManager.StartDialogue(manager, OnLeftDialogueAsset);
        }

        public void OnRightInteract(PlayerManager manager)
        {
            if (OnRightDialogueAsset == null || OnRightDialogueAsset.Dialogues.Count <= 0) return;
            
            _dialogueManager.StartDialogue(manager, OnRightDialogueAsset);
        }
    }
}
