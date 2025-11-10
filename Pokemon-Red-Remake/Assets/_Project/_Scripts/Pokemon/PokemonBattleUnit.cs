using UnityEngine;
using UnityEngine.UI;

namespace Game.Pokemons
{
    public class PokemonBattleUnit : MonoBehaviour
    {
        [SerializeField] private Image _pokemonPortrait;
        [SerializeField] private Animator _animator;

        public void Setup(Pokemon pokemon)
        {
            _pokemonPortrait.sprite = pokemon.Base.Icon;

            _animator.runtimeAnimatorController = pokemon.Base.AnimOverride;
        }

        public void PlayAttack() => _animator.Play("Attack");
    }
}
