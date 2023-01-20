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

<<<<<<< Updated upstream
=======
        [SerializeField]
        private MinigameCommunicator communicator;

        [SerializeField]
        private int maximumCuts = 3;
        private int cuts = 0;

>>>>>>> Stashed changes
        private bool lost = false;

        void Awake() {
            blade.cuttedObject += checkForLose;
            blade.sawed += () => cuts++;
        }

        void OnValidate() {
            nonHittables = FindObjectsOfType<NonHittable>();
            blade = FindObjectOfType<Blade>();
            // communicator = FindObjectOfType<MinigameCommunicator>(true);
        }

        public void Update() {
            Debug.LogFormat(Lost() ? "Lost" : (Won() ? "Won!" : "Game is in progress"));
<<<<<<< Updated upstream
=======

            if (Won()) {
                communicator.woodWon();

                // Put somewhere else later
                if (cutsLeft() == 0) {
                    Debug.Log("Was a close one");
                } else if (cuts == 1) {
                    Debug.Log("How?");
                } else {
                    Debug.Log("Average");
                }
            }
>>>>>>> Stashed changes
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
