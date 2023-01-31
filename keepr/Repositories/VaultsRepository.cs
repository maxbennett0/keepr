namespace keepr.Repositories;

public class VaultsRepository
{
  private readonly IDbConnection _db;

  public VaultsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Vault Create(Vault vaultData)
  {
    string sql = @"
      INSERT INTO vaults
      (name, description, img, creatorId, isPrivate)
      VALUES
      (@name, @description, @img, @creatorId, @isPrivate);
      SELECT LAST_INSERT_ID();
    ";
    int id = _db.ExecuteScalar<int>(sql, vaultData);
    vaultData.Id = id;
    return vaultData;
  }

  internal bool Edit(Vault original)
  {
    string sql = @"
    UPDATE vaults SET
    name = @name,
    description = @description,
    img = @img
    WHERE id = @id;
    ";
    int rows = _db.Execute(sql, original);
    return rows > 0;
  }

  internal Vault GetOne(int id)
  {
    string sql = @"
    SELECT
    *
    FROM vaults
    JOIN accounts ON accounts.id = vaults.creatorId
    WHERE vaults.id = @id;
    ";
    Vault vault = _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
    {
      vault.Creator = profile;
      return vault;
    }, new { id }).FirstOrDefault();
    return vault;
  }

  internal List<Vault> GetProfileVaults(string accountId)
  {
    string sql = @"
    SELECT
    *
    FROM vaults
    JOIN accounts ON accounts.id = vaults.creatorId
    WHERE vaults.creatorId = @accountId;
    ";
    List<Vault> vaults = _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
    {
      if (vault.IsPrivate == true)
      {
        vault.Creator = profile;
        return vault;
      }
      return vault;
    }, new { accountId }).ToList();
    return vaults;
  }

  internal void Remove(int id)
  {
    string sql = @"
    DELETE FROM vaults
    WHERE id = @id;
    ";
    _db.Execute(sql, new { id });
  }
}
