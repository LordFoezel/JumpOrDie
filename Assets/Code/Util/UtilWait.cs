using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public static class UtilWait
{
   public static IEnumerator WaitSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    public static IEnumerator<bool> WaitSecondsAndReturnBool(float seconds)
    {
        new WaitForSeconds(seconds);
        yield return true;
    }
}

