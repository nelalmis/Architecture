using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Architecture.WCFDeneme
{
    public class Client
    {
        public void ClientEvent()
        {
            RestClient rClient = new RestClient();
            rClient.endPoint = "";
            /*
            if (rdoBasicAuth.Checked)
            {
                rClient.authType = authenticationType.Basic;
                debugOutput("authenticationType.Basic");
            }
            else
            {
                rClient.authType = authenticationType.NTLM;
                debugOutput("authenticationType.NTLM");
            }

            if (rdoRollOwn.Checked)
            {
                rClient.authTech = autheticationTechnique.RollYourOwn;
                debugOutput("autheticationTechnique.RollYourOwn;");
            }
            else
            {
                rClient.authTech = autheticationTechnique.NetworkCredential;
                debugOutput("autheticationTechnique.NetworkCredential");
            }

            rClient.userName = txtUserName.Text;
            rClient.userPassword = txtPassword.Text;

            debugOutput("REst Client Created");

            string strResponse = string.Empty;

            strResponse = rClient.makeRequest();

            debugOutput(strResponse);
            */
        }
        private void debugOutput(string strDebugText)
        {
            try
            {
                System.Diagnostics.Debug.Write(strDebugText + Environment.NewLine);
                //devmı önemsiz.
            }
            catch (Exception ex)
            {

            }
        }
    }
}