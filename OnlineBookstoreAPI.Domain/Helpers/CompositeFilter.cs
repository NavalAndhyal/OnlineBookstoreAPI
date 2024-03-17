using OnlineBookstoreAPI.Domain.Models;
using OnlineBookstoreAPI.Domain.Models.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OnlineBookstoreAPI.Domain.Helpers
{
    public static class CompositeFilter<T> where T : class
    {
        static bool IsNullableType(Type t)
        {
            return t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
        private static Expression BuildFilterExpression(Filter filter, ParameterExpression parameter)
        {
            if (filter.Filters != null && filter.Filters.Any())
            {
                if (filter.Logic?.ToLower() == "and")
                {
                    var andFilters = filter.Filters.Select(f => BuildFilterExpression(f, parameter));
                    return andFilters.Aggregate(Expression.AndAlso);
                }
                else if (filter.Logic?.ToLower() == "or")
                {
                    var orFilters = filter.Filters.Select(f => BuildFilterExpression(f, parameter));
                    return orFilters.Aggregate(Expression.OrElse);
                }
            }

            if (filter.Value == null || string.IsNullOrWhiteSpace(filter.Value.ToString()))
                return null;

            var property = Expression.Property(parameter, filter.Field);
            var constant = Expression.Constant(filter.Value);

            if (property.Type == typeof(string))
            {
                constant = Expression.Constant(filter.Value.ToString());
            }
            else if (property.Type == typeof(int))
            {
                //constant = Expression.Constant(Convert.ToInt32(filter.Value));
                if (IsNullableType(property.Type) && !IsNullableType(constant.Type))
                    constant = Expression.Constant(Convert.ToInt32(((JsonElement)filter.Value).GetString()), property.Type);
                else
                    constant = Expression.Constant(Convert.ToInt32(((JsonElement)filter.Value).GetString()));
            }
            else if (property.Type == typeof(DateTime?))
            {
                if (IsNullableType(property.Type) && !IsNullableType(constant.Type))
                    constant = Expression.Constant(((JsonElement)filter.Value).GetDateTime(),property.Type);
                //var date = ((JsonElement)filter.Value).GetString();
                //DateTime dateTime = new DateTime();
                //if (DateTime.TryParse(date, out dateTime))
                //{
                //    Nullable<DateTime> dateTime1 = dateTime;
                //    constant = Expression.Constant(dateTime1);
                //}
                //else
                //{
                //    throw new ArgumentException($"Unsupported Value of DateTime: {filter.Field}");
                //}
            }

            //if (IsNullableType(property.Type) && !IsNullableType(constant.Type))
            //    constant = Expression.Constant(filter.Value);
            //else if (!IsNullableType(property.Type) && IsNullableType(constant.Type))
            //    property = Expression.Property(Expression.Convert(property, constant.Type), filter.Field);

            switch (filter.Operator.ToLower())
            {
                case "eq":
                    return Expression.Equal(property, constant);
                case "neq":
                    return Expression.NotEqual(property, constant);
                case "lt":
                    return Expression.LessThan(property, constant);
                case "lte":
                    return Expression.LessThanOrEqual(property, constant);
                case "gt":
                    return Expression.GreaterThan(property, constant);
                case "gte":
                    return Expression.GreaterThanOrEqual(property, constant);
                case "contains":
                    var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                    return Expression.Call(property, containsMethod, constant);
                case "startswith":
                    var startsWithMethod = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });

                    // Convert the constant value to lowercase for case-insensitive comparison
                    var constantLower = Expression.Call(constant, typeof(string).GetMethod("ToLower", Type.EmptyTypes));

                    return Expression.Call(property, startsWithMethod, constantLower);
                case "endswith":
                    var endsWithMethod = typeof(string).GetMethod("EndsWith", new[] { typeof(string) });

                    // Convert the constant value to lowercase for case-insensitive comparison
                    var cl = Expression.Call(constant, typeof(string).GetMethod("ToLower", Type.EmptyTypes));

                    return Expression.Call(property, endsWithMethod, cl);
                // Add more operators as needed...
                default:
                    throw new ArgumentException($"Unsupported operator: {filter.Operator}");
            }
            
        }

        private static Expression<Func<T, bool>> GetAndFilterExpression<T>(List<Filter> filters)
        {
            if (filters == null || !filters.Any())
                return null;

            var parameter = Expression.Parameter(typeof(T), "x");
            Expression andExpression = null;

            foreach (var filter in filters)
            {
                var filterExpression = BuildFilterExpression(filter, parameter);
                if (filterExpression != null)
                {
                    if (andExpression == null)
                    {
                        andExpression = filterExpression;
                    }
                    else
                    {
                        andExpression = Expression.AndAlso(andExpression, filterExpression);
                    }
                }
            }

            if (andExpression == null)
            {
                // Return default expression that always evaluates to false
                andExpression = Expression.Constant(false);
            }

            return Expression.Lambda<Func<T, bool>>(andExpression, parameter);
        }

        private static Expression<Func<T, bool>> GetOrFilterExpression<T>(List<Filter> filters)
        {
            if (filters == null || !filters.Any())
                return null;

            var parameter = Expression.Parameter(typeof(T), "x");
            Expression orExpression = null;

            foreach (var filter in filters)
            {
                var filterExpression = BuildFilterExpression(filter, parameter);
                if (filterExpression != null)
                {
                    if (orExpression == null)
                    {
                        orExpression = filterExpression;
                    }
                    else
                    {
                        orExpression = Expression.OrElse(orExpression, filterExpression);
                    }
                }
            }

            if (orExpression == null)
            {
                // Return default expression that always evaluates to false
                orExpression = Expression.Constant(false);
            }

            return Expression.Lambda<Func<T, bool>>(orExpression, parameter);
        }
        public static IQueryable<T> ApplyFilter<T>(IQueryable<T> query, FilterUtility filter)
        {
            if (filter == null || filter.Filters == null || !filter.Filters.Any())
                return query;

            Expression<Func<T, bool>> compositeFilterExpression = null;

            if (filter.Logic?.ToLower() == "and")
            {
                compositeFilterExpression = GetAndFilterExpression<T>(filter.Filters);
            }
            else if (filter.Logic?.ToLower() == "or")
            {
                compositeFilterExpression = GetOrFilterExpression<T>(filter.Filters);
            }

            return compositeFilterExpression != null
                ? query.Where(compositeFilterExpression)
                : query;
        }

      

    }
}
