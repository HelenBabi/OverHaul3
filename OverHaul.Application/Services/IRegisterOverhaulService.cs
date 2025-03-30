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
    Task<JsonResponse> AddAsync(RegisterOverhaulModel model, CancellationToken ct);

}

public class RgisterOverhaulService(IUnitOfWork _unitOfWork) : IRegisterOverhaulService

{
    public async Task<JsonResponse> AddAsync(RegisterOverhaulModel model, CancellationToken ct)
    {
       var _registerOverhaulRepository = _unitOfWork.Repository<RegisterOverhaul>();

        if (_registerOverhaulRepository.GetQuery().Any(x => x.Nationality == model.Nationality))
        {
            return new JsonResponse
            {
                IsSuccess = false,
                Message = "با این کدملی قبلا ثبت نام شده است"
            };

        }
        model.State = true;
        var entity = model.Map();
        await _registerOverhaulRepository.AddAsync(entity, ct);
        _unitOfWork.SaveChanges();
        return new JsonResponse
        {
            IsSuccess = true,
            Data = entity
        };
    }
}

