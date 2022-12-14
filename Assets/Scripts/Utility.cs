/**
 * Name: Laurence van Leuken
 * Date: 10/12/2022
 * Code: Utility
 */

/// Has all the common utilities needed to do programming
/// To reduce coding time and to reduce duplication

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DGP {

    public static class Utility {

        public static T getObjectInAbsence<T>(this MonoBehaviour monoBehaviour, in T value) {
            if (value == null && monoBehaviour.TryGetComponent<T>(out T current)) {
                return current;
            }

            return value;
        }
    }
}

