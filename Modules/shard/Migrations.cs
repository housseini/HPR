using Microsoft.Data.SqlClient;

namespace HPRBackend.Modules.shard
{
    public class Migrations
    {
        private static SqlConnection? connection = null;


        public static async Task<Message> create_table_Patient()
        {
            try
            {
                string query = "If not exists (select * from sysobjects where name = 'Patient')" +
                    " CREATE TABLE [dbo].[Patient] " +
                    "([Id]  BIGINT IDENTITY(1, 1) NOT NULL," +
                    "[Nom] VARCHAR(200) NOT NULL," +
                    "[Age] BIGINT  NOT NULL," +
                    "[Prenom]  NVARCHAR(MAX) NOT NULL," +
                    "[DateNaissance]   NVARCHAR(200) NOT NULL," +
                    "[LieuNaissance]    NVARCHAR(200) NOT NULL," +
                    "[TypeCarte]   NVARCHAR(50)  NOT NULL," +
                    "[NumeroCarte]  NVARCHAR(20)  NOT NULL," +
                    "[Nationnalite]  NVARCHAR(MAX) NOT NULL," +
                    "[Ville]  NVARCHAR(50) NULL," +
                    "[Adresse]  NVARCHAR(200)  NULL," +
                    "[Telephone]  NVARCHAR(50) NOT NULL," +
                    "[Sexe]  NVARCHAR(200) NOT NULL ," +
                    "[Ethnie]  NVARCHAR(200) NOT NULL ," +
                    "[RegiondOrigine]  NVARCHAR(200) NOT NULL ," +
                    "[ReferencePatient]  NVARCHAR(200) NOT NULL ," +
                    "[Date_Ajout]       DATETIME      DEFAULT (getdate()) NOT NULL," +
                    "PRIMARY KEY CLUSTERED([Id] ASC));" +
                    "ALTER TABLE [dbo].[Patient] ADD CONSTRAINT Uk_NumeroCarte_Patient UNIQUE (NumeroCarte); " +
                    "ALTER TABLE [dbo].[Patient] ADD CONSTRAINT Uk_Telephone_Patient UNIQUE (Telephone);";
                  //  +

                  //" CREATE   TRIGGER ReferencePatient_Reference" +
                  //"ON dbo.Patient" +
                  //"AFTER INSERT" +
                  //"AS" +
                  //"BEGIN" +
                  //"UPDATE dbo.Patient" +
                  //" SET ReferencePatient = CONCAT('P000', inserted.Id)" +
                  //"FROM dbo.Patient" +
                  //" INNER JOIN inserted ON Patient.Id = inserted.Id" +
                  //"END;"+
                  // " CREATE   TRIGGER ReferencePatient_Reference1" +
                  //"ON dbo.Patient" +
                  //"AFTER UPDATE" +
                  //"AS" +
                  //"BEGIN" +
                  //"UPDATE dbo.Patient" +
                  //" SET ReferencePatient = CONCAT('P000', inserted.Id)" +
                  //"FROM dbo.Patient" +
                  //" INNER JOIN inserted ON Patient.Id = inserted.Id" +
                  //"END;";


                connection = Dbconnection.GetConnection();
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand(query, connection);
                await cmd.ExecuteNonQueryAsync();

                return new Message(true, "Table Patient  create");
            }
            catch (Exception e)
            {
                return new Message(false, e.Message);
            }
            finally
            {
                connection.Close();
            }


        }

        public static async Task<Message> create_table_PriseEncharge()
        {
            try
            {
                string query = "If not exists (select * from sysobjects where name = 'PriseEnCharge')" +
                    " CREATE TABLE [dbo].[PriseEnCharge] " +
                    "([Id]  BIGINT IDENTITY(1, 1) NOT NULL," +
                    "[PatientID]  BIGINT NOT NULL  ," +
                    "[NumeroPriseEnCharge]    NVARCHAR(100) NOT NULL ," +
                    "[CompagnieID]  BIGINT NOT   NULL ," +
                    "[LienParente]   NVARCHAR(200)   NOT  NULL ," +
                    "[Pourcentage]    BIGINT NOT   NULL ," +
                    "[Path]    NVARCHAR(200)    NULL ," +
                    "[DateExpiration]         NVARCHAR(200)   NOT NULL," +
                     
                    "PRIMARY KEY CLUSTERED([Id] ASC));" +
                    "ALTER TABLE [dbo].[PriseEnCharge] ADD  CONSTRAINT fk_Patient_PriseEncharge FOREIGN KEY  ([PatientId])  REFERENCES [dbo].[Patient] ([Id]) ;";
                connection = Dbconnection.GetConnection();
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand(query, connection);
                await cmd.ExecuteNonQueryAsync();

                return new Message(true, "Table PriseEncharge  create");
            }
            catch (Exception e)
            {
                return new Message(false, e.Message);
            }
            finally
            {
                connection.Close();
            }


        }

        public static async Task<Message> create_table_ServicesType()
        {
            try
            {
                string query = "If not exists (select * from sysobjects where name = 'ServicesType')" +
                    " CREATE TABLE [dbo].[ServicesType] " +
                    "([Id]  BIGINT IDENTITY(1, 1) NOT NULL," +

                    "[Type]   NVARCHAR(200) NOT NULL," +

                    "[Date_Ajout]       DATETIME      DEFAULT (getdate()) NOT NULL," +
                    "PRIMARY KEY CLUSTERED([Id] ASC));" +
                    "ALTER TABLE [dbo].[ServicesType] ADD CONSTRAINT Uk_Code UNIQUE (Nom); " +
                    "ALTER TABLE [dbo].[ServicesType] ADD CONSTRAINT Uk_Type UNIQUE (Type); ";
                connection = Dbconnection.GetConnection();
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand(query, connection);
                await cmd.ExecuteNonQueryAsync();

                return new Message(true, "Table create_table_ServicesType  create");
            }
            catch (Exception e)
            {
                return new Message(false, e.Message);
            }
            finally
            {
                connection.Close();
            }


        }
        public static async Task<Message> create_table_Agent()
        {
            try
            {
                string query = "If not exists (select * from sysobjects where name = 'Agent')" +
                    " CREATE TABLE [dbo].[Agent] " +
                    "([Id]  BIGINT IDENTITY(1, 1) NOT NULL," +
                    "[Nom] VARCHAR(200) NOT NULL," +
                    "[Prenom]  NVARCHAR(MAX) NOT NULL," +
                    "[Matricule]   NVARCHAR(200) NOT NULL," +
                    "[Email]    NVARCHAR(200) NOT NULL," +
                    "[Contacte]   NVARCHAR(50)  NOT NULL," +
                    "[Status]   NVARCHAR(50)  NOT NULL," +
                    "[DateNaissance]  NVARCHAR(20)  NOT NULL," +
                    "[Adresse]  NVARCHAR(MAX) NOT NULL," +
                    "[LieuNaissance]  NVARCHAR(50) NULL," +
                    "[Sexe]  NVARCHAR(20) NOT NULL," +
                    "[Ville]  NVARCHAR(50) NOT NULL," +
                    "[CodePostal]  NVARCHAR(10)  NULL  ," +
                    "[Situation_matri]  NVARCHAR(200) NOT NULL ," +
                    "[Date_Ajout]       DATETIME      DEFAULT (getdate()) NOT NULL," +
                    "PRIMARY KEY CLUSTERED([Id] ASC));" +
                    "ALTER TABLE [dbo].[Agent] ADD CONSTRAINT Uk_Matricule UNIQUE (Matricule); " +
                    "ALTER TABLE [dbo].[Agent] ADD CONSTRAINT Uk_Email UNIQUE (Email); " +
                    "ALTER TABLE [dbo].[Agent] ADD CONSTRAINT Uk_Contacte UNIQUE (Contacte);";
                connection = Dbconnection.GetConnection();
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand(query, connection);
                await cmd.ExecuteNonQueryAsync();

                return new Message(true, "Table Agent  create");
            }
            catch (Exception e)
            {
                return new Message(false, e.Message);
            }
            finally
            {
                connection.Close();
            }


        }

        public static async Task<Message> create_table_Service()
        {
            try
            {
                string query = "If not exists (select * from sysobjects where name = 'Service')" +
                    " CREATE TABLE [dbo].[Service] " +
                    "([Id]  BIGINT IDENTITY(1, 1) NOT NULL," +
                    "[Nom] VARCHAR(200) NOT NULL," +
                    "[Emplacement] VARCHAR(	MAX)  NULL," +
                    "[NombreEtage]  BIGINT NOT NULL," +
                    "[MedecinId]  BIGINT NOT NULL," +
                    "[Type]   NVARCHAR(200) NOT NULL," +
                    "[Date_Ajout]       DATETIME      DEFAULT (getdate()) NOT NULL," +
                    "PRIMARY KEY CLUSTERED([Id] ASC));" +
                    "ALTER TABLE [dbo].[Service] ADD CONSTRAINT Uk_Service_Nom UNIQUE (Nom); " +
                    "ALTER TABLE [dbo].[Service] ADD CONSTRAINT Uk_Service_MedecinId UNIQUE (MedecinId); " +
                    "ALTER TABLE [dbo].[Service] ADD  CONSTRAINT [fk_Service_servicetype_type] FOREIGN KEY([Type])  REFERENCES [dbo].[ServicesType] ([Type])  ON UPDATE CASCADE";
                connection = Dbconnection.GetConnection();
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand(query, connection);
                await cmd.ExecuteNonQueryAsync();

                return new Message(true, "Table Service  create");
            }
            catch (Exception e)
            {
                return new Message(false, e.Message);
            }
            finally
            {
                connection.Close();
            }


        }
        public static async Task<Message> create_table_ServiceEtage()
        {
            try
            {
                string query = "If not exists (select * from sysobjects where name = 'ServiceEtage')" +
                    " CREATE TABLE [dbo].[ServiceEtage] " +
                    "([Id]  BIGINT IDENTITY(1, 1) NOT NULL," +
                    "[NumeroEtage]  BIGINT NOT NULL," +
                    "[NombreChambre]  BIGINT NOT NULL," +
                    "[Nombrelit]  BIGINT NOT NULL," +
                    "[ServiceId]  BIGINT NOT NULL," +

                    "[Date_Ajout]       DATETIME      DEFAULT (getdate()) NOT NULL," +
                    "PRIMARY KEY CLUSTERED([Id] ASC));" +
                    "ALTER TABLE [dbo].[ServiceEtage] ADD CONSTRAINT fk_Service_Etage FOREIGN KEY   (ServiceId) REFERENCES  [dbo].[Service] ([Id])   ON DELETE CASCADE; ";

                connection = Dbconnection.GetConnection();
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand(query, connection);
                await cmd.ExecuteNonQueryAsync();

                return new Message(true, "Table ServiceEtage  create");
            }
            catch (Exception e)
            {
                return new Message(false, e.Message);
            }
            finally
            {
                connection.Close();
            }


        }
        public static async Task<Message> create_table_TypeChambre()
        {
            try
            {
                string query = "If not exists (select * from sysobjects where name = 'TypeChambre')" +
                    " CREATE TABLE [dbo].[TypeChambre] " +
                    "([Id]  BIGINT IDENTITY(1, 1) NOT NULL," +
                    "[Nom]  VARCHAR(200)  NOT NULL," +
                    "[PrixParNuit]     BIGINT NOT NULL," +
                    "PRIMARY KEY CLUSTERED([Id] ASC));" +
                    "ALTER TABLE [dbo].[TypeChambre] ADD CONSTRAINT Uk_NomTYPEcHAMBRE UNIQUE (Nom); ";
                ;

                connection = Dbconnection.GetConnection();
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand(query, connection);
                await cmd.ExecuteNonQueryAsync();

                return new Message(true, "Table TypeChambre  create");
            }
            catch (Exception e)
            {
                return new Message(false, e.Message);
            }
            finally
            {
                connection.Close();
            }


        }
        public static async Task<Message> create_table_PrixChambre()
        {
            try
            {
                string query = "If not exists (select * from sysobjects where name = 'PrixChambre')" +
                    " CREATE TABLE [dbo].[PrixChambre] " +
                    "([Id]  BIGINT IDENTITY(1, 1) NOT NULL," +
                    "[ChambreId]   BIGINT NOT NULL," +
                    "[PrixJours]     BIGINT NOT NULL," +
                    "PRIMARY KEY CLUSTERED([Id] ASC));" +
                  "ALTER TABLE [dbo].[PrixChambre] ADD CONSTRAINT fk_chambre_Prix FOREIGN KEY   (ChambreId) REFERENCES  [dbo].[chambre] ([Id])   ON DELETE CASCADE; ";
                ;

                connection = Dbconnection.GetConnection();
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand(query, connection);
                await cmd.ExecuteNonQueryAsync();

                return new Message(true, "Table PrixChambre  create");
            }
            catch (Exception e)
            {
                return new Message(false, e.Message);
            }
            finally
            {
                connection.Close();
            }


        }


