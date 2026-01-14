using TrabalhoLab.Data.Migrations;

namespace TrabalhoLab.Models
{
    public class Report
    {
        public int ReportId { get; set; }
        public string Description { get; set; }
        public string ReportResult { get; set; }
        public DateTime ReportDate { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
