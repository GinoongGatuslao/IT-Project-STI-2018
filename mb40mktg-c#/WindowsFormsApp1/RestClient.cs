using System;
using System.IO;
using System.Net;
using System.Text;

namespace WindowsFormsApp1
{
    class RestClient
    {
        public string endPoint { get; set; }
        public string postJSON { get; set; }
        //public string username { get; set; }
        //public string password { get; set; }

        public RestClient()
        {
            endPoint = string.Empty;
        }

        public string LoginRequest()
        {
            string responseValue = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endPoint);
            request.Method = "POST"; //change in the future

            //String authHeader = System.Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(username + ":" + password));
            //request.Headers.Add("Authorizationasic " + authHeader);

            if (request.Method == "POST" && postJSON != string.Empty)
            {
                request.ContentType = "application/json"; //Really Important
                using (StreamWriter swJSONPayload = new StreamWriter(request.GetRequestStream()))
                {
                    swJSONPayload.Write(postJSON);
                    swJSONPayload.Close();
                }
            }

            HttpWebResponse response = null;

            try
            {
                response = (HttpWebResponse)request.GetResponse();

                using (Stream responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            responseValue = response.StatusCode.ToString() + "|" + reader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                responseValue = "{\"errorMessages\":[\"" + ex.Message.ToString() + "\"],\"errors\":{}}";
            }
            finally
            {
                if (response != null)
                {
                    ((IDisposable)response).Dispose();
                }
            }

            /**using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new ApplicationException("error code: " + response.StatusCode.ToString());
                }
                //process the response string
                using (Stream responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        using(StreamReader reader = new StreamReader(responseStream))
                        {
                            responseValue = reader.ReadToEnd();
                        }
                    }
                }
            }**/

            return responseValue;
        }
    }
}