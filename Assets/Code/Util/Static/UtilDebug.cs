using System.Collections;
using UnityEngine;

public static class UtilDebug
{
    public static void Log(object input = null, string message = "Log")
    {
        if (input is null) Debug.Log(message + "(" + input.GetType() + "): ");
        else if (input is string) Debug.Log(message + "(" + input.GetType() + "): " + input);
        else if (input is int) Debug.Log(message + "(" + input.GetType() + "): " + input.ToString());
        else if (input is float) Debug.Log(message + "(" + input.GetType() + "): " + input.ToString());
        else if (input is IList)
        {
            string log = "";
            foreach (var listItem in (IEnumerable)input)
            {
                log += listItem.ToString() + ", ";
            }
            Debug.Log(message + "(" + input.GetType() + "): " + log);
        }
        else Debug.Log(message + "(" + input.GetType() + "): " + input.ToString());
    }
}
