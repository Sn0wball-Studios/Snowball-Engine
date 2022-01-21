import glob
import os
headers = glob.glob("*.h")
sources = glob.glob("*.c")
buildScript = "gcc -Wall -g -Wno-missing-braces -Wunused-variable -Wunused-function"

for h in headers:
    buildScript = buildScript + " " + h + " "
for c in sources:
    buildScript = buildScript + " " + c + " "

buildScript = buildScript + " -o game -lSDL2 -lSDL2_ttf -lSDL2_image -lSDL2_mixer -ffast-math -lsquirrel3 -lsqstdlib3 -lm -lxml2 -lcjson -I /usr/include/libxml2/ -I /usr/include/SDL2/"

os.system(buildScript)