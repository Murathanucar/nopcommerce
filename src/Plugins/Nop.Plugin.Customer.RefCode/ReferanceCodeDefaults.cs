using Nop.Core;

namespace Nop.Plugin.Client.ReferanceCode
{
    /// <summary>
    /// Represents plugin constants
    /// </summary>
    public class ReferanceCodeDefaults
    {
        /// <summary>
        /// Gets the plugin system name
        /// </summary>
        public static string SystemName => "Client.ReferanceCode";

        /// <summary>
        /// Gets the user agent used to request third-party services
        /// </summary>
        public static string UserAgent => $"nopCommerce-{NopVersion.CURRENT_VERSION}";

        /// <summary>
        /// Gets the CyberSource payment token list route name
        /// </summary>
        public static string ReferanceCodeRouteName => "Plugin.Client.ReferanceCode.CodeView";

        public static int ReferanceCodeMenuTab => 600;

        public static string ReferanceCodeClassName => "cybersource-tokens";

        /// <summary>
        /// Gets a type of the synchronization schedule task
        /// </summary>
        public static string SynchronizationTask => "Nop.Plugin.Client.ReferanceCode.Services.SynchronizationTask";

        /// <summary>
        /// Gets a default synchronization period in hours
        /// </summary>
        public static int DefaultSynchronizationPeriod => 12;

        /// <summary>
        /// Gets a name of the synchronization schedule task
        /// </summary>
        public static string SynchronizationTaskName => "Synchronization (Referance Code Reward Plugin)";
    }
}