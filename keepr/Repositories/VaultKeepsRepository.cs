namespace keepr.Repositories;

public class VaultKeepsRepository
{
  private readonly IDbConnection _db;

  public VaultKeepsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal VaultKeep Create(VaultKeep vkData)
  {
    string sql = @"
    INSERT INTO vaultkeeps
    (vaultId, keepId, creatorId)
    VALUES
    (@vaultId, @keepId, @creatorId);
    SELECT LAST_INSERT_ID();
    ";
    int id = _db.ExecuteScalar<int>(sql, vkData);
    vkData.Id = id;
    return vkData;
  }

  internal bool Delete(int vaultKeepId)
  {
    string sql = @"
    DELETE FROM vaultkeeps
    WHERE id = @vaultKeepId;
    ";
    int rows = _db.Execute(sql, new { vaultKeepId });
    return rows > 0;
  }

  internal List<MyKeeps> GetKeepsInVault(int vaultId)
  {
    string sql = @"
    SELECT
    vk.*,
    k.*,
    a.*
    FROM vaultkeeps vk
    JOIN keeps k ON vk.keepId = k.id
    JOIN accounts a ON k.creatorId = a.id
    WHERE vk.vaultId = @vaultId;
    ";

    List<MyKeeps> mykeeps = _db.Query<VaultKeep, MyKeeps, Profile, MyKeeps>(sql, (vk, mykeep, profile) =>
    {
      mykeep.VaultKeepId = vk.Id;
      mykeep.Creator = profile;
      return mykeep;
    }, new { vaultId }).ToList();
    return mykeeps;
  }

  internal VaultKeep GetVaultMember(int vaultKeepId)
  {
    string sql = @"
    SELECT
    *
    FROM vaultkeeps
    WHERE id = @vaultKeepId;
    ";
    return _db.Query<VaultKeep>(sql, new { vaultKeepId }).FirstOrDefault();
  }
}
