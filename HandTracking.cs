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
        string data = udpReceive.data;
        string handType = udpReceive.handType;
        data = data.Remove(0, 1);
        data = data.Remove(data.Length-1, 1);
        
        print(data);
        string[] points = data.Split(',');
        print(points[0]);

        for ( int i=0; i<21;  i++ ){
        float x1 = float.Parse(points[i*3])/80;
        float y1 = float.Parse(points[i*3 + 1])/80;
        float z1 = float.Parse(points[i*3 + 2])/80;


        float x2 = float.Parse(points[i*3])/80;
        float y2 = float.Parse(points[i*3 + 1])/80;
        float z2 = float.Parse(points[i*3 + 2])/80;

        
        //In the 'Main.py' find a way to print out the "handType" as a string 'Left' or 'Right'
        
        //Alt outcomes use booleans instead of strings
        if(handType == "Right"){
            handPoints[i].transform.localPosition = new Vector3(x1, y1, z1);
        }
        else{print("Not Right");}

        if(handType == "Left"){
            handPointsB[i].transform.localPosition = new Vector3(x2, y2, z2);
        }
        else{print("Not Left");}
        


        //handPoints[i].transform.localPosition = new Vector3(x, y, z);
        //handPointsB[i].transform.localPosition = new Vector3(x, y, z);

        }
    }

    
}
