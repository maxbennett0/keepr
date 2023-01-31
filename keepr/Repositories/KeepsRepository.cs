namespace keepr.Repositories;

public class KeepsRepository
{
  private readonly IDbConnection _db;

  public KeepsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Keep Create(Keep keepData)
  {
    string sql = @"
      INSERT INTO keeps
      (name, description, img, creatorId)
      VALUES
      (@name, @description, @img, @creatorId);
      SELECT LAST_INSERT_ID();
    ";
    int id = _db.ExecuteScalar<int>(sql, keepData);
    keepData.Id = id;
    return keepData;
  }

  internal List<Keep> GetAll()
  {
    string sql = @"
    SELECT
    keeps.*,
    accounts.*
    FROM keeps
    JOIN accounts ON accounts.id = keeps.creatorId
    ";
    List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, (keep, account) =>
    {
      keep.Creator = account;
      return keep;
    }).ToList();
    return keeps;
  }

  internal List<Keep> GetKeepsByProfile(string accountId)
  {
    string sql = @"
    SELECT
    *
    FROM keeps
    JOIN accounts ON accounts.id = keeps.creatorId
    WHERE keeps.creatorId = @accountId;
    ";
    List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
    {
      keep.Creator = profile;
      return keep;
    }, new { accountId }).ToList();
    return keeps;
  }

  internal Keep GetOne(int id)
  {
    string sql = @"
    SELECT
    *
    FROM keeps
    JOIN accounts ON accounts.id = keeps.creatorId
    WHERE keeps.id = @id;
    ";
    Keep keep = _db.Query<Keep, Profile, Keep>(sql, (keep, prof) =>
    {
      keep.Creator = prof;
      return keep;
    }, new { id }).FirstOrDefault();
    return keep;
  }

  internal void Remove(int id)
  {
    string sql = @"
    DELETE FROM keeps
    WHERE id = @id;
    ";
    _db.Execute(sql, new { id });
  }

  internal bool Update(Keep update)
  {
    string sql = @"
    UPDATE keeps SET
    name = @name,
    description = @description,
    img = @img
    WHERE id = @id;
    ";
    int rows = _db.Execute(sql, update);
    return rows > 0;
  }
}