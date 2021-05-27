using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using System.Text;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SendMail : Singleton<SendMail>
{

    //private IEnumerator SendTo(string mailTo, string subject, string content)
    //{

    //    SmtpMail oMail = new SmtpMail("TryIt");

    //    oMail.From = "emotion.hunters.firebase@gmail.com";

    //    oMail.To = mailTo;

    //    oMail.Subject = subject;

    //    oMail.TextBody = content;

    //    SmtpServer oServer = new SmtpServer("smtp.gmail.com");




    //    oServer.User = "emotion.hunters.firebase@gmail.com";
    //    oServer.Password = "XrgrQTtp8rF7C4s";

    //    oServer.ConnectType = SmtpConnectType.ConnectTryTLS;

    //    oServer.Port = 587;

    //    //oServer.Port = 465; // 25 or 587 or 465
    //    //oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

    //    //Debug.Log("start to send email ...");

    //    SmtpClient oSmtp = new SmtpClient();

    //    oSmtp.SaveCopy = false;

    //    SmtpClientAsyncResult oResult = oSmtp.BeginSendMail(
    //                    oServer, oMail, null, null);
    //    // Wait for the email sending...
    //    while (!oResult.IsCompleted)
    //    {
    //        //Debug.Log("waiting..., you can do other thing!");
    //        oResult.AsyncWaitHandle.WaitOne(50, false);
    //    }
    //    oSmtp.EndSendMail(oResult);

    //    //oSmtp.SendMailToQueue(oServer, oMail);

    //    yield break;
    //    //Debug.Log("email was sent to queue successfully!");



    //}

    public void Send(string mailTo, string subject, string content)
    {
        //StartCoroutine(SendTo(mailTo, subject, content));

        var smtp = new SmtpClient()
        {
            Host = "smtp.gmail.com",
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network
        };
        var credentials = new NetworkCredential();

        smtp.UseDefaultCredentials = false;

        credentials.UserName = "emotion.hunters.firebase@gmail.com";
        credentials.Password = "XrgrQTtp8rF7C4s";

        smtp.Credentials = credentials;

        var from = new MailAddress("emotion.hunters.firebase@gmail.com", "Caçadores de Emoção");
        var to = new MailAddress(mailTo, mailTo);
        var msg = new MailMessage(from, to)
        {
            Subject = "Nova senha",
            Body = content,
            IsBodyHtml = false,
            Priority = MailPriority.Normal
        };

        

        smtp.Send(msg);
    }

    private void Awake()
    {
        base.Awake();
    }
}