using UnityEbgube;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class UDPReceive: MonoBehaviour{
  Thread receiveThread;
  UdpClient client;
  public int port = 5052;
  public bool printToConsole = false;
  public string data;

  public void Start(){
    receiveThread = new Thread(
      new ThreadStart(ReceiveData));
    receiveThread.IsBackground = true;
    receiveThread.Start();
  }

  //Actual Receiving The Thread and stuff
  private void RecevieData(){
    client = new UdpClient(port);
    while (startRecieving){

      try{
        IPEndPoint anyIP = new IPEndPoint(IP Address.Any, 0);
        byte[] dataByte = client.Recevie(ref anyIP);
        data = Encoding.UTF8.GetString(dataByte);

        if(printToConsole){print(data);}
        }
        catch (Exception err)
        {
          print(err.ToString())
        }
      }
    }
  }
  
  //19:19 In vid
}
