using System.Text.Json.Serialization;

namespace FinalProjectMVC.ViewModels.Account
{
    public class GetProfileVM
    {
        public string? FullName { get; set; }
        public string? UserName { get; set; }

        [JsonPropertyName("profileImage")]
        public string? ProfileImageUrl { get; set; }
    }
}
