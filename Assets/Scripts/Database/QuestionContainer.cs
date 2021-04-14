using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(), System.Serializable]
public class QuestionContainer : ScriptableObject
{
    [System.Serializable]
    public enum QuestionType
    {
        InputField, MultipleChoice
    }

    [System.Serializable]
    public struct Question
    {
        public QuestionType questionType;
        public string question;
        public string questionAnswers;

    }

    public Question[] questions;
    public string[] answers;

}
