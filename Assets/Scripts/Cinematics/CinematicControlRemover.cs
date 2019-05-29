using RPG.Contral;
using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {

        GameObject player;
        PlayableDirector playableDirector;
        ActionScheduler actionScheduler;
        PlayerController playerController;

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindWithTag("Player");
            playableDirector = GetComponent<PlayableDirector>();
            actionScheduler = player.GetComponent<ActionScheduler>();
            playerController = player.GetComponent<PlayerController>();

            playableDirector.played += EnableControl;
            playableDirector.stopped += DisableControl;
        }
        void DisableControl(PlayableDirector obj)
        {
            actionScheduler.CancelCurrentAction();
            playerController.enabled = false;
        }

        void EnableControl(PlayableDirector obj)
        {
            playerController.enabled = true;
        }
    }
}