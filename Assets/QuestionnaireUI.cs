using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Questionnaire
{
    public class QuestionnaireUI : MonoBehaviour
    {

        public static QuestionnaireUI instance;

        public void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void Welcome_Info()
        {
            QuestionnaireManager.instance.UI.WelcomePage.SetActive(false);
            QuestionnaireManager.instance.UI.Info.SetActive(true);
        }

        public void Info_Register()
        {
            QuestionnaireManager.instance.UI.Info.SetActive(false);
            QuestionnaireManager.instance.UI.Register.SetActive(true);
        }

        public void RegisterType_RegisterTeen()
        {
            QuestionnaireManager.instance.UI.Register.SetActive(false);
            QuestionnaireManager.instance.UI.RegisterTeen.SetActive(true);
        }

        public void RegisterType_RegisterParent()
        {
            QuestionnaireManager.instance.UI.Register.SetActive(false);
            QuestionnaireManager.instance.UI.RegisterParent.SetActive(true);
        }

        public void RegisterType_TCLE()
        {
            QuestionnaireManager.instance.UI.Register.SetActive(false);
            QuestionnaireManager.instance.UI.TCLE.SetActive(true);
        }

        public void RegisterParent_SDData()
        {
            QuestionnaireManager.instance.UI.RegisterParent.SetActive(false);
            QuestionnaireManager.instance.UI.SDData.SetActive(true);
        }

        public void TCLE_RegisterParent()
        {
            QuestionnaireManager.instance.UI.TCLE.SetActive(false);
            QuestionnaireManager.instance.UI.RegisterParent.SetActive(true);
        }

        public void TCLE_RegisterTeen()
        {
            QuestionnaireManager.instance.UI.TCLE.SetActive(false);
            QuestionnaireManager.instance.UI.RegisterTeen.SetActive(true);
        }

        public void TCLE_Info()
        {
            QuestionnaireManager.instance.UI.TCLE.SetActive(false);
            QuestionnaireManager.instance.UI.Info.SetActive(true);
        }

        public void SDData_Final()
        {
            QuestionnaireManager.instance.UI.SDData.SetActive(false);
            QuestionnaireManager.instance.UI.RegisterFinalScreen.SetActive(true);
        }

        public void Final_Info()
        {
            QuestionnaireManager.instance.UI.RegisterFinalScreen.SetActive(false);
            QuestionnaireManager.instance.UI.Info.SetActive(false);
        }



        bool IsNull(GameObject obj) { return obj == null ? true : false; }
    } 
}