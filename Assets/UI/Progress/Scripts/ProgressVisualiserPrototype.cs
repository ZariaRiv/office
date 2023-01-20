/**
 * Name: Laurence van Leuken
 * Date: 10/12/2022
 * Code: Progress Visualiser Prototype
 */

/// Prototype of visualising progress bar

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace DGP {

    public class ProgressVisualiserPrototype : MonoBehaviour {
        
        public ProgressObserverPrototype progress = new ProgressObserverPrototype();
        
        [SerializeField]
        private RectTransform rectTransform;

        public void OnValidate() {
            rectTransform = this.getMonobehaviourInAbsence(rectTransform);
            updateTransform();
        }

        public void Awake() {
            progress.addListener(updateTransform);
        }

        private void updateTransform() {
            Vector2 anchor = rectTransform.anchorMax;

            anchor.x = progress;

            rectTransform.anchorMax = anchor;
        }
    }
}
