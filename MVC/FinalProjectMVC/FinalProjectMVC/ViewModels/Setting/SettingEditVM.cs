using System.ComponentModel.DataAnnotations;

namespace FinalProjectMVC.ViewModels.Setting
{
    public class SettingEditVM
    {
        public int Id { get; set; }
        public string Key { get; set; }

        [Required(ErrorMessage = "Value is required")]
        public string Value { get; set; }
    }
}
