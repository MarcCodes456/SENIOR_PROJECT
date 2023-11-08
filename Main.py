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
serverAddressPort = ("127.0.0.1",6969) #5052 alt port

while True:
    success, img = cap.read()
    hands, img = detector.findHands(img)
    cv2.imshow("Keaton Oppenheimer", img)
    
    cv2.waitKey(1)
    
    data = []


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
        
        sock.sendto(str.encode(str(data)), serverAddressPort)
        sock.sendto(str.encode(str(handType1)), serverAddressPort)

        #second hand
        if len(hands) == 2:
            hand2 = hands[0]
            lmList2 = hand2['lmList']
            handType2 = hand2["type"]
            print(lmList2)
            for lm in lmList2:
                data.extend([lm[0], height - lm[1], lm[2]])
            print(data)
            if handType2 == "Left":
                for ht in handType2:
                    print(handType2)

            sock.sendto(str.encode(str(data)), serverAddressPort)
            sock.sendto(str.encode(str(handType2)), serverAddressPort)
        print(" ")  # New line for better readability of the printed output

    

# 10/3 Create Unity Project and Send Data their
