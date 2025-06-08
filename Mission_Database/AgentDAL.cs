using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Mission_Database
{
    public class AgentDAL
    {
        private string connStr = "server=localhost;user=root;password=;database=eagleeyedb";

        public void AddAgent(Agent agent)
        {
            try
            {
                using (var conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    Console.WriteLine("Connection successful.");
                    string query = "INSERT INTO agents (codeName, realName, location, status, missionsCompleted) " +
                    "VALUES (@codeName, @realName, @location, @status, @missionsCompleted)";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@codeName", agent.CodeName);
                        cmd.Parameters.AddWithValue("@realName", agent.RealName);
                        cmd.Parameters.AddWithValue("@location", agent.Location);
                        cmd.Parameters.AddWithValue("@status", agent.Status);
                        cmd.Parameters.AddWithValue("@missionsCompleted", agent.MissionsCompleted);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

        }
        public List<Agent> GetAllAgents()
        {
            List<Agent> agentList = new List<Agent>();
            string query = "SELECT * FROM agents;";

            try
            {
                using (var conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    Console.WriteLine("Connection successful.");
                    using (var cmd = new MySqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            int agentid = reader.GetInt32("id");
                            string codeName = reader.GetString("codeName");
                            string realName = reader.GetString("realName");
                            string location = reader.GetString("location");
                            string status = reader.GetString("status");
                            int missionsCompleted = reader.GetInt32("missionsCompleted");

                            Agent agent = new Agent(agentid, codeName, realName, location, status, missionsCompleted);
                            agentList.Add(agent);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while fetching agents: {ex.Message}");
            }

            return agentList;
        }
        public void UpdateAgentLocation(int agentId, string newLocation)
        {   
            try
            {
                using (var conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    Console.WriteLine("Connection successful.");
                    string query = "UPDATE agents SET location = @newLocation WHERE id = @agentId";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@newLocation", newLocation);
                        cmd.Parameters.AddWithValue("@agentId", agentId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        public void DeleteAgent(int agentId)
        {
            try
            {
                using (var conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    Console.WriteLine("Connection successful.");
                    string query = "DELETE FROM agents WHERE id = @agentId";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@agentId", agentId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

}
