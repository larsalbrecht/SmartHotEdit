using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using NLog.Common;
using System.Reflection;
using System.Diagnostics;
using NLog.Config;

namespace SmartHotEdit.NLogger.LayoutRenderer
{
    [NLog.LayoutRenderers.LayoutRenderer("buildConfiguration")]
    [ThreadAgnostic]
    class NLogBuildTypeLayoutRenderer : NLog.LayoutRenderers.LayoutRenderer
    {
        /// <summary>
        /// Specifies the assembly name for which the type will be displayed.
        /// By default the calling assembly is used.
        /// </summary>
        public String AssemblyName { get; set; }

        private String asmver = null;
        private String GetAssemblyBuildType()
        {
            InternalLogger.Debug("Assembly name '{0}' not yet loaded: ", AssemblyName);
            if (!String.IsNullOrEmpty(AssemblyName))
            {
                // try to get assembly based on its name
                return Convert.ToBoolean(AppDomain.CurrentDomain.GetAssemblies()
                                      .Where(a => String.Equals(a.GetName().Name, AssemblyName, StringComparison.InvariantCultureIgnoreCase))
                                      .Select(a => this.IsAssemblyDebugBuild(a)).FirstOrDefault()) ? "Debug" : "Release";
            }
            // get entry assembly
            var entry = Assembly.GetEntryAssembly();

            asmver = this.IsAssemblyDebugBuild(entry) ? "Debug" : "Release";
            return asmver;
        }

        private bool IsAssemblyDebugBuild(Assembly assembly)
        {
            return assembly.GetCustomAttributes(false).OfType<DebuggableAttribute>().Select(da => da.IsJITTrackingEnabled).FirstOrDefault();
        }

        /// <summary>
        /// Renders the current trace activity ID.
        /// </summary>
        /// <param name="builder">The <see cref="StringBuilder"/> to append the rendered data to.</param>
        /// <param name="logEvent">Logging event.</param>
        protected override void Append(StringBuilder builder, LogEventInfo logEvent)
        {
            builder.Append(GetAssemblyBuildType());
        }
    }
}
