using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TextAsset : ScriptableObject
{
    [field: SerializeField, TextArea(15, 20)]
    public string Text { get; private set; }
    
    

}
