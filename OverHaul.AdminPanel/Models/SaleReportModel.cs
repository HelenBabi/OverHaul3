using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Overhaul.AdminPanel.Models
{
    public class SaleReportModel
    {

        [Required(ErrorMessage = "این فیلد الزامی میباشد")]
        [DisplayName("از تاریخ")]
        public string StartDate { get; set; }

        [Required(ErrorMessage = "این فیلد الزامی میباشد")]
        [DisplayName("تا تاریخ")]
        public string EndDate { get; set; }

        [Required(ErrorMessage = "این فیلد الزامی میباشد")]
        [DisplayName("رستوران")]
        public int RestaurantId { get; set; }

        [Required(ErrorMessage = "این فیلد الزامی میباشد")]
        [DisplayName("محل فروش")]
        public int SaleGatewayId { get; set; }

        [DisplayName("شرکت")]
        public int PersonalGroupId { get; set; }
    }
    public class MaterialSegregationReport : SaleReportModel
    {

    }
}
