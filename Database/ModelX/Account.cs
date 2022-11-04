using System.ComponentModel.DataAnnotations;

namespace ModelX;

public class Account
{
    public int Id { get; set; }

    [Required]
    [MaxLength(64)]
    public string Owner { get; set; }

    //public string Details { get; set; }
    public AccountDetails Details { get; set; }

    public List<Blog> Blogs { get; set; } = new();
}
