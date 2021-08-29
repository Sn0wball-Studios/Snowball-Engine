# Snowball-Engine
light weight modular game engine

## features
- easy lua api.
- can be easily ported to your game library of choice(SDL2, OpenTK, etc).
<br></br>
## creating a game
to create a game simply run "./Snowball create game GameName" this will create a new skeleton game in the GameName directory

for examples on the acual coding of games,
see the demo game in the "demo" directory.<br></br>
<br></br>

you can reference these from Snowball.dll,
then compile them to a dll, then place it in the "backends/" directory, and edit the engine.json file to include the names of your new classes



## dependencies
- [BBQLib](https://github.com/BBQGiraffe/BBQLib)
- [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json/)
