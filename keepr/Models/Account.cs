namespace keepr.Models;

public class Account
{
  public string Id { get; set; }
  public string Name { get; set; }
  public string Picture { get; set; }
}

public class Profile : Account
{
  public string Email { get; set; }

}

public class Vaultist : Profile
{
  public int VaultKeepId { get; set; }
}
