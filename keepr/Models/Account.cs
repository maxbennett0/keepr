namespace keepr.Models;

public class Account
{
  public string Id { get; set; }
  public string Name { get; set; }
  public string Picture { get; set; }
  public string Email { get; set; }
  public string CoverImg { get; set; }
}

public class Profile : Account
{
  // THIS PROP MUST NOT BE DELETED IT IS THE ONLY THING KEEPING THE CODE WORKING
  public string? DONOTDELETE { get; set; }
}

public class Vaultist : Profile
{
  public int VaultKeepId { get; set; }
}
