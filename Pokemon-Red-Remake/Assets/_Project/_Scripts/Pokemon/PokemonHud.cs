using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Pokemons
{
    public class PokemonHud : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TMP_Text _pokemonNameText;
        [SerializeField] private TMP_Text _pokemonLevelText;
        [SerializeField] private Slider _hpBar;
        [SerializeField] private TMP_Text _hpText;
        [SerializeField] private Slider _expBar;
        [SerializeField] private bool _isPlayer;

        private Pokemon _pokemon;

        public void Setup(Pokemon pokemon)
        {
            _pokemon = pokemon;

            _pokemonNameText.text = $"{pokemon.Base.Name.ToUpper()} {GetGender(_pokemon.Gender)}";
            _pokemonLevelText.text = $"LV.{pokemon.Level}";
            _hpBar.maxValue = _pokemon.Stats.MaxHP;
            _hpBar.value = _pokemon.CurrentHP;
            if (_isPlayer) _hpText.text = $"{_pokemon.CurrentHP}/{_pokemon.Stats.MaxHP}";

            _pokemon.OnHPChanged += UpdateHud;
        }

        public void Cleanup()
        {
            _pokemon.OnHPChanged -= UpdateHud;
        }

        public void UpdateHud()
        {
            _hpBar.value = _pokemon.CurrentHP;

            if (_isPlayer) _hpText.text = $"{_pokemon.CurrentHP}/{_pokemon.Stats.MaxHP}";
        }

        private string GetGender(PokemonGender gender) => gender switch
        {
            PokemonGender.None => string.Empty,
            PokemonGender.Male => "<sprite name=\"male\">",
            PokemonGender.Female => "<sprite name=\"female\">",
            _ => string.Empty
        };
    }
}
