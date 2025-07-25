﻿using DAL.Models;

namespace DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(string id, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(T entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
        Task<PaginatedList<T>> GetAllPaginated(int pageIndex, int pageSize, CancellationToken cancellationToken = default);
    }
}
