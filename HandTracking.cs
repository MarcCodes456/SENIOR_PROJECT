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

        
        if (handType == "Right" && handType == "Left"){
            handType = "Both";
        }


        Debug.Log("handType: " + handType);

        if (handType == "Right" || handType == "Both")
        {
            handPoints[i].transform.localPosition = new Vector3(x1, y1, z1);
        }
        else if (handType == "Left" || handType == "Both")
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
