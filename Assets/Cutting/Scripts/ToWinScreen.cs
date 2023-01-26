using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToWinScreen : MonoBehaviour
{
    [SerializeField]
    private string scene = "ProjectFinished";

    [SerializeField]
    private float delay;

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(loadSceneDelayed(scene, delay));
    }

    private IEnumerator loadSceneDelayed(string scene, float delay) {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(scene);
    }
}