        public static async Task<Message> create_table_Chambre()
        {
            try
            {
                string query = "If not exists (select * from sysobjects where name = 'Chambre')" +
                    " CREATE TABLE [dbo].[Chambre] " +
                    "([Id]  BIGINT IDENTITY(1, 1) NOT NULL," +
                    "[EtageServiceId]  BIGINT NOT NULL," +
                    "[TypeChambreId]  BIGINT NOT NULL," +
                    "[NumeroChambre]  BIGINT NOT NULL," +
                    "[NumeroLit]  BIGINT NOT NULL," +
                    "[Etat]   bit NOT NULL DEFAULT(1)," +
                    "[Status]  bit NOT NULL DEFAULT(0)," +


                    "PRIMARY KEY CLUSTERED([Id] ASC));" +
                    "ALTER TABLE [dbo].[Chambre] ADD CONSTRAINT  fk_Service_Etage_Chambre  FOREIGN KEY   (EtageServiceId) REFERENCES  [dbo].[ServiceEtage]([Id])   ON DELETE CASCADE; " +
                "ALTER TABLE [dbo].[Chambre] ADD CONSTRAINT  fk__chambre_TypeChambre FOREIGN KEY   (TypeChambreId) REFERENCES  [dbo].[TypeChambre]([Id])   ON DELETE CASCADE; ";

                connection = Dbconnection.GetConnection();
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand(query, connection);
                await cmd.ExecuteNonQueryAsync();

                return new Message(true, "Table chambre  create");
            }
            catch (Exception e)
            {
                return new Message(false, e.Message);
            }
            finally
            {
                connection.Close();
            }


        }

        public static async Task<Message> create_table_Assurance()
        {
            try
            {
                string query = "If not exists (select * from sysobjects where name = 'Assurance')" +
                    " CREATE TABLE [dbo].[Assurance] " +
                    "([Id]  BIGINT IDENTITY(1, 1) NOT NULL," +
                    "[Nom] NVARCHAR(250)  NOT  NULL," +
                    "[Date_Ajout]       DATETIME      DEFAULT (getdate()) NOT NULL," +
                    "PRIMARY KEY CLUSTERED([Id] ASC));" +
                     "ALTER TABLE [dbo].[Assurance] ADD CONSTRAINT Uk_Assurance_Nom UNIQUE (Nom); ";
                connection = Dbconnection.GetConnection();
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand(query, connection);
                await cmd.ExecuteNonQueryAsync();

                return new Message(true, "Table Assurance  create");
            }
            catch (Exception e)
            {
                return new Message(false, e.Message);
            }
            finally
            {
                connection.Close();
            }


        }

        public static async Task<Message> create_table_MotifAdmission()
        {
            try
            {
                string query = "If not exists (select * from sysobjects where name = 'MotifAdmission')" +
                    " CREATE TABLE [dbo].[MotifAdmission] " +
                    "(" +
                    "[Id]  BIGINT IDENTITY(1, 1) NOT NULL," +
                    "[Nom]  NVARCHAR(200)  NOT NULL," +

                    "PRIMARY KEY CLUSTERED([Id] ASC));"
                   ;

                connection = Dbconnection.GetConnection();
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand(query, connection);
                await cmd.ExecuteNonQueryAsync();

                return new Message(true, "Table  MotifAdmission  create");
            }
            catch (Exception e)
            {
                return new Message(false, e.Message);
            }
            finally
            {
                connection.Close();
            }


        }
        public static async Task<Message> create_table_TypesPrestation()
        {
            try
            {
                string query = "If not exists (select * from sysobjects where name = 'TypesPrestation')" +
                    " CREATE TABLE [dbo].[TypesPrestation] " +
                    "([Id]  BIGINT IDENTITY(1, 1) NOT NULL," +
                    "[Nom]  NVARCHAR(200)  NOT NULL," +
                    "PRIMARY KEY CLUSTERED([Id] ASC));";

                connection = Dbconnection.GetConnection();
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand(query, connection);
                await cmd.ExecuteNonQueryAsync();

                return new Message(true, "Table Types Prestation  create");
            }
            catch (Exception e)
            {
                return new Message(false, e.Message);
            }
            finally
            {
                connection.Close();
            }


        }

        public static async Task<Message> create_table_Prestation()
        {
            try
            {
                string query = "If not exists (select * from sysobjects where name = 'Prestation')" +
                    " CREATE TABLE [dbo].[Prestation] " +
                    "(" +
                    "[Id]  BIGINT IDENTITY(1, 1) NOT NULL," +
                    "[IdTypePrestation]  BIGINT  NOT NULL," +
                    "[TVA]  BIGINT  NOT NULL," +
                    "[Nom]  NVARCHAR(200)  NOT NULL," +
                    "[Prix] FLOAT(20) NOT NULL," +
                    "PRIMARY KEY CLUSTERED([Id] ASC));" +
                    "ALTER TABLE [dbo].[Prestation] ADD  CONSTRAINT [fk_Prestation_TypesPrestation] FOREIGN KEY([IdTypePrestation])  REFERENCES [dbo].[TypesPrestation] ([Id]); ";

                connection = Dbconnection.GetConnection();
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand(query, connection);
                await cmd.ExecuteNonQueryAsync();

                return new Message(true, "Table  Prestation  create");
            }
            catch (Exception e)
            {
                return new Message(false, e.Message);
            }
            finally
            {
                connection.Close();
            }


        }

        public static async Task<Message> create_table_Admission_Prestation()
        {
            try
            {
                string query = "If not exists (select * from sysobjects where name = 'Admission_Prestation')" +
                    " CREATE TABLE [dbo].[Admission_Prestation] " +
                    "(" +
                    "[Id]  BIGINT IDENTITY(1, 1) NOT NULL," +
                    "[AdmissionId]  BIGINT  NOT NULL," +
                    "[PrestationId]  BIGINT  NOT NULL," +
                    "[Nombre]  BIGINT  NOT NULL," +

                    "PRIMARY KEY CLUSTERED([Id] ASC));";

                connection = Dbconnection.GetConnection();
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand(query, connection);
                await cmd.ExecuteNonQueryAsync();

                return new Message(true, "Table  Prestation  create");
            }
            catch (Exception e)
            {
                return new Message(false, e.Message);
            }
            finally
            {
                connection.Close();
            }


        }
        public static async Task<Message> create_table_PreAdmission()
        {
            try
            {
                string query = "If not exists (select * from sysobjects where name = 'PreAdmission')" +
                    " CREATE TABLE [dbo].[PreAdmission] " +
                    "([Id]  BIGINT IDENTITY(1, 1) NOT NULL," +
                    "[IdPatient]  BIGINT NOT NULL  ," +
                    "[IdAgent]  BIGINT NOT NULL  ," +
                    "[Date_Ajout]       NVARCHAR(200)  NOT NULL," +
                    "PRIMARY KEY CLUSTERED([Id] ASC));";
                ;
                connection = Dbconnection.GetConnection();
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand(query, connection);
                await cmd.ExecuteNonQueryAsync();

                return new Message(true, "Table PreAdmission  create");
            }
            catch (Exception e)
            {
                return new Message(false, e.Message);
            }
            finally
            {
                connection.Close();
            }


        }
        public static async Task<Message> create_table_FactureAdmission()
        {
            try
            {
                string query = "If not exists (select * from sysobjects where name = 'FactureAdmission')" +
                    " CREATE TABLE [dbo].[FactureAdmission] " +
                    "([Id]  BIGINT IDENTITY(1, 1) NOT NULL," +
                    "[IdPrisEncharge]  BIGINT NULL    ," +
                    "[IdAdmission]  BIGINT NOT NULL  ," +
                    "[PEC]  BIGINT NOT NULL  ," +
                    "[ReferenceFacture]  NVARCHAR(200)  NOT NULL  ," +
                    "[MontantTotale]  FLOAT(20)   NULL  ," +
                    "[MontantPatient]  FLOAT(20)  NULL  ," +
                    "[MontantPriseEncharge]  FLOAT(20)  NULL  ," +
                    "[Date_Ajout]       NVARCHAR(200)  NOT NULL," +
                    "PRIMARY KEY CLUSTERED([Id] ASC));";
                ;
                connection = Dbconnection.GetConnection();
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand(query, connection);
                await cmd.ExecuteNonQueryAsync();

                return new Message(true, "Table FactureAdmission  create");
            }
            catch (Exception e)
            {
                return new Message(false, e.Message);
            }
            finally
            {
                connection.Close();
            }


        }

        public static async Task<Message> create_table_FacturePreAdmission()
        {
            try
            {
                string query = "If not exists (select * from sysobjects where name = 'FacturePreAdmission')" +
                    " CREATE TABLE [dbo].[FacturePreAdmission] " +
                    "([Id]  BIGINT IDENTITY(1, 1) NOT NULL," +
                    "[IdPrisEncharge]  BIGINT NULL    ," +
                    "[IdPreAdmission]  BIGINT NOT NULL  ," +
                    "[PEC]  BIGINT NOT NULL  ," +
                    "[ReferenceFacture]  NVARCHAR(200)  NOT NULL  ," +
                    "[MontantTotale]  FLOAT(20)   NULL  ," +
                    "[MontantPatient]  FLOAT(20)  NULL  ," +
                    "[MontantPriseEncharge]  FLOAT(20)  NULL  ," +
                    "[Date_Ajout]       NVARCHAR(200)  NOT NULL," +
                    "PRIMARY KEY CLUSTERED([Id] ASC));";
                ;
                connection = Dbconnection.GetConnection();
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand(query, connection);
                await cmd.ExecuteNonQueryAsync();

                return new Message(true, "Table FacturePreAdmission  create");
            }
            catch (Exception e)
            {
                return new Message(false, e.Message);
            }
            finally
            {
                connection.Close();
            }


        }

