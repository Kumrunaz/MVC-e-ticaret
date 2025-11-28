using MVC_Kutuphane_Otomasyonu.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Kutuphane_Otomasyonu.Entities.Repository
{
    public class GenericRepository<TContext,TEntity>:IGenericRepository<TContext,TEntity>
          where TContext : DbContext, new()
        where TEntity : class, new()
    {
        //Metotlarımız
        public void Delete(TContext context, Expression<Func<TEntity, bool>> filter)
        {
            var model = context.Set<TEntity>().FirstOrDefault(filter);

            context.Set<TEntity>().Remove(model);
        }

        public List<TEntity> GetAll(TContext context, Expression<Func<TEntity, bool>> filter = null, string tbl = null)
        {
            return filter == null ? tbl==null ? context.Set<TEntity>().ToList() :  context.Set<TEntity>().Include(tbl).ToList()
                

               :tbl==null ? context.Set<TEntity>().Where(filter).ToList(): context.Set<TEntity>().Include(tbl).Where(filter).ToList();
        }

        public TEntity GetByFiltre(TContext context, Expression<Func<TEntity, bool>> filter, string tbl = null)
        {
            return tbl==null ?  context.Set<TEntity>().FirstOrDefault(filter) : context.Set<TEntity>().Include(tbl).FirstOrDefault(filter);
        }

        public TEntity GetById(TContext context, int? id) //Id'e göre tek kayıt arama metodu
        {
            return context.Set<TEntity>().Find(id);
        }

        public void InsertorUpdate(TContext context, TEntity entity) //Ekleme ve güncelleme metodumuz
        {
            context.Set<TEntity>().AddOrUpdate(entity);
        }

        public void Save(TContext context) // Veritabanına kaydetme
        {
            try
            {
                // DEBUG: context'teki tüm Roller kayıtlarını yazdır
                foreach (var entry in context.ChangeTracker.Entries())
                {
                    var roller = entry.Entity as MVC_Kutuphane_Otomasyonu.Entities.Model.Roller;
                    if (roller != null)
                    {
                        System.Diagnostics.Debug.WriteLine(
                            $"ENTRY Roller -> State={entry.State}, Id={roller.Id}, Rol='{(roller.Rol ?? "NULL")}'"
                        );
                    }
                }

                // Asıl kaydetme
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine($"Property: {ve.PropertyName}, Error: {ve.ErrorMessage}");
                    }
                }
                throw; // hatayı yukarı fırlat ki görelim
            }
        }

    }
}
