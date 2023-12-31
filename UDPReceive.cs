using UnityEngine;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class UDPReceive: MonoBehaviour{
  Thread receiveThread;
  UdpClient client;
  UdpClient clientAlt;
  public int port = 5052;
  public int portAlt = 4200;
  public bool startRecieving = true;
  public bool printToConsole = false;
  public string data;
  public string handType;
  

  public void Start(){
    receiveThread = new Thread(
      new ThreadStart(ReceiveData));
    receiveThread.IsBackground = true;
    receiveThread.Start();
  }

  //Actual Receiving The Thread and stuff
  private void ReceiveData(){
    client = new UdpClient(port);
    clientAlt = new UdpClient(portAlt);
    while (startRecieving){

      try{
        IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
        //Pretty much just duplicated these
        byte[] dataByte = client.Receive(ref anyIP);
        byte[] handByte = clientAlt.Receive(ref anyIP);

        data = Encoding.UTF8.GetString(dataByte);
        handType = Encoding.UTF8.GetString(handByte);
        if(printToConsole)
        {
          print(data);
          print(handType);
        } 
        }
        catch (Exception err)
        {
          print(err.ToString());
        }
      }
    }
  }

