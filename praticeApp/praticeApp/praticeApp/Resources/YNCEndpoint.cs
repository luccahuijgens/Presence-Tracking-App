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
        public HttpClient client;
        protected String baseUrl;

        public YNCEndpoint(String APIBaseUrl, String authToken)
        {
            client = new HttpClient();
            baseUrl = APIBaseUrl.TrimEnd('\\', '/');
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
        }

        public async Task<YNCEndpointStatus> AddBeaconRegistration(String jsonBeacons)
        {

            var content = new StringContent(jsonBeacons, Encoding.UTF8, "application/json");

            //var task = Task.Factory.StartNew(() => { PerformPOSTRequest("/registration", content)} );
            HttpResponseMessage response = await PerformPOSTRequest("/registration", content);

            String responseContent = response.Content.ReadAsStringAsync().Result;
            YNCEndpointStatus status = CheckYNCResponse(response);

            if (status != YNCEndpointStatus.OK) return status;


            Debug.WriteLine(responseContent);
           

            return YNCEndpointStatus.OK;
        }



        private YNCEndpointStatus CheckYNCResponse(HttpResponseMessage response)
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
            //var uri = new Uri(baseUrl + "/registration");
            //var content = new StringContent(jsonBeacons, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;

            response = await client.PostAsync(uri, content);

            return response;
        }
    }
}
