using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Info
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Size { get; set; }
        public string FileName { get; set; }
    }
}