        public static async Task<Message> create_table_PreAdmission_Prestation()
        {
            try
            {
                string query = "If not exists (select * from sysobjects where name = 'PreAdmission_Prestation')" +
                    " CREATE TABLE [dbo].[PreAdmission_Prestation] " +
                    "(" +
                    "[Id]  BIGINT IDENTITY(1, 1) NOT NULL," +
                    "[PreAdmissionId]  BIGINT  NOT NULL," +
                    "[PrestationId]  BIGINT  NOT NULL," +
                    "[Nombre]  BIGINT  NOT NULL," +
                    "PRIMARY KEY CLUSTERED([Id] ASC));";

                connection = Dbconnection.GetConnection();
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand(query, connection);
                await cmd.ExecuteNonQueryAsync();

                return new Message(true, "Table  PreAdmission_Prestation  create");
            }
            catch (Exception e)
            {
                return new Message(false, e.Message);
            }
            finally
            {
                connection.Close();
            }


        }

        public static async Task<Message> create_table_PayementFacturePA()
        {
            try
            {
                string query = "If not exists (select * from sysobjects where name = 'PayementFacturePA')" +
                    " CREATE TABLE [dbo].[PayementFacturePA] " +
                    "([Id]  BIGINT IDENTITY(1, 1) NOT NULL," +
                    "[IdFactureAdmission]  BIGINT NULL    ," +
                    "[IdAgent]  BIGINT NOT NULL  ," +
                    "[ReferencePayement]  NVARCHAR(200)  NOT NULL  ," +
                    "[ModePayement]  NVARCHAR(200) NULL  ," +
                    "[MontantTotalePayer]  FLOAT(20)  NULL  ," +
                    "[MontantRestantPayer]  FLOAT(20)  NULL  ," +
                    "[Date_Ajout]       NVARCHAR(200)  NOT NULL," +
                    "PRIMARY KEY CLUSTERED([Id] ASC));";
                ;
                connection = Dbconnection.GetConnection();
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand(query, connection);
                await cmd.ExecuteNonQueryAsync();

                return new Message(true, "Table PayementFacturePA  create");
            }
            catch (Exception e)
            {
                return new Message(false, e.Message);
            }
            finally
            {
                connection.Close();
            }


        }
























        public static async Task<Message> create_table_DossierPatient()
        {
            try
            {
                string query = "If not exists (select * from sysobjects where name = 'DossierPatient')" +
                    " CREATE TABLE [dbo].[DossierPatient] " +
                    "([Id]  BIGINT IDENTITY(1, 1) NOT NULL," +
                    "[PatientId]  BIGINT NOT NULL  ," +
                    "[ServiceId]  BIGINT NOT NULL ," +
                    "[ReferenceDossier]  NVARCHAR(200) NOT NULL ," +

                    "[Date_Ajout]       DATETIME      DEFAULT (getdate()) NOT NULL," +
                    "PRIMARY KEY CLUSTERED([Id] ASC));" +
                    "ALTER TABLE [dbo].[DossierPatient] ADD CONSTRAINT Uk_DossierPatientPatientId UNIQUE (PatientId); " +
                    "ALTER TABLE [dbo].[DossierPatient] ADD  CONSTRAINT [fk_Patient_DossierPatient] FOREIGN KEY([PatientId])  REFERENCES [dbo].[Patient] ([Id])  ON UPDATE CASCADE ON DELETE CASCADE ;" +
                    "ALTER TABLE [dbo].[DossierPatient] ADD  CONSTRAINT [fk_Service_DossierPatient] FOREIGN KEY([ServiceId])  REFERENCES [dbo].[Service] ([Id]) ;";

                ///scrip pour creer le tigrre
                //"USE Likitadb;" +

                //  "CREATE   TRIGGER ReferenceDossierPatient_Reference" +
                //  "ON dbo.DossierPatient" +
                //  "AFTER INSERT" +
                //  "AS" +
                //  "BEGIN" +
                //  "UPDATE dbo.DossierPatient" +
                //  " SET ReferenceDossier = CONCAT('D000', inserted.Id)" +
                //  "FROM dbo.DossierPatient" +
                //  " INNER JOIN inserted ON DossierPatient.Id = inserted.Id" +
                //  "END;";


                connection = Dbconnection.GetConnection();
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand(query, connection);
                await cmd.ExecuteNonQueryAsync();

                return new Message(true, "Table DossierPatient  create");
            }
            catch (Exception e)
            {
                return new Message(false, e.Message);
            }
            finally
            {
                connection.Close();
            }


        }
        public static async Task<Message> create_table_Hospitalisation()
        {
            try
            {
                string query = "If not exists (select * from sysobjects where name = 'Hospitalisation')" +
                    " CREATE TABLE [dbo].[Hospitalisation] " +
                    "([Id]  BIGINT IDENTITY(1, 1) NOT NULL," +
                   " [Rang]  BIGINT  NOT NULL," +
                    "[PatientId]  BIGINT NOT NULL  ," +
                    "[ServiceId]  BIGINT NOT NULL ," +
                    "[MedecindemandeurId]  BIGINT NOT NULL ," +
                    "[litId]  BIGINT NOT NULL ," +
                    "[Date_Entré]  DATETIME NOT NULL ," +
                    "[Date_Sortis]  DATETIME  NULL ," +
                    "[Status]  NVARCHAR(200)  NOT NULL ," +
                    "[Type]  NVARCHAR(200)  NOT NULL ," +
                    "[Motif]  NVARCHAR(MAX) NOT NULL ," +
                    "[Remarque]  NVARCHAR(MAX)  NULL ," +

                    "[Date_Ajout]       DATETIME      DEFAULT (getdate()) NOT NULL," +
                    "PRIMARY KEY CLUSTERED([Id] ASC));" +
                    "ALTER TABLE [dbo].[Hospitalisation] ADD  CONSTRAINT fk_Patient_Hospitalisations FOREIGN KEY  ([PatientId])  REFERENCES [dbo].[Patient] ([Id]) ;" +
                    "ALTER TABLE [dbo].[Hospitalisation] ADD  CONSTRAINT fk_Medicindemandeur_Hospitalisations FOREIGN KEY  ([MedecindemandeurId])  REFERENCES [dbo].[Medecin] ([Id]) ;" +
                    "ALTER TABLE [dbo].[Hospitalisation] ADD  CONSTRAINT fk_Chambre_Hospitalisations FOREIGN KEY  ([litId])  REFERENCES [dbo].[Chambre] ([Id]) ;" +
                    "ALTER TABLE [dbo].[Hospitalisation] ADD  CONSTRAINT fk_Service_Hospitalisationts FOREIGN KEY  ([ServiceId])  REFERENCES [dbo].[Service] ([Id]) ;";

                ///scrip pour creer le tigrre
                //"USE Likitadb;" +

                //  "CREATE   TRIGGER ReferenceDossierPatient_Reference" +
                //  "ON dbo.DossierPatient" +
                //  "AFTER INSERT" +
                //  "AS" +
                //  "BEGIN" +
                //  "UPDATE dbo.DossierPatient" +
                //  " SET ReferenceDossier = CONCAT('D000', inserted.Id)" +
                //  "FROM dbo.DossierPatient" +
                //  " INNER JOIN inserted ON DossierPatient.Id = inserted.Id" +
                //  "END;";


                connection = Dbconnection.GetConnection();
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand(query, connection);
                await cmd.ExecuteNonQueryAsync();

                return new Message(true, "Table Hospitalisation  create");
            }
            catch (Exception e)
            {
                return new Message(false, e.Message);
            }
            finally
            {
                connection.Close();
            }


        }
        public static async Task<Message> create_table_HistoriqueMaladie()
        {
            try
            {
                string query = "If not exists (select * from sysobjects where name = 'HistoriqueMaladie')" +
                    " CREATE TABLE [dbo].[HistoriqueMaladie] " +
                    "([Id]  BIGINT IDENTITY(1, 1) NOT NULL," +
                    "[PatientId]  BIGINT NOT NULL  ," +
                    "[HospitalisationId]  BIGINT NOT NULL ," +
                    "[MODE_DE_DEBUT_DES_SYMPTOMES]  NVARCHAR(MAX)  NOT NULL ," +
                    "[NATURES_DES_SYMPTOMES]  NVARCHAR(MAX) NOT NULL ," +
                    "[DUREE_EVOLUTION_DES_SYMPTOMES]  NVARCHAR(MAX)  NULL ," +
                    "[Edite]  NVARCHAR(20)  NULL ," +
                    "[TRAITEMENT_RECU]  NVARCHAR(MAX)  NULL ," +
                    "[EVOLUTION_SOUS_TRAITEMENT]  NVARCHAR(MAX)  NULL ," +
                    "[DUREE_DE_CE_TRAITEMENT_A_ADMISSION]  NVARCHAR(MAX)  NULL ," +

                    "[Date_Ajout]       DATETIME      DEFAULT (getdate()) NOT NULL," +
                    "PRIMARY KEY CLUSTERED([Id] ASC));" +
                    "ALTER TABLE [dbo].[HistoriqueMaladie] ADD  CONSTRAINT fk_Patient_HistoriqueMaladie FOREIGN KEY  ([PatientId])  REFERENCES [dbo].[Patient] ([Id]) ;" +
                    "ALTER TABLE [dbo].[HistoriqueMaladie] ADD  CONSTRAINT fk_Medicindemandeur_HistoriqueMaladie FOREIGN KEY  ([HospitalisationId])  REFERENCES [dbo].[Hospitalisation] ([Id]) ;";

                ///scrip pour creer le tigrre
                //"USE Likitadb;" +

                //  "CREATE   TRIGGER ReferenceDossierPatient_Reference" +
                //  "ON dbo.DossierPatient" +
                //  "AFTER INSERT" +
                //  "AS" +
                //  "BEGIN" +
                //  "UPDATE dbo.DossierPatient" +
                //  " SET ReferenceDossier = CONCAT('D000', inserted.Id)" +
                //  "FROM dbo.DossierPatient" +
                //  " INNER JOIN inserted ON DossierPatient.Id = inserted.Id" +
                //  "END;";


                connection = Dbconnection.GetConnection();
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand(query, connection);
                await cmd.ExecuteNonQueryAsync();

                return new Message(true, "Table HistoriqueMaladie  create");
            }
            catch (Exception e)
            {
                return new Message(false, e.Message);
            }
            finally
            {
                connection.Close();
            }


        }
        public static async Task<Message> create_table_ATCDMEDICAUX()
        {
            try
            {
                string query = "If not exists (select * from sysobjects where name = 'ATCDMEDICAUX')" +
                    " CREATE TABLE [dbo].[ATCDMEDICAUX] " +
                    "([Id]  BIGINT IDENTITY(1, 1) NOT NULL," +
                    "[PatientId]  BIGINT NOT NULL  ," +
                    "[Diabete]   NVARCHAR(20)  NOT  NULL ," +
                    "[TypeDiabete]  NVARCHAR(50)   NULL ," +
                    "[HTA]   NVARCHAR(20)  NOT  NULL ," +
                    "[Tuberculose]  NVARCHAR(20)  NOT  NULL ," +
                    "[TypeTuberculose]  NVARCHAR(200)  NULL ," +
                     "[Traiteetguerie]   NVARCHAR(200)  NOT  NULL ," +
                    "[DateTraiteetguerie]  NVARCHAR(50)  NULL ," +
                    "[Asthme]   NVARCHAR(20)  NOT  NULL ," +
                     "[DuredevolutionAsthme]  NVARCHAR(50)  NULL ," +
                     "[TraitementAsthme]  NVARCHAR(200)  NULL ," +
                     "[DREPANOCYTOSE]   NVARCHAR(20)  NOT  NULL ," +
                     "[IRC]   NVARCHAR(20)  NOT  NULL ," +
                      "[StadeIRC]  NVARCHAR(200)  NULL ," +
                      "[DuredevolutionIRC]  NVARCHAR(200)  NULL ," +
                      "[CauseIRC]  NVARCHAR(200)  NULL ," +
                      "[TraitementIRC]  NVARCHAR(200)  NULL ," +
                      "[InsuffisanceCardiaque]    NVARCHAR(20)  NOT  NULL," +
                      "[TypeInsuffisanceCardiaque]  NVARCHAR(200)  NULL ," +
                       "[DuredevolutionInsuffisanceCardiaque]  NVARCHAR(200)  NULL ," +
                       "[CauseInsuffisanceCardiaque]  NVARCHAR(200)  NULL ," +
                       "[TraitementInsuffisanceCardiaque]  NVARCHAR(200)  NULL ," +

                    "[Date_Ajout]       DATETIME      DEFAULT (getdate()) NOT NULL," +
                    "PRIMARY KEY CLUSTERED([Id] ASC));" +
                    "ALTER TABLE [dbo].[ATCDMEDICAUX] ADD  CONSTRAINT fk_Patient_ATCDMEDICAUX FOREIGN KEY  ([PatientId])  REFERENCES [dbo].[Patient] ([Id]) ;";

                ///scrip pour creer le tigrre
                //"USE Likitadb;" +

                //  "CREATE   TRIGGER ReferenceDossierPatient_Reference" +
                //  "ON dbo.DossierPatient" +
                //  "AFTER INSERT" +
                //  "AS" +
                //  "BEGIN" +
                //  "UPDATE dbo.DossierPatient" +
                //  " SET ReferenceDossier = CONCAT('D000', inserted.Id)" +
                //  "FROM dbo.DossierPatient" +
                //  " INNER JOIN inserted ON DossierPatient.Id = inserted.Id" +
                //  "END;";


                connection = Dbconnection.GetConnection();
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand(query, connection);
                await cmd.ExecuteNonQueryAsync();

                return new Message(true, "Table ATCDMEDICAUX  create");
            }
            catch (Exception e)
            {
                return new Message(false, e.Message);
            }
            finally
            {
                connection.Close();
            }


        }


