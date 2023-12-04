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
  UdpClient HTclient;
  UdpClient HTclientAlt;
  
//Port For Rdata
  public int port = 6969;
//Port For Ldata
  public int portAlt = 1111;
//Port For HandType1
  public int portHT = 4200;
//Port For HandType1
  public int portHT_ALT = 1748;

  public bool startRecieving = true;
  public bool printToConsole = false;
  public string dataR;
  public string dataL;
  public string handType;
  public string handTypeAlt;
  

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
    HTclient = new UdpClient(portHT);
    HTclientAlt = new UdpClient(portHT_ALT);
    while (startRecieving){

      try{
        IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
        //Pretty much just duplicated these
        byte[] dataByteR = client.Receive(ref anyIP);
        byte[] dataByteL = clientAlt.Receive(ref anyIP);
        byte[] handByte = HTclient.Receive(ref anyIP);
        byte[] handByteAlt = HTclientAlt.Receive(ref anyIP);

        dataR = Encoding.UTF8.GetString(dataByteR);
        dataL = Encoding.UTF8.GetString(dataByteL);
        handType = Encoding.UTF8.GetString(handByte);
        handTypeAlt = Encoding.UTF8.GetString(handByteAlt);

        if(printToConsole)
        {
          print(dataR);
          print(dataL);
          print(handType);
          print(handTypeAlt);
        } 
        }
        catch (Exception err)
        {
          print(err.ToString());
        }
      }
    }
  }

