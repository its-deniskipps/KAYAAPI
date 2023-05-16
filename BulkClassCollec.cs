using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KAYAAPI
{
    public class Accountholder
    {
        public bool validField { get; set; }
    }
    public class ArgumentsField
    {
        public string nameField { get; set; }
        public string valueField { get; set; }
    }


    public class Error
    {
        public ArgumentsField argumentsField { get; set; }
        public string errorcodeField { get; set; }
    }
    public class NameField
    {
        public string prefixField { get; set; }
        public string firstnameField { get; set; }
        public string surnameField { get; set; }
    }

    public class BirthField
    {
        public DateTime dateField { get; set; }
        public string countryField { get; set; }
    }

    public class ClubAccount
    {
        public string clubName { get; set; }
        public string clubAccount { get; set; }
    }

    public class AccountsResponse
    {
        public string StatusCode { get; set; }
        public string CName { get; set; }
        public List<ClubAccount> ClubAccounts { get; set; }
    }
    public class InformationField
    {
        public NameField nameField { get; set; }
        public BirthField birthField { get; set; }
        public string occupationField { get; set; }
        public string residentialstatusField { get; set; }
    }

    public class Accountinfo
    {
        public InformationField informationField { get; set; }
    }
    public class RootObject
    {
        public Accountholder accountholder { get; set; }
        public Error error { get; set; }
        public object envelope { get; set; }
        public Accountinfo accountinfo { get; set; }

    }


    public class DepositResponse
    {
        public string StatusCode { get; set; }
        public string Description { get; set; }
        public string ReferenceNo { get; set; }
    }


    public class KAYAPayment
    {
        private string kayarequestCode, custref, custname, custype, custphone, amount, paymentreference, payType;


        public string KayarequestCode
        {
            get
            {
                return kayarequestCode;
            }
            set
            {
                kayarequestCode = value;
            }
        }
        public string PayType
        {
            get
            {
                return payType;
            }
            set
            {
                payType = value;
            }
        }
        public string Custref
        {
            get
            {
                return custref;
            }
            set
            {
                custref = value;
            }
        }
        public string Custname
        {
            get
            {
                return custname;
            }
            set
            {
                custname = value;
            }
        }
        public string Custype
        {
            get
            {
                return custype;
            }
            set
            {
                custype = value;
            }
        }
        public string Custphone
        {
            get
            {
                return custphone;
            }
            set
            {
                custphone = value;
            }
        }
        public string Amount
        {
            get
            {
                return amount;
            }
            set
            {
                amount = value;
            }
        }
        public string Paymentreference
        {
            get
            {
                return paymentreference;
            }
            set
            {
                paymentreference = value;
            }
        }

    }
    public class InquirePaymentt
    {
        private string requestCode, custref, custname;


        public string RequestCode
        {
            get
            {
                return requestCode;
            }
            set
            {
                requestCode = value;
            }
        }
        public string Custref
        {
            get
            {
                return custref;
            }
            set
            {
                custref = value;
            }
        }
        public string Custname
        {
            get
            {
                return custname;
            }
            set
            {
                custname = value;
            }
        }
        public string firstname
        {
            get
            {
                return firstname;
            }
            set
            {
                firstname = value;
            }
        }
        public string email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
            }
        }

    }
    public class Response
    {
        public string message { get; set; }
        public string status { get; set; }
        public string tpgoReference { get; set; }
        public object cin { get; set; }
        public string customerName { get; set; }
        public string customerPhone { get; set; }
        public string systemName { get; set; }
        public string systemCode { get; set; }
        public string prn { get; set; }
        public string prnDate { get; set; }
        public string expiryDate { get; set; }
        public string paymentAmount { get; set; }
        public string paymentCurrency { get; set; }
        public string allowPartial { get; set; }
        public string success { get; set; }
        public string errorCode { get; set; }
        public string refCheck { get; set; }
        public string sessionKey { get; set; }
        public string tpgoTrackingID { get; set; }
        public string trackingRef { get; set; }
    }

    public class QueryAccountResultField
    {
        public string statusDescField { get; set; }
        public string mininumAmountField { get; set; }
        public string areaField { get; set; }
        public string custNameField { get; set; }
        public string accountRefField { get; set; }
        public string statusCodeField { get; set; }
        public string meterNumberField { get; set; }
    }

    public class ConfirmPaymentResultField
    {
        public string statusDescField { get; set; }
        public string taxField { get; set; }
        public string meterTypeField { get; set; }
        public string serviceChargeField { get; set; }
        public string accountRefField { get; set; }
        public string creditAmountField { get; set; }
        public string tokenField { get; set; }
        public string pricePerUnitField { get; set; }
        public string unitsField { get; set; }
        public string transactionIDField { get; set; }
        public string statusCodeField { get; set; }
        public string receiptNumberField { get; set; }
        public string tenderAmountField { get; set; }
        public string meterNumberField { get; set; }
    }

    public class ProfileField
    {
        public string firstNameField { get; set; }
        public string lastNameField { get; set; }
        public string initialField { get; set; }
        public int accountNumberField { get; set; }
        public bool accountNumberFieldSpecified { get; set; }
        public int customerNumberField { get; set; }
        public bool customerNumberFieldSpecified { get; set; }
        public int accountStatusField { get; set; }
        public bool accountStatusFieldSpecified { get; set; }
        public bool hasBoxOfficeField { get; set; }
        public bool hasBoxOfficeFieldSpecified { get; set; }
        public int invoicePeriodField { get; set; }
        public bool invoicePeriodFieldSpecified { get; set; }
        public double totalBalanceField { get; set; }
        public bool totalBalanceFieldSpecified { get; set; }
    }

    public class QuoteField
    {
        public DateTime dueDateField { get; set; }
        public bool dueDateFieldSpecified { get; set; }
        public double productsTotalAmountField { get; set; }
        public bool productsTotalAmountFieldSpecified { get; set; }
        public double balanceDueField { get; set; }
        public bool balanceDueFieldSpecified { get; set; }
        public double totalAmountField { get; set; }
        public bool totalAmountFieldSpecified { get; set; }
    }

    public class KayaResponse
    {
        public string statusDescriptionField { get; set; }
        public string statusCodeField { get; set; }
        public string customerRefField { get; set; }
        public string customerNameField { get; set; }
        public string customerTypeField { get; set; }
        public double balanceField { get; set; }
        public double creditField { get; set; }
    }

    public class RootObjectKaya
    {
        public int error { get; set; }
        public string response { get; set; }
        public KayaResponse KayaResponse { get; set; }
    }

}