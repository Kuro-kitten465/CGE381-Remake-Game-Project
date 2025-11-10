using Game.Controllers;
using Game.Managers;
using UnityEngine;

namespace Game
{
    public class BlueNPC : NPC
    {
        [Header("Blue Override")]
        [SerializeField] private Dialogue _dialogueOnmeetingOak;
        [SerializeField] private Dialogue _dialogueAftermeetingOak;

        [SerializeField] private MovementController _mover;
        [SerializeField] private CharacterAnimatorController _anim;

        public override void OnUpInteracted(PlayerManager manager, Interaction interaction)
        {
            ChangeSprite(interaction.InteractDirection);

            if (!GameManager.Instance.IsProgressComplete(ProgressID.PROGRESS_ON_OAK_MEET))
                DialogueManager.Run(_dialogueOnmeetingOak);
        }

        public override void OnDownInteracted(PlayerManager manager, Interaction interaction)
        {
            ChangeSprite(interaction.InteractDirection);

            if (!GameManager.Instance.IsProgressComplete(ProgressID.PROGRESS_ON_OAK_MEET))
                DialogueManager.Run(_dialogueOnmeetingOak);
        }

        public override void OnLeftInteracted(PlayerManager manager, Interaction interaction)
        {
            ChangeSprite(interaction.InteractDirection);

            if (!GameManager.Instance.IsProgressComplete(ProgressID.PROGRESS_ON_OAK_MEET))
                DialogueManager.Run(_dialogueOnmeetingOak);
        }

        public override void OnRightInteracted(PlayerManager manager, Interaction interaction)
        {
            ChangeSprite(interaction.InteractDirection);

            if (!GameManager.Instance.IsProgressComplete(ProgressID.PROGRESS_ON_OAK_MEET))
                DialogueManager.Run(_dialogueOnmeetingOak);
        }
    }
}
