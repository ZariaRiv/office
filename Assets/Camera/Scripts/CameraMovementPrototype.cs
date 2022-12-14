/**
 * Name: Laurence van Leuken
 * Date: 14/12/2022
 * Code: Camera Movement Prototype
 */

/// Monolothic prototype of camera movement

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DGP2 {

    public class CameraMovementPrototype : MonoBehaviour {
        
        [SerializeField]
        private float relativeSpeed = 5f;

        public void LateUpdate() {
            if (Input.GetKey(KeyCode.W)) {
                moveCameraOnXZPlaneWithRespectToAngle(0f, 1f);
            } else if (Input.GetKey(KeyCode.S)) {
                moveCameraOnXZPlaneWithRespectToAngle(0f, -1f);
            }
            
            if (Input.GetKey(KeyCode.A)) {
                moveCameraOnXZPlaneWithRespectToAngle(-1f, 0);
            } else if (Input.GetKey(KeyCode.D)) {
                moveCameraOnXZPlaneWithRespectToAngle(1f, 0);
            }
        }

        private void moveCameraOnXZPlaneWithRespectToAngle(in float xDir, in float zDir) {
            // Something
            moveCameraOnXZPlane(in xDir, in zDir);
        }

        private void moveCameraOnXZPlane(in float xDir, in float zDir) {
            Vector3 position = transform.position;

            position += new Vector3(xDir, 0, zDir) * Time.deltaTime * relativeSpeed;

            transform.position = position;
        }
    }
}
