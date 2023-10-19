using CrowdFunding.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdFunding.DAL.Repositories
{
    public interface IDonnerRepository
    {
        void Don(DonnerEntity don);

        IEnumerable<DonnerEntity> GetAll();
    }
}
