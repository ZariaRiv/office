using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Name: Laurence van Leuken
 * Date: 21/12/2022
 * Code: Non Hittable
 */

/// Detect which object attached to

namespace DGP2
{
    public class NonHittable : MonoBehaviour
    {
        private GameObject attached;

        public GameObject getAttachment() {
            return attached;
        }

        private void OnTriggerEnter(Collider other) {
            attached = other.gameObject;
        }
    }
}
