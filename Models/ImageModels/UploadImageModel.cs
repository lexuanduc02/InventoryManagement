using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Models.ImageModels
{
    public class UploadImageModel
    {
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public string FileName { get; set; }
        [Required]
        public int With { get; set; }
        [Required]
        public int Height { get; set; }
        [Required]
        public string PublicId { get; set; }
        [Required]
        public string Folder { get; set; }
    }
}
