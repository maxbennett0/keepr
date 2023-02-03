namespace keepr.Services;

public class VaultKeepsService
{
  private readonly VaultKeepsRepository _repo;
  private readonly KeepsRepository _krepo;
  private readonly KeepsService _keepsService;
  private readonly VaultsService _vaultsService;

  public VaultKeepsService(VaultKeepsRepository repo, KeepsService keepsService, VaultsService vaultsService = null, KeepsRepository krepo = null)
  {
    _repo = repo;
    _keepsService = keepsService;
    _vaultsService = vaultsService;
    _krepo = krepo;
  }

  internal VaultKeep Create(VaultKeep vkData, string userId)
  {
    Keep keep = _keepsService.GetOne(vkData.KeepId, userId);
    Vault vault = _vaultsService.GetOne(vkData.VaultId, userId);
    if (vkData.KeepId != keep.Id)
    {
      throw new Exception("why aren't you logged in? kinda weird you would want to use the website without showing you aren't a hacker");
    }
    if (vault.CreatorId != userId)
    {
      throw new Exception("hmmm... this person is lying. i wont let them through");
    }
    keep.Kept++;
    _krepo.Update(keep);
    vkData.KeepId = keep.Id;
    VaultKeep vk = _repo.Create(vkData);
    return vk;
  }

  internal string Delete(int vaultKeepId, string userId)
  {
    VaultKeep vk = _repo.GetVaultMember(vaultKeepId);
    if (vk == null)
    {
      throw new Exception($"there is nobody home at {vaultKeepId}, go away");
    }
    if (userId != vk.CreatorId)
    {
      throw new Exception("you are simply a theif and there is a strict to tolerance policy towards theives");
    }
    Keep keep = _keepsService.GetOne(vk.KeepId, userId);
    keep.Kept--;
    _krepo.Update(keep);
    bool result = _repo.Delete(vaultKeepId);
    if (result == false)
    {
      throw new Exception("nobody was there at that id");
    }
    return "you have been removed from the vault";
  }

  internal List<MyKeeps> GetKeepsInVault(int vaultId, string userId)
  {
    Vault vault = _vaultsService.GetOne(vaultId, userId);
    if (vault.IsPrivate == true && vault.CreatorId != userId)
    {
      throw new Exception("ITS PRIVATE DONT GO IN THERE AHHHHHHH");
    }
    else
    {
      List<MyKeeps> keeps = _repo.GetKeepsInVault(vaultId);
      return keeps;
    }
  }
}
