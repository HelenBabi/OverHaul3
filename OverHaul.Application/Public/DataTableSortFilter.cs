using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace Overhaul.Application.Public
{
    public static class DataTableSortFilter
    {
        public static IQueryable<T> SortRowsPerRequestParamters<T>(
          IQueryable<T> iQueryableOfAnonymous, DataTables.AspNet.Core.IDataTablesRequest request)
        {
            if (request != null && request.Columns != null && request.Columns.Any())
            {
                var sortingColumns = request.Columns.Where(p => p.IsSortable && p.Sort != null).OrderBy(p => p.Sort.Order).ToList();
                Type objType = typeof(T);
                var ListOfAllPropertiesInT = objType.GetProperties().Select(p => p.Name).ToList();

                if (sortingColumns != null && sortingColumns.Count > 0)
                {
                    string dynamicLinqOrder = string.Empty;
                    bool isFirstString = true;

                    for (int i = 0; i < sortingColumns.Count; i++)
                    {
                        var column = sortingColumns[i];
                        if (ListOfAllPropertiesInT.Contains(column.Name))
                        {
                            if (isFirstString)
                            {
                                isFirstString = false;
                            }
                            else
                            {
                                dynamicLinqOrder += ", ";
                            }

                            dynamicLinqOrder += column.Name;
                            if (column.Sort.Direction == DataTables.AspNet.Core.SortDirection.Descending)
                            {
                                dynamicLinqOrder += " desc";
                            }
                        }
                    }

                    if (dynamicLinqOrder.Length > 0)
                    {
                        iQueryableOfAnonymous = iQueryableOfAnonymous.OrderBy(dynamicLinqOrder);
                    }
                };
            }

            return iQueryableOfAnonymous;
        }
        public static IQueryable<T> FilterRowsPerRequestParameters<T>(
            IQueryable<T> iQueryableOfAnonymous, DataTables.AspNet.Core.IDataTablesRequest request)
        {

            if (request != null && request.Search != null && !System.String.IsNullOrEmpty(request.Search.Value))
            {
                string pattern = request.Search.Value?.Trim() ?? System.String.Empty;

                var searchingColumns = request.Columns.Where(p => p.IsSearchable).ToList();
                var config = new ParsingConfig { ResolveTypesBySimpleName = true };

                Type objType = typeof(T);
                var ListOfAllPropertiesInT = objType.GetProperties().Select(p => p.Name).ToList();

                if (searchingColumns.Count > 0)
                {
                    string dynamicLinqSearch = string.Empty;
                    bool isFirstString = true;

                    for (int i = 0; i < searchingColumns.Count; i++)
                    {
                        var column = searchingColumns[i];
                        if (ListOfAllPropertiesInT.Contains(column.Name))
                        {
                            if (isFirstString)
                            {
                                isFirstString = false;
                            }
                            else
                            {
                                dynamicLinqSearch += " or ";
                            }

                            dynamicLinqSearch += $"""({column.Name} != null && {column.Name}.ToString().Contains("{pattern}"))""";
                            //dynamicLinqSearch += $"""{column.Name.ToString()}.ToString().Contains("{pattern}")""";
                        }
                    }

                    if (dynamicLinqSearch.Length > 0)
                    {
                        iQueryableOfAnonymous = iQueryableOfAnonymous.Where(config, dynamicLinqSearch);
                    }
                }
            }

            return iQueryableOfAnonymous;
        }
    }
}
