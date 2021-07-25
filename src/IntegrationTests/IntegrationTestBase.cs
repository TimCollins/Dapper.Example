using DataAccess.Impl;
using DataAccess.Interfaces;
using SimpleInjector;

namespace IntegrationTests
{
    public abstract class IntegrationTestBase
    {
        internal readonly Container _container;

        protected IntegrationTestBase()
        {
            _container = new Container();
            _container.Register<ICategoryRepository, CategoryRepository>();
            _container.Verify();
        }
    }
}
