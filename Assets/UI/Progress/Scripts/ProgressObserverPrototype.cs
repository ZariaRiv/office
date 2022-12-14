/**
 * Name: Laurence van Leuken
 * Date: 10/12/2022
 * Code: Progress Observer Prototype
 */

/// Prototype of making the progress bar reactive

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace DGP {

    [System.Serializable]
    public class ProgressObserverPrototype : ProgressPrototype {

        // ? Does this need serializing ?
        private Action changed;

        protected override void progressChanged() {
            changed?.Invoke();
        }

        public void addListener(Action listener) {
            changed += listener;
        }

        public void removeListener(Action listener) {
            changed -= listener;
        }
    }
}