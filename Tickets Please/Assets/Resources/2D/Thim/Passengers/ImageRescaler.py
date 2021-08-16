import cv2
import os
from os import listdir
from os.path import isfile, join

mainPath = "G:/Rest/UNF/GDC/2019/PB/Kvadrater"
mainDest = mainPath + "/Test"
if not os.path.exists(mainDest):
    os.makedirs(mainDest)

onlyfiles = [f for f in listdir(mainPath) if isfile(join(mainPath, f))]
print("")
print("Found ("+str(len(onlyfiles))+") files:")
for file in onlyfiles:
    #print(file)
    path = mainPath+"/"+file
    print(path)
    img = cv2.imread(path)
    img = cv2.resize(img, (300,300), interpolation=cv2.INTER_AREA)
    cv2.imwrite(mainDest+"/"+file, img)

print("Done work on above files")
print("")