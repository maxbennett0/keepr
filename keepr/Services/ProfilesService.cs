namespace keepr.Services;

public class ProfilesService
{
  private readonly ProfilesRepository _repo;

  public ProfilesService(ProfilesRepository repo)
  {
    _repo = repo;
  }

  internal Account GetProfileById(string accountId)
  {
    Account profile = _repo.GetProfileById(accountId);
    if (profile == null)
    {
      throw new Exception("no id for that profile");
    }
    return profile;
  }
}
