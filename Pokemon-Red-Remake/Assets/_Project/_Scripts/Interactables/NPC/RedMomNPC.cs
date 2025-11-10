using Game.Managers;
using UnityEngine;

namespace Game
{
    public class RedMomNPC : NPC
    {
        [SerializeField] private Dialogue _dialogueBeforemeetingOak;
        [SerializeField] private Dialogue _dialogueAftermeetingOak;

        public override void OnUpInteracted(PlayerManager manager, Interaction interaction)
        {
            ChangeSprite(interaction.InteractDirection);

            if (!GameManager.Instance.IsProgressComplete(ProgressID.PROGRESS_BEFORE_OAK_MEET))
                DialogueManager.Run(_dialogueBeforemeetingOak);
        }

        public override void OnDownInteracted(PlayerManager manager, Interaction interaction)
        {
            ChangeSprite(interaction.InteractDirection);

            if (!GameManager.Instance.IsProgressComplete(ProgressID.PROGRESS_BEFORE_OAK_MEET))
                DialogueManager.Run(_dialogueBeforemeetingOak);
        }

        public override void OnLeftInteracted(PlayerManager manager, Interaction interaction)
        {
            ChangeSprite(interaction.InteractDirection);

            if (!GameManager.Instance.IsProgressComplete(ProgressID.PROGRESS_BEFORE_OAK_MEET))
                DialogueManager.Run(_dialogueBeforemeetingOak);
        }

        public override void OnRightInteracted(PlayerManager manager, Interaction interaction)
        {
            ChangeSprite(interaction.InteractDirection);

            if (!GameManager.Instance.IsProgressComplete(ProgressID.PROGRESS_BEFORE_OAK_MEET))
                DialogueManager.Run(_dialogueBeforemeetingOak);
        }
    }
}
