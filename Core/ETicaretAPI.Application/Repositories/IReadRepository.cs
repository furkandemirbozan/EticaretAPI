using ETicaretAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Repositories
{
    //Read den kasıtımız   veri tabanından sorgu talep edeceksem buraya yazıcam
    public interface IReadRepository<T> : IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll(bool tracking = true);
        IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true);

        Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true);//FirstOrDefault un async olarak kullanıcak
        Task<T> GetByIdAsync(string id, bool tracking = true);//asenkron olarak çalışacaklar

    }
}



//NOTLAR

//Tracking leri true verdim çünkü bu metotlarda changtracking mekanizmasının takip etmesini istiyorum,Default olarak geliyor ancak opimize etmek için istediğim yererde false yapabilirim