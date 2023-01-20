using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace DGP2 {    
    public class LoadMinigame : MonoBehaviour {

        [SerializeField]
        public MinigameCommunicator communicator;

        [SerializeField]
        private Camera headCamera = Camera.main;

        [SerializeField]
        private string[] minigames;
        private int current = 0;
        private string currentlyLoadedMinigame;

        private void Start() {
            communicator.wantToReturn += deLoadLast;
            communicator.woodWon += () => current++;
        }
        
        public void serialMinigames() {
            if (current < minigames.Length) {
                load(minigames[current]);
            }
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
