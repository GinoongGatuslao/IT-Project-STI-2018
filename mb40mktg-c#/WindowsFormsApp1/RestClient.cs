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
        public bool login { get; set; }
        public string header { get; set; }
        public string tag { get; set; }

        public RestClient()
        {
            endPoint = string.Empty;
        }

        public string PostRequest()
        {
            string responseValue = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endPoint);
            request.Method = "POST";

            if (!login)
            {
                request.Headers.Add("Authorization", "Bearer " + Login.api_token);
            }

            if (request.Method == "POST" && postJSON != string.Empty)
            {
                request.ContentType = "application/json";
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
            
            return responseValue;
        }

        public string PutRequest()
        {
            string responseValue = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endPoint);
            request.Method = "PUT";

            if (!login)
            {
                request.Headers.Add("Authorization", "Bearer " + Login.api_token);
            }

            if (request.Method == "PUT" && postJSON != string.Empty)
            {
                request.ContentType = "application/json";
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

            return responseValue;
        }

        public string GetRequest()
        {
            string responseValue = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endPoint);
            request.Method = "GET";
            request.Headers.Add("Authorization", "Bearer " + Login.api_token);

            switch(tag)
            {
                case "daily":
                    request.Headers.Add("day", header);
                    break;
                case "weekly":
                    request.Headers.Add("week", header);
                    break;
                case "monthly":
                    request.Headers.Add("month", header);
                    break;
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
                            responseValue = reader.ReadToEnd();
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

            return responseValue;
        }
    }
}