namespace POS.Application.Interfaces
{
	public interface IGenericRepository<T> where T : class
	{
		Task Insert(T entity);
		void Update(T entity);
		void Delete(T entity);
		Task<T> GetById(int id);
		Task<IEnumerable<T>> GetAll();
	}
}
