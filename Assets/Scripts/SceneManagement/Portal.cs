using RPG.Saving;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        enum DestinationIdentifier
        {
            A, B, C, D
        }


        [SerializeField] int sceneToLoad = -1;
        [SerializeField] Transform spawnpoint;
        [SerializeField] DestinationIdentifier destination;
        [SerializeField] float fadeOutTime = .5f;
        [SerializeField] float fadeInTime = 1f;
        [SerializeField] float fadeWaitTime = 0.5f;
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                StartCoroutine(Transition());
            }
        }
        private IEnumerator Transition()
        {
            if (sceneToLoad < 0)
            {
                Debug.LogError("Scene to load not set.");
                yield break;
                    
            }
            DontDestroyOnLoad(gameObject);
            Fader fader = FindObjectOfType<Fader>();
            SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();
            yield return fader.FadeOut(fadeOutTime);
            savingWrapper.Save();
            yield return SceneManager.LoadSceneAsync(sceneToLoad);
            savingWrapper.Load();
            Portal otherPortal = getOtherPortal();

            UpdatePlayer(otherPortal);
            savingWrapper.Save();

            yield return new WaitForSeconds(fadeWaitTime);
            yield return fader.FadeIn(fadeInTime);
            Destroy(gameObject);
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = otherPortal.spawnpoint.position;
            player.transform.rotation = otherPortal.spawnpoint.rotation;
            player.GetComponent<NavMeshAgent>().enabled = true;
        }

        private Portal getOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;
                if (portal.destination != this.destination) continue;

                return portal;
            }
            return null;
        }
    }
}