using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overhaul.Application.Public;

public class PagedList<T>
{
    public IEnumerable<T>? Items { get; set; }
    public int PageSize { get; set; } = 10;
    public int TotalCount { get; set; }
    public int PageNumber { get; set; } = 1;
}
