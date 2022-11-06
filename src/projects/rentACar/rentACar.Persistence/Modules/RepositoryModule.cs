
using Autofac;
using rentACar.Persistence.Contexts;
using System.Reflection;
using Module = Autofac.Module;

namespace rentACar.Persistence.Modules
{
    public class RepositoryModule : Module
    {
        protected override void Load(Autofac.ContainerBuilder builder)
        {
            var apiAssembly = Assembly.GetExecutingAssembly();
            var repoAssembly = Assembly.GetAssembly(typeof(BaseDbContext));

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly).Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();
        }


    }
}
