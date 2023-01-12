using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BLINDED_AM_ME;

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

        private bool lost = false;

        void Awake() {
            blade.cutted += checkForLose;
        }

        void OnValidate() {
            if (nonHittables == null) {
                nonHittables = FindObjectsOfType<NonHittable>();
            }
        }

        public void Update() {
            Debug.LogFormat(Lost() ? "Lost" : (Won() ? "Won!" : "Game is in progress"));

            if (Won()) {
                communicator.woodWon();
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
            lost |= gameObject.GetComponent<NonHittable>() != null;
        }

        public bool Lost() {
            return lost;
        }
    }
}
