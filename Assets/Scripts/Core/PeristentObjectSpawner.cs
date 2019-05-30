using System;
using UnityEngine;

namespace RPG.Core
{
    public class PeristentObjectSpawner : MonoBehaviour
    {
        [SerializeField] GameObject presistentObjectPrefab;
        static bool hasSpawne = false;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            if (hasSpawne) return;
            SpawnPersistentObject();
            hasSpawne = true;
        }

        private void SpawnPersistentObject()
        {
            GameObject peristentObject = Instantiate(presistentObjectPrefab);
            DontDestroyOnLoad(peristentObject);
        }
    }
}