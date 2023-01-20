using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace DGP2 {

    [CreateAssetMenu(menuName = "Communication", fileName = "Minigame")]
    public class MinigameCommunicator : ScriptableObject {
        public Action woodWon; 
        public Action wantToReturn;

        public void Return() {
            wantToReturn?.Invoke();
        }
    }
}
