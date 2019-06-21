using Newtonsoft.Json;
using praticeApp.DataAccess;
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
        ConnectionFailed,
        NoYNCBeaconsFound,
        OK
    }

    class YNCEndpoint
    {
        protected HttpClient client;
        protected String baseUrl;
        protected bool _verboseMode;
        private String _authToken;

        public YNCEndpoint(String APIBaseUrl, bool verboseMode = false, String authToken = null)
        {
            _verboseMode = verboseMode;

            // Create a new http client.
            client = new HttpClient();
            baseUrl = APIBaseUrl.TrimEnd('\\', '/');

            // API only accepts application/json
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (authToken == null)
            {
                // If access token has not been set manually, try to do it automatically.
                InjectAuthToken();
            } else
            {
                // Set API access token.
                SetAccessToken(authToken);
            }
                
        }

        public void SetAccessToken(String authToken)
        {
            _authToken = authToken;

            // Set auth header
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authToken);
        }

        public async Task<YNCEndpointStatus> AddBeaconRegistration(String jsonBeacons)
        {
            HttpResponseMessage response;

            try
            {
                response = await PerformPOSTRequestJSONWithToken("/registration", jsonBeacons);

                String responseContent = response.Content.ReadAsStringAsync().Result;
                YNCEndpointStatus status = CheckAuthFailure(response);

                if (_verboseMode)
                    Debug.WriteLine("JSON request: " + jsonBeacons);
                    Debug.WriteLine("Token: " + _authToken);
                    Debug.WriteLine(responseContent);

                if (status != YNCEndpointStatus.OK) return status;

                return YNCEndpointStatus.OK;
            } catch (Exception ex)
            {
                if (_verboseMode)
                    Debug.WriteLine(ex.Message);
              

                return YNCEndpointStatus.UnexpectedHttpError;
            }
        }

        protected YNCEndpointStatus CheckAuthFailure(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                    return YNCEndpointStatus.AuthFailed;

                if (response.StatusCode == (System.Net.HttpStatusCode) 422)
                    return YNCEndpointStatus.NoYNCBeaconsFound;

                return YNCEndpointStatus.UnexpectedHttpError;
            }

            return YNCEndpointStatus.OK;
        }

        protected bool InjectAuthToken()
        {
            if (_authToken != null)
                return true;

            String token = new String(new char[] { });

            if ((new ConfigLoader()).LoadConfigTokenOutFile(ref token))
            {
                SetAccessToken(token);

                return true;
            }

            return false;
        }

        protected async Task<HttpResponseMessage> PerformPOSTRequestWithToken(String endpoint, StringContent content)
        {
            Uri uri = new Uri(baseUrl + endpoint);
            client.CancelPendingRequests();

            return await client.PostAsync(uri, content);
        }

        protected async Task<HttpResponseMessage> PerformPOSTRequestJSONWithToken(String endpoint, String json)
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

        public bool IsAppRegistered()
        {
            return (_authToken != null && _authToken.Length > 0);
        }
    }
}
