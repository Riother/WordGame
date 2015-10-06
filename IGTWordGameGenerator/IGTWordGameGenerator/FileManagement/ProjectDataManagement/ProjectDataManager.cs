using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGTWordGameGenerator.FileManagement
{
    /// <summary>
    /// Object that will handle ProjectData in some way
    /// </summary>
    public abstract class ProjectDataManager
    {
        /// <summary>
        /// Default file extension for the project being saved
        /// </summary>
        protected const string DEFAULT_FILE_EXTENSION = ".wggproj";
        /// <summary>
        /// Filters out files that do not have the default file extension
        /// </summary>
        protected const string FILTER = "Word Game Generator Project (" + DEFAULT_FILE_EXTENSION + ")|*" + DEFAULT_FILE_EXTENSION;
    }
}
