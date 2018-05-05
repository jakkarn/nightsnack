using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalScript : MonoBehaviour
{

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
    public static EnemyType encounter = EnemyType.NONE;
    public static int charisma = 0;
    public static int numFriendsMade = 0;
}
