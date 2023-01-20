using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BLINDED_AM_ME.Extensions;
using System.Linq;
using System.Threading;
using System;
using System.Threading.Tasks;

namespace BLINDED_AM_ME
{
    // Cursed script. No SRP, no integrity, code duplication
    public class Blade : MonoBehaviour
    {
        private const int ACTIVE_PLAY_HEIGHT = 1;
        public Material CapMaterial;

        public Action<GameObject> cuttedObject;
        public Action sawed;

        [SerializeField]
        private float cutWidth = 0.3f;

        // private CancellationTokenSource _previousTaskCancel;

        private int state = 0;
        private Vector3 start, end;
        public void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (state == 0) {
                    state = 1;
                    start = getProjectedMousePosition();
                } else if (state == 1) {
                    state = 0;
                    end = getProjectedMousePosition();
                    Vector3 intermediate = new Vector3(end.x, 5, end.z);
                    setPositionAndRotation(start, end, intermediate);
                    Vector3 angles = transform.eulerAngles;
                    angles.x = 0;
                    angles.z = 0;
                    transform.eulerAngles = angles; 
                    saw(cutWidth);
                }
            } 

            Vector3 getProjectedMousePosition()
            {
                // Source: |https://www.google.com/search?q=screen+pos+to+world+pos&oq=screenPos+to+&aqs=edge.1.69i57j0i22i30.4608j0j1&sourceid=chrome&ie=UTF-8|
                Vector3 screenPos = Input.mousePosition;
                Vector3 pos = Camera.main.ScreenToWorldPoint(screenPos, Camera.MonoOrStereoscopicEye.Mono);
                return new Vector3(pos.x, ACTIVE_PLAY_HEIGHT, pos.z);
            }
        }

        void setPositionAndRotation(Vector3 start, Vector3 end, Vector3 up) {
            transform.position = start;
            transform.LookAt(end, up);
        }

        void Cut()
        {
            Vector3 offsetToRight = transform.right * cutWidth * 0.5f;
            Vector3 hitPositionLeft = transform.position - offsetToRight;
            Vector3 hitPositionRight = transform.position + offsetToRight;

            foreach (RaycastHit hit in Physics.RaycastAll(hitPositionLeft, transform.forward))
            {
                var timeLimit = new CancellationTokenSource(TimeSpan.FromSeconds(5)).Token;

                GameObject hitGameObject = hit.collider.gameObject;

                // this will hold up everything
                foreach (GameObject gameObject in Cut(hitGameObject, hitPositionLeft, timeLimit)) {
                    Vector3 center = gameObject.transform.position;
                    
                    if (Vector3.Dot(hitPositionLeft - center, transform.right) > 0f && Vector3.Dot(hitPositionRight - center, transform.right) < 0f) {
                        Destroy(gameObject);
                    }
                }

                // this won't hold up everything
                // StartCoroutine(CutCoroutine(hit.collider.gameObject, timeLimit));

                cuttedObject?.Invoke(hit.collider.gameObject);
            }
        }

        void saw(float width) {
            Vector3 offsetToRight = transform.right * width * 0.5f;
            Vector3 hitPositionLeft = transform.position - offsetToRight;
            Vector3 hitPositionRight = transform.position + offsetToRight;
            float maxDistance = Vector3.Distance(start, end);
            // float maxDistance = 500;

            foreach (RaycastHit hit in Physics.RaycastAll(hitPositionLeft, transform.forward, maxDistance)) {
                var timeLimit = new CancellationTokenSource(TimeSpan.FromSeconds(5)).Token;

                GameObject hitGameObject = hit.collider.gameObject;

                // this will hold up everything
                foreach (GameObject pieceOfHit in Cut(hitGameObject, hitPositionLeft, timeLimit)) {
                    Vector3 center = pieceOfHit.transform.position;

                    if (inSideSaw(pieceOfHit)) {
                        Destroy(pieceOfHit);
                    }
                }

                // this won't hold up everything
                // StartCoroutine(CutCoroutine(hit.collider.gameObject, timeLimit));

                cuttedObject?.Invoke(hit.collider.gameObject);
            }

            foreach (RaycastHit hit in Physics.RaycastAll(hitPositionRight, transform.forward, maxDistance)) {
                var timeLimit = new CancellationTokenSource(TimeSpan.FromSeconds(5)).Token;

                GameObject hitGameObject = hit.collider.gameObject;

                // this will hold up everything
                foreach (GameObject pieceOfHit in Cut(hitGameObject, hitPositionRight, timeLimit)) {
                    if (inSideSaw(pieceOfHit)) {
                        Destroy(pieceOfHit);
                    }
                }

                // this won't hold up everything
                // StartCoroutine(CutCoroutine(hit.collider.gameObject, timeLimit));

                cuttedObject?.Invoke(hit.collider.gameObject);
            }

            sawed?.Invoke();

            bool inSideSaw(GameObject pieceOfHit) {
                Vector3 center = Vector3.zero;
                Vector3[] vertices = pieceOfHit.GetComponent<MeshFilter>().mesh.vertices;
                float length = vertices.Length;

                foreach (Vector3 vector in vertices) {
                    center += vector;
                }

                center /= length;

                center = straightMultiply(center, pieceOfHit.transform.lossyScale);

                bool rightFromSaw = Vector3.Dot(hitPositionLeft - center, transform.right) < 0f;
                bool leftFromSaw = Vector3.Dot(hitPositionRight - center, transform.right) > 0f;
                return rightFromSaw && leftFromSaw;

                Vector3 straightMultiply(Vector3 p1, Vector3 p2) {
                    return new Vector3(
                        p1.x * p2.x,
                        p1.y * p2.y,
                        p1.z * p2.z
                    );
                }
            }
        }

        // this will hold up the UI thread
        private GameObject[] Cut(GameObject target, Vector3 start, CancellationToken cancellationToken = default)
        {
            try
            {
                // _previousTaskCancel?.Cancel();
                // _previousTaskCancel = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                // cancellationToken = _previousTaskCancel.Token;
                cancellationToken.ThrowIfCancellationRequested();

                // get the victims mesh
                var leftSide = target;
                var leftMeshFilter = leftSide.GetComponent<MeshFilter>();
                var leftMeshRenderer = leftSide.GetComponent<MeshRenderer>();

                var materials = new List<Material>();
                leftMeshRenderer.GetSharedMaterials(materials);

                // the insides
                var capSubmeshIndex = 0;
                if (materials.Contains(CapMaterial))
                    capSubmeshIndex = materials.IndexOf(CapMaterial);
                else
                {
                    capSubmeshIndex = materials.Count;
                    materials.Add(CapMaterial);
                }

                // set the blade relative to victim
                var blade = new Plane(
                    leftSide.transform.InverseTransformDirection(transform.right),
                    leftSide.transform.InverseTransformPoint(start));

                var mesh = leftMeshFilter.sharedMesh;
                //var mesh = leftMeshFilter.mesh;

                // Cut
                var pieces = mesh.Cut(blade, capSubmeshIndex, cancellationToken);

                leftSide.name = "LeftSide";
                leftMeshFilter.mesh = pieces.Item1;
                leftMeshRenderer.sharedMaterials = materials.ToArray();
                //leftMeshRenderer.materials = materials.ToArray();

                var rightSide = new GameObject("RightSide");
                // Improvement: Do only things that will save time, but are functionally equivalent as to try more and experiment and many more.
                // * Maybe improve when having extra minigame
                UnityEngine.SceneManagement.SceneManager.MoveGameObjectToScene(rightSide, leftSide.scene);
                var rightMeshFilter = rightSide.AddComponent<MeshFilter>();
                var rightMeshRenderer = rightSide.AddComponent<MeshRenderer>();

                rightSide.transform.SetPositionAndRotation(leftSide.transform.position, leftSide.transform.rotation);
                rightSide.transform.localScale = leftSide.transform.localScale;

                rightMeshFilter.mesh = pieces.Item2;
                rightMeshRenderer.sharedMaterials = materials.ToArray();
                //rightMeshRenderer.materials = materials.ToArray();
                rightMeshRenderer.shadowCastingMode = leftMeshRenderer.shadowCastingMode;

                // Physics 
                bool trigger = leftSide.GetComponent<Collider>().isTrigger;
                
                foreach (Collider collider in leftSide.GetComponents<Collider>()) {
                    Destroy(collider);
                }

                // Replace
                
                var leftCollider = leftSide.AddComponent<MeshCollider>();
                leftCollider.convex = true;
                leftCollider.sharedMesh = pieces.Item1;
                leftCollider.isTrigger = trigger;

                var rightCollider = rightSide.AddComponent<MeshCollider>();
                rightCollider.convex = true;
                rightCollider.sharedMesh = pieces.Item2;
                rightCollider.isTrigger = trigger;

                rightMeshRenderer.enabled = leftMeshRenderer.enabled;

                // rigidbody
                if (leftSide.TryGetComponent<Rigidbody>(out Rigidbody rigidBody))
                {
                    Rigidbody rightRigidBody = rightSide.AddComponent<Rigidbody>();
                    rightRigidBody.constraints = rigidBody.constraints;
                    rightRigidBody.isKinematic = rigidBody.isKinematic;
                }

                return new GameObject[] {
                    leftSide,
                    rightSide
                };
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
            }

            return null;
        }

        // this will not hold up the UI thread
        private IEnumerator CutCoroutine(GameObject target, CancellationToken cancellationToken = default)
        {
            // _previousTaskCancel?.Cancel();
            // _previousTaskCancel = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            // cancellationToken = _previousTaskCancel.Token;

            // get the victims mesh
            var leftSide = target;
            var leftMeshFilter = leftSide.GetComponent<MeshFilter>();
            var leftMeshRenderer = leftSide.GetComponent<MeshRenderer>();

            var materials = new List<Material>();
            leftMeshRenderer.GetSharedMaterials(materials);

            // the insides
            var capSubmeshIndex = 0;
            if (materials.Contains(CapMaterial))
                capSubmeshIndex = materials.IndexOf(CapMaterial);
            else
            {
                capSubmeshIndex = materials.Count;
                materials.Add(CapMaterial);
            }

            // set the blade relative to victim
            var blade = new Plane(
                leftSide.transform.InverseTransformDirection(transform.right),
                leftSide.transform.InverseTransformPoint(transform.position));

            var mesh = leftMeshFilter.sharedMesh;
            //var mesh = leftMeshFilter.mesh;

            // Cut
            yield return mesh.CutCoroutine(blade,
                (pieces) =>
                {
                    leftSide.name = "LeftSide";
                    leftMeshFilter.mesh = pieces.Item1;
                    leftMeshRenderer.sharedMaterials = materials.ToArray();
                    //leftMeshRenderer.materials = materials.ToArray();

                    var rightSide = new GameObject("RightSide");
                    var rightMeshFilter = rightSide.AddComponent<MeshFilter>();
                    var rightMeshRenderer = rightSide.AddComponent<MeshRenderer>();

                    rightSide.transform.SetPositionAndRotation(leftSide.transform.position, leftSide.transform.rotation);
                    rightSide.transform.localScale = leftSide.transform.localScale;

                    rightMeshFilter.mesh = pieces.Item2;
                    rightMeshRenderer.sharedMaterials = materials.ToArray();
                    //rightMeshRenderer.materials = materials.ToArray();

                    // Physics 
                    bool trigger = leftSide.GetComponent<Collider>().isTrigger;
                    Destroy(leftSide.GetComponent<Collider>());

                    // Replace
                    var leftCollider = leftSide.AddComponent<MeshCollider>();
                    leftCollider.convex = true;
                    leftCollider.sharedMesh = pieces.Item1;
                    leftCollider.isTrigger = trigger;

                    var rightCollider = rightSide.AddComponent<MeshCollider>();
                    rightCollider.convex = true;
                    rightCollider.sharedMesh = pieces.Item2;
                    rightCollider.isTrigger = trigger;

                    // rigidbody
                    if (leftSide.TryGetComponent<Rigidbody>(out Rigidbody rigidBody))
                    {
                        Rigidbody rightRigidBody = rightSide.AddComponent<Rigidbody>();
                        rightRigidBody.constraints = rigidBody.constraints;
                    }

                    rightSide.layer = leftSide.layer;

                    Vector3 normal = blade.normal;
                    leftSide.transform.position -= normal * 0.1f;
                    rightSide.transform.position += normal * 0.1f;

                }, capSubmeshIndex, cancellationToken);
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.green;

            drawSawGizmos(
                transform.position,
                transform.forward,
                transform.right,
                cutWidth
            );
        }

        void drawBladeGizmos(Vector3 position, Vector3 forward, Vector3 right, float length = 5f, float height = 0.5f) {
            Vector3 up = Vector3.Cross(forward, right);

            Vector3 top = position + up * height;
            Vector3 bottom = position - up * height;

            Gizmos.DrawRay(top, transform.forward * length);
            Gizmos.DrawRay(position, transform.forward * length);
            Gizmos.DrawRay(bottom, transform.forward * length);
            Gizmos.DrawLine(top, bottom);
        }

        void drawSawGizmos(Vector3 position, Vector3 forward, Vector3 right, float width = 0.3f, float length = 5f, float height = 0.5f) {
            Vector3 up = Vector3.Cross(forward, right);

            Vector3 offset = right * width * 0.5f;
            drawBladeGizmos(position - offset, forward, right, length, height);
            drawBladeGizmos(position + offset, forward, right, length, height);
        }
    }
}
