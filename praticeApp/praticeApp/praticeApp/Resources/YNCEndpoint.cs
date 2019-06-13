using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace praticeApp.Resources
{

    enum YNCEndpointStatus {
        UnexpectedHttpError,
        AlreadyRegistered,
        AuthFailed,
        OK
    }

    class YNCEndpoint
    {
        protected HttpClient client;
        protected String baseUrl;
        protected bool _verboseMode;

        public YNCEndpoint(String APIBaseUrl, String authToken, bool verboseMode = false)
        {
            _verboseMode = verboseMode;

            client = new HttpClient();
            baseUrl = APIBaseUrl.TrimEnd('\\', '/');
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
        }

        public void SetAccessToken(String authToken)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
        }

        public async Task<YNCEndpointStatus> AddBeaconRegistration(String jsonBeacons)
        {
            HttpResponseMessage response = await PerformPOSTRequestJSON("/registration", jsonBeacons);

            String responseContent = response.Content.ReadAsStringAsync().Result;
            YNCEndpointStatus status = CheckAuthFailure(response);

            if (_verboseMode)
                Debug.WriteLine(responseContent);

            if (status != YNCEndpointStatus.OK) return status;
           
            return YNCEndpointStatus.OK;
        }

        protected YNCEndpointStatus CheckAuthFailure(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    return YNCEndpointStatus.AuthFailed;
                }

                return YNCEndpointStatus.UnexpectedHttpError;
            }

            return YNCEndpointStatus.OK;
        }

        protected async Task<HttpResponseMessage> PerformPOSTRequest(String endpoint, StringContent content)
        {
            Uri uri = new Uri(baseUrl + endpoint);
            client.CancelPendingRequests();

            return await client.PostAsync(uri, content);
        }

        protected async Task<HttpResponseMessage> PerformPOSTRequestJSON(String endpoint, String json)
        {
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            Uri uri = new Uri(baseUrl + endpoint);
            client.CancelPendingRequests();

            return await client.PostAsync(uri, content);
        }

        public String GetBaseURL()
        {
            return baseUrl;
        }
    }
}
