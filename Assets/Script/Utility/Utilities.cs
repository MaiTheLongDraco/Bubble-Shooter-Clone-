using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities
{
    public static void Log(string message, object info)
    {
        Debug.Log($"{message}: {info}");
    }
    public static void Log(string message)
    {
        Debug.Log($"{message}");
    }
}
