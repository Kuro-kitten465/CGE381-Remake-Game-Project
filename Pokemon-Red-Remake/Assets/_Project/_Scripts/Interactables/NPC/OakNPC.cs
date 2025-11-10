using Game.Managers;
using UnityEngine;

namespace Game
{
    public class OakNPC : NPC
    {
        [Header("Oak Override")]
        [SerializeField] private Dialogue _dialogueBeforemeetingOak;
        [SerializeField] private Dialogue _dialogueOnmeetingOak;
        [SerializeField] private Dialogue _dialogueAftermeetingOak;

        public override void OnUpInteracted(PlayerManager manager, Interaction interaction)
        {
            ChangeSprite(interaction.InteractDirection);

            if (!GameManager.Instance.IsProgressComplete(ProgressID.PROGRESS_BEFORE_OAK_MEET))
                DialogueManager.Run(_dialogueBeforemeetingOak, () =>
                {
                    GameManager.Instance.ProgressUpdate(ProgressID.PROGRESS_BEFORE_OAK_MEET);
                });
            else if (!GameManager.Instance.IsProgressComplete(ProgressID.PROGRESS_ON_OAK_MEET))
                DialogueManager.Run(_dialogueOnmeetingOak);

        }

        public override void OnDownInteracted(PlayerManager manager, Interaction interaction)
        {
            ChangeSprite(interaction.InteractDirection);

            if (!GameManager.Instance.IsProgressComplete(ProgressID.PROGRESS_ON_OAK_MEET))
                DialogueManager.Run(_dialogueOnmeetingOak);

            /* if (GameManager.Instance.IsProgressComplete(ProgressID.PROGRESS_BEFORE_OAK_MEET))
                DialogueManager.Run(_dialogueAftermeetingOak); */
        }

        public override void OnLeftInteracted(PlayerManager manager, Interaction interaction)
        {
            ChangeSprite(interaction.InteractDirection);

            if (!GameManager.Instance.IsProgressComplete(ProgressID.PROGRESS_ON_OAK_MEET))
                DialogueManager.Run(_dialogueOnmeetingOak);

            /* if (GameManager.Instance.IsProgressComplete(ProgressID.PROGRESS_BEFORE_OAK_MEET))
                DialogueManager.Run(_dialogueAftermeetingOak); */
        }

        public override void OnRightInteracted(PlayerManager manager, Interaction interaction)
        {
            ChangeSprite(interaction.InteractDirection);

            if (!GameManager.Instance.IsProgressComplete(ProgressID.PROGRESS_ON_OAK_MEET))
                DialogueManager.Run(_dialogueOnmeetingOak);

            /* if (GameManager.Instance.IsProgressComplete(ProgressID.PROGRESS_BEFORE_OAK_MEET))
                DialogueManager.Run(_dialogueAftermeetingOak); */
        }
    }
}
