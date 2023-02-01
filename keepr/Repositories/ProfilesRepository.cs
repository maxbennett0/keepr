namespace keepr.Repositories;

public class ProfilesRepository
{
  private readonly IDbConnection _db;

  public ProfilesRepository(IDbConnection db)
  {
    _db = db;
  }


  internal Account GetProfileById(string accountId)
  {
    string sql = @"
    SELECT
    *
    FROM accounts
    WHERE id = @accountId;
    ";
    Account profile = _db.Query<Account>(sql, new { accountId }).FirstOrDefault();
    return profile;
  }
}
