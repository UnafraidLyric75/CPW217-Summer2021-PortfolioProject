using System;
using System.ComponentModel.DataAnnotations;

namespace StellarisPlanetList.Models
{
    /// <summary>
    /// All data for user in the database
    /// </summary>
    public class UserAccounts
    {

        [Key]
        public int UserId { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public DateTime? DateOfBirth { get; set; }
    }

    /// <summary>
    /// Makes sure the data is up to standard
    /// </summary>
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [StringLength(276)]
        public string Email { get; set; }

        [Required]
        [Compare(nameof(Email))]
        [Display(Name = "Confirm Email")]
        public string ConfirmEmail { get; set; }

        [Required]
        [StringLength(20)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(120, MinimumLength = 8, ErrorMessage = "Password must be between {2} and {1}")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
    }

    /// <summary>
    /// Login reqs
    /// </summary>
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Username or Email")]
        public string UsernameOrEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
