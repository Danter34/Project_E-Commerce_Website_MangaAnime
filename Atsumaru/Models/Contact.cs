using System.ComponentModel.DataAnnotations;

namespace Atsumaru.Models
{
    public class Contact
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên không được để trống.")]
        [StringLength(100, ErrorMessage = "Tên không được vượt quá 100 ký tự.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email không được để trống.")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ.")]
        [StringLength(100, ErrorMessage = "Email không được vượt quá 100 ký tự.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Nội dung tin nhắn không được để trống.")]
        [StringLength(1000, ErrorMessage = "Nội dung tin nhắn không được vượt quá 1000 ký tự.")]
        public string Message { get; set; }

        public DateTime SentDate { get; set; } = DateTime.Now;
    }
}
