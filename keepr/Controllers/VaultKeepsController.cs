namespace keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultKeepsController : ControllerBase
{
  private readonly VaultKeepsService _vkService;
  private readonly Auth0Provider _auth;

  public VaultKeepsController(VaultKeepsService vkService, Auth0Provider auth)
  {
    _vkService = vkService;
    _auth = auth;
  }

  [HttpPost]
  [Authorize]
  public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep vkData)
  {
    try
    {
      Vaultist userInfo = await _auth.GetUserInfoAsync<Vaultist>(HttpContext);
      vkData.CreatorId = userInfo.Id;
      VaultKeep vk = _vkService.Create(vkData, userInfo?.Id);
      return Ok(vkData);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  [HttpDelete("{id}")]
  [Authorize]
  public async Task<ActionResult<string>> Delete(int id)
  {
    try
    {
      Profile userInfo = await _auth.GetUserInfoAsync<Profile>(HttpContext);
      string message = _vkService.Delete(id, userInfo.Id);
      return Ok(message);

    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
