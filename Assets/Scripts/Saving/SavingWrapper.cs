using UnityEngine;

namespace RPG.Saving
{
    class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "save";
        SavingSystem savingSystem;

        private void Start()
        {
            savingSystem = GetComponent<SavingSystem>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                savingSystem.Save(defaultSaveFile);
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                savingSystem.Load(defaultSaveFile);
            }
        }
    }
}
