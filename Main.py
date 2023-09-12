Import cv2
#https://www.youtube.com/watch?v=RQ-2JWzNc6k Move Code Over(2:42) And dowload stuff
from cvzone.HandTrackingModule import HandDetector
# Parameters
width, height = 1280, 720

#Webcam
cap = cv2.VideoCapture(0)
cap.set(3, width)
cap.set(4, height)

# Hand Detector
detector = HandDetector(maxHands=1, detectionCon=0.8)

while True:
    success, img = cap.read()
    hands, detector.findHands(img)
    cv2.imshow("Image", img)
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

## [6:42 Install Mediapip]
# [14:39 Sending Data to Unity]
