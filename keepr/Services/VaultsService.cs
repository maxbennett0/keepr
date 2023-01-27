namespace keepr.Services;

public class VaultsService
{
  private readonly VaultsRepository _repo;

  public VaultsService(VaultsRepository repo)
  {
    _repo = repo;
  }

  internal Vault Create(Vault vaultData)
  {
    Vault vault = _repo.Create(vaultData);
    return vault;
  }

  internal Vault Edit(Vault vaultData, string userId)
  {
    Vault original = GetOne(vaultData.Id, userId);
    if (original.CreatorId != vaultData.CreatorId) throw new Exception("if you are seeing this, you know what you did wrong. go think about your life for a while and come back later");
    original.Name = vaultData.Name ?? original.Name;
    original.Description = vaultData.Description ?? original.Description;
    original.Img = vaultData.Img ?? original.Img;

    _repo.Edit(original);
    return original;
  }

  internal Vault GetOne(int id, string userId)
  {
    Vault vault = _repo.GetOne(id);
    if (vault == null)
    {
      throw new Exception("no vault here. this isnt fallout.");
    }
    if (vault.CreatorId != userId)
    {
      throw new Exception("dont try to steal vaults that aren't yours");
    }
    return vault;
  }

  internal string Remove(int id, string userId)
  {
    Vault vault = GetOne(id, userId);
    if (vault.CreatorId != userId)
    {
      throw new Exception("why would you even want to delete someone elses' collection?");
    }
    _repo.Remove(id);
    return $"{vault.Name} was deleted :(";
  }
}