        public static async Task<Message> create_table_ATCDCHIRURGICAUX()
        {
            try
            {
                string query = "If not exists (select * from sysobjects where name = 'ATCDCHIRURGICAUX')" +
                    " CREATE TABLE [dbo].[ATCDCHIRURGICAUX] " +
                    "([Id]  BIGINT IDENTITY(1, 1) NOT NULL," +
                    "[PatientId]  BIGINT NOT NULL  ," +
                    "[Nom]   NVARCHAR(250)  NOT  NULL ," +
                    "[Quand]  NVARCHAR(250) NOT  NULL ," +
                    "[Dequoi]   NVARCHAR(200)  NOT  NULL ," +
                    "[Par]  NVARCHAR(200)  NOT  NULL ," +
                    "[Suite]  NVARCHAR(250)  NULL ," +

                    "[Date_Ajout]       DATETIME      DEFAULT (getdate()) NOT NULL," +
                    "PRIMARY KEY CLUSTERED([Id] ASC));" +
                    "ALTER TABLE [dbo].[ATCDCHIRURGICAUX] ADD  CONSTRAINT fk_Patient_ATCDCHIRURGICAUX FOREIGN KEY  ([PatientId])  REFERENCES [dbo].[Patient] ([Id]) ;";

                ///scrip pour creer le tigrre
                //"USE Likitadb;" +

                //  "CREATE   TRIGGER ReferenceDossierPatient_Reference" +
                //  "ON dbo.DossierPatient" +
                //  "AFTER INSERT" +
                //  "AS" +
                //  "BEGIN" +
                //  "UPDATE dbo.DossierPatient" +
                //  " SET ReferenceDossier = CONCAT('D000', inserted.Id)" +
                //  "FROM dbo.DossierPatient" +
                //  " INNER JOIN inserted ON DossierPatient.Id = inserted.Id" +
                //  "END;";


                connection = Dbconnection.GetConnection();
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand(query, connection);
                await cmd.ExecuteNonQueryAsync();

                return new Message(true, "Table ATCDCHIRURGICAUX  create");
            }
            catch (Exception e)
            {
                return new Message(false, e.Message);
            }
            finally
            {
                connection.Close();
            }


        }


        public static async Task<Message> create_table_ATCDFAMILIAUX()
        {
            try
            {
                string query = "If not exists (select * from sysobjects where name = 'ATCDFAMILIAUX')" +
                    " CREATE TABLE [dbo].[ATCDFAMILIAUX] " +
                    "([Id]  BIGINT IDENTITY(1, 1) NOT NULL," +
                    "[PatientId]  BIGINT NOT NULL  ," +
                    "[AscendantPere]   NVARCHAR(250)  NOT  NULL ," +
                    "[CausePere]  NVARCHAR(250)   NULL ," +
                    "[AscendantMere]   NVARCHAR(200)  NOT  NULL ," +
                    "[CauseMere]  NVARCHAR(200)  NULL ," +
                    "[Nombresdefreres]   BIGINT NOT NULL ," +
                    "[NombresdefreresVBSA]   BIGINT NOT NULL ," +
                    "[NombresdefreresDCD]   BIGINT NOT NULL ," +
                    "[FreresDCDCause]    NVARCHAR(200)  NULL ," +
                    "[Nombresdesoeurs]   BIGINT NOT NULL ," +
                    "[NombresdesoeursVBSA]   BIGINT NOT NULL ," +
                    "[NombresdesoeursDCD]   BIGINT NOT NULL ," +
                    "[FreressoeursCause]    NVARCHAR(200) NOT NULL ," +
                    "[RangFraterie]   BIGINT NOT NULL ," +
                    "[Nombresdesenfaants]   BIGINT NOT NULL ," +
                    "[Nombresdesfilles]   BIGINT NOT NULL ," +
                    "[Nombresdesgarcons]   BIGINT NOT NULL ," +
                    "[NombresdenfantVBSA]   BIGINT NOT NULL ," +
                    "[NombresdentDCD]   BIGINT NOT NULL ," +
                    "[EnfantCause]    NVARCHAR(200)  NULL ," +

                    "[Date_Ajout]       DATETIME      DEFAULT (getdate()) NOT NULL," +
                    "PRIMARY KEY CLUSTERED([Id] ASC));" +
                    "ALTER TABLE [dbo].[ATCDFAMILIAUX] ADD  CONSTRAINT fk_Patient_ATCDFAMILIAUX FOREIGN KEY  ([PatientId])  REFERENCES [dbo].[Patient] ([Id]) ;";

                ///scrip pour creer le tigrre
                //"USE Likitadb;" +

                //  "CREATE   TRIGGER ReferenceDossierPatient_Reference" +
                //  "ON dbo.DossierPatient" +
                //  "AFTER INSERT" +
                //  "AS" +
                //  "BEGIN" +
                //  "UPDATE dbo.DossierPatient" +
                //  " SET ReferenceDossier = CONCAT('D000', inserted.Id)" +
                //  "FROM dbo.DossierPatient" +
                //  " INNER JOIN inserted ON DossierPatient.Id = inserted.Id" +
                //  "END;";


                connection = Dbconnection.GetConnection();
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand(query, connection);
                await cmd.ExecuteNonQueryAsync();

                return new Message(true, "Table ATCDFAMILIAUX  create");
            }
            catch (Exception e)
            {
                return new Message(false, e.Message);
            }
            finally
            {
                connection.Close();
            }


        }
        public static async Task<Message> create_table_ATCDGYNECOOBSTETRICAUX()
        {
            try
            {
                string query = "If not exists (select * from sysobjects where name = 'ATCDGYNECOOBSTETRICAUX')" +
                    " CREATE TABLE [dbo].[ATCDGYNECOOBSTETRICAUX] " +
                    "([Id]  BIGINT IDENTITY(1, 1) NOT NULL," +
                    "[PatientId]  BIGINT NOT NULL  ," +
                    "[Agedemenarches]   BIGINT NOT NULL ," +
                    "[MenstruationDureducycle]   BIGINT NOT NULL ," +
                    "[MenstruationRegulariteducycle]   BIGINT NOT NULL ," +
                    "[MenstruationAbondance]    NVARCHAR(200)  NULL ," +
                    "[MenstruationG]  NVARCHAR(200)  NOT NULL ," +
                    "[MenstruationP]    NVARCHAR(200) NOT NULL ," +
                    "[ModedaccouchementVoiebasse]     NVARCHAR(200)  NULL ," +
                    "[Poidsdenaissancedesenfants]   BIGINT NOT NULL ," +
                    "[macrosomie]     NVARCHAR(200)  NULL ," +
                    "[Datedelapremieregrossesse]    NVARCHAR(200)  NULL ," +
                    "[Datedeladernieregrossesse]    NVARCHAR(200)  NULL ," +
                    "[Notiondemenopause]    NVARCHAR(200)  NULL ," +
                    "[Notiondureeevolution]    NVARCHAR(200)  NULL ," +
                    "[Notionsignesdeclimatere]    NVARCHAR(200)  NULL ," +
                    "[Notionmortinutero]    NVARCHAR(200)  NULL ," +
                    "[Notiondiabetegestationne]    NVARCHAR(200)  NULL ," +
                    "[Notiondepreclampsie]    NVARCHAR(200)  NULL ," +
                    "[Notioneclampsie]    NVARCHAR(200)  NULL ," +


                    "PRIMARY KEY CLUSTERED([Id] ASC));" +
                    "ALTER TABLE [dbo].[ATCDGYNECOOBSTETRICAUX] ADD  CONSTRAINT fk_Patient_ATCDGYNECOOBSTETRICAUX FOREIGN KEY  ([PatientId])  REFERENCES [dbo].[Patient] ([Id]) ;";

                ///scrip pour creer le tigrre
                //"USE Likitadb;" +

                //  "CREATE   TRIGGER ReferenceDossierPatient_Reference" +
                //  "ON dbo.DossierPatient" +
                //  "AFTER INSERT" +
                //  "AS" +
                //  "BEGIN" +
                //  "UPDATE dbo.DossierPatient" +
                //  " SET ReferenceDossier = CONCAT('D000', inserted.Id)" +
                //  "FROM dbo.DossierPatient" +
                //  " INNER JOIN inserted ON DossierPatient.Id = inserted.Id" +
                //  "END;";


                connection = Dbconnection.GetConnection();
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand(query, connection);
                await cmd.ExecuteNonQueryAsync();

                return new Message(true, "Table ATCDGYNECOOBSTETRICAUX  create");
            }
            catch (Exception e)
            {
                return new Message(false, e.Message);
            }
            finally
            {
                connection.Close();
            }


        }



