using Microsoft.AspNetCore.Mvc.Rendering;
using Overhaul.Application.AutoFac;
using Overhaul.Application.Contracts;
using Overhaul.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overhaul.Application.Services;

public interface IIsargariTypesServise:IScopedDependency
{
    SelectList GetSelectList(object selectedValue = null);
}
public class IsargariTypesServise(IRepository<IsargariType> repository) : IIsargariTypesServise
{
    public SelectList GetSelectList(object selectedValue = null)
    {
        var query = repository.GetQuery();

        var list = query
           .Select(x => new { x.Id,   x.Title })
           .ToList();

        return new SelectList(list, "Id", "Title", selectedValue);
    }
}
