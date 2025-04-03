using Overhaul.Application.AutoFac;
using Overhaul.Application.Contracts;
using Overhaul.Application.Models;
using Overhaul.Application.Public;
using Overhaul.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overhaul.Application.Services;

public interface IRegisterOverhaulService : IScopedDependency
{
    Task<string> AddAsync(RegisterOverhaulModel model, CancellationToken ct);

}

public class RgisterOverhaulService(IUnitOfWork _unitOfWork) : IRegisterOverhaulService

{
    public async Task<string> AddAsync(RegisterOverhaulModel model, CancellationToken ct)
    {
       var _registerOverhaulRepository = _unitOfWork.Repository<RegisterOverhaul>();


        if (_registerOverhaulRepository.GetQuery().Any(x => x.Nationality == model.Nationality))
        {
            return "این کد ملی قبلا ثبت شده است";

        }
        Random rnd = new Random();
        var newId = rnd.Next(111111, 999999);

        model.State = true;
        var entity = model.Map();
        entity.Id = newId;
        await _registerOverhaulRepository.AddAsync(entity, ct);
        _unitOfWork.SaveChanges();
        return newId.ToString();  
    }
}