        public static async Task<Message> create_table_EXAMENGENERAL()
        {
            try
            {
                string query = "If not exists (select * from sysobjects where name = 'EXAMENGENERAL')" +
                    " CREATE TABLE [dbo].[EXAMENGENERAL] " +
                    "([Id]  BIGINT IDENTITY(1, 1) NOT NULL," +
                    "[PatientId]  BIGINT NOT NULL  ," +
                    "[HospitalisationId]  BIGINT NOT NULL  ," +
                    "[Etatdelalangue]   NVARCHAR(250)  NOT  NULL ," +
                    "[Desconjonctives]  NVARCHAR(250)   NULL ," +
                    "[Temperature]  BIGINT NOT  NULL ," +
                    "[POIDS]  BIGINT NOT  NULL ," +
                    "[TAILLE]  BIGINT NOT  NULL ," +
                    "[BMI]  BIGINT NOT  NULL ," +
                    "[TOURDETAILLE]  BIGINT NOT  NULL ," +
                    "[TOURDEHANCHE]  BIGINT NOT  NULL ," +
                    "[OBESITE]  NVARCHAR(200)  NULL ," +
                    "[OBESITEType]    NVARCHAR(200)  NULL ," +
                    "[OBESITEClasse]    NVARCHAR(200)  NULL ," +
                    "[COUCHE]    NVARCHAR(200) NOT NULL ," +
                    "[Edite]    NVARCHAR(20) NOT NULL ," +
                    "[DEBOUT]    NVARCHAR(200) NOT   NULL ," +
                    "[Date_Ajout]       DATETIME      DEFAULT (getdate()) NOT NULL," +
                    "PRIMARY KEY CLUSTERED([Id] ASC));" +
                    "ALTER TABLE [dbo].[EXAMENGENERAL] ADD  CONSTRAINT fk_Patient_EXAMENGENERAL FOREIGN KEY  ([PatientId])  REFERENCES [dbo].[Patient] ([Id]) ;" +
                    "ALTER TABLE [dbo].[EXAMENGENERAL] ADD  CONSTRAINT fk_Hospitalisation_EXAMENGENERAL FOREIGN KEY  ([HospitalisationId])  REFERENCES [dbo].[Hospitalisation] ([Id]) ;";

                ///scrip pour creer le tigrre
                //"USE Likitadb;" +

                //  "CREATE   TRIGGER ReferenceDossierPatient_Reference" +
                //  "ON dbo.DossierPatient" +
                //  "AFTER INSERT" +
                //  "AS" +
                //  "BEGIN" +
                //  "UPDATE dbo.DossierPatient" +
                //  " SET ReferenceDossier = CONCAT('D000', inserted.Id)" +
                //  "FROM dbo.DossierPatient" +
                //  " INNER JOIN inserted ON DossierPatient.Id = inserted.Id" +
                //  "END;";


                connection = Dbconnection.GetConnection();
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand(query, connection);
                await cmd.ExecuteNonQueryAsync();

                return new Message(true, "Table EXAMENGENERAL  create");
            }
            catch (Exception e)
            {
                return new Message(false, e.Message);
            }
            finally
            {
                connection.Close();
            }


        }


        public static async Task<Message> create_table_Appareilpulmonaire()
        {
            try
            {
                string query = "If not exists (select * from sysobjects where name = 'Appareilpulmonaire')" +
                    " CREATE TABLE [dbo].[Appareilpulmonaire] " +
                    "([Id]  BIGINT IDENTITY(1, 1) NOT NULL," +
                    "[PatientId]  BIGINT NOT NULL  ," +
                    "[HospitalisationId]  BIGINT NOT NULL  ," +
                    "[Edite]    NVARCHAR(20) NOT NULL ," +
                    "[Inspection]   NVARCHAR(MAX)  NOT  NULL ," +
                    "[Palpation]  NVARCHAR(50) NOT   NULL ," +
                    "[Percussion]  NVARCHAR(50) NOT  NULL ," +
                    "[Auscultation]  NVARCHAR(50) NOT  NULL ," +
                    "[Conclusion]   NVARCHAR(MAX)    NULL ," +
                    "[Date_Ajout]       DATETIME      DEFAULT (getdate()) NOT NULL," +
                    "PRIMARY KEY CLUSTERED([Id] ASC));" +
                    "ALTER TABLE [dbo].[Appareilpulmonaire] ADD  CONSTRAINT fk_Patient_Appareilpulmonaire FOREIGN KEY  ([PatientId])  REFERENCES [dbo].[Patient] ([Id]) ;" +
                "ALTER TABLE [dbo].[Appareilpulmonaire] ADD  CONSTRAINT fk_Hospitalisation_Appareilpulmonaire FOREIGN KEY  ([HospitalisationId])  REFERENCES [dbo].[Hospitalisation] ([Id]) ;";

                ///scrip pour creer le tigrre
                //"USE Likitadb;" +

                //  "CREATE   TRIGGER ReferenceDossierPatient_Reference" +
                //  "ON dbo.DossierPatient" +
                //  "AFTER INSERT" +
                //  "AS" +
                //  "BEGIN" +
                //  "UPDATE dbo.DossierPatient" +
                //  " SET ReferenceDossier = CONCAT('D000', inserted.Id)" +
                //  "FROM dbo.DossierPatient" +
                //  " INNER JOIN inserted ON DossierPatient.Id = inserted.Id" +
                //  "END;";


                connection = Dbconnection.GetConnection();
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand(query, connection);
                await cmd.ExecuteNonQueryAsync();

                return new Message(true, "Table Appareilpulmonaire  create");
            }
            catch (Exception e)
            {
                return new Message(false, e.Message);
            }
            finally
            {
                connection.Close();
            }


        }




        public static async Task<Message> create_table_Appareilcardiovasculaire()
        {
            try
            {
                string query = "If not exists (select * from sysobjects where name = 'Appareilcardiovasculaire')" +
                    " CREATE TABLE [dbo].[Appareilcardiovasculaire] " +
                    "([Id]  BIGINT IDENTITY(1, 1) NOT NULL," +
                    "[PatientId]  BIGINT NOT NULL  ," +
                    "[HospitalisationId]  BIGINT NOT NULL  ," +
                    "[Edite]    NVARCHAR(20) NOT NULL ," +
                    "[Inspection]   NVARCHAR(MAX)  NOT  NULL ," +
                    "[Palpation]  NVARCHAR(MAX) NOT   NULL ," +
                    "[Percussion]  NVARCHAR(MAX) NOT  NULL ," +
                    "[Auscultation]  NVARCHAR(MAX) NOT  NULL ," +
                    "[BDC]  NVARCHAR(Max) NOT  NULL ," +
                    "[Soufflesetroulements]  NVARCHAR(MAX) NOT  NULL ," +
                    "[Frottementpericardique]   NVARCHAR(MAX)    NULL ," +
                                        "[Conclusion]   NVARCHAR(MAX)    NULL ," +

                    "[Date_Ajout]       DATETIME      DEFAULT (getdate()) NOT NULL," +
                    "PRIMARY KEY CLUSTERED([Id] ASC));" +
                    "ALTER TABLE [dbo].[Appareilcardiovasculaire] ADD  CONSTRAINT fk_Patient_Appareilcardiovasculaire FOREIGN KEY  ([PatientId])  REFERENCES [dbo].[Patient] ([Id]) ;" +
                "ALTER TABLE [dbo].[Appareilcardiovasculaire] ADD  CONSTRAINT fk_Hospitalisation_Appareilcardiovasculaire FOREIGN KEY  ([HospitalisationId])  REFERENCES [dbo].[Hospitalisation] ([Id]) ;";

                ///scrip pour creer le tigrre
                //"USE Likitadb;" +

                //  "CREATE   TRIGGER ReferenceDossierPatient_Reference" +
                //  "ON dbo.DossierPatient" +
                //  "AFTER INSERT" +
                //  "AS" +
                //  "BEGIN" +
                //  "UPDATE dbo.DossierPatient" +
                //  " SET ReferenceDossier = CONCAT('D000', inserted.Id)" +
                //  "FROM dbo.DossierPatient" +
                //  " INNER JOIN inserted ON DossierPatient.Id = inserted.Id" +
                //  "END;";


                connection = Dbconnection.GetConnection();
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand(query, connection);
                await cmd.ExecuteNonQueryAsync();

                return new Message(true, "Table Appareilcardiovasculaire  create");
            }
            catch (Exception e)
            {
                return new Message(false, e.Message);
            }
            finally
            {
                connection.Close();
            }


        }



        public static async Task<Message> create_table_Arteriel()
        {
            try
            {
                string query = "If not exists (select * from sysobjects where name = 'Arteriel')" +
                    " CREATE TABLE [dbo].[Arteriel] " +
                    "([Id]  BIGINT IDENTITY(1, 1) NOT NULL," +
                    "[PatientId]  BIGINT NOT NULL  ," +
                   "[HospitalisationId]  BIGINT NOT NULL  ," +
                    "[Edite]    NVARCHAR(20) NOT NULL ," +
                    "[Clinique]   NVARCHAR(MAX)  NOT  NULL ," +
                    "[IPS]  NVARCHAR(MAX) NOT   NULL ," +


                    "[Date_Ajout]       DATETIME      DEFAULT (getdate()) NOT NULL," +
                    "PRIMARY KEY CLUSTERED([Id] ASC));" +
                    "ALTER TABLE [dbo].[Arteriel] ADD  CONSTRAINT fk_Patient_Arteriel FOREIGN KEY  ([PatientId])  REFERENCES [dbo].[Patient] ([Id]) ;" +
                "ALTER TABLE [dbo].[Arteriel] ADD  CONSTRAINT fk_Hospitalisation_Arteriel FOREIGN KEY  ([HospitalisationId])  REFERENCES [dbo].[Hospitalisation] ([Id]) ;";

                ///scrip pour creer le tigrre
                //"USE Likitadb;" +

                //  "CREATE   TRIGGER ReferenceDossierPatient_Reference" +
                //  "ON dbo.DossierPatient" +
                //  "AFTER INSERT" +
                //  "AS" +
                //  "BEGIN" +
                //  "UPDATE dbo.DossierPatient" +
                //  " SET ReferenceDossier = CONCAT('D000', inserted.Id)" +
                //  "FROM dbo.DossierPatient" +
                //  " INNER JOIN inserted ON DossierPatient.Id = inserted.Id" +
                //  "END;";


                connection = Dbconnection.GetConnection();
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand(query, connection);
                await cmd.ExecuteNonQueryAsync();

                return new Message(true, "Table Arteriel  create");
            }
            catch (Exception e)
            {
                return new Message(false, e.Message);
            }
            finally
            {
                connection.Close();
            }


        }
        public static async Task<Message> create_table_Veineux()
        {
            try
            {
                string query = "If not exists (select * from sysobjects where name = 'Veineux')" +
                    " CREATE TABLE [dbo].[Veineux] " +
                    "([Id]  BIGINT IDENTITY(1, 1) NOT NULL," +
                    "[PatientId]  BIGINT NOT NULL  ," +
                    "[HospitalisationId]  BIGINT NOT NULL  ," +
                    "[Edite]    NVARCHAR(20) NOT NULL ," +
                    "[Examenveineuxdesmembresinferieurs]   NVARCHAR(MAX)  NOT  NULL ," +
                    "[Examendesveinesjugulaires]  NVARCHAR(MAX) NOT   NULL ," +


                    "[Date_Ajout]       DATETIME      DEFAULT (getdate()) NOT NULL," +
                    "PRIMARY KEY CLUSTERED([Id] ASC));" +
                    "ALTER TABLE [dbo].[Veineux] ADD  CONSTRAINT fk_Patient_Veineux FOREIGN KEY  ([PatientId])  REFERENCES [dbo].[Patient] ([Id]) ;" +
                "ALTER TABLE [dbo].[Veineux] ADD  CONSTRAINT fk_Hospitalisation_Veineux FOREIGN KEY  ([HospitalisationId])  REFERENCES [dbo].[Hospitalisation] ([Id]) ;";

                ///scrip pour creer le tigrre
                //"USE Likitadb;" +

                //  "CREATE   TRIGGER ReferenceDossierPatient_Reference" +
                //  "ON dbo.DossierPatient" +
                //  "AFTER INSERT" +
                //  "AS" +
                //  "BEGIN" +
                //  "UPDATE dbo.DossierPatient" +
                //  " SET ReferenceDossier = CONCAT('D000', inserted.Id)" +
                //  "FROM dbo.DossierPatient" +
                //  " INNER JOIN inserted ON DossierPatient.Id = inserted.Id" +
                //  "END;";


                connection = Dbconnection.GetConnection();
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand(query, connection);
                await cmd.ExecuteNonQueryAsync();

                return new Message(true, "Table Veineux  create");
            }
            catch (Exception e)
            {
                return new Message(false, e.Message);
            }
            finally
            {
                connection.Close();
            }


        }
        public static async Task<Message> create_table_TaFc()
        {
            try
            {
                string query = "If not exists (select * from sysobjects where name = 'TaFc')" +
                    " CREATE TABLE [dbo].[TaFc] " +
                    "([Id]  BIGINT IDENTITY(1, 1) NOT NULL," +
                    "[PatientId]  BIGINT NOT NULL  ," +
                     "[HospitalisationId]  BIGINT NOT NULL  ," +
                    "[Edite]    NVARCHAR(20) NOT NULL ," +
                    "[FC]   NVARCHAR(MAX)  NOT  NULL ," +
                    "[TA]  NVARCHAR(MAX) NOT   NULL ," +


                    "[Date_Ajout]       DATETIME      DEFAULT (getdate()) NOT NULL," +
                    "PRIMARY KEY CLUSTERED([Id] ASC));" +
                    "ALTER TABLE [dbo].[TaFc] ADD  CONSTRAINT fk_Patient_TaFc FOREIGN KEY  ([PatientId])  REFERENCES [dbo].[Patient] ([Id]) ;" +
                "ALTER TABLE [dbo].[TaFc] ADD  CONSTRAINT fk_Hospitalisation_TaFc FOREIGN KEY  ([HospitalisationId])  REFERENCES [dbo].[Hospitalisation] ([Id]) ;";

                ///scrip pour creer le tigrre
                //"USE Likitadb;" +

                //  "CREATE   TRIGGER ReferenceDossierPatient_Reference" +
                //  "ON dbo.DossierPatient" +
                //  "AFTER INSERT" +
                //  "AS" +
                //  "BEGIN" +
                //  "UPDATE dbo.DossierPatient" +
                //  " SET ReferenceDossier = CONCAT('D000', inserted.Id)" +
                //  "FROM dbo.DossierPatient" +
                //  " INNER JOIN inserted ON DossierPatient.Id = inserted.Id" +
                //  "END;";


                connection = Dbconnection.GetConnection();
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand(query, connection);
                await cmd.ExecuteNonQueryAsync();

                return new Message(true, "Table TaFc  create");
            }
            catch (Exception e)
            {
                return new Message(false, e.Message);
            }
            finally
            {
                connection.Close();
            }


        }


