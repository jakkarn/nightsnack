using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalScript : MonoBehaviour
{

    public Player player;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


public class Global
{
    public static EnemyType encounter;
    public static int charisma = 0;
    public static int numFriendsMade = 3;
    public static Vector3 playerPos = new Vector3(0,0);
    public static bool hasBattled = false;
}
