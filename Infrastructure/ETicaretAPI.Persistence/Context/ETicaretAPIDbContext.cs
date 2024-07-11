using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ETicaretAPI.Persistence.Context
{
    public class ETicaretAPIDbContext : DbContext
    {
        public ETicaretAPIDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }

        //Interceptor demek bir işlem başlamadan yada veya sonlandıktan sonra araya girerek ekstra işlemler yapabilirim
        //Interceptor oluşturdum ve
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)//SaveChanges override ettim
        {
            //ChangeTracker : Entity ler tarafından yapılan değişiklerin veya eklnen  verilerin yakalanmasını sağlayan bir prop dur. Update Track edilen verileri ekde etmemizi sağlar
            var datas = ChangeTracker.Entries<BaseEntity>();//Entries<BaseEntity>() metodu, bu bağlamda takip edilen tüm BaseEntity türü Bu, eklendiğinde, güncellendiğinde veya silindiğinde takip edilir.

            foreach (var data in datas)//ChangeTracker'dan alınan bu entity nesneleri üzerinden iterasyon yapar. Her bir iterasyonda, data değişkeni BaseEntity türünden bir entity'yi temsil eder.
            {
                //atama işlemi yapmadım(discard yöntemi)
                _ = data.State switch
                {
                    //Bu kodda, iki durum kontrol edilir: EntityState.Added ve EntityState.Modified
                    EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
                    EntityState.Modified => data.Entity.UpdateDate = DateTime.UtcNow,
                    _=> DateTime.UtcNow

                };
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
        //public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        //{
        //    // ChangeTracker : Entity ler tarafından yapılan değişiklerin veya eklnen verilerin yakalanmasını sağlayan bir prop dur. Update Track edilen verileri ekde etmemizi sağlar
        //    var datas = ChangeTracker.Entries<BaseEntity>();//Entries<BaseEntity>() metodu, bu bağlamda takip edilen tüm BaseEntity türü Bu, eklendiğinde, güncellendiğinde veya silindiğinde takip edilir.

        //    foreach (var data in datas)//ChangeTracker'dan alınan bu entity nesneleri üzerinden iterasyon yapar. Her bir iterasyonda, data değişkeni BaseEntity türünden bir entity'yi temsil eder.
        //    {
        //        switch (data.State)
        //        {
        //            case EntityState.Added:
        //                data.Entity.CreatedDate = DateTime.UtcNow;
        //                break;
        //            case EntityState.Modified:
        //                data.Entity.UpdateDate = DateTime.UtcNow;
        //                break;
        //            default:
        //                break;
        //        }
        //    }

        //    return await base.SaveChangesAsync(cancellationToken);
        //}


    }
}
