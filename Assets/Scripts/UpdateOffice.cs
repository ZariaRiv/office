using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DGP2;

public class UpdateOffice : MonoBehaviour
{
    private const string TR = "TR";

    // level 1
    public GameObject couchObject;
    
    // level 2
    public GameObject snakePlantObject;

    // level 3
    public GameObject bookObject;

    public MinigameCommunicator minigameCommunicator;

    // level 4

    // level 5


    // Start is called before the first frame update
    void Start()
    {
        // level 1
        GameObject couch = Instantiate(couchObject, new Vector3(-3, 0.56f, 40), transform.rotation);
        couch.transform.Rotate(0, 45, 0);

        // level 2
        GameObject plant = Instantiate(snakePlantObject, new Vector3(-18.5f, 0f, 40), transform.rotation);

        // level 3

        // level 4
        
        // level 5

        minigameCommunicator.woodWon += () => UpdateOffices(TR);
        Debug.Log("couch is placed");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // level counters
    public int levelTR = 0;    // technology & realisation
    public int levelCA = 0;    // creativity & aesthetics
    public int levelMDC = 0;   // math, data & computing
    public int levelUS = 0;    // user & society
    public int levelBE = 0;    // business & entrepeneurship

    // This void can be called whenever a minigame is passed, 
    // it then automatically updates that part of the office
    void UpdateLevel(string department)
    {
        switch(department)
        {
            case TR:
                if (levelTR < 5){
                    levelTR += 1;
                }
                else {
                    Debug.Log("Level is already at 5, cannot be higher.");
                }
                break;

            case "CA":
                if (levelCA < 5){
                    levelCA += 1;
                }
                else {
                    Debug.Log("Level is already at 5, cannot be higher.");
                }
                break;

            case "MDC":
                if (levelMDC < 5){
                    levelMDC += 1;
                }
                else {
                    Debug.Log("Level is already at 5, cannot be higher.");
                }
                break;

            case "US":
                if (levelUS < 5){
                    levelUS += 1;
                }
                else {
                    Debug.Log("Level is already at 5, cannot be higher.");
                }
                break;

            case "BE":
                if (levelBE < 5){
                    levelBE += 1;
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
                switch(levelTR){
                    case 1:
                        //add stuff

                        GameObject couch = Instantiate(couchObject, new Vector3(0, 0, 0), transform.rotation);
                        couch.transform.Rotate(0, 45, 0);

                        break;
                    case 2:
                        //more stuff
                        break;
                    case 3:
                        //even more stuff
                        break;
                    case 4:
                        // giant amounts of stuff
                        break;
                    case 5:
                        // almost too much stuff
                        break;
                }
            break;

            //other cases will probably not be done
        }

    }

}
