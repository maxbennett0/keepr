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
}
