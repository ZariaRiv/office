using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DGP2 {    
    public class LoadMinigame : MonoBehaviour {

        [SerializeField]
        private MinigameCommunicator minigameCommunicator;

        [SerializeField]
        private Camera headCamera = Camera.main;

        private string currentlyLoadedMinigame;

        private void Start() {
            minigameCommunicator.woodWon += deLoadLast;
        }

        public void load(string scene) {
            currentlyLoadedMinigame = scene;
            SceneManager.LoadScene(scene, LoadSceneMode.Additive);
            headCamera.enabled = false;
        }

        public void deLoadLast() {
            SceneManager.UnloadSceneAsync(currentlyLoadedMinigame);
            headCamera.enabled = true;
        }
    }
}
