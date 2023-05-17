using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.Net.Security;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;
using System.Data;

namespace KAYAAPI
{
    /// <summary>
    [WebService(Namespace = "http://localhost:18752/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class webservice : System.Web.Services.WebService
    {

        LogWriter log = new LogWriter();
        ProcessLogic pl = new ProcessLogic();
        UtilityTransaction utilTran = new UtilityTransaction();
        DataAccess da = new DataAccess();

        [WebMethod]
        public string InquireCustomer(string requestCode, string custRef, string ApiUsername, string ApiPassword)
        {
            log.LogWrite("", "");
            log.LogWrite("", "");
            log.LogWrite("APIMethod", "InquireCustomer");
            log.LogWrite("requestCode", requestCode);
            log.LogWrite("custRef", custRef);
            log.LogWrite("ApiUsername", ApiUsername);
            string Response = "";

            string InqStatus = "E";

            try
            {
                if (ApiUsername.Trim() == "KAYAUSER" && ApiPassword.Trim() == "!@#$1234")
                {
                    if (requestCode != null && !requestCode.Trim().Equals(""))
                    {
                        if (custRef != null && !custRef.Trim().Equals(""))
                        {
                            if (requestCode == "001")
                            {
                                Response = InquirePayment(requestCode, custRef);
                            }
                            else
                            {
                                Response = "|002|WRONG CODE PROVIDED!";
                            }
                        }
                        else
                        {
                            Response = InqStatus + "|444|INVALID CUSTOMER REFERENCE SUPPLIED";
                        }
                    }
                    else
                    {
                        Response = InqStatus + "|333|INVALID UTILITY CODE SUPPLIED";
                    }
                }
                else
                {
                    Response = InqStatus + "|222|INVALID API CREDENTIALS";
                }
            }
            catch (Exception ex)
            {
                Response = InqStatus + "|999|INQ GENERAL ERROR";
                log.LogWrite("Response", Response);
                log.LogWrite("Exception", ex.Message);
            }
            log.LogWrite("InquireCustomerResponse", Response);
            return Response;
        }

        private string InquirePayment(string requestCode, string custRef)
        {
            string Resp = "";
            string InqStatus = "E";
            //string phone = "";

            try
            {
                InquirePaymentt inquirecust = new InquirePaymentt();
                inquirecust.RequestCode = requestCode;
                inquirecust.Custref = custRef;
                Resp = da.SelectCustomer(inquirecust);
            }
            catch (Exception ex)
            {
                Resp = InqStatus + "|999|KAYA NEW INQ GENERAL ERROR";
                log.LogWrite("KAYANEWException", ex.Message);
                log.LogWrite("KAYANEWINQException", ex.InnerException.ToString());
            }

            log.LogWrite("InquirePayment", Resp);
            return Resp;
        }

        [WebMethod]
        public string PostPaymentTransaction(string kayarequestCode, string custRef, string ApiUsername, string ApiPassword, string custPhone, string Amount)
        {
            log.LogWrite("", "");
            log.LogWrite("", "");
            log.LogWrite("APIMethod", "PostPaymentTransaction");
            log.LogWrite("requestCode", kayarequestCode);
            log.LogWrite("custRef", custRef);
            log.LogWrite("ApiUsername", ApiUsername);
            log.LogWrite("custPhone", custPhone);
            log.LogWrite("Amount", Amount);


            string Response = "";
            string postRes = "E";


            try
            {
                if (ApiUsername.Trim() == "KAYAUSER" && ApiPassword.Trim() == "!@#$1234")
                {
                    if (kayarequestCode != null && !kayarequestCode.Trim().Equals(""))
                    {
                        if (custRef != null && !custRef.Trim().Equals(""))
                        {
                            if (Amount != null && !Amount.Trim().Equals("") && long.TryParse(Amount, out long resul))
                            {
                                if (kayarequestCode == "001")
                                {
                                    KAYAPayment kayaPay = new KAYAPayment();
                                    kayaPay.Amount = Amount;
                                    kayaPay.Custphone = custPhone;
                                    kayaPay.Custref = custRef;
                                    kayaPay.KayarequestCode = kayarequestCode;
                                    Response = NotifyKayaParked(kayaPay);
 
                                }
                            }
                            else
                            {
                                Response = postRes + "|INVALID AMOUNT SUPPLIED|445|XX";
                            }
                        }
                        else
                        {
                            Response = postRes + "|INVALID CUSTOMER REFERENCE SUPPLIED|444|XX";
                        }
                    }
                    else
                    {
                        Response = postRes + "|INVALID UTILITY CODE SUPPLIED|333|XX";
                    }
                }
                else
                {
                    Response = postRes + "|INVALID API CREDENTIALS|222|XX";
                }
            }
            catch (Exception ex)
            {
                Response = postRes + "|NOTIFY GENERAL ERROR|999|XX";
                log.LogWrite("Response", Response);
                log.LogWrite("Exception", ex.Message);
            }
            log.LogWrite("NotifyUtilityResponse", Response);
            return Response;
        }

        private string NotifyKayaParked(KAYAPayment kayaPay)
        {
            string Resp = "";
            string InqStatus = "E";

            try
            {
                //Map to Utility Transaction
                utilTran.Amount = kayaPay.Amount;
                utilTran.Cust_name = kayaPay.Custname;
                utilTran.Custref = kayaPay.Custref;
                utilTran.Cust_phone = kayaPay.Custphone;
                utilTran.Utility_code = kayaPay.KayarequestCode;
                utilTran.Tranref = kayaPay.Paymentreference;
                //utilTran.Field1 = kayaPay.Custype;
                //utilTran.Field2 = kayaPay.PayType;
                int minAmt = 1000;
                int maxAmt = 1000000;
                int amt = Int32.Parse(utilTran.Amount);

                var chars = "0123456789";
                var stringChars = new char[24];
                var random = new Random();

                for (int i = 0; i < stringChars.Length; i++)
                {
                    stringChars[i] = chars[random.Next(chars.Length)];
                }
                var TokenGen = new String(stringChars);
                int Tokensize = (amt/500);
                //var Tokensize = Int32.Parse(Tokensize1);
                //Console.WriteLine(Decimal.ToInt32(Tokensize));

                //log.LogWrite("TokenSizee", Tokensize);
                string TokenSent = kayaPay.Custref +"_"+ Tokensize +"_" +TokenGen;
                log.LogWrite("Token Sent", TokenSent);

                if (TokenGen != null)
                {
                    if (amt >= minAmt)
                    {
                        if (amt <= maxAmt)
                        {
                            string insertResp = da.InsertUtilityTransaction(utilTran, TokenSent);
                            InqStatus = "Success| " + "Token :" + TokenSent + "^" + "TokenUnits :" + Tokensize;
                            Resp = InqStatus;
                        }
                        else
                        {
                            Resp = InqStatus + "|778|AMOUT PAID IS ABOVE MAXIMUM FOR TOKEN!";
                        }
                    }
                    else
                    {
                        Resp = InqStatus + "|778|AMOUT PAID IS BELOW MINIMUM FOR TOKEN!";
                    }
                }
                else
                {
                    Resp = InqStatus + "|777|KAYA PAYMENT RETURNED NO REFERENCE OR FAILED!";
                }

            }
            catch (Exception ex)
            {
                Resp = InqStatus + "|999|KAYA PAYMENT PARKING INSERT ERROR";
                log.LogWrite("NotifyKayaParked", ex.Message);
            }

            log.LogWrite("NotifyKayaParked", Resp);
            return Resp;
        }
    }
}
