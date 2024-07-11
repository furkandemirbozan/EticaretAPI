using ETicaretAPI.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Repositories
{
    //GenericRepository de genelde koşul olarak where T :class yapılır ancak burada bir id gelecek ve o id yi Bu repository de tanıması için bizi class dan değil BaseEntity yazmamız gerekli
    public interface IRepository<T> where T : BaseEntity
    {
        DbSet<T> Table {  get; }
    }
}
