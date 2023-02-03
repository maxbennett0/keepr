namespace keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfilesController : ControllerBase
{
  private readonly ProfilesService _profilesService;
  private readonly KeepsService _keepsService;
  private readonly VaultsService _vaultsService;
  private readonly Auth0Provider _auth;

  public ProfilesController(ProfilesService profilesService, Auth0Provider auth = null, KeepsService keepsService = null, VaultsService vaultsService = null)
  {
    _profilesService = profilesService;
    _auth = auth;
    _keepsService = keepsService;
    _vaultsService = vaultsService;
  }

  [HttpGet("{id}")]

  public ActionResult<Account> GetProfileById(string id)
  {
    try
    {
      Account profile = _profilesService.GetProfileById(id);
      return Ok(profile);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{id}/keeps")]

  public ActionResult<List<Keep>> GetProfilesKeeps(string id)
  {
    try
    {
      List<Keep> keeps = _keepsService.GetKeepsByProfile(id);
      return Ok(keeps);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{id}/vaults")]

  public ActionResult<List<Vault>> GetProfileVaults(string id)
  {
    try
    {
      List<Vault> vaults = _vaultsService.GetProfileVaults(id);
      return vaults;
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
