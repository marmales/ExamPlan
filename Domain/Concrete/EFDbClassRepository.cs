using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Abstract;
namespace Domain.Concrete
{
    public class EFDbClassRepository : IClassRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Class> Classes { get { return context.Classes; } }
    }
}
