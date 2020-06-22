﻿using CKSource.CKFinder.Connector.Config;
using CKSource.CKFinder.Connector.Core.Builders;
using CKSource.CKFinder.Connector.Core.Logs;
using CKSource.CKFinder.Connector.Host.Owin;
using CKSource.CKFinder.Connector.KeyValue.FileSystem;
using CKSource.CKFinder.Connector.Logs.NLog;
using CKSource.FileSystem.Ftp;
using CKSource.FileSystem.Local;
using Microsoft.Owin;
using Owin;
using RateListing.Models;

[assembly: OwinStartupAttribute(typeof(RateListing.Ui.Startup))]
namespace RateListing.Ui
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            /*
           * If you installed CKSource.CKFinder.Connector.Logs.NLog you can start the logger:
           * LoggerManager.LoggerAdapterFactory = new NLogLoggerAdapterFactory();
           * Keep in mind that the logger should be initialized only once and before any other
           * CKFinder method is invoked.
           */
            LoggerManager.LoggerAdapterFactory = new NLogLoggerAdapterFactory();

            /*
             * Register the "local" type backend file system.
             */
            FileSystemFactory.RegisterFileSystem<LocalStorage>();
            FileSystemFactory.RegisterFileSystem<FtpStorage>();

            /*
             * Map the CKFinder connector service under a given path. By default the CKFinder JavaScript
             * client expects the ASP.NET connector to be accessible under the "/ckfinder/connector" route.
             */
            app.Map("/ckfinder/connector", SetupConnector);

            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(
            //                    Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/ProductLogo")),

            //    RequestPath = "/productLogo"
            //});
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(
            //                    Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/ProductLogo")),

            //    RequestPath = "/productLogo"
            //});
        }

        private static void SetupConnector(IAppBuilder app)
        {
            /*
             * Create a connector instance using ConnectorBuilder. The call to the LoadConfig() method
             * will configure the connector using CKFinder configuration options defined in Web.config.
             */
            var connectorFactory = new OwinConnectorFactory();
            var connectorBuilder = new ConnectorBuilder();

            /*
             * Create an instance of authenticator implemented in the previous step.
             */
            var customAuthenticator = new CustomCKFinderAuthenticator();


            connectorBuilder
                /*
                 * Provide the global configuration.
                 *
                 * If you installed CKSource.CKFinder.Connector.Config, you should load the static configuration
                 * from XML:
                 * connectorBuilder.LoadConfig();
                 */
                .LoadConfig()
                .SetAuthenticator(customAuthenticator)
                .SetRequestConfiguration(
                    (request, config) =>
                    {
                        /*
                         * If you installed CKSource.CKFinder.Connector.Config, you might want to load the static
                         * configuration from XML as a base configuration to modify:
                         */
                        config.LoadConfig();

                        /*
                         * Configure settings per request.
                         *
                         * The minimal configuration has to include at least one backend, one resource type
                         * and one ACL rule.
                         *
                         * For example:
                         */


                        //config.AddBackend("default", new LocalStorage(@"\Files"));
                        //config.AddResourceType("images", builder => builder.SetBackend("default", "images"));
                        //config.AddAclRule(new AclRule(
                        //    new StringMatcher("*"),
                        //    new StringMatcher("*"),
                        //    new StringMatcher("*"),
                        //    new Dictionary<Permission, PermissionType> { { Permission.All, PermissionType.Allow } }));


                        /*
                         * If you installed CKSource.CKFinder.Connector.KeyValue.FileSystem, you may enable caching:
                         */
                        var defaultBackend = config.GetBackend("default");
                        var keyValueStoreProvider = new FileSystemKeyValueStoreProvider(defaultBackend);
                        config.SetKeyValueStoreProvider(keyValueStoreProvider);
                    }
                );

            /*
             * Build the connector middleware.
             */
            var connector = connectorBuilder
                .Build(connectorFactory);

            /*
             * Add the CKFinder connector middleware to the web application pipeline.
             */
            app.UseConnector(connector);
        }
    }
}