        public static async Task<Message> create_table_ConclusionApCV()
        {
            try
            {
                string query = "If not exists (select * from sysobjects where name = 'ConclusionApCV')" +
                    " CREATE TABLE [dbo].[ConclusionApCV] " +
                    "([Id]  BIGINT IDENTITY(1, 1) NOT NULL," +
                    "[PatientId]  BIGINT NOT NULL  ," +
                    "[HospitalisationId]  BIGINT NOT NULL  ," +
                    "[Edite]    NVARCHAR(20) NOT NULL ," +
                    "[Conclusion]   NVARCHAR(MAX)  NOT  NULL ," +


                    "[Date_Ajout]       DATETIME      DEFAULT (getdate()) NOT NULL," +
                    "PRIMARY KEY CLUSTERED([Id] ASC));" +
                    "ALTER TABLE [dbo].[ConclusionApCV] ADD  CONSTRAINT fk_Patient_ConclusionApCV FOREIGN KEY  ([PatientId])  REFERENCES [dbo].[Patient] ([Id]) ;" +
                "ALTER TABLE [dbo].[ConclusionApCV] ADD  CONSTRAINT fk_Hospitalisation_ConclusionApCV FOREIGN KEY  ([HospitalisationId])  REFERENCES [dbo].[Hospitalisation] ([Id]) ;";

                ///scrip pour creer le tigrre
                //"USE Likitadb;" +

                //  "CREATE   TRIGGER ReferenceDossierPatient_Reference" +
                //  "ON dbo.DossierPatient" +
                //  "AFTER INSERT" +
                //  "AS" +
                //  "BEGIN" +
                //  "UPDATE dbo.DossierPatient" +
                //  " SET ReferenceDossier = CONCAT('D000', inserted.Id)" +
                //  "FROM dbo.DossierPatient" +
                //  " INNER JOIN inserted ON DossierPatient.Id = inserted.Id" +
                //  "END;";


                connection = Dbconnection.GetConnection();
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand(query, connection);
                await cmd.ExecuteNonQueryAsync();

                return new Message(true, "Table ConclusionApCV  create");
            }
            catch (Exception e)
            {
                return new Message(false, e.Message);
            }
            finally
            {
                connection.Close();
            }


        }

        public static async Task<Message> create_table_ModedeVie()
        {
            try
            {
                string query = "If not exists (select * from sysobjects where name = 'ModedeVie')" +
                    " CREATE TABLE [dbo].[ModedeVie] " +
                    "([Id]  BIGINT IDENTITY(1, 1) NOT NULL," +
                    "[PatientId]  BIGINT NOT NULL  ," +
                    "[HospitalisationId]  BIGINT NOT NULL  ," +
                    "[Edite]    NVARCHAR(20) NOT NULL ," +
                    "[Activitephysique]   NVARCHAR(20)  NOT  NULL ," +
                    "[Alcool]   NVARCHAR(20)  NOT  NULL ," +
                    "[fumee]   NVARCHAR(20)  NOT  NULL ," +
                    "[Regimediabetique]   NVARCHAR(20)  NOT  NULL ," +
                    "[Regimesansgraisse]   NVARCHAR(20)  NOT  NULL ," +


                    "[Date_Ajout]       DATETIME      DEFAULT (getdate()) NOT NULL," +
                    "PRIMARY KEY CLUSTERED([Id] ASC));" +
                    "ALTER TABLE [dbo].[ModedeVie] ADD  CONSTRAINT fk_Patient_ModedeVie FOREIGN KEY  ([PatientId])  REFERENCES [dbo].[Patient] ([Id]) ;" +
                "ALTER TABLE [dbo].[ModedeVie] ADD  CONSTRAINT fk_Hospitalisation_ModedeVie FOREIGN KEY  ([HospitalisationId])  REFERENCES [dbo].[Hospitalisation] ([Id]) ;";

                ///scrip pour creer le tigrre
                //"USE Likitadb;" +

                //  "CREATE   TRIGGER ReferenceDossierPatient_Reference" +
                //  "ON dbo.DossierPatient" +
                //  "AFTER INSERT" +
                //  "AS" +
                //  "BEGIN" +
                //  "UPDATE dbo.DossierPatient" +
                //  " SET ReferenceDossier = CONCAT('D000', inserted.Id)" +
                //  "FROM dbo.DossierPatient" +
                //  " INNER JOIN inserted ON DossierPatient.Id = inserted.Id" +
                //  "END;";


                connection = Dbconnection.GetConnection();
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand(query, connection);
                await cmd.ExecuteNonQueryAsync();

                return new Message(true, "Table ModedeVie  create");
            }
            catch (Exception e)
            {
                return new Message(false, e.Message);
            }
            finally
            {
                connection.Close();
            }


        }


        public static async Task<Message> create_table_Appareildigestif()
        {
            try
            {
                string query = "If not exists (select * from sysobjects where name = 'Appareildigestif')" +
                    " CREATE TABLE [dbo].[Appareildigestif] " +
                    "([Id]  BIGINT IDENTITY(1, 1) NOT NULL," +
                    "[PatientId]  BIGINT NOT NULL  ," +
                      "[HospitalisationId]  BIGINT NOT NULL  ," +
                    "[Edite]    NVARCHAR(20) NOT NULL ," +
                    "[Inspection]   NVARCHAR(MAX)  NOT  NULL ," +
                    "[Palpation]  NVARCHAR(MAX) NOT   NULL ," +
                    "[Percussion]  NVARCHAR(MAX) NOT  NULL ," +
                    "[Auscultation]  NVARCHAR(MAX) NOT  NULL ," +
                    "[Conclusion]   NVARCHAR(MAX)    NULL ," +
                    "[Date_Ajout]       DATETIME      DEFAULT (getdate()) NOT NULL," +
                    "PRIMARY KEY CLUSTERED([Id] ASC));" +
                    "ALTER TABLE [dbo].[Appareildigestif] ADD  CONSTRAINT fk_Patient_Appareildigestif FOREIGN KEY  ([PatientId])  REFERENCES [dbo].[Patient] ([Id]) ;" +
                "ALTER TABLE [dbo].[Appareildigestif] ADD  CONSTRAINT fk_Hospitalisation_Appareildigestif FOREIGN KEY  ([HospitalisationId])  REFERENCES [dbo].[Hospitalisation] ([Id]) ;";

                ///scrip pour creer le tigrre
                //"USE Likitadb;" +

                //  "CREATE   TRIGGER ReferenceDossierPatient_Reference" +
                //  "ON dbo.DossierPatient" +
                //  "AFTER INSERT" +
                //  "AS" +
                //  "BEGIN" +
                //  "UPDATE dbo.DossierPatient" +
                //  " SET ReferenceDossier = CONCAT('D000', inserted.Id)" +
                //  "FROM dbo.DossierPatient" +
                //  " INNER JOIN inserted ON DossierPatient.Id = inserted.Id" +
                //  "END;";


                connection = Dbconnection.GetConnection();
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand(query, connection);
                await cmd.ExecuteNonQueryAsync();

                return new Message(true, "Table Appareildigestif  create");
            }
            catch (Exception e)
            {
                return new Message(false, e.Message);
            }
            finally
            {
                connection.Close();
            }


        }


