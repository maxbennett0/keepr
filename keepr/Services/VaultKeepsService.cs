namespace keepr.Services;

public class VaultKeepsService
{
  private readonly VaultKeepsRepository _repo;
  private readonly KeepsService _keepsService;
  private readonly VaultsService _vaultsService;

  public VaultKeepsService(VaultKeepsRepository repo, KeepsService keepsService, VaultsService vaultsService = null)
  {
    _repo = repo;
    _keepsService = keepsService;
    _vaultsService = vaultsService;
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
    Vault vault = _vaultsService.GetOne(vk.VaultId, userId);
    if (userId != vault.CreatorId)
    {
      throw new Exception("you are simply a theif and there is a strict to tolerance policy towards theives");
    }
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
    if (vault.IsPrivate == true)
    {
      throw new Exception("ITS PRIVATE DONT GO IN THERE AHHHHHHH");
    }
    List<MyKeeps> keeps = _repo.GetKeepsInVault(vaultId);
    return keeps;
  }
}
