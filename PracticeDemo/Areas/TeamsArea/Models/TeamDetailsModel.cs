using Microsoft.AspNetCore.Mvc;

namespace PracticeDemo.Areas.TeamsArea.Models
{
    
    public class TeamDetailsModel
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public string Batch { get; set; }
        public int TeamCompanyId { get; set; }

    }
}
