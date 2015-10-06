using IGT.WordGameGenerator.Common;
using IGT.WordGameGenerator.Common.Division;
using IGT.WordGameGenerator.Common.PrizeLevel;
using IGTWordGameGenerator.FileManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Documents;

namespace IGTWordGameGenerator.Services.PlayGeneration
{
    /// <summary>
    /// Interaction logic for ProcessingWindow.xaml
    /// </summary>
    public partial class ProcessingWindow : Window
    {
        private AllModelsModel _projectModel;
        private bool _processCanceled = false;
        private BackgroundWorker _worker;

        /// <summary>
        /// Window that shows that the file generation process is still occurring
        /// </summary>
        public ProcessingWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets the file paths for valid word file and banned word file
        /// </summary>
        /// <param name="model">The model containing the models of all the panels in the project</param>
        public void SetModel(AllModelsModel model)
        {
            _projectModel = model;   
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            if (Parent != null && Parent is MainWindow)
            {
                (Parent as MainWindow).IsEnabled = false;
            }

            _worker = new BackgroundWorker()
            {
                WorkerReportsProgress = true
            };
            _worker.DoWork += (s, e1) =>
            {
                ProcessThread(e1);
            };
            _worker.RunWorkerCompleted += (s, e1) =>
            {
                if (!_processCanceled)
                {
                    MessageBox.Show("File Generated!");
                }
                Close();
            };
            _worker.RunWorkerAsync();
        }

        /// <summary>
        /// The thread to run to create the board file.
        /// </summary>
        private void ProcessThread(DoWorkEventArgs e)
        {
            //open save dialog
            string filename = OpenSaveWindow();

            if (!string.IsNullOrEmpty(filename))
            {
                string validWords = _projectModel.GameSetupModel.WordListsModel.ValidWords;
                string bannedWords = _projectModel.GameSetupModel.WordListsModel.BannedWords;

                ResultsWriter writer = new ResultsWriter();
                writer.CreateGeneratedFile(filename, validWords, bannedWords, _projectModel);
            }
            else
            {
                _processCanceled = true;
            }
        }

        /// <summary>
        /// Opens the standard save menu for the user to specify the save location
        /// Initiates generation of the file once the user is finished
        /// </summary>
        private string OpenSaveWindow()
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Word-Game-File"; // Default file name
            dlg.DefaultExt = ".txt"; // Default file extension
            dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            // Show save file dialog box
            bool? result = dlg.ShowDialog();

            string filename = "";
            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                filename = dlg.FileName;
            }

            return filename;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (_worker != null && _worker.IsBusy)
            {
                var result = MessageBox.Show("Not finished, cancel?", "Cancel", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result.Equals(MessageBoxResult.Yes) && _worker != null && _worker.IsBusy)
                {
                    _worker.CancelAsync();
                    while (_worker.IsBusy) ;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
