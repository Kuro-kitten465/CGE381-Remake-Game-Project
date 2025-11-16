using UnityEngine;

namespace Game.Controllers
{
    public class CharacterAnimatorController : MonoBehaviour
    {
        [SerializeField] private Animator _anim;
        private Direction _direction = Direction.Down;

        public void SetDirection(Direction dir)
        {
            _direction = dir;
        }

        public void PlayIdle()
        {
            _anim.Play($"Anim_Idle_{_direction}");
        }

        public void PlayWalk()
        {
            _anim.Play($"Anim_Walk_{_direction}");
        }
    }
}
