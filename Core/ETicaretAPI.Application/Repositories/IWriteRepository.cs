using ETicaretAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Repositories
{
    //Crud işlemleri burada olacak.Yani update delete insert falan 
    public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity//class olma garantisi yerine BaseEntity verdim
    {
        //bool vermedeki sebep eklediysem yada sildiysem yada güncellediysem sonuç true yada false dönecek
        Task<bool> AddAsync(T model);
        Task<bool> AddRangeAsync(List<T> datas);//Birden fazla veri ekleme
        bool Remove(T model);
        Task<bool> RemoveAsync(string id);
        bool RemoveRange(List<T> datas);
        bool Update(T model);


        Task<int> SaveAsync();

    }
}
