using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Overhaul.Domain.Entities;

namespace Overhaul.Domain.Repositories;

public interface IAppUserRepository
{
    void Add(AppUser appUser);
    void Update(AppUser appUser);
    Task<int> SaveChangeAsync();
}
