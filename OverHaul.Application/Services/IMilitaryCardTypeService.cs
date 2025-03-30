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

public interface IMilitaryCardTypeService : IScopedDependency
{
    SelectList GetSelectList( object selectedValue = null);

}
public class MilitaryCardTypeService(IRepository<MilitaryCardType> repository) : IMilitaryCardTypeService
{
    public SelectList GetSelectList( object selectedValue = null)
    {
        var query = repository.GetQuery();
       
        var list = query
           .Select(x => new { x.Id, Title =  x.MilitaryName })
           .ToList();

        return new SelectList(list, "Id", "Title", selectedValue);
    }
}
