using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public static class InputUtils 
{
    public static string CPFINput(string cpf)
    {
        return Regex.Replace(cpf, "[^0-9]", "");
    }
}
