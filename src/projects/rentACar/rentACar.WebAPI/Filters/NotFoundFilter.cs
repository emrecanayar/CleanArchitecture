using Core.CrossCuttingConcerns.Exceptions;
using Core.Domain.Entities.Base;
using Core.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using rentACar.Persistence.Contexts;

namespace Core.CrossCuttingConcerns.Filters
{
    public class NotFoundFilter<T> : EfRepositoryBase<T, BaseDbContext>, IAsyncActionFilter where T : Entity, new()
    {
        public NotFoundFilter(BaseDbContext context) : base(context)
        {
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var idValue = context.ModelState.Values.FirstOrDefault().RawValue;
            if (idValue == null) { await next.Invoke(); return; }

            var id = int.Parse(idValue.ToString());
            var anyEntity = await Context.Set<T>().AnyAsync(x => x.Id == id);
            if (anyEntity) { await next.Invoke(); return; }

            throw new NotFoundException($"{typeof(T).Name}({id} not found.)");
        }
    }
}
