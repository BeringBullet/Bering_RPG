using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

namespace RPG.Saving
{
    class SavingSystem : MonoBehaviour
    {
        string path(string savefile) => (GetPathFromSaveFile(savefile));
        public void Save(string savefile)
        {
            using (FileStream stream = File.Open(path(savefile), FileMode.Create))
            {
                Transform playerTransform = GetPlayerTransform();
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, new SerializableVector3(playerTransform.position));
            }
        }



        public void Load(string savefile)
        {
            using (FileStream stream = File.Open(path(savefile), FileMode.Open))
            {
                Transform playerTransform = GetPlayerTransform();
                BinaryFormatter formatter = new BinaryFormatter();
                Vector3 position = ((SerializableVector3)formatter.Deserialize(stream)).ToVector3();
                playerTransform.position = position;
            }
        }

        private Transform GetPlayerTransform()
        {
            return GameObject.FindWithTag("Player").transform;
        }

        private string GetPathFromSaveFile(string savefile) => ($"{ Application.persistentDataPath }/{ savefile }.sav");
    }
}
