import cv2
from HandTrackingModule import HandDetector
import numpy as np
import mediapipe as mp
import socket


# Parameters
width, height = 1280, 720

#Webcam
cap = cv2.VideoCapture(0)
cap.set(3, width)
cap.set(4, height)

# Hand Detector
# Change to 2 Hands
detector = HandDetector(maxHands=2, detectionCon=0.8)

#Communication To Unity
sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
serverAddressPort_RIGHT = ("127.0.0.1",6969) #5052 alt port
serverAddressPort_LEFT = ("127.0.0.1",1111) #5052 alt port
serverAddressPort_HT1 = ("127.0.0.1",4200) #5052 alt port
serverAddressPort_HT2 = ("127.0.0.1",1748) #5052 alt port

while True:
    success, img = cap.read()
    hands, img = detector.findHands(img)
    cv2.imshow("Keaton Oppenheimer", img)
    
    cv2.waitKey(1)
    
    data = []
    dataB = []


    # Landmark Values - (x,y,z) * 21
    
    if hands:
        hand1 = hands[0]
        lmList = hand1['lmList']
        handType1 = hand1["type"]
        print(lmList)
        for lm in lmList:
            data.extend([lm[0], height - lm[1], lm[2]])
        print(data)
        if handType1 == "Right":
            for ht in handType1:
                print(handType1)
        
        sock.sendto(str.encode(str(data)), serverAddressPort_RIGHT)
        sock.sendto(str.encode(str(handType1)), serverAddressPort_HT1)

        #second hand
        if len(hands) == 2:
            hand2 = hands[0]
            lmList2 = hand2['lmList']
            handType2 = hand2["type"]
            print(lmList2)
            for lm in lmList2:
                dataB.extend([lm[0], height - lm[1], lm[2]])
            print(dataB)
            if handType2 == "Left":
                for ht in handType2:
                    print(handType2)

            sock.sendto(str.encode(str(dataB)), serverAddressPort_LEFT)
            sock.sendto(str.encode(str(handType2)), serverAddressPort_HT2)
        print(" ")  # New line for better readability of the printed output

        
    

        #BOTH HANNNDDSS
        #if len(hands) == 2:
        #    bHands = hands[0]
        #    lmList3 = bHands['lmList']
        #    bothHands = bHands["type"]
        #    print(lmList3)
        #    for lm in lmList3:
        #        data.extend([lm[0], height - lm[1], lm[2]])
        #    print(data)
        #    if bothHands == handType2 + handType1:
        #        for ht in bothHands:
        #            print("Both")
        #    sock.sendto(str.encode(str(data)), serverAddressPort)
        #    sock.sendto(str.encode(str(bothHands)), serverAddressPort_Alt)
        #print(" ")
# 10/3 Create Unity Project and Send Data their
