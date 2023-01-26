using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

namespace DGP2 {

    [CreateAssetMenu(menuName = "Communication", fileName = "Minigame")]
    public class MinigameCommunicator : ScriptableObject {
        public Action woodWon; 
        public Action wantToReturn;

        public GameObject taskTemplate;

        public void Return() {
            wantToReturn?.Invoke();
        }

        public void InstantiateTask(GameObject parent, string header, string description) {
            GameObject newTask = GameObject.Instantiate(taskTemplate);
            newTask.transform.parent = parent.transform;
            SetHeader(newTask, header);
            SetDescription(newTask, description);
        }

        private void SetHeader(GameObject task, string header) {
            task.GetComponentsInChildren<TMP_Text>()[0].text = header;
        }

        private void SetDescription(GameObject task, string description) {
            task.GetComponentsInChildren<TMP_Text>()[1].text = description;
        }

            public int levelTR = 0;    // technology & realisation
    public int levelCA = 0;    // creativity & aesthetics
    public int levelMDC = 0;   // math, data & computing
    public int levelUS = 0;    // user & society
    public int levelBE = 0;    // business & entrepeneurship
    }
}
