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
detector = HandDetector(maxHands=1, detectionCon=0.8)

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
        hand = hands[0]
        lmList = hand['lmList']
        print(lmList)
        for lm in lmList:
            data.extend([lm[0], height - lm[1], lm[2]])
        print(data)
        sock.sendto(str.encode(str(data)), serverAddressPort)

# 10/3 Create Unity Project and Send Data their
