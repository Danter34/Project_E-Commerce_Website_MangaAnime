namespace Atsumaru.Models.ViewModel
{
    public class UserProfileViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; } 
        public string Email { get; set; }

        public ChangePasswordViewModel ChangePasswordForm { get; set; } = new ChangePasswordViewModel();
        public List<Product> FavoriteProducts { get; set; } = new List<Product>();
    }
}
