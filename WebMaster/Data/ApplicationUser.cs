using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
    [StringLength(100)]
    [MaxLength(100)]
    [Required]
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Type { get; set; }  // Este campo se usará para almacenar el rol
}