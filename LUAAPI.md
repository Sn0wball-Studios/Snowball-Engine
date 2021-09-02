# some information on the Lua api

## Input functions

- InputIsKeyDown(Keyboardkey Key) returns true if a key is down
- InputGetAxis(string axis) returns a -1 to 1 float for a axis

## Memory 
- Allocate(int size) returns a new byte array

## File IO
- ReadBinaryFile(string file) returns the contents of a file as a byte array
- SaveBinaryFile(string file, byte[] data) saves binary data to a file
- BinaryReader(string file) returns a new binary reader

## Vector2
the Vector2 class is a simple 2D vector with X and Y values

### Vector2 functions
- Vec2(float x, float y) returns a new Vector2
- Vec2GetAngle(Vector2 me, Vector2 target) returns the angle between two points
- Vec2GetDirection(Vector2 me, Vector2 target) returns the direction between two points
- Vec2GetLength(Vector2 x) returns the length of a Vector2
- Vec2Distance(Vector2 me, Vector2 target) returns the distance between two points


## Color
the Color class has 4 values:
- float r
- float g 
- float b
- float a

these use 0 to 1 values like in GLSL shaders, however you can use Color255 to convert byte colors to float colors

ex: Color255(255,255,255,255) returns 1,1,1,1

### color functions
- Color(float r, float g, float b, float a) creates a new GLSL style color(0 - 1)
- Color255(byte r, byte g, byte b, byte a) creates a new color from normal rgb values (0-255)
- ColorMix(Color a, Color b, float weight) returns a and b mixed together by weight

