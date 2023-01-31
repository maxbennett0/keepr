namespace keepr.Repositories;

public class ProfilesRepository
{
  private readonly IDbConnection _db;

  public ProfilesRepository(IDbConnection db)
  {
    _db = db;
  }


  internal Profile GetProfileById(string accountId)
  {
    string sql = @"
    SELECT
    *
    FROM accounts
    WHERE id = @accountId;
    ";
    Profile profile = _db.Query<Profile>(sql, new { accountId }).FirstOrDefault();
    return profile;
  }
}
