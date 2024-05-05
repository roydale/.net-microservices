namespace CommandService.Api.Data.Repositories
{
	public abstract class BaseRepository(AppDbContext _context) : IBaseRepository
    {
        public virtual bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
