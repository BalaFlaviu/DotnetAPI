namespace DotnetAPI.Models
{
    public partial class UserJobInfo
    {
        public int UserId { get; set; }
        public string JobInfo { get; set; }
        public string Department { get; set; }

        public UserJobInfo()
        {
            if (JobInfo == null)
            {
                JobInfo = "";
            }

            if (Department == null)
            {
                Department = "";
            }
        }

    }
}
