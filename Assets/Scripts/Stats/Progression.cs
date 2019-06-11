using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Pregression", menuName = "Stats/New Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClass = null;

        [Serializable]
        class ProgressionCharacterClass
        {
            [SerializeField] CharacterClass characterClass;
            [SerializeField] float[] health = null;
            [SerializeField] float[] damange = null;
        }
    }
}