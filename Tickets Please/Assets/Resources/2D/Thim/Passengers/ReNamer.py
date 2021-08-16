import os
from shutil import copyfile
from os import listdir
from os.path import isfile, join

def CreateFile(side,file, dest, name, state):
    copyfile(file, dest+"/"+ state + side + " - "+name+".png")

def CreateFiles(side,file, dest, name):
    CreateFile(side,file, dest, name, "Walk")
    CreateFile(side,file, dest, name, "BackWalk")
    CreateFile(side,file, dest, name, "Idle")
    CreateFile(side,file, dest, name, "Sit")

name = "Pants 3"
pname = "/FeetParts/MalePants3"
mainPath = "G:/Game Dev/Unity games/Git/Tickets Please/tickets-please/Tickets Please/Assets/Resources/2D/Thim/Passengers" + pname
mainDest = "G:/Game Dev/Unity games/Git/Tickets Please/tickets-please/Tickets Please/Assets/Resources/2D/Thim/Passengers" + pname
if not os.path.exists(mainDest):
    os.makedirs(mainDest)

onlyfiles = [f for f in listdir(mainPath) if isfile(join(mainPath, f)) & (".meta" not in f) & (".png" in f)]
print("")
print("Found ("+str(len(onlyfiles))+") files:")
for file in onlyfiles:
    print(file)
    path = mainPath+"/"+file
    if len(onlyfiles)==2:
        if "Spejl" in file:
            CreateFiles("L", path, mainDest, name)
        else:
            CreateFiles("R", path, mainDest, name)
        os.remove(path)

    elif len(onlyfiles)==4:
        if "Spejl" in file:
            if "Sidde" in file:
                CreateFile("R", path, mainDest, name, "Sit")
            else:
                CreateFile("R", path, mainDest, name, "Walk")
                CreateFile("R", path, mainDest, name, "BackWalk")
                CreateFile("R", path, mainDest, name, "Idle")
        else:
            if "Sidde" in file:
                CreateFile("L", path, mainDest, name, "Sit")
            else:
                CreateFile("L", path, mainDest, name, "Walk")
                CreateFile("L", path, mainDest, name, "BackWalk")
                CreateFile("L", path, mainDest, name, "Idle")

        os.remove(path)
        

if len(onlyfiles)==2 or len(onlyfiles)==4:
    print("Did work on above files")
else:
    print("Not correct format for this code")
print("")