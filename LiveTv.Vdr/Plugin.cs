using LiveTv.Vdr.Configuration;
using MediaBrowser.Common.Plugins;
using MediaBrowser.Model.Plugins;
using System;
using System.Collections.Generic;
using MediaBrowser.Common.Configuration;
using MediaBrowser.Model.Serialization;
using MediaBrowser.Model.Drawing;
using System.IO;

namespace LiveTv.Vdr
{
    public class Plugin : BasePlugin<PluginConfiguration>, IHasWebPages, IHasThumbImage
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

        public override string Description
        {
            get
            {
                return "Provides live TV using VDR with streamdev and restfulapi as a back-end.";
            }
        }        

        private Guid _id = new Guid("3086D448-45FD-42EF-9446-F3E1B6960E36");
        public override Guid Id
        {
            get { return _id; }
        }
        public Stream GetThumbImage()
        {
            var type = GetType();
            return type.Assembly.GetManifestResourceStream(type.Namespace + ".vdrlogo.png");
        }

        public ImageFormat ThumbImageFormat
        {
            get
            {
                return ImageFormat.Png;
            }
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
