using System;
using System.Linq.Expressions;

namespace DeploymentSettings.Repositories.DataProviders.Interfaces
{
    public interface IDataProvider
    {
        T GetValue<T>(Expression<Func<T, bool>> filterExpression, string collection);

        void AddOrUpdate<T>(T value, string collection, Expression<Func<T, bool>> filterExpression);
    }
}
