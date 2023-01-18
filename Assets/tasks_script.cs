using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class tasks_script : MonoBehaviour
{
public void AcceptTask(GameObject thing){
        SceneManager.LoadScene(2);
        Destroy(thing, 1);

    }

public void WinGameButton(){
        SceneManager.LoadScene(0);
}

public void DeclineProject(GameObject thing){
        Destroy(thing, 1);
}
}

