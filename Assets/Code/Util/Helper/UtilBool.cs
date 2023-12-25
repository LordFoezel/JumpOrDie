using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilBool
{
    public static bool IntToBool(int var){
        if(var == 0) return false;
        return true;
    }

     public static bool StringToBool(string var){
        if(var == "False") return false;
        if(var == "false") return false;
        if(var == "") return false;
        return true;
    }

    public static int BoolToInt(bool var){
        if(var) return 1;
        return 0;
    }

}
