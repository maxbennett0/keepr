namespace keepr.Services;

public class KeepsService
{
  private readonly KeepsRepository _repo;

  public KeepsService(KeepsRepository repo)
  {
    _repo = repo;
  }

  internal Keep Create(Keep keepData)
  {
    Keep keep = _repo.Create(keepData);
    return keep;
  }

  internal Keep Edit(Keep keepData, string userId)
  {
    Keep original = GetOne(keepData.Id, userId);
    if (original.CreatorId != keepData.CreatorId) throw new Exception("are you really trying to get someone else's keep? not cool dawg");
    original.Name = keepData.Name ?? original.Name;
    original.Description = keepData.Description ?? original.Description;
    original.Img = keepData.Img ?? original.Img;
    if (keepData.CreatorId != original.CreatorId)
    {
      original.Kept = keepData.Kept;
    }

    _repo.Update(original);
    return original;
  }

  internal List<Keep> GetAll()
  {
    List<Keep> keeps = _repo.GetAll();
    return keeps;
  }

  internal List<Keep> GetKeepsByProfile(string accountId)
  {
    List<Keep> keeps = _repo.GetKeepsByProfile(accountId);
    return keeps;
  }

  internal Keep GetOne(int id, string userId)
  {
    Keep keep = _repo.GetOne(id);
    if (keep == null)
    {
      throw new Exception($"there isnt a keep at this id: ${id}");
    }
    keep.Views++;
    _repo.Update(keep);
    if (keep.CreatorId != userId)
    {
      Keep newKeep = _repo.GetOne(id);
    }
    return keep;
  }

  internal string Remove(int id, string userId)
  {
    Keep keep = GetOne(id, userId);
    if (keep.CreatorId != userId)
    {
      throw new Exception("you really just tried to do that??? why??");
    }
    _repo.Remove(id);
    return $"{keep.Name} was deleted.................. PERMANENTLY!!!";
  }
}
