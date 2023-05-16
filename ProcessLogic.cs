using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Web.Hosting;
using System.Xml;

namespace KAYAAPI
{
    public class ProcessLogic
    {
        LogWriter log = new LogWriter();

        private async Task<string> GetRespString(HttpResponseMessage response)
        {
            string responseBody = "";
            try
            {
                responseBody = await response.Content.ReadAsStringAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return responseBody;
        }
	}
}