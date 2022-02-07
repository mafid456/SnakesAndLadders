using Microsoft.Extensions.DependencyInjection;
using SnakesAndLadders.BackEnd.Contracts.Services;
using SnakesAndLadders.BackEnd.Services;
using System;

namespace SnakesAndLadders.BackEnd
{
    /// <summary>
    /// Internal dependency injection wrapper.
    /// </summary>
    /// <remarks>Use this class to register the service implementations, in <see cref="RegisterServices"/> method, used by this library. This class is autoinitialized.</remarks>
    internal static class InternalServiceManager
    {
        #region Internal vars
        private static IServiceCollection _services;
        private static IServiceProvider _provider;
        #endregion

        #region Constructor
        static InternalServiceManager()
        {
            InternalServiceManager.CreateServiceCollection();
            InternalServiceManager.RegisterServices();
            InternalServiceManager.InitializeServiceProvider();
        }
        #endregion

        #region Methods & Functions
        /// <summary>
        /// Gets a new instance of the requested service.
        /// </summary>
        /// <typeparam name="T">Service interface to request.</typeparam>
        /// <returns>Returns the new instance of the service.</returns>
        public static T GetService<T>() => InternalServiceManager._provider.GetService<T>();

        private static void CreateServiceCollection() => InternalServiceManager._services = new ServiceCollection();

        private static void InitializeServiceProvider() => InternalServiceManager._provider = InternalServiceManager._services.BuildServiceProvider();

        private static void AddService<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            InternalServiceManager._services.AddTransient<TService, TImplementation>();
        }

        private static void AddSingletonService<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            InternalServiceManager._services.AddSingleton<TService, TImplementation>();
        }

        private static void RegisterServices()
        {
            InternalServiceManager.AddSingletonService<IBoard, Board>();
            InternalServiceManager.AddService<IPlayerToken, PlayerToken>();
            InternalServiceManager.AddSingletonService<IDice, Dice>();
        }
        #endregion
    }
}
