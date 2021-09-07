using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.Logging;


namespace Willcox.Services
{
    /// <summary>
    /// Base class for all the services.
    /// </summary>
    public abstract class BaseService
    {
        protected readonly ILogger Logger;

        public BaseService(ILogger logger)
        {
            Logger = logger;
        }

        /// <summary>
        /// returns bin location path
        /// </summary>
        protected string BinDir
        {
            get { return Path.GetDirectoryName(Assembly.GetEntryAssembly().Location); }
        }
    }
}
