using HPRBackend.Modules.Paramettres.GestionDesAgents.Models;
using HPRBackend.Modules.shard;
using Microsoft.EntityFrameworkCore;

namespace HPRBackend.Modules.Paramettres.GestionDesAgents.DAL
{
    public class DAL_Agent
    {
        private readonly DataBaseContext DataBaseContext;

        public DAL_Agent(DataBaseContext _DataBaseContext)
        {
            DataBaseContext = _DataBaseContext;
        }
        /// <summary>
        /// AOUTER UN Agent
        /// </summary>
        /// <param name="Agent"></param>
        /// <returns></returns>
        public async Task<Message> ADD(Agent Agent)
        {
            try
            {


                await DataBaseContext.Agent.AddAsync(Agent);
                await DataBaseContext.SaveChangesAsync();


                return new Message(true, " Agent Ajouter avec succés");

            }
            catch (DbUpdateException e)
            {

                if (e.InnerException.Message.Contains("Violation of UNIQUE KEY constraint 'Uk_Matricule'"))
                {
                    return new Message(false, " le Matricule existe");


                }
                if (e.InnerException.Message.Contains("Violation of UNIQUE KEY constraint 'Uk_Email'"))
                {
                    return new Message(false, " l'adresse email existe");


                }
                if (e.InnerException.Message.Contains("Violation of UNIQUE KEY constraint 'Uk_Contacte'"))
                {
                    return new Message(false, " le numero de telephone existe");


                }
                return new Message(false, e.Message);
            }

        }
        /// <summary>
        /// liste des Agents
        /// </summary>
        /// <returns></returns>
        public async Task<List<Agent>> GetAll()
        {
            //await Migrations.Migrations.create_table_Affectation();
            //await Migrations.Migrations.create_table_Service();
            await Migrations.create_table_Agent();
            return await DataBaseContext.Agent.ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        public async Task<List<Agent>> GetAllMedecin()
        {
            //await Migrations.Migrations.create_table_Affectation();
            //await Migrations.Migrations.create_table_Service();
            return await DataBaseContext.Agent.Where(p=>p.Status== "Medecin").ToListAsync();
        }
        /// <summary>
        /// retour le Agent selon Id du Agent 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<Agent?> GetById(long Id)
        {
            var Agent = await DataBaseContext.Agent.FirstOrDefaultAsync(m => m.Id == Id);
            if (Agent != null)
                return Agent;
            return null;
        }




        /// <summary>
        /// modifier un Agent
        /// </summary>
        /// <param name="Agent"></param>
        /// <returns></returns>
        public async Task<Message> Update(Agent Agent)
        {
            try
            {
                DataBaseContext.Agent.Update(Agent);
                await DataBaseContext.SaveChangesAsync();
                return new Message(true, " Agent modifier avec succes");
            }
            catch (DbUpdateException e)
            {

                if (e.InnerException.Message.Contains("Violation of UNIQUE KEY constraint 'Uk_Matricule'"))
                {
                    return new Message(false, " le Matricule existe");


                }
                if (e.InnerException.Message.Contains("Violation of UNIQUE KEY constraint 'Uk_Email'"))
                {
                    return new Message(false, " l'adresse email existe");


                }
                if (e.InnerException.Message.Contains("Violation of UNIQUE KEY constraint 'Uk_Contacte'"))
                {
                    return new Message(false, " le numero de telephone existe");


                }
                return new Message(false, " erreur " + e.Message);
            }
        }

        /// <summary>
        /// suppression de Agent selon un Id 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>

        public async Task<Message> DELETEbYId(long Id)
        {
            var Agent = await DataBaseContext.Agent.FirstOrDefaultAsync(m => m.Id == Id);
            if (Agent != null)
            {
                DataBaseContext.Agent.Remove(Agent);
                await DataBaseContext.SaveChangesAsync();
                return new Message(true, "Agent supprimer avec succés");
            }
            return new Message(false, "erreur de suppression de Agent ");


        }
        /// <summary>
        /// renvoie les données statistique sur le Agents 
        /// </summary>
        /// <returns></returns>


        public Dictionary<string, long> Counts()
        {
            var keyValuePairs = new Dictionary<string, long>();

            keyValuePairs.Add("Total", DataBaseContext.Agent.ToList().Count());

            keyValuePairs.Add("Homme", DataBaseContext.Agent.Where(p => p.Sexe == "Homme").Count());
            keyValuePairs.Add("Femme", DataBaseContext.Agent.Where(p => p.Sexe == "Femme").Count());
            keyValuePairs.Add("mariee", DataBaseContext.Agent.Where(p => p.Situation_matri == "mariée").Count());
            keyValuePairs.Add("celibataire", DataBaseContext.Agent.Where(p => p.Situation_matri == "célibataire").Count());
            keyValuePairs.Add("veuve", DataBaseContext.Agent.Where(p => p.Situation_matri == "veuve").Count());
            keyValuePairs.Add("divorcee", DataBaseContext.Agent.Where(p => p.Situation_matri == "divorcée").Count());


            return keyValuePairs;




        }
    }
}
