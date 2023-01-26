using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BLINDED_AM_ME;
using DGP;

/**
 * Name: Laurence van Leuken
 * Date: 21/12/2022
 * Code: Cutting Game Statistics
 */

/// Responsible for determining whether win or lost or undertimened.
/// -> SRP Do MonoBehaviour

namespace DGP2 {
    public class CuttingGameStatistics : MonoBehaviour {
        
        [SerializeField]
        private NonHittable[] nonHittables;

        [SerializeField]
        private Blade blade;
        [SerializeField]
        private MinigameCommunicator communicator;

        [SerializeField]
        private int optimalCuts = 1;

        [SerializeField]
        private int maximumCuts = 3;
        private int cuts = 0;

        [SerializeField]
        private DisplayMessage displayer;

        [SerializeField]
        public GameObject[] gameObjects = new GameObject[0];
        public GameObject[] gameObjectsLose = new GameObject[0];

        private bool lost = false, won = false;

        void Awake() {
            blade.cuttedObject += checkForLose;
            blade.sawed += () => cuts++;
            blade.sawed += Step;
        }

        void OnValidate() {
            nonHittables = FindObjectsOfType<NonHittable>();
            blade = FindObjectOfType<Blade>();
            if (displayer == null) {
                displayer = FindObjectOfType<DisplayMessage>();
            }
            // communicator = FindObjectOfType<MinigameCommunicator>(true);
        }

        public void Update() {
            Step();
        }

        public void Step() {
            Debug.LogFormat(Lost() ? "Lost" : (Won() ? "Won!" : "Game is in progress"));

            if (Won() && !won) {

                // Put somewhere else later
                if (cutsLeft() == 0) {
                    displayer.displayMessageMomentarely("That was a close one!");
                } else if (cuts == optimalCuts) {
                    displayer.displayMessageMomentarely("How?!");
                }  else {
                    displayer.displayMessageMomentarely("Pretty good");
                }
                won = true;

                foreach (GameObject obj in gameObjects) {
                    obj.SetActive(true);
                }
                communicator?.woodWon();
            }

            if (Lost()) {
                displayer.displayMessageMomentarely("You tried. That's what counts!");
                blade.enabled = false;
                this.enabled = false;

                foreach (GameObject obj in gameObjectsLose) {
                    obj.SetActive(true);
                }
            }
        }

        public bool Won()
        {
            return AllDistinct(nonHittables) && !Lost();
        }

        private bool AllDistinct(NonHittable[] nonHittables)
        {
            ISet<GameObject> nonHittablesSet = new HashSet<GameObject>();

            foreach (NonHittable nonHittable in nonHittables)
            {
                nonHittablesSet.Add(nonHittable.getAttachment());
            }

            return nonHittablesSet.Count == nonHittables.Length;
        }

        // private bool AllDistinct(NonHittable[] nonHittables) {
        //     for (int i = 0; i < nonHittables.Length; i++) {
        //         for (int j = 0; j < nonHittables.Length; j++) {
        //             if (i != j && nonHittables[i].getAttachment() == nonHittables[j].getAttachment()) {
        //                 return false;
        //             }
        //         }
        //     }

        //     return true;
        // }

        private void checkForLose(GameObject gameObject) {
            lost |= gameObject.GetComponent<NonHittable>() != null && cuts <= maximumCuts;
        }

        public bool Lost() {
            return lost;
        }

        public int cutsLeft() {
            return maximumCuts - cuts;
        }
    }
}
