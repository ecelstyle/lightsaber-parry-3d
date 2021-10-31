using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public static class Parameters
{
    public static float simulationSpeed;
    public static string playerTag;
    public static string opponentTag;
    public static string collideMessage;

    static Parameters()
    {
        simulationSpeed = 2;
        playerTag = "Player";
        opponentTag = "Opponent";
        collideMessage = "Sabers will collide";
    }

}
