using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DGP2;
using DGP;

public class UpdateOffice : MonoBehaviour
{
    private const string TR = "TR";
    public MinigameCommunicator minigameCommunicator;
    public ProgressVisualiserPrototype progress;

    // level 1
    public GameObject chairObject;
    List<GameObject> chairList = new();

    // level 2
    public GameObject couchObject;

    // level 3
    public GameObject plantObject;
    public GameObject endTableObject;

    // level 4
    public GameObject tableObject;
    
    // level 5
    public GameObject snakePlantObject;

    public GameObject taskHolder;

    public string[] headers = new string[0];
    public string[] descriptions = new string[0];


    // Start is called before the first frame update
    void Start()
    {
        minigameCommunicator.woodWon += () => UpdateOffices(TR);

        // GameObject couch = Instantiate(couchObject, new Vector3(-3, 0.56f, 40), transform.rotation);
        // couch.transform.Rotate(0, 45, 0);       
        
        // GameObject plant = Instantiate(snakePlantObject, new Vector3(-18.5f, 0f, 40), transform.rotation);


    }

    private GameObject CreateChair(float x){
        GameObject chair = Instantiate(chairObject, new Vector3(x, 0.5923386f, 37.35f), transform.rotation);
        chair.transform.Rotate(0, 180, 0);
        return chair;
    }

    // Update is called once per frame
    void Update()
    {
                    progress.progress.setProgress(minigameCommunicator.levelTR/5f);
        
    }

    // This void can be called whenever a minigame is passed, 
    // it then automatically updates that part of the office
    void UpdateLevel(string department)
    {
        switch(department)
        {
            case TR:
                if (minigameCommunicator.levelTR < 5){
                    if (minigameCommunicator.levelTR == 2) {
                        int i = Random.Range(0, headers.Length);
                        minigameCommunicator.InstantiateTask(taskHolder, headers[i], descriptions[i]);
                    }
                    minigameCommunicator.levelTR += 1;
                    progress.progress.setProgress(minigameCommunicator.levelTR/5f);
                }
                else {
                    Debug.Log("Level is already at 5, cannot be higher.");
                }
                break;

            case "CA":
                if (minigameCommunicator.levelCA < 5){
                    minigameCommunicator.levelCA += 1;
                }
                else {
                    Debug.Log("Level is already at 5, cannot be higher.");
                }
                break;

            case "MDC":
                if (minigameCommunicator.levelMDC < 5){
                    minigameCommunicator.levelMDC += 1;
                }
                else {
                    Debug.Log("Level is already at 5, cannot be higher.");
                }
                break;

            case "US":
                if (minigameCommunicator.levelUS < 5){
                    minigameCommunicator.levelUS += 1;
                }
                else {
                    Debug.Log("Level is already at 5, cannot be higher.");
                }
                break;

            case "BE":
                if (minigameCommunicator.levelBE < 5){
                    minigameCommunicator.levelBE += 1;
                }
                else {
                    Debug.Log("Level is already at 5, cannot be higher.");
                }
                break;
        }
    }

    public void UpdateOffices(string department){
        // Update the level counter
        UpdateLevel(department);

        switch(department){
            case TR:
                switch(minigameCommunicator.levelTR){
                    case 1:
                        //add stuff
                        for (int i = 0; i < 6; i++){
                        chairList.Add(CreateChair(-18.13f + i*1.7f));
                        }
                        break;

                    case 2:
                        //more stuff
                        GameObject couch = Instantiate(couchObject, new Vector3(-3, 0.56f, 40), transform.rotation);
                        couch.transform.Rotate(0, 90, 0);
                        break;

                    case 3:
                        //even more stuff
                        GameObject plant = Instantiate(plantObject, new Vector3(-6.13f, 1.17f, 40.58f), transform.rotation);       
                        GameObject endTable = Instantiate(endTableObject, new Vector3(-6.13f, 0.6f, 40.58f), transform.rotation);  
                        break;

                    case 4:
                        // giant amounts of stuff
                             
                        GameObject table = Instantiate(tableObject, new Vector3(-4.7f, 0.2799848f, 39.85f), transform.rotation);
                        table.transform.Rotate(0, 90, 0);               
                        break;
                    
                    case 5:
                        // almost too much stuff
                        GameObject snakeplant = Instantiate(snakePlantObject, new Vector3(-18.5f, 0f, 40), transform.rotation);
                        break;
                }
            break;

            //other cases will probably not be done
        }

    }

}
