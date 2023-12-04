using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTracking : MonoBehaviour
{

    public UDPReceive udpReceive;
    public GameObject[] handPoints;
    public GameObject[] handPointsB;


    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {

        string handType = udpReceive.handType;
        string handTypeAlt = udpReceive.handTypeAlt;


    //RIGHT HAND DATA JUNK
        string dataR = udpReceive.dataR;
        dataR = dataR.Remove(0, 1);
        dataR = dataR.Remove(dataR.Length-1, 1);
        print(dataR);
        string[] pointsR = dataR.Split(',');
        print(pointsR[0]);


    //LEFT HAND DATA JUNK
        string dataL = udpReceive.dataL;
        dataL = dataL.Remove(0, 1);
        dataL = dataL.Remove(dataL.Length-1, 1);
        print(dataL);
        string[] pointsL = dataL.Split(',');
        print(pointsL[0]);


        for ( int i=0; i<21;  i++ ){
    //RIGHT
        float x1 = float.Parse(pointsR[i*3])/80;
        float y1 = float.Parse(pointsR[i*3 + 1])/80;
        float z1 = float.Parse(pointsR[i*3 + 2])/80;

    //LEFT
        float x2 = float.Parse(pointsL[i*3])/80;
        float y2 = float.Parse(pointsL[i*3 + 1])/80;
        float z2 = float.Parse(pointsL[i*3 + 2])/80;

        
        if (handType == "Right" && handType == "Left"){
            handType = "Both";
        }


        Debug.Log("handType: " + handType);

        if (handType == "Right" || handTypeAlt == "Right")
        {
            handPoints[i].transform.localPosition = new Vector3(x1, y1, z1);
        }
        else if (handType == "Left" || handTypeAlt == "Right")
        {
            handPointsB[i].transform.localPosition = new Vector3(x2, y2, z2);
        }
        else
        {
            Debug.LogWarning("Unexpected handType: " + handType);
        }


        }
    }

    
}
