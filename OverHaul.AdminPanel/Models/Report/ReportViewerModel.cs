namespace Overhaul.AdminPanel.Models.Report
{
    public class ReportViewerModel
    {
        public string ReportFileName { get; set; }
        public string ReportTitle { get; set; }
        public Dictionary<string, object> DataSets { get; set; }
        public Dictionary<string, object> Variables { get; set; }
    }
}
