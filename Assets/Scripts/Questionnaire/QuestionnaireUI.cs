using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Questionnaire
{
    public class QuestionnaireUI : Singleton<QuestionnaireUI>
    {

        public void Awake()
        {
            base.Awake();
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
            QuestionnaireManager.instance.UI.Title.SetActive(false);
        }

        public void Info_Login()
        {
            QuestionnaireManager.instance.UI.Info.SetActive(false);
            QuestionnaireManager.instance.UI.Login.SetActive(true);
            QuestionnaireManager.instance.UI.Title.SetActive(false);
        }

        public void Login_Info()
        {
            QuestionnaireManager.instance.UI.Info.SetActive(true);
            QuestionnaireManager.instance.UI.Login.SetActive(false);
            QuestionnaireManager.instance.UI.Title.SetActive(true);
        }

        public void Login_ForgotPassword()
        {
            QuestionnaireManager.instance.UI.Login.SetActive(false);
            QuestionnaireManager.instance.UI.ForgotPassword.SetActive(true);
        }

        public void ForgotPassword_Login()
        {
            QuestionnaireManager.instance.UI.ForgotPassword.SetActive(false);
            QuestionnaireManager.instance.UI.Login.SetActive(true);
        }

        public void LoginWrapper()
        {
            StartCoroutine(Login_Game());
        }


        public IEnumerator Login_Game()
        {

            //Debug.Log("QUI Waiting " + LoginManager.instance._loginState);
            StartCoroutine(LoginManager.instance.Login());

            yield return new WaitWhile(() => LoginManager.instance._loginState == LoginManager.LoginState.None);

            //Debug.Log("QUI Finised Waiting" + LoginManager.instance._loginState);

            if (LoginManager.instance._loginState == LoginManager.LoginState.Successful)
            {
                QuestionnaireManager.instance.UI.Login.SetActive(false);
                QuestionnaireManager.instance.Game.SetActive(true);
                QuestionnaireManager.instance.UI.Title.SetActive(true);
            }
            else if(LoginManager.instance._loginState == LoginManager.LoginState.PasswordDoesNotMatch)
            {
                Debug.LogError("Password Mismatch");
            }
            else if (LoginManager.instance._loginState == LoginManager.LoginState.MissingFromDataBase)
            {
                Debug.LogError("Missing from Database");
            }
            else if (LoginManager.instance._loginState == LoginManager.LoginState.NotAuthorized)
            {
                Debug.LogError("Teen is not Authorized");
            }



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
        public void RegisterType_TALE()
        {
            QuestionnaireManager.instance.UI.Register.SetActive(false);
            QuestionnaireManager.instance.UI.TALE.SetActive(true);
        }

        public void TALE_RegisterTeen()
        {
            QuestionnaireManager.instance.UI.RegisterTeen.SetActive(true);
            QuestionnaireManager.instance.UI.TALE.SetActive(false);
        }

        public void TALE_Info()
        {
            QuestionnaireManager.instance.UI.TALE.SetActive(false);
            QuestionnaireManager.instance.UI.Info.SetActive(true);
            QuestionnaireManager.instance.UI.Title.SetActive(true);
        }


        public void RegisterParent_SDData()
        {

            StartCoroutine(RegisterParent_SDDataRoutine());

           

        }


        private IEnumerator RegisterParent_SDDataRoutine()
        {

            StartCoroutine(FirestoreManager.instance.CheckRegisterParentData());

            yield return new WaitWhile(() => FirestoreManager.instance._state == DataBaseState.None);

            if(FirestoreManager.instance._state != DataBaseState.Ok)
            {
                Debug.LogError("PARENT ALREADY EXISTS IN DATABASE");

            }
            else
            {
                QuestionnaireManager.instance.UI.RegisterParent.SetActive(false);
                QuestionnaireManager.instance.UI.SDData.SetActive(true);
            }


            

            //FirestoreManager.instance.WriteRegisterData();

        }

        public void RegisterParent_Info()
        {
            QuestionnaireManager.instance.UI.RegisterParent.SetActive(false);
            QuestionnaireManager.instance.UI.Info.SetActive(true);
            QuestionnaireManager.instance.UI.Title.SetActive(true);
        }

        public void FinalRegisterTeen_Info()
        {
            QuestionnaireManager.instance.UI.RegisterTeenFinalScreen.SetActive(false);
            QuestionnaireManager.instance.UI.Info.SetActive(true);
            QuestionnaireManager.instance.UI.Title.SetActive(true);
            // StartCoroutine(FirestoreManager.instance.WriteRegisterTeenData());

        }


        public void RegisterTeen_FinalRegister()
        {

            StartCoroutine(RegisterTeen_FinalRegisterRoutine());

            
        }

        private IEnumerator RegisterTeen_FinalRegisterRoutine()
        {

            StartCoroutine(FirestoreManager.instance.CheckRegisterTeenData());

            yield return new WaitWhile(() => FirestoreManager.instance._state == DataBaseState.None);

            if (FirestoreManager.instance._state != DataBaseState.Ok)
            {
                Debug.LogError("Teen ALREADY EXISTS IN DATABASE");

            }
            else
            {
                QuestionnaireManager.instance.UI.RegisterTeen.SetActive(false);
                QuestionnaireManager.instance.UI.RegisterTeenFinalScreen.SetActive(true);

                StartCoroutine(FirestoreManager.instance.WriteRegisterTeenData());
            }



           

        }


        public void BackRegisterTeen_Info()
        {
            QuestionnaireManager.instance.UI.RegisterTeen.SetActive(false);
            QuestionnaireManager.instance.UI.Info.SetActive(true);
            QuestionnaireManager.instance.UI.Title.SetActive(true);

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
            QuestionnaireManager.instance.UI.Title.SetActive(true);

        }

        public void SDData_Final()
        {
            QuestionnaireManager.instance.UI.SDData.SetActive(false);
            QuestionnaireManager.instance.UI.RegisterFinalScreen.SetActive(true);
            StartCoroutine(FirestoreManager.instance.WriteRegisterParentData());
        }

        public void Final_Info()
        {
            QuestionnaireManager.instance.UI.RegisterFinalScreen.SetActive(false);
            QuestionnaireManager.instance.UI.Info.SetActive(true);
            QuestionnaireManager.instance.UI.Title.SetActive(true);

        }




    } 
}