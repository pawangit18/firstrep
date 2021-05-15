using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Windows.Forms;

namespace TreeView.Class
{
    /// <summary>
    /// Handles the web requests to external system.
    /// </summary>
    public class APIWebRequest
    {
        #region"varibales"
        System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
        #endregion
        #region"Properties"
        private ServiceRequest ServiceRequest
        {
            get;
            set;
        }
        private string API_BaseUrl
        {
            get;
            set;
        }
        private string API_User
        {
            get;
            set;
        }
        private string API_Pwd
        {
            get;
            set;
        }
        private string Log_Path
        {
            get;
            set;
        }
        private bool RequestLogFlag
        {
            get;
            set;
        }
        private bool ResponseLogFlag
        {
            get;
            set;
        }
        private bool InvalidResponseLogFlag
        {
            get;
            set;
        }
        private string UseTokenService
        {
            get;
            set;
        }
        #endregion
        #region"Public Members"
        /// <summary>
        /// Call the external api
        /// </summary>
        public APIWebRequest(ServiceRequest _serviceReq)
        {
            try
            {
                ServiceRequest = _serviceReq;
                UseTokenService = ConfigurationManager.AppSettings["UseTokenService"].ToString();
                // API_User = ConfigurationManager.AppSettings["CMUApiUser"].ToString();
                // API_Pwd = ConfigurationManager.AppSettings["CMUApiPwd"].ToString();
                 API_BaseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"].ToString();
                // Log_Path = ConfigurationManager.AppSettings["ServiceLogPath"].ToString();
                //RequestLogFlag = Convert.ToBoolean(ConfigurationManager.AppSettings["LogRequest"]);
                // ResponseLogFlag = Convert.ToBoolean(ConfigurationManager.AppSettings["LogResponse"]);
                // InvalidResponseLogFlag = Convert.ToBoolean(ConfigurationManager.AppSettings["LogInvalidResponse"]);
            }
            catch (Exception ex)
            {
                WriteLogData("APIWebRequest()", ex);
            }
        }
        public string CallAPI()
        {
           //"ccadtestuser", "CCAD%$#9812"
           // url = API_BaseUrl + url;
           // LogMessage(url + Environment.NewLine + requestBody,LogType.REQUEST_PAYLOAD);           
            string apiResponse = string.Empty;            
            try
            {  
                //if(UseTokenService =="1")
                //{
                //    //string token = GetAPIToken();
                //}

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                WebRequest request = WebRequest.Create(ServiceRequest.Endpoint);
                //request.ContentType = "application/x-www-form-urlencoded";   
                request.ContentType = ServiceRequest.MediaType;
                foreach(ServiceHeader header in ServiceRequest.ServiceHeaders )
                {
                    request.Headers.Add(header.Name, header.Value);
                }
                if (UseTokenService == "1")
                {
                    request.Headers.Add("Authorization", "Bearer " + GetAPIToken());
                }
                //request.Headers.Add("Authorization", "Bearer " + token);
                request.Method = ServiceRequest.Method;
                // if (!string.IsNullOrEmpty(ServiceRequest.RequestData))
                if (request.Method != "GET")
                {
                    //request.ContentType = "application/json";
                    Byte[] byteArray = encoding.GetBytes(ServiceRequest.RequestData);
                    request.ContentLength = byteArray.Length;
                    using (Stream stream = request.GetRequestStream())
                    {
                        stream.Write(byteArray, 0, byteArray.Length);
                    }
                }
                WebResponse response = request.GetResponse();
                // this.Status = ((HttpWebResponse)response).StatusDescription;
                // Get the stream containing all content returned by the requested server.               
                Stream dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content fully up to the end.
                apiResponse = reader.ReadToEnd();               
                // Clean up the streams.
                reader.Close();
                dataStream.Close();
                response.Close();
                //if (method == WebRequestMethod.POST && response.Headers != null && string.IsNullOrEmpty(apiResponse) && response.Headers["location"]!=null)
                //{
                //    apiResponse = GetSuccessJson(response.Headers["location"]);
                //}
                //else if (method == WebRequestMethod.PUT && response.Headers != null && string.IsNullOrEmpty(apiResponse) && response.Headers["content-location"] != null)
                //{
                //    apiResponse = GetSuccessJson(response.Headers["content-location"]);
                //}
            }
            catch (WebException wex)
            {                 
                if (wex.Response != null)
                {
                    using (var errorResponse = (HttpWebResponse)wex.Response)
                    {
                        using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                        {
                            apiResponse = reader.ReadToEnd();
                           // {"resourceType":"OperationOutcome","issue":[{"severity":"error","code":"login","details":{"text":"not authenticated"}}]}
                        }
                    }
                }
                else
                {
                    apiResponse = wex.Message;
                    //WriteLogData("Web exception in CallAPI(" + url + "," + method.ToString() + ")", wex);
                }
            }
            catch (Exception ex)
            {
                apiResponse = ex.Message;
                //WriteLogData("Exception in CallAPI(" + url + "," + method.ToString() + ")", ex);
            }

            // throw response as exception if not a valid json.
            //if (!string.IsNullOrEmpty(apiResponse) && !ValidateJSON(apiResponse))
            //{
            //    WriteLogData("Invalid response received from inhealth.[" + url + "]", null);
            //    LogMessage(apiResponse, LogType.INVALID_RESPONSE);                
            //    apiResponse = GetErrorJson(ServiceMessage.INHEALTH_INVALID_RESPONSE);
            //}
            return apiResponse;
        }
        /// <summary>
        /// Create FHIR based json for successful post operations
        /// </summary>
        /// <param name="resourceString"></param>
        /// <returns></returns>
        private string GetSuccessJson(string resourceString)
        {
            List<string> strList = resourceString.Split('/').ToList();
            string resourceId = strList[strList.IndexOf("_history") - 1];
            return "{\"resourceType\":\"OperationOutcome\",\"issue\":[{\"severity\":\"information\",\"code\":\"informational\",\"details\":{\"text\":\"" + resourceId + "\"}}]}";
        }
        /// <summary>
        /// Create FHIR based json for errors
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public string GetErrorJson(string error)
        {
            if (error.Contains("OperationOutcome"))
            {
                return error;
            }
            return "{\"resourceType\":\"OperationOutcome\",\"issue\":[{\"severity\":\"error\",\"code\":\"exception\",\"details\":{\"text\":\"" + error + "\"}}]}";
        }
        #endregion
        #region"Private Members"
        /// <summary>
        /// Dump the request payload.
        /// </summary>
        /// <param name="requestData"></param>
        private void LogMessage(string content,LogType logType)
        {
            try
            {
                bool logEnabled = false;
                string fullPath = string.Empty;
                if (string.IsNullOrEmpty(Log_Path))
                {
                    return;
                }
                switch(logType)
                {
                    case LogType.REQUEST_PAYLOAD:
                        {
                            logEnabled = RequestLogFlag;
                            fullPath = Log_Path + "\\" + DateTime.Now.ToString("yyyyMMdd") +  "\\RequestPayload" ;
                            break;
                        }
                    case LogType.RESPONSE_PAYLOAD:
                        {
                            logEnabled = ResponseLogFlag;
                            fullPath = Log_Path + "\\" + DateTime.Now.ToString("yyyyMMdd") + "\\ServiceResponse";                            
                            break;
                        }
                    case LogType.INVALID_RESPONSE:
                        {
                            logEnabled = InvalidResponseLogFlag;
                            fullPath = Log_Path + "\\" +  DateTime.Now.ToString("yyyyMMdd") + "\\InvalidResponse";                            
                            break;
                        }
                }
                if (logEnabled)
                {
                    StreamWriter writer = null;
                    if (!Directory.Exists(fullPath))
                    {
                        Directory.CreateDirectory(fullPath);
                    }

                    string logFilePath = fullPath + "\\" + DateTime.Now.ToString("yyyyMMdd_HH-mm-ss-fff_") + Guid.NewGuid().ToString() + ".txt";
                    //File.WriteAllText(logFilePath, content);
                    writer = File.CreateText(logFilePath);
                    writer.WriteLine(content);
                    writer.Flush();
                    writer.Close();
                    writer = null;
                }
            }
            catch (Exception ex)
            {
                WriteLogData("LogMessage()", ex);
            }
        }
        /// <summary>
        /// Gets the token for external api
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        private string GetAPIToken()
        {            
            string token = string.Empty;
            //error = string.Empty;         
            try
            {
                //string url = //API_BaseUrl + "/cas/oidc/token";
                API_User = ConfigurationManager.AppSettings["TokenApiUser"].ToString();
                API_Pwd = ConfigurationManager.AppSettings["TokenApiPwd"].ToString();
                string url = API_BaseUrl + ConfigurationManager.AppSettings["TokenApiUrl"].ToString();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                WebRequest request = WebRequest.Create(url);
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = "POST";             

                request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes(API_User + ":" + API_Pwd)));
                string postParametersForToken = "grant_type=client_credentials";

