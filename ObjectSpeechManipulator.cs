using System;
namespace Snowball
{
    public class ObjectSpeechManipulator : ObjectManipulator
    {

        float time = 0;
        float timer = 0;
        string dialog;
        string font;
        public ObjectSpeechManipulator(string dialog, float time, string font, ScriptedObject scriptedObject)
        {
            this.dialog = dialog;
            this.time = time;
            this.font = font;
            this.scriptedObject = scriptedObject;
        }
        
        public override bool Update()
        {
            timer += Engine.deltaTime;
            var textPos = scriptedObject.position + scriptedObject.boundingBox.topCenter - new System.Numerics.Vector2(0, 15);
            Engine.window.DrawText(dialog, font, textPos, OriginType.bottomCenter);
            complete = timer >= time;
            return complete;
        }
    }
}