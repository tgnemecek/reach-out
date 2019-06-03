using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemConstants : MonoBehaviour
{
    public static readonly float SCREEN_BOTTOM = -4.5f;
    public static readonly float SCREEN_TOP = 4.5f;
    public static readonly float SCREEN_LEFT = -6f;
    public static readonly float SCREEN_RIGHT = 2.3f;

    public static readonly float BULLET_SPEED = 10f;
    public static readonly float PLAYER_SPEED = 5f;
    public static readonly float UNIT_SPEED = 6f;

    public static readonly int DESTRUCTION_PENALTY = 20;
    public static readonly int CONECTION_BONUS = 20;
    public static readonly int INITIAL_POINTS = 200;

    public static int WINCONDITION = 4;
}
