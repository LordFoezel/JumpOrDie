using System.Collections;
using UnityEngine;

public static class UtilDebug
{
    public static void Log(string message = "Log", object input = null)
    {
        if (input is null) Debug.Log(message);
        else if (input is string) Debug.Log(message + "(string): " + input);
        else if (input is int) Debug.Log(message + "(int): " + input.ToString());
        else if (input is float) Debug.Log(message + "(float): " + input.ToString());
        else if (input is IList)
        {
            string log = "";
            foreach (var listItem in (IEnumerable)input)
            {
                log += listItem.ToString() + ", ";
            }
            Debug.Log(message + "(list): " + log);
        }
    }
}
