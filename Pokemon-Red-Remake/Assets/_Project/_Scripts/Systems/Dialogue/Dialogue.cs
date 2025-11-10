using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue")]
    public class Dialogue : ScriptableObject
    {
        public List<string> Sequence;
    }
}
