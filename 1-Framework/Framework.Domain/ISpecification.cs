using System;
using System.Linq.Expressions;

namespace Framework.Domain
{
    public interface ISpecification<T>
    {
        bool IsSatisfied(T entity);
        Expression<Func<T, bool>> Expression { get; }
        ISpecification<T> And(ISpecification<T> right);
        ISpecification<T> Or(ISpecification<T> right);
        ISpecification<T> Not();
    }

    public abstract class Specification<T> : ISpecification<T>
    {
        protected Specification()
        {
            
        }
        protected Specification(Expression<Func<T, bool>> expression)
        {
            Expression = expression;
        }
        public virtual bool IsSatisfied(T entity)
        {
            return Expression.Compile().Invoke(entity);
        }
        public virtual Expression<Func<T, bool>> Expression { get; protected set; }
        public ISpecification<T> And(ISpecification<T> right)
        {
            return new AndSpesification<T>(this, right);
        }

        public ISpecification<T> Or(ISpecification<T> right)
        {
            return new OrSpesification<T>(this, right);
        }

        public ISpecification<T> Not()
        {
            return new NotSpesification<T>(this);

        }
    }

    public class AndSpesification<T>:Specification<T>
    {
        public AndSpesification(Specification<T> left, ISpecification<T> right)
        {
            Expression = arg => left.IsSatisfied(arg) && right.IsSatisfied(arg);
        }
    }
    public class OrSpesification<T>:Specification<T>
    {
        public OrSpesification(Specification<T> left, ISpecification<T> right)
        {
            Expression = arg => left.IsSatisfied(arg) || right.IsSatisfied(arg);
        }
    }
    public class NotSpesification<T>:Specification<T>
    {
        public NotSpesification(Specification<T> left)
        {
            Expression = arg => !left.IsSatisfied(arg) ;
        }
    }
}