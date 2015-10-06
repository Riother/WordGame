using IGT.WordGameGenerator.Common;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace IGTWordGameGenerator.FileManagement
{
    /// <summary>
    /// Reads in ProjectData from the designated file
    /// </summary>
    public class ProjectReader : ProjectDataManager
    {
        /// <summary>
        /// Loads in ProjectData from designated file
        /// </summary>
        /// <param name="filePath">Path to the file to be read</param>
        /// <returns>ProjectData if the file is a valid project, null if the file is invalid or the project cannot be loaded</returns>
        public AllModelsModel OpenProject(string filePath)
        {
			AllModelsModel loadedProject = null;
            
            bool isCorrectFileType = Regex.IsMatch(filePath, DEFAULT_FILE_EXTENSION);

            if (isCorrectFileType)
            {
                IFormatter formatter = new BinaryFormatter();
                Stream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                loadedProject = formatter.Deserialize(fileStream) as AllModelsModel;
            }
            else
            {
                MessageBox.Show("The file must be of type " + DEFAULT_FILE_EXTENSION);
            }

            return loadedProject;
        }
    }
}
