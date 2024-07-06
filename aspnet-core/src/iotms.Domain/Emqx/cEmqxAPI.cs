using iotms.Devices;
using Newtonsoft.Json;
using RestSharp;
using System;
 

namespace iotms.Emqx_UserAuth
{
    public class cEmqxAPI
    {
        const string emqx_url = "http://pms-db003.fandaqah.com:18083"; // "http://192.168.1.24:18083";
        const string emqx_authenticator_id = "password_based:built_in_database";
        const string user_url = "api/v5/authentication/" + emqx_authenticator_id + "/users/";
        const string client_url = "api/v5/clients/";
        const string content_type = "application/json";
        const string authorization_token = "Basic YTA1ZmI5MzNmMGQ1NjQ2MToyNGc5QTlBSlVzakZGcVc2QWdlOUNoaVBaeFR1MUo2b25KTmtFcFBRMWp2ZzZK";

        public RestResponse AddAuthUsers(string username, string pwd, bool is_superuser)
        {
            var options = new RestClientOptions(emqx_url)
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest(user_url, Method.Post);
            request.AddHeader("Content-Type", content_type);
            request.AddHeader("Authorization", authorization_token);
            cEmqxUsers oEmqxusr = new cEmqxUsers();

            oEmqxusr.user_id = username;
            oEmqxusr.password = pwd;
            oEmqxusr.is_superuser = is_superuser;
            var body = JsonConvert.SerializeObject(oEmqxusr, Formatting.Indented);
            request.AddStringBody(body, DataFormat.Json);
            RestResponse response = null;
            try
            {
                response = client.Execute(request);
                string json = JsonConvert.SerializeObject(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return response;
        }

        public RestResponse DeleteUsers(Device input)
        {
            var resp = GetUserById(input.Name);
            if (resp.StatusDescription == "Not Found") return resp;

            var respClient = KickOutClientById(input.Name);


            var options = new RestClientOptions(emqx_url)
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest(user_url + input.Name, Method.Delete);
            request.AddHeader("Accept", content_type);
            request.AddHeader("Authorization", authorization_token);
            RestResponse response = client.Execute(request);
            return response;
        }

        private RestResponse GetUserById(string username)
        {
            var options = new RestClientOptions(emqx_url)
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest(user_url + username, Method.Get);
            request.AddHeader("Accept", content_type);
            request.AddHeader("Authorization", authorization_token);
            RestResponse response = client.Execute(request);
            return response;
        }

        public RestResponse KickOutClientById(string ClientId)
        {
            var options = new RestClientOptions(emqx_url)
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest(client_url + ClientId, Method.Delete);
            request.AddHeader("Accept", content_type);
            request.AddHeader("Authorization", authorization_token);
            RestResponse response = client.Execute(request);
            return response;
        }
    }
}
