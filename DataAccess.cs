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

        internal string InsertUtilityTransaction(UtilityTransaction Tran, string TokenSent)
        {
            log.LogWrite("InsertUtilityTransaction", "ENTERED DATAACESS FUNCTION");
            log.LogWrite("InsertUtilityTransaction", "INSERTING TRANSACTION TO PARK....");
            //if (Tran.Field2 != null)
            //{
            //    log.LogWrite("InsertUtilityTransaction", "Inserting Field2 As: " + Tran.Field2);
            //}
            
            //DataTable results;
            string insertedId = "F";
            try
            {
                // string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=KAYADB;User Id=apiuser ;Password=Manager123;";
                string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=KAYADB;Integrated Security=true;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    String query = "insert into payments (custcode,paymentamt,token,createdtime) values (@custcode,@paymentAmt,@token,GETDATE());";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@paymentAmt", Tran.Amount);
                        command.Parameters.AddWithValue("@token", TokenSent);
                        command.Parameters.AddWithValue("@custcode", Tran.Custref);

                        command.ExecuteNonQuery();
                    }
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