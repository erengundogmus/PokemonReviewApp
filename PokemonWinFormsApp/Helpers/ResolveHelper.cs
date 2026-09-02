using Autofac;

namespace PokemonWinFormsApp
{
    public static class ResolveHelper
    {
        public static T GetInstance<T>()
        {
            return Program.Container.Resolve<T>();
        }
    }
}