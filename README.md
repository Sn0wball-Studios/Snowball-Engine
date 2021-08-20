# Snowball-Engine
light weight modular game engine

## features
- easy lua api.
- can be easily ported to your game library of choice(SDL2, OpenTK, etc).
- scripted sequences. 
<br></br>
## creating a game
to create a game simply run "./Snowball create game GameName" this will create a new skeleton game in the GameName directory

for examples on the acual coding of games,
see the demo game in the "demo" directory.<br></br>
beware that the engine uses SFML by default, which does not support MP3, so stick to WAV or OGG(or write your own SoundFactory and SoundSource implementation)
<br></br>

## creating custom engine backends
SFML is pretty neat but if you need to use
a different library, you only need to write implementations for a few classes.
<br></br>
- SoundFactory
- SoundSouce
- WindowImplementation

you can reference these from Snowball.dll,
then compile them to a dll, then place it in the "backends/" directory, and edit the engine.json file to include the names of your new classes

