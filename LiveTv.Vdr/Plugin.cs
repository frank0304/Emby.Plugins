using LiveTv.Vdr.Configuration;
using MediaBrowser.Common.Plugins;
using MediaBrowser.Model.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaBrowser.Common.Configuration;
using MediaBrowser.Model.Serialization;

namespace LiveTv.Vdr
{
    class Plugin : BasePlugin<PluginConfiguration>, IHasWebPages
    {
        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static Plugin Instance { get; private set; }

        public Plugin(IApplicationPaths applicationPaths, IXmlSerializer xmlSerializer) : 
            base(applicationPaths, xmlSerializer)
        {
            Instance = this;
        }

        public override string Name
        {
            get { return "LiveTV.Vdr"; }
        }

        public IEnumerable<PluginPageInfo> GetPages()
        {
            return new[]
            {
                new PluginPageInfo
                {
                    Name = Name,
                    EmbeddedResourcePath = GetType().Namespace + ".Configuration.PluginConfigurationPage.html"
                }
            };
        }
    }
}
