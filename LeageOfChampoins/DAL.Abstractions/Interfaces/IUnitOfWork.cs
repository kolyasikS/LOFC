using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Absractions.Interfaces
{
    public interface IUnitOfWork
    {
        Task SaveAsync();
        void Dispose();
        void CreateTransaction();
        void RollBack();
        void Commit();
    }
}
