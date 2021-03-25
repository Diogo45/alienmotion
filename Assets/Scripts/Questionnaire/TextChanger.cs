using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextChanger : MonoBehaviour
{

    [SerializeField] private TextAsset _textAsset;

    [SerializeField] private TMPro.TMP_Text OutText;

    // Start is called before the first frame update
    void Start()
    {
        OutText.text = _textAsset.Text;
    }

    public void SetText(TextAsset text)
    {
        _textAsset = text;
    }

    public void UpdateText()
    {
        OutText.text = _textAsset.Text;
    }

}