        public static async Task<Message> create_table_PoulPedieux()
        {
            try
            {
                string query = "If not exists (select * from sysobjects where name = 'PoulPedieux')" +
                    " CREATE TABLE [dbo].[PoulPedieux] " +
                    "([Id]  BIGINT IDENTITY(1, 1) NOT NULL," +
                    "[PatientId]  BIGINT NOT NULL  ," +
                         "[HospitalisationId]  BIGINT NOT NULL  ," +
                    "[Edite]    NVARCHAR(20) NOT NULL ," +
                    "[Pedieux]   NVARCHAR(MAX)  NOT  NULL ," +
                    "[Date_Ajout]       DATETIME      DEFAULT (getdate()) NOT NULL," +
                    "PRIMARY KEY CLUSTERED([Id] ASC));" +
                    "ALTER TABLE [dbo].[PoulPedieux] ADD  CONSTRAINT fk_Patient_PoulPedieux FOREIGN KEY  ([PatientId])  REFERENCES [dbo].[Patient] ([Id]) ;" +
                "ALTER TABLE [dbo].[PoulPedieux] ADD  CONSTRAINT fk_Hospitalisation_PoulPedieux FOREIGN KEY  ([HospitalisationId])  REFERENCES [dbo].[Hospitalisation] ([Id]) ;";

                ///scrip pour creer le tigrre
                //"USE Likitadb;" +

                //  "CREATE   TRIGGER ReferenceDossierPatient_Reference" +
                //  "ON dbo.DossierPatient" +
                //  "AFTER INSERT" +
                //  "AS" +
                //  "BEGIN" +
                //  "UPDATE dbo.DossierPatient" +
                //  " SET ReferenceDossier = CONCAT('D000', inserted.Id)" +
                //  "FROM dbo.DossierPatient" +
                //  " INNER JOIN inserted ON DossierPatient.Id = inserted.Id" +
                //  "END;";


                connection = Dbconnection.GetConnection();
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand(query, connection);
                await cmd.ExecuteNonQueryAsync();

                return new Message(true, "Table PoulPedieux  create");
            }
            catch (Exception e)
            {
                return new Message(false, e.Message);
            }
            finally
            {
                connection.Close();
            }


        }
        public static async Task<Message> create_table_EspacesReflexes()
        {
            try
            {
                string query = "If not exists (select * from sysobjects where name = 'EspacesReflexes')" +
                    " CREATE TABLE [dbo].[EspacesReflexes] " +
                    "([Id]  BIGINT IDENTITY(1, 1) NOT NULL," +
                    "[PatientId]  BIGINT NOT NULL  ," +
                           "[HospitalisationId]  BIGINT NOT NULL  ," +
                    "[Edite]    NVARCHAR(20) NOT NULL ," +
                    "[Intertrigo]   NVARCHAR(20)  NOT  NULL ," +
                    "[Plaiesdepieds]   NVARCHAR(20)  NOT  NULL ," +
                    "[Hyperkeratose]   NVARCHAR(20)  NOT  NULL ," +
                    "[Sensibilitetactileaumonofilament]   NVARCHAR(20)  NOT  NULL ," +
                    "[Deformationdepieds]   NVARCHAR(20)  NOT  NULL ," +
                    "[Intertrigointerorteils]   NVARCHAR(20)  NOT  NULL ," +
                    "[Achilleen]   NVARCHAR(20)  NOT  NULL ," +
                    "[Rotulien]   NVARCHAR(20)  NOT  NULL ," +
                    "[Date_Ajout]       DATETIME      DEFAULT (getdate()) NOT NULL," +
                    "PRIMARY KEY CLUSTERED([Id] ASC));" +
                    "ALTER TABLE [dbo].[EspacesReflexes] ADD  CONSTRAINT fk_Patient_EspacesReflexes FOREIGN KEY  ([PatientId])  REFERENCES [dbo].[Patient] ([Id]) ;" +
                "ALTER TABLE [dbo].[EspacesReflexes] ADD  CONSTRAINT fk_Hospitalisation_EspacesReflexes FOREIGN KEY  ([HospitalisationId])  REFERENCES [dbo].[Hospitalisation] ([Id]) ;";

                ///scrip pour creer le tigrre
                //"USE Likitadb;" +

                //  "CREATE   TRIGGER ReferenceDossierPatient_Reference" +
                //  "ON dbo.DossierPatient" +
                //  "AFTER INSERT" +
                //  "AS" +
                //  "BEGIN" +
                //  "UPDATE dbo.DossierPatient" +
                //  " SET ReferenceDossier = CONCAT('D000', inserted.Id)" +
                //  "FROM dbo.DossierPatient" +
                //  " INNER JOIN inserted ON DossierPatient.Id = inserted.Id" +
                //  "END;";


                connection = Dbconnection.GetConnection();
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand(query, connection);
                await cmd.ExecuteNonQueryAsync();

                return new Message(true, "Table EspacesReflexes  create");
            }
            catch (Exception e)
            {
                return new Message(false, e.Message);
            }
            finally
            {
                connection.Close();
            }


        }


        public static async Task<Message> create_table_Examencutane()
        {
            try
            {
                string query = "If not exists (select * from sysobjects where name = 'Examencutane')" +
                    " CREATE TABLE [dbo].[Examencutane] " +
                    "([Id]  BIGINT IDENTITY(1, 1) NOT NULL," +
                    "[PatientId]  BIGINT NOT NULL  ," +
                    "[HospitalisationId]  BIGINT NOT NULL  ," +
                    "[Edite]    NVARCHAR(20) NOT NULL ," +
                    "[Cutanee]   NVARCHAR(MAX)  NOT  NULL ," +
                    "[Date_Ajout]       DATETIME      DEFAULT (getdate()) NOT NULL," +
                    "PRIMARY KEY CLUSTERED([Id] ASC));" +
                    "ALTER TABLE [dbo].[Examencutane] ADD  CONSTRAINT fk_Patient_Examencutane FOREIGN KEY  ([PatientId])  REFERENCES [dbo].[Patient] ([Id]) ;" +
                "ALTER TABLE [dbo].[Examencutane] ADD  CONSTRAINT fk_Hospitalisation_Examencutane FOREIGN KEY  ([HospitalisationId])  REFERENCES [dbo].[Hospitalisation] ([Id]) ;";

                ///scrip pour creer le tigrre
                //"USE Likitadb;" +

                //  "CREATE   TRIGGER ReferenceDossierPatient_Reference" +
                //  "ON dbo.DossierPatient" +
                //  "AFTER INSERT" +
                //  "AS" +
                //  "BEGIN" +
                //  "UPDATE dbo.DossierPatient" +
                //  " SET ReferenceDossier = CONCAT('D000', inserted.Id)" +
                //  "FROM dbo.DossierPatient" +
                //  " INNER JOIN inserted ON DossierPatient.Id = inserted.Id" +
                //  "END;";


                connection = Dbconnection.GetConnection();
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand(query, connection);
                await cmd.ExecuteNonQueryAsync();

                return new Message(true, "Table Examencutane  create");
            }
            catch (Exception e)
            {
                return new Message(false, e.Message);
            }
            finally
            {
                connection.Close();
            }


        }


        public static async Task<Message> create_table_Examenbuccodentaire()
        {
            try
            {
                string query = "If not exists (select * from sysobjects where name = 'Examenbuccodentaire')" +
                    " CREATE TABLE [dbo].[Examenbuccodentaire] " +
                    "([Id]  BIGINT IDENTITY(1, 1) NOT NULL," +
                    "[PatientId]  BIGINT NOT NULL  ," +
                    "[HospitalisationId]  BIGINT NOT NULL  ," +
                    "[Edite]    NVARCHAR(20) NOT NULL ," +
                    "[Caries]   NVARCHAR(20)  NOT  NULL ," +
                    "[Gingivites]   NVARCHAR(20)  NOT  NULL ," +
                    "[Remarque]   NVARCHAR(MAX)  NOT  NULL ," +
                    "[Date_Ajout]       DATETIME      DEFAULT (getdate()) NOT NULL," +
                    "PRIMARY KEY CLUSTERED([Id] ASC));" +
                    "ALTER TABLE [dbo].[Examenbuccodentaire] ADD  CONSTRAINT fk_Patient_Examenbuccodentaire FOREIGN KEY  ([PatientId])  REFERENCES [dbo].[Patient] ([Id]) ;" +
                "ALTER TABLE [dbo].[Examenbuccodentaire] ADD  CONSTRAINT fk_Hospitalisation_Examenbuccodentaire FOREIGN KEY  ([HospitalisationId])  REFERENCES [dbo].[Hospitalisation] ([Id]) ;";

                ///scrip pour creer le tigrre
                //"USE Likitadb;" +

                //  "CREATE   TRIGGER ReferenceDossierPatient_Reference" +
                //  "ON dbo.DossierPatient" +
                //  "AFTER INSERT" +
                //  "AS" +
                //  "BEGIN" +
                //  "UPDATE dbo.DossierPatient" +
                //  " SET ReferenceDossier = CONCAT('D000', inserted.Id)" +
                //  "FROM dbo.DossierPatient" +
                //  " INNER JOIN inserted ON DossierPatient.Id = inserted.Id" +
                //  "END;";


                connection = Dbconnection.GetConnection();
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand(query, connection);
                await cmd.ExecuteNonQueryAsync();

                return new Message(true, "Table Examenbuccodentaire  create");
            }
            catch (Exception e)
            {
                return new Message(false, e.Message);
            }
            finally
            {
                connection.Close();
            }


        }

        public static async Task<Message> create_table_Examenduconduitauditifexterne()
        {
            try
            {
                string query = "If not exists (select * from sysobjects where name = 'Examenduconduitauditifexterne')" +
                    " CREATE TABLE [dbo].[Examenduconduitauditifexterne] " +
                    "([Id]  BIGINT IDENTITY(1, 1) NOT NULL," +
                    "[PatientId]  BIGINT NOT NULL  ," +
                          "[HospitalisationId]  BIGINT NOT NULL  ," +
                    "[Edite]    NVARCHAR(20) NOT NULL ," +
                    "[Otorrhee]   NVARCHAR(MAX)  NOT  NULL ," +

                    "[Remarque]   NVARCHAR(MAX)  NOT  NULL ," +
                    "[Date_Ajout]       DATETIME      DEFAULT (getdate()) NOT NULL," +
                    "PRIMARY KEY CLUSTERED([Id] ASC));" +
                    "ALTER TABLE [dbo].[Examenduconduitauditifexterne] ADD  CONSTRAINT fk_Patient_Examenduconduitauditifexterne FOREIGN KEY  ([PatientId])  REFERENCES [dbo].[Patient] ([Id]) ;" +
                "ALTER TABLE [dbo].[Examenduconduitauditifexterne] ADD  CONSTRAINT fk_Hospitalisation_Examenduconduitauditifexterne FOREIGN KEY  ([HospitalisationId])  REFERENCES [dbo].[Hospitalisation] ([Id]) ;";

                ///scrip pour creer le tigrre
                //"USE Likitadb;" +

                //  "CREATE   TRIGGER ReferenceDossierPatient_Reference" +
                //  "ON dbo.DossierPatient" +
                //  "AFTER INSERT" +
                //  "AS" +
                //  "BEGIN" +
                //  "UPDATE dbo.DossierPatient" +
                //  " SET ReferenceDossier = CONCAT('D000', inserted.Id)" +
                //  "FROM dbo.DossierPatient" +
                //  " INNER JOIN inserted ON DossierPatient.Id = inserted.Id" +
                //  "END;";


                connection = Dbconnection.GetConnection();
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand(query, connection);
                await cmd.ExecuteNonQueryAsync();

                return new Message(true, "Table Examenduconduitauditifexterne  create");
            }
            catch (Exception e)
            {
                return new Message(false, e.Message);
            }
            finally
            {
                connection.Close();
            }


        }
        public static async Task<Message> create_table_Examendelappareilgenital()
        {
            try
            {
                string query = "If not exists (select * from sysobjects where name = 'Examendelappareilgenital')" +
                    " CREATE TABLE [dbo].[Examendelappareilgenital] " +
                    "([Id]  BIGINT IDENTITY(1, 1) NOT NULL," +
                    "[PatientId]  BIGINT NOT NULL  ," +
                   "[HospitalisationId]  BIGINT NOT NULL  ," +
                    "[Edite]    NVARCHAR(20) NOT NULL ," +
                    "[Prepuce]   NVARCHAR(20)    NULL ," +
                    "[TR]   NVARCHAR(MAX)    NULL ," +
                    "[Vulve]   NVARCHAR(MAX)    NULL ," +
                    "[TV]   NVARCHAR(MAX)    NULL ," +
                    "[Remarque]   NVARCHAR(MAX)  NOT  NULL ," +
                    "[Date_Ajout]       DATETIME      DEFAULT (getdate()) NOT NULL," +
                    "PRIMARY KEY CLUSTERED([Id] ASC));" +
                    "ALTER TABLE [dbo].[Examendelappareilgenital] ADD  CONSTRAINT fk_Patient_Examendelappareilgenital FOREIGN KEY  ([PatientId])  REFERENCES [dbo].[Patient] ([Id]) ;" +
                "ALTER TABLE [dbo].[Examendelappareilgenital] ADD  CONSTRAINT fk_Hospitalisation_Examendelappareilgenital FOREIGN KEY  ([HospitalisationId])  REFERENCES [dbo].[Hospitalisation] ([Id]) ;";

                ///scrip pour creer le tigrre
                //"USE Likitadb;" +

                //  "CREATE   TRIGGER ReferenceDossierPatient_Reference" +
                //  "ON dbo.DossierPatient" +
                //  "AFTER INSERT" +
                //  "AS" +
                //  "BEGIN" +
                //  "UPDATE dbo.DossierPatient" +
                //  " SET ReferenceDossier = CONCAT('D000', inserted.Id)" +
                //  "FROM dbo.DossierPatient" +
                //  " INNER JOIN inserted ON DossierPatient.Id = inserted.Id" +
                //  "END;";


                connection = Dbconnection.GetConnection();
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand(query, connection);
                await cmd.ExecuteNonQueryAsync();

                return new Message(true, "Table Examendelappareilgenital  create");
            }
            catch (Exception e)
            {
                return new Message(false, e.Message);
            }
            finally
            {
                connection.Close();
            }


        }

