using Core.Application.Base.Constants;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Domain.Entities.Base;
using Core.Persistence.Repositories;
using System.Linq.Expressions;

namespace Core.Application.Base.Rules
{
    public class BaseBusinessRules<TEntity> : BaseBusinessRules
         where TEntity : Entity
    {
        private readonly IAsyncRepository<TEntity> _asyncRepository;

        public BaseBusinessRules(IAsyncRepository<TEntity> asyncRepository)
        {
            _asyncRepository = asyncRepository;
        }

        public async Task BaseIdShouldExistWhenSelected(int id)
        {
            TEntity? baseData = await _asyncRepository.GetAsync(x => x.Id == id, enableTracking: false);
            if (baseData is null) throw new BusinessException(typeof(TEntity).Name + BaseMessages.EntityDoesNotExist);
        }

        public Task BaseShouldBeExist(TEntity? baseData)
        {
            if (baseData is null) throw new NotFoundException(typeof(TEntity).Name + BaseMessages.EntityDoesNotExist);
            return Task.CompletedTask;
        }

        public LambdaExpression GetIncludeLambda(Type entityType, string include)
        {
            string[] navigationProperties = include.Split('.');
            ParameterExpression parameterExpression = Expression.Parameter(entityType);
            Expression expression = parameterExpression;

            foreach (string navigationProperty in navigationProperties)
            {
                expression = Expression.PropertyOrField(expression, navigationProperty);
            }

            return Expression.Lambda(expression, parameterExpression);
        }


    }
}
