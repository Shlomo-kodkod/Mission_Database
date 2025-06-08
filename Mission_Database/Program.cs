using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mission_Database
{
    public class Program
    {
       public static void Main()
        {
            Agent newAgent1 = new Agent("101", "dan", "home", "Active", 3);
            Agent newAgent2 = new Agent("202", "isreal", "car", "Retired", 1);
            AgentDAL agentDal = new AgentDAL();
            agentDal.AddAgent(newAgent1);
            agentDal.AddAgent(newAgent2);
            List<Agent> ageList = agentDal.GetAllAgents();
            agentDal.DeleteAgent(ageList[0].Id);



        }

    }
}
