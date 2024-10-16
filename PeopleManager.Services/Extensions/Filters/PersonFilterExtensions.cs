using PeopleManager.Dto.Filters;
using PeopleManager.Dto.Results;
using PeopleManager.Model;

namespace PeopleManager.Services.Extensions.Filters
{
    public static class PersonFilterExtensions
    {
        public static IQueryable<PersonResult> ApplyFilter(this IQueryable<PersonResult> query, PersonFilter? filter)
        {
            if (filter is null)
            {
                return query;
            }

            if (filter.OrganizationId.HasValue)
            {
                query = query.Where(p => p.OrganizationId == filter.OrganizationId);
            }

            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                query = query.Where(p =>
                    p.FirstName.Contains(filter.Search)
                    || p.LastName.Contains(filter.Search)
                    || (p.Email != null && p.Email.Contains(filter.Search))
                    || (p.OrganizationName != null && p.OrganizationName.Contains(filter.Search))
                );
            }

            return query;
        }
    }
}
