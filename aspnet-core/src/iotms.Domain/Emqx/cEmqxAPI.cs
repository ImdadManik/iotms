using iotms.Devices;
using iotms.Emqx;
using Newtonsoft.Json;
using RestSharp;
using System;


namespace iotms.Emqx_UserAuth
{
    public static class cEmqxAPI
    {
        private static readonly int port = 18083;
        private static readonly string network_url = "pms-db003.fandaqah.com";
        private static readonly string emqx_url = $"http://{network_url}:{port}"; // "http://192.168.1.24:18083";
        private static readonly string emqx_authenticator_id = "password_based:built_in_database";
        private static readonly string user_url = "api/v5/authentication/" + emqx_authenticator_id + "/users/";
        private static readonly string client_url = "api/v5/clients/";
        private static readonly string delete_retain_msgs_by_topic = "/api/v5/mqtt/retainer/message/";
        private static readonly string content_type = "application/json";
        private static readonly string authorization_token = "Basic YTA1ZmI5MzNmMGQ1NjQ2MToyNGc5QTlBSlVzakZGcVc2QWdlOUNoaVBaeFR1MUo2b25KTmtFcFBRMWp2ZzZK";

        public static RestResponse AddAuthUsers(string username, string pwd, bool is_superuser)
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

        public static RestResponse DeleteUsers(Device input)
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

        private static RestResponse GetUserById(string username)
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

        public static RestResponse KickOutClientById(string username)
        {
            var resp = DeleteRetainMsgsByTopics(username);
            RestResponse? response = null;
            if (resp.StatusDescription == "Ok")
            {
                var options = new RestClientOptions(emqx_url)
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest(client_url + username, Method.Delete);
                request.AddHeader("Accept", content_type);
                request.AddHeader("Authorization", authorization_token);
                response = client.Execute(request);
            }
            return response;
        }

        public static RestResponse DeleteRetainMsgsByTopics(string username)
        {
            string[] retainMsgTopics = ["device/", "device/connected"];
            RestResponse? response = null;
            foreach (string topic in retainMsgTopics)
            {
                response = null;
                var options = new RestClientOptions(emqx_url)
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest(delete_retain_msgs_by_topic + topic + username, Method.Delete);
                request.AddHeader("Accept", content_type);
                request.AddHeader("Authorization", authorization_token);
                response = client.Execute(request);
            }
            return response;
        }
    }
}
