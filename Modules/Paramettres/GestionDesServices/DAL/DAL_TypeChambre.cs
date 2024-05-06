using HPRBackend.Modules.Paramettres.GestionDesServices.Models;
using HPRBackend.Modules.shard;
using Microsoft.EntityFrameworkCore;

namespace HPRBackend.Modules.Paramettres.GestionDesServices.DAL
{
    public class DAL_TypeChambre
    {
        public readonly DataBaseContext serviceContext;
        public DAL_TypeChambre(DataBaseContext serviceContext)
        {
            this.serviceContext = serviceContext;
        }

        public async Task<Message> Add(TypeChambre TypeChambre)
        {
            try
            {

                this.serviceContext.TypeChambre.Add(TypeChambre);
                this.serviceContext.SaveChanges();

                return new Message(true, "TypeChambre ajouté  avec succées  ");



            }
            catch (DbUpdateException e)
            {

                if (e.InnerException.Message.Contains("Violation of UNIQUE KEY constraint "))
                {
                    return new Message(false, " un TypeChambre  de meme nom existe");


                }

                return new Message(false, "erreur :" + e.Message);

            }
        }

        public async Task<Message> Update(TypeChambre TypeChambre)
        {
            try
            {

                this.serviceContext.TypeChambre.Update(TypeChambre);
                this.serviceContext.SaveChanges();

                return new Message(true, "TypeChambre ajouté  avec succées  ");



            }
            catch (DbUpdateException e)
            {

                if (e.InnerException.Message.Contains("Violation of UNIQUE KEY constraint "))
                {
                    return new Message(false, " un TypeChambre  de meme nom existe");


                }

                return new Message(false, "erreur :" + e.Message);

            }
        }


        public async Task<List<TypeChambre>> GetAll()
        {
            await Migrations.create_table_TypeChambre();
            var TypeChambre = await this.serviceContext.TypeChambre.ToListAsync();
            return TypeChambre;
        }

        public TypeChambre GetById(long Id)
        {


            var Service = this.serviceContext.TypeChambre.Where(m =>
            m.Id == Id


             )
                .ToList()[0];
            return Service;
        }


        public async Task<Message> Delete(long Id)
        {
            try
            {
                var T = GetById(Id);
                if (T != null)
                {
                    this.serviceContext.TypeChambre.Remove(T);
                    await this.serviceContext.SaveChangesAsync();

                }



                return new Message(true, "TypeChambre Supprimée   avec succées  ");



            }
            catch (Exception ex)
            {
                return new Message(false, "erreur :" + ex.Message);
            }
        }


    }
}
