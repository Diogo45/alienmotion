using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text;

// Add EASendMail namespace
using EASendMail;
using System.Collections;

public class SendMail : Singleton<MonoBehaviour>
{

    private IEnumerator SendTo(string mailTo)
    {
        try
        {
            SmtpMail oMail = new SmtpMail("TryIt");

            oMail.From = "emotion.hunters.firebase@gmail.com";

            oMail.To = mailTo;

            oMail.Subject = "test email from c# project";

            oMail.TextBody = "this is a test email sent from c# queue";

            SmtpServer oServer = new SmtpServer("smtp.gmail.com");

            oServer.User = "emotion.hunters.firebase@gmail.com";
            oServer.Password = "XrgrQTtp8rF7C4s";

            oServer.ConnectType = SmtpConnectType.ConnectTryTLS;

            oServer.Port = 587;

            //oServer.Port = 465; // 25 or 587 or 465
            //oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

            //Debug.Log("start to send email ...");

            SmtpClient oSmtp = new SmtpClient();
            oSmtp.SendMail(oServer, oMail);

            //Debug.Log("email was sent to queue successfully!");
        }
        catch (Exception e)
        {
            Debug.LogError("Couldnt send mail to " + mailTo);
            Debug.LogError(e);
        }


        yield return null;
    }

    private void Awake()
    {
        base.Awake();
    }
}