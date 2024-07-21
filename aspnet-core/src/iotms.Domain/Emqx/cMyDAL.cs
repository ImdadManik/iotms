using MySql.Data.MySqlClient;
using System;
using Newtonsoft;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace iotms.Emqx
{
    public static class cMyDAL
    {
        static string connectionString = "server=pms-db003.fandaqah.com;port=3307;user=IOTPortal;database=IOTPortal;password=Fandaqah@2020";
     
        public static string GetDeviceSettings(string username)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    // Open the connection
                    connection.Open();

                    // Create a MySqlCommand object with the SQL statement and connection
                    using (MySqlCommand command = new MySqlCommand("SELECT JSON_OBJECT(id, id, name, Name, msg_data, msg_data, topics, topics, msg_type, msg_type) AS json_payload FROM IOTPortal.sensors_msgs WHERE Name = @name;", connection))
                    {
                        // Add parameter to the command
                        command.Parameters.AddWithValue("@name", username);

                        // Execute the command
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            // Check if any rows were returned
                            if (reader.HasRows)
                            {
                                // Read the rows and create a list of JSON strings
                                List<string> jsonList = new List<string>();
                                while (reader.Read())
                                {
                                    jsonList.Add(reader["json_payload"].ToString());
                                }

                                // Convert the list of JSON strings to a single JSON array
                                string jsonArray = "[" + string.Join(",", jsonList) + "]";

                                // Display the JSON array
                                Console.WriteLine(jsonArray);
                            }
                            else
                            {
                                Console.WriteLine("No data found for client_id 'imdad'.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return "";
        }
        /*
        public static string UpdateRetrieveDeviceSettings(string topics, heart_beat heart_Beat)
        {
            string[] _info = topics.Split('/');
            int rowsAffected = 0;
            string json_return = string.Empty;
            if (_info[2].Length > 0)
            {
                // Create a MySqlConnection object
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string qry = string.Empty;
                    try
                    {
                        // Open the connection
                        connection.Open();
                        qry = "UPDATE `AppDevices` SET `Connection`=@Connection WHERE `Name`=@Name;";

                        // Create a MySqlCommand object with the SQL statement and connection
                        using (MySqlCommand command = new MySqlCommand(qry, connection))
                        {
                            // Add parameters to the command
                            command.Parameters.AddWithValue("@Name", _info[2]);
                            command.Parameters.AddWithValue("@Connection", heart_Beat.IsConnected.Equals("connected") ? 1 : 0);

                            // Execute the command
                            rowsAffected = command.ExecuteNonQuery();

                            // Check if the insert was successful
                            Console.WriteLine(rowsAffected > 0 ? $"User {topics} Updated successfully." : "Failed to insert data.");

                            if (rowsAffected > 0)
                            {
                                qry = @"SELECT JSON_OBJECT('Id', d.Id, 'AccountId', d.AccountId, 'NAME', d.NAME, 'DeviceStatus', d.Status, 
'AccountStatus', a.Status, 'Temp', d.Temp, 'Door', d.Door, 'LDR', d.LDR, 'PIR', d.PIR, 'LDRAlertFreq', d.LDRAlertFreq,
'MinLDRAlert', d.MinLDRAlert, 'MinTempAlert', d.MinTempAlert, 
'TempAlertFreq', d.TempAlertFreq, 'CONNECTION', d.CONNECTION) AS json_payload 
FROM AppDevices d
INNER JOIN AppAccounts a ON a.Id = d.AccountId 
WHERE Name = @username ";
                                qry += heart_Beat.Id is null ? "" :  " AND Id = @Id  ";
                                qry += heart_Beat.AccountId is null ? "" :  " AND AccountId = @AccountId  ";
                                qry += "AND IsDeleted = 0  ";
                                qry += "AND deleterId IS NULL  ";
                                qry += "AND DeletionTime IS NULL  ";

                                // Create a MySqlCommand object with the SQL statement and connection
                                using (MySqlCommand command1 = new MySqlCommand(qry, connection))
                                {
                                    // Add parameter to the command
                                    command1.Parameters.AddWithValue("@username", _info[2]);
                                    command1.Parameters.AddWithValue("@Id", heart_Beat.Id);
                                    command1.Parameters.AddWithValue("@AccountId", heart_Beat.AccountId);

                                    //Execute the command
                                    using (MySqlDataReader reader = command1.ExecuteReader())
                                    {
                                        // Check if any rows were returned
                                        if (reader.HasRows)
                                        {
                                            while (reader.Read())
                                            {
                                                var data = reader["json_payload"].ToString();
                                                //SensorPayload sensorPayload = JsonConvert.DeserializeObject<SensorPayload>(data);
                                                //json_return = JsonConvert.SerializeObject(sensorPayload);
                                            }
                                            return json_return;
                                        }
                                        else
                                            Console.WriteLine($"No data found for client_id '{_info[2]}'.");
                                    }
                                }
                            }
                            return json_return;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                        return json_return;
                    }
                }
            }
            return json_return;
        }*/
    }
}