using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public static class InputUtils 
{
    public static string OnlyNumbers(string cpf)
    {
        return Regex.Replace(cpf, "[^0-9]", "");
    }

    public static bool IsOnlyNumbers(string cpf)
    {
        return Regex.IsMatch(cpf, "[0-9]");
    }

}
