namespace Infrastructure.Common
{
	public interface IGenericRepository<T> where T : class
	{
		IEnumerable<T> GetAll();
		T GetById(object id);
		IEnumerable<T> FindAll(params object[] param);
		T Find(params object[] param);
        void Insert(T obj);
		void Update(T obj);
		void Delete(object id);
	}
}

