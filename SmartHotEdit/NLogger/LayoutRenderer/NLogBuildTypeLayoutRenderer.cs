using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using NLog;
using NLog.Common;
using NLog.Config;
using NLog.LayoutRenderers;

namespace SmartHotEdit.NLogger.LayoutRenderer
{
    [LayoutRenderer("buildConfiguration")]
    [ThreadAgnostic]
    internal class NLogBuildTypeLayoutRenderer : NLog.LayoutRenderers.LayoutRenderer
    {
        private string _asmver;

        public NLogBuildTypeLayoutRenderer(string assemblyName)
        {
            AssemblyName = assemblyName;
        }

        /// <summary>
        ///     Specifies the assembly name for which the type will be displayed.
        ///     By default the calling assembly is used.
        /// </summary>
        private string AssemblyName { get; }

        private string GetAssemblyBuildType()
        {
            InternalLogger.Debug("Assembly name '{0}' not yet loaded: ", AssemblyName);
            if (!string.IsNullOrEmpty(AssemblyName))
            {
                // try to get assembly based on its name
                return Convert.ToBoolean(AppDomain.CurrentDomain.GetAssemblies()
                    .Where(
                        a => string.Equals(a.GetName().Name, AssemblyName, StringComparison.InvariantCultureIgnoreCase))
                    .Select(this.IsAssemblyDebugBuild).FirstOrDefault())
                    ? "Debug"
                    : "Release";
            }
            // get entry assembly
            var entry = Assembly.GetEntryAssembly();

            _asmver = this.IsAssemblyDebugBuild(entry) ? "Debug" : "Release";
            return _asmver;
        }

        private bool IsAssemblyDebugBuild(Assembly assembly)
        {
            return
                assembly.GetCustomAttributes(false)
                    .OfType<DebuggableAttribute>()
                    .Select(da => da.IsJITTrackingEnabled)
                    .FirstOrDefault();
        }

        /// <summary>
        ///     Renders the current trace activity ID.
        /// </summary>
        /// <param name="builder">The <see cref="StringBuilder" /> to append the rendered data to.</param>
        /// <param name="logEvent">Logging event.</param>
        protected override void Append(StringBuilder builder, LogEventInfo logEvent)
        {
            builder.Append(GetAssemblyBuildType());
        }
    }
}