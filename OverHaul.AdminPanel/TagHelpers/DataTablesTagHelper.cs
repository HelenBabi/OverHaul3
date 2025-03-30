using Microsoft.AspNetCore.Razor.TagHelpers;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System.ComponentModel;
using System.Reflection;
using System.Text;

[HtmlTargetElement("datatables-grid")]
public class DataTableTagHelper : TagHelper
{
    public string Id { get; set; } = "DataTablesGrid";
    public string Url { get; set; }
    public string StartDateElement { get; set; } = "StartDate";
    public string EndDateElement { get; set; } = "EndDate";
    public string FilterButtonId { get; set; } = "GridFilterButton";
    public string LanguageUrl { get; set; } = "/lib/dataTables/fa.json";

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {

        
        output.TagName = "div";
        output.Attributes.SetAttribute("class", "row");

        var tableHtml = new StringBuilder();
        tableHtml.Append($@"
                <div class='col-12'>
                    <table id='{Id}' class='table table-striped table-bordered' style='text-align:center;'></table>
                </div>
            ");

        output.Content.SetHtmlContent(tableHtml.ToString());

        var script = $@"
                <script type=""text/javascript"">

    document.addEventListener(""DOMContentLoaded"", FillGrid);

    function FillGrid() {{

        MyGrid = {{
            dt: null,

            init: function () {{
                dt = $('#DataTablesGrid').DataTable({{
                    processing: true, // غیرفعال کردن پردازش اولیه
                    // deferLoading: 1, // جلوگیری از لود اولیه داده‌ها
                    scrollX: true,
                    paging: true,
                    info: true,
                    ordering: true,
                    searching: true,
                    buttons: false,
                    search: {{
                        return: true
                    }},
                    autoWidth: false,
                    lengthMenu: [10, 15, 25, 50, 100],
                    pageLength: 15,

                    order: [[1, 'asc']],
                    serverSide: true,

                    ajax: {{
                        url: ""{ Url}"",
                        type: ""POST"",
                        datatype: ""json"",
                        data: function (data) {{
                            data.startDate = $('#StartDate').val();
                            data.endDate = $('#EndDate').val();
                        }}
                    }},
                  
                    language:
                    {{
                        url: '/lib/dataTables/fa.json',

                    }},
                    columns: [
                        {{/*  0 */
                            name: 'Id', data: 'id', title: ""کد"", orderable: true, searchable: false, type: 'num', visible: false
                        }},
                        {{/*  1 */
                            name: 'RestaurantTitle', data: 'restaurantTitle', title: ""رستوران"", orderable: true, searchable: true, type: 'string', visible: true
                        }},
                        {{/*  2 */
                            name: 'MealTypeTitle', data: ""mealTypeTitle"", title: ""وعده"", orderable: true, searchable: true, type: 'string', visible: true
                        }},
                        {{/*  3 */
                            name: 'CompanyName', data: ""companyName"", title: ""شرکت"", orderable: true, searchable: true, type: 'num-string', visible: true
                        }},
                        {{/*  4 */
                            name: 'ContractName', data: 'contractName', title: ""قرارداد"", orderable: true, searchable: false, type: 'string', visible: true
                        }},
                        {{/*  5 */
                            name: 'ReserveTimeTitle', data: 'reserveTimeTitle', title: ""ساعت"", orderable: true, searchable: false, type: 'string', visible: true
                        }},
                        {{/*  6 */
                            name: 'PersenelCode', data: 'persenelCode', title: ""کد پرسنل"", orderable: true, searchable: false, type: 'string', visible: true
                        }},
                        {{/*  7 */
                            name: 'PersenelNameFamily', data: 'persenelNameFamily', title: ""نام پرسنل"", orderable: true, searchable: false, type: 'string', visible: true
                        }},
                        {{/*  8 */
                            name: 'ServeTypeTitle', data: 'serveTypeTitle', title: ""سرو"", orderable: true, searchable: false, type: 'string', visible: true
                        }},
                        {{/*  9 */
                            name: 'ReserveTypeTitle', data: 'reserveTypeTitle', title: ""نوع رزرو"", orderable: true, searchable: false, type: 'string', visible: true
                        }},
                        {{/*  10 */
                            name: 'FoodCount', data: 'foodCount', title: ""تعداد"", orderable: true, searchable: false, type: 'num', visible: true
                        }},
                        {{/*  11 */
                            name: 'ReserveDate', data: 'reserveDate', title: ""تاریخ رزرو"", orderable: true, searchable: false, type: 'string', visible: true
                        }},


                    ],
                    lengthMenu: [10, 15, 25, 50, 100],
                }});
            }},

            refresh: function () {{
                dt.ajax.reload();
            }}
        }}

        // Advanced Search Modal Search button click handler
        $('#GridFilterButton').on(""click"", MyGrid.refresh);

        // initialize the datatables
        MyGrid.init();
    }}



</script>


            ";

        output.PostContent.SetHtmlContent(script);
    }
}