using System.ComponentModel.DataAnnotations;

namespace allspice.Models;

// NOTE Separating Email from other class properties so that it does not need to be sent over network unless needed for security reasons.

public class Account : Profile
{
  [MaxLength(255)] public string Email { get; set; }

}

public class Profile : RepoItem<string>
{
  [MaxLength(255)] public string Name { get; set; }
  [MaxLength(2000)] public string Picture { get; set; }
}
