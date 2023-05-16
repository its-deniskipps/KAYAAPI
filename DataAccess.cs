using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using System.Net;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace KAYAAPI
{
    public class DataAccess
    {
        private Database KAYADB;
        private DbCommand mycommand;
        LogWriter log = new LogWriter();

        public DataAccess()
        {
            try
            {
                KAYADB = DatabaseFactory.CreateDatabase("TestKayaConnectionString");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal string InsertUtilityTransaction(UtilityTransaction Tran)
        {
            log.LogWrite("InsertUtilityTransaction", "ENTERED DATAACESS FUNCTION");
            log.LogWrite("InsertUtilityTransaction", "INSERTING TRANSACTION TO PARK....");
            if (Tran.Field2 != null)
            {
                log.LogWrite("InsertUtilityTransaction", "Inserting Field2 As: " + Tran.Field2);
            }
            
            DataTable results;
            string insertedId = "F";
            try
            {
                mycommand = KAYADB.GetStoredProcCommand("sp_InsertUtilityTransaction", Tran.Amount, Tran.Custref, Tran.Cust_name, Tran.Cust_phone, Tran.Tranref, 
                    Tran.Utility_code, Tran.Field1, Tran.Field2, Tran.Field3, Tran.Field4, Tran.Field5, Tran.Field6, Tran.Field7, Tran.Field8, Tran.Field9
                    , Tran.Field10, Tran.Field11, Tran.Field12, Tran.Field13, Tran.Field14, Tran.Field15, Tran.Field16, Tran.Field17, Tran.Field18, Tran.Field19
                    , Tran.Field20, Tran.Field21, Tran.Field22, Tran.Field23, Tran.Field24, Tran.Field25, Tran.Field26, Tran.Field27, Tran.Field28,
                    Tran.Field29, Tran.Field30);
                DataSet ds = KAYADB.ExecuteDataSet(mycommand);
                results = ds.Tables[0];
                if (results.Rows.Count > 0)
                {
                    insertedId = results.Rows[0]["InsertedId"].ToString();
                }
                if (Tran.Field2 != null)
                {
                    log.LogWrite("InsertUtilityTransaction", "After Insert, Field2 Is: " + Tran.Field2);
                }
            }
            catch (Exception ex)
            {
                log.LogWrite("InsertUtilityTransaction", ex.Message.ToString());
            }
            log.LogWrite("InsertUtilityTransaction", Tran.Utility_code + " INSERTED ID: " + insertedId);
            return insertedId;
        }
        internal string SelectCustomer(InquirePaymentt inquirecust)
        {
            //DataTable results;
            string selectionId = "F";
            try
            {
                // string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=KAYADB;User Id=apiuser ;Password=Manager123;";
                string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=KAYADB;Integrated Security=true;";
                SqlConnection connection = new SqlConnection(connectionString);

                string query = "SELECT FirstName,email FROM customers WHERE custcode = @custref";
                log.LogWrite("SelectCustomer", "Query Is: " + query);
                SqlCommand command = new SqlCommand(query, connection);

                SqlParameter param = new SqlParameter("@custref", SqlDbType.NVarChar);
                param.Value = inquirecust.Custref;
                log.LogWrite("SelectCustomer", "parameter Is: " + param.Value);
                command.Parameters.Add(param);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string firstname = reader.GetString(0);
                    log.LogWrite("SelectCustomer", "parameter Is: " + firstname);
                    string email = reader.GetString(1);
                    log.LogWrite("SelectCustomer", "parameter Is: " + email);
                }
                selectionId = "Success" + "|" + "FirstName : " + reader.GetString(0) + "^ Email :" +  reader.GetString(1);
                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                log.LogWrite("SelectCustomer", ex.Message.ToString());
                selectionId = ex.Message.ToString();
            }
            // selectionId = "S|" + "Dennis^" + "Chirchir^" + "denisekipps@gmail.com";
            log.LogWrite("SelectCustomer", inquirecust.Custref + " INSERTED ID: " + selectionId);
            return selectionId;
        }
    }
}