        public static async Task<Message> create_table_Examenthyroidien()
        {
            try
            {
                string query = "If not exists (select * from sysobjects where name = 'Examenthyroidien')" +
                    " CREATE TABLE [dbo].[Examenthyroidien] " +
                    "([Id]  BIGINT IDENTITY(1, 1) NOT NULL," +
                    "[PatientId]  BIGINT NOT NULL  ," +
                   "[HospitalisationId]  BIGINT NOT NULL  ," +
                    "[Edite]    NVARCHAR(20) NOT NULL ," +
                    "[type]   NVARCHAR(20)  NOT   NULL ," +
                    "[Caracterisques]   NVARCHAR(MAX)   NOT  NULL ," +
                    "[Remarque]   NVARCHAR(MAX)  NOT  NULL ," +
                    "[Date_Ajout]      DATETIME      DEFAULT (getdate()) NOT NULL," +
                    "PRIMARY KEY CLUSTERED([Id] ASC));" +
                    "ALTER TABLE [dbo].[Examenthyroidien] ADD  CONSTRAINT fk_Patient_Examenthyroidien FOREIGN KEY  ([PatientId])  REFERENCES [dbo].[Patient] ([Id]) ;" +
                    "ALTER TABLE [dbo].[Examenthyroidien] ADD  CONSTRAINT fk_Hospitalisation_Examenthyroidien FOREIGN KEY  ([HospitalisationId])  REFERENCES [dbo].[Hospitalisation] ([Id]) ;";
                connection = Dbconnection.GetConnection();
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand(query, connection);
                await cmd.ExecuteNonQueryAsync();

                return new Message(true, "Table Examenthyroidien  create");
            }
            catch (Exception e)
            {
                return new Message(false, e.Message);
            }
            finally
            {
                connection.Close();
            }


        }

        public static async Task<Message> create_table_RendezVousPatient()
        {
            try
            {
                string query = "If not exists (select * from sysobjects where name = 'RendezVousPatient')" +
                    " CREATE TABLE [dbo].[RendezVousPatient] " +
                    "([Id]  BIGINT IDENTITY(1, 1) NOT NULL," +
                    "[IdDocteur]  BIGINT NOT NULL  ," +
                    "[IdPatient]  BIGINT NOT NULL  ," +
                    "[IdService]  BIGINT NOT NULL  ," +
                    "[DateRendeZ]    NVARCHAR(100) NOT NULL ," +
                    "[Heure]   NVARCHAR(200)  NOT   NULL ," +
                    "[Status]   NVARCHAR(200)  NOT   NULL ," +
                    "[Motif]   NVARCHAR(MAX)   NOT  NULL ," +
                    "[Date_Ajout]      DATETIME      DEFAULT (getdate()) NOT NULL," +
                    "PRIMARY KEY CLUSTERED([Id] ASC));" +
                    "ALTER TABLE [dbo].[RendezVousPatient] ADD  CONSTRAINT fk_Patient_RendezVousPatient FOREIGN KEY  ([IdPatient])  REFERENCES [dbo].[Patient] ([Id]) ;" +
                    "ALTER TABLE [dbo].[RendezVousPatient] ADD  CONSTRAINT fk_Docteur_RendezVousPatient FOREIGN KEY  ([IdDocteur])  REFERENCES [dbo].[Medecin] ([Id]) ;" +
                    "ALTER TABLE [dbo].[RendezVousPatient] ADD  CONSTRAINT fk_Service_RendezVousPatient FOREIGN KEY  ([IdService])  REFERENCES [dbo].[Service] ([Id]) ;"
                    ;
                connection = Dbconnection.GetConnection();
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand(query, connection);
                await cmd.ExecuteNonQueryAsync();

                return new Message(true, "Table fk_Service_RendezVousPatient  create");
            }
            catch (Exception e)
            {
                return new Message(false, e.Message);
            }
            finally
            {
                connection.Close();
            }


        }
        public static async Task<Message> create_table_Consultation()
        {
            try
            {
                string query = "If not exists (select * from sysobjects where name = 'Consultation')" +
                    " CREATE TABLE [dbo].[Consultation] " +
                    "([Id]  BIGINT IDENTITY(1, 1) NOT NULL," +
                    "[IdPatient]  BIGINT NOT NULL  ," +
                    "[IdPriseEncharge]  BIGINT  NULL  ," +
                    "[IdConsultation]  BIGINT NOT NULL  ," +
                    "[IdMedecin]  BIGINT NOT NULL  ," +
                    "[Idservice]  BIGINT NOT NULL  ," +
                    "[Date]    NVARCHAR(100) NOT NULL ," +
                    "[Nom]    NVARCHAR(100) NOT NULL ," +
                    "[Heure]   NVARCHAR(200)  NOT   NULL ," +
                    "[Status]   NVARCHAR(200)  NOT   NULL ," +
                    "[Date_Modification]    NVARCHAR(200)  NOT  NULL ," +
                    "[Date_Ajout]      DATETIME      DEFAULT (getdate()) NOT NULL," +
                    "PRIMARY KEY CLUSTERED([Id] ASC));" +
                    "ALTER TABLE [dbo].[Consultation] ADD  CONSTRAINT fk_Patient_Consultation FOREIGN KEY  ([IdPatient])  REFERENCES [dbo].[Patient] ([Id]) ;" +
                    "ALTER TABLE [dbo].[Consultation] ADD  CONSTRAINT fk_Consultation_Consultation FOREIGN KEY  ([IdConsultation])  REFERENCES [dbo].[PrixConsultation] ([Id]) ;" +
                    "ALTER TABLE [dbo].[Consultation] ADD  CONSTRAINT fk_Docteur_Consultation FOREIGN KEY  ([IdMedecin])  REFERENCES [dbo].[Medecin] ([Id]) ;" +
                    "ALTER TABLE [dbo].[Consultation] ADD  CONSTRAINT fk_Service_Consultation FOREIGN KEY  ([Idservice])  REFERENCES [dbo].[Service] ([Id]) ;"


                ;
                connection = Dbconnection.GetConnection();
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand(query, connection);
                await cmd.ExecuteNonQueryAsync();

                return new Message(true, "Table Consultation  create");
            }
            catch (Exception e)
            {
                return new Message(false, e.Message);
            }
            finally
            {
                connection.Close();
            }


        }
        public static async Task<Message> create_table_ActmedicalPatient()
        {
            try
            {
                string query = "If not exists (select * from sysobjects where name = 'ActmedicalPatient')" +
                    " CREATE TABLE [dbo].[ActmedicalPatient] " +
                    "([Id]  BIGINT IDENTITY(1, 1) NOT NULL," +
                    "[PatientId]  BIGINT NOT NULL  ," +
                    "[HospitalisationId]  BIGINT NOT NULL  ," +
                    "[PriseEnchargeId]  BIGINT NULL  ," +
                    "[MedecinsdemandeId]  BIGINT NOT NULL  ," +
                    "[MedecinExcuteurId]  BIGINT NOT NULL  ," +
                    "[Prix]  BIGINT NOT NULL  ," +
                    "[ActeMedicalId]  BIGINT NOT NULL  ," +
                    "[Rang]  BIGINT NOT NULL  ," +
                    "[DatePour]    NVARCHAR(100) NOT NULL ," +
                    "[Payer]    NVARCHAR(100) NOT NULL ," +
                    "[Heure]   NVARCHAR(200)  NOT   NULL ," +
                    "[Status]   NVARCHAR(200)  NOT   NULL ," +
                    "[Remarque]   NVARCHAR(MAX)   NOT  NULL ," +
                    "[Date_Ajout]      DATETIME      DEFAULT (getdate()) NOT NULL," +
                    "PRIMARY KEY CLUSTERED([Id] ASC));" +
                    "ALTER TABLE [dbo].[ActmedicalPatient] ADD  CONSTRAINT fk_Patient_ActmedicalPatient FOREIGN KEY  ([PatientId])  REFERENCES [dbo].[Patient] ([Id]) ;" +
                    "ALTER TABLE [dbo].[ActmedicalPatient] ADD  CONSTRAINT fk_DocteurP_ActmedicalPatient FOREIGN KEY  ([MedecinsdemandeId])  REFERENCES [dbo].[Medecin] ([Id]) ;" +
                    "ALTER TABLE [dbo].[ActmedicalPatient] ADD  CONSTRAINT fk_DocteurE_ActmedicalPatient FOREIGN KEY  ([MedecinExcuteurId])  REFERENCES [dbo].[Medecin] ([Id]) ;" +
                    "ALTER TABLE [dbo].[ActmedicalPatient] ADD  CONSTRAINT fk_Service_ActmedicalPatient FOREIGN KEY  ([ActeMedicalId])  REFERENCES [dbo].[ActeMedical] ([Id]) ;" +
                    "ALTER TABLE [dbo].[ActmedicalPatient] ADD  CONSTRAINT fk_Hospitalisation_ActmedicalPatient FOREIGN KEY  ([HospitalisationId])  REFERENCES [dbo].[Hospitalisation] ([Id]) ;"


                ;
                connection = Dbconnection.GetConnection();
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand(query, connection);
                await cmd.ExecuteNonQueryAsync();

                return new Message(true, "Table ActmedicalPatient  create");
            }
            catch (Exception e)
            {
                return new Message(false, e.Message);
            }
            finally
            {
                connection.Close();
            }


        }


        public static async Task<Message> create_table_Admission()
        {
            try
            {
                string query = "If not exists (select * from sysobjects where name = 'Admission')" +
                    " CREATE TABLE [dbo].[Admission] " +
                    "([Id]  BIGINT IDENTITY(1, 1) NOT NULL," +
                    "[IdPatient]  BIGINT NOT NULL  ," +
                    "[IdMotif]  BIGINT  NULL  ," +
                    "[IdMedecin]  BIGINT NOT NULL  ," +
                    "[IdService]  BIGINT NOT NULL  ," +
                    "[IdAgent]  BIGINT NOT NULL  ," +
                    "[TypeHopitale]    NVARCHAR(100) NOT NULL ," +
                    "[Date_Ajout]      NVARCHAR(200) NOT NULL," +
                    "PRIMARY KEY CLUSTERED([Id] ASC));";
                ;
                connection = Dbconnection.GetConnection();
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand(query, connection);
                await cmd.ExecuteNonQueryAsync();

                return new Message(true, "Table Admission  create");
            }
            catch (Exception e)
            {
                return new Message(false, e.Message);
            }
            finally
            {
                connection.Close();
            }


        }
      









    }
}
