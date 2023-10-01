using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Therapy.Core.Utils;
using Therapy.Domain.Entities;
using Therapy.Domain.Models;

namespace Therapy.Core.Extensions
{
  public static class PaginationExtensions {
    public static IQueryable<T> Paginate<T> (
      this IQueryable<T> query,
      PaginationFilter filter,
      Func<string, Expression<Func<T, bool>>> searchPredicate
    ) where T : BaseEntity {

      // Apply search filter if provided
      if (!string.IsNullOrWhiteSpace(filter.Search))
      {
        var searchExpression = searchPredicate(filter.Search);
        query = query.Where(searchExpression);
      }

      // Additional filters like based on cursor
      if(!string.IsNullOrEmpty(filter.After))
      {
          var (id, createdAt) = Cursor.DecodeCursor(filter.After);
          query = query.Where(item => item.CreatedAt > createdAt || (item.CreatedAt == createdAt && item.Id > id));
      }

      if(!string.IsNullOrEmpty(filter.Before))
      {
        var (id, createdAt) = Cursor.DecodeCursor(filter.Before);
        query = query.Where(item => item.CreatedAt < createdAt || (item.CreatedAt == createdAt && item.Id < id));
      }


      // Ordering the filtered results
      query = query.OrderBy(item => item.CreatedAt);

      // Paginating the ordered results
      if(filter.PageSize.HasValue && filter.PageSize.Value > 0)
      {
        if(!string.IsNullOrEmpty(filter.Before))
        {
          query = query.OrderByDescending(item => item.CreatedAt).Take(filter.PageSize.Value);
        }
        else
        {
          query = query.Take(filter.PageSize.Value);
        }
      }

      return query.OrderBy(item => item.CreatedAt);
    }

    public static async Task<PaginationResponse<TDto>> ToPaginationResponse<TEntity, TDto> (
      this IQueryable<TEntity> query,
      IMapper mapper,
      DateTime? earliestDate,
      DateTime? latestDate
    ) where TEntity : BaseEntity {

      var items = await query.ToListAsync();

      var firstItem = items.FirstOrDefault();
      var lastItem = items.LastOrDefault();

      return new PaginationResponse<TDto>
      {
          Data = mapper.Map<IEnumerable<TDto>>(items),
          FirstCursor = firstItem != null ? Cursor.EncodeCursor(firstItem.Id, firstItem.CreatedAt) : null,
          LastCursor = lastItem != null ? Cursor.EncodeCursor(lastItem.Id, lastItem.CreatedAt) : null,
          HasPrevPage = firstItem != null && firstItem.CreatedAt > earliestDate,
          HasNextPage = lastItem != null && lastItem.CreatedAt < latestDate
      };
    }
    
    // Evaluate After field to get Expression for Where clause
    public static Expression<Func<T, bool>> GetAfterWhereExpression<T> (bool after, string cursor) where T : BaseEntity {
      var (id, createdAt) = Cursor.DecodeCursor(cursor);

      if (!after) {
        return item => item.CreatedAt < createdAt || (item.CreatedAt == createdAt && item.Id < id);
      }
      return item => item.CreatedAt > createdAt || (item.CreatedAt == createdAt && item.Id > id);
    }

  }
}