                request.ContentType = @"application/x-www-form-urlencoded";
                Byte[] byteArray = encoding.GetBytes(postParametersForToken);
                request.ContentLength = byteArray.Length;
                using (Stream dataStream = request.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                }

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                        //return reader.ReadToEnd();
                        string result = reader.ReadToEnd();
                        //var converter = new ExpandoObjectConverter();
                        //dynamic message = JsonConvert.DeserializeObject<ExpandoObject>(result, converter);
                        Token tokenObj = JsonConvert.DeserializeObject<Token>(result);
                        token = tokenObj.access_token;
                    }
                }
            }
            catch (WebException wex)
            {
                //error = wex.Message;
                WriteLogData("Web exp in GetAPIToken()", wex);
                throw wex;
            }
            catch (Exception ex)
            {
                //error = ex.Message;
                WriteLogData("Exception in GetAPIToken()", ex);
                throw ex;
            }           
            return token;
        }
        /// <summary>
        /// Validate json using JToken
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private bool ValidateJSON(string s)
        {
            try
            {
                JToken.Parse(s);
                return true;
            }
            catch (Exception ex)
            {                
                return false;
            }
        }
        /// <summary>
        /// Write the log in file
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        private void WriteLogData(string message, Exception ex)
        {
            StreamWriter writer = null;
            try
            {
            //string logFilePath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + "\\logs.txt";
            string logPath =string.Empty ;
            //if (string.IsNullOrEmpty(Log_Path))
            //{
            //    logPath = ConfigurationManager.AppSettings["ServiceLogPath"].ToString() +  "\\ServiceLog";
            //}
            if (!string.IsNullOrEmpty(Log_Path))
            {
                logPath = Log_Path + "\\" + DateTime.Now.ToString("yyyyMMdd") + "\\ServiceLog";            
            }
            else
            {                
                logPath = Path.GetDirectoryName(Application.ExecutablePath) + "\\Log";   
            }
            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }
            string logFilePath = logPath + "\\log_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
           
            
                if (File.Exists(logFilePath))
                {
                    writer = File.AppendText(logFilePath);
                }
                else
                {
                    writer = File.CreateText(logFilePath);
                    writer.WriteLine("---------------------Service Logs-------------------");
                }

                if (ex == null)
                {

                    writer.WriteLine(DateTime.Now + "- INFO -" + message);
                }
                else
                {
                    writer.WriteLine(DateTime.Now + "- ERROR -" + message + ". Error info :- " + ex.Message + ".Inner message:-" + ex.InnerException + ".Stack trace:-" + ex.StackTrace);
                }
                writer.Flush();
                if (writer != null)
                {
                    writer.Close();
                    writer = null;
                }
            }
            catch (SecurityException se)
            {
                if (writer != null)
                {
                    writer.Close();
                    writer = null;
                }
            }
            catch (Exception ex1)
            {
                if (writer != null)
                {
                    writer.Close();
                    writer = null;
                }
            }
        }
        #endregion
    }    
}