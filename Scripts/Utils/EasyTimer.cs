using System;
using Godot;

namespace Platformer.Scripts.Utils;

public class EasyTimer
{
    public Timer Timer { get; private set; }

    public EasyTimer(Node node, Action timeOut, float waitTime = 1.0f, bool oneShot = true)
    {
        Timer = new Timer();
        Timer.OneShot = oneShot;
        Timer.WaitTime = waitTime;
        node.AddChild(Timer);
        Timer.Timeout += timeOut;
    }

    public void Start() => Timer.Start();

    public void Stop()
    {
        Timer.Stop();
        if (Timer.OneShot)
            Timer.QueueFree();
    }
}