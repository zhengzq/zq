using System.Data.Entity;
using System.Reflection;
using Zq.UnitOfWork;

namespace Example.Core.Data
{
    public class EfDbContext : DbContext, IDbContext
    {
        /// <summary>
        /// 构造函数，初始化一个新的<c>lDbContext</c>实例。
        /// </summary>
        public EfDbContext()
            : base("Example")
        {

            this.Configuration.ProxyCreationEnabled = true;
            this.Configuration.AutoDetectChangesEnabled = true;
            this.Configuration.LazyLoadingEnabled = true;

        }

        #region Protected Methods
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(Assembly.Load("Example.Core"));

            base.OnModelCreating(modelBuilder);
        }
        #endregion

    }
}
