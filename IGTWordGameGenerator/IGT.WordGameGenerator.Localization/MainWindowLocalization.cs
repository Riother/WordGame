using IGT.WordGameGenerator.Localization.Division;
using IGT.WordGameGenerator.Localization.ErrorService;
using IGT.WordGameGenerator.Localization.GameSetup;
using IGT.WordGameGenerator.Localization.PrizeLevel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGT.WordGameGenerator.Localization
{
	[Serializable]
	public class MainWindowLocalization : MainLocalization
	{
		public string File
		{
			get { return _file; }
			set
			{
				_file = value;
				SendPropertyChanged(this, new PropertyChangedEventArgs("File"));
			}
		}
		public string New
		{
			get { return _new; }
			set
			{
				_new = value;
				SendPropertyChanged(this, new PropertyChangedEventArgs("New"));
			}
		}
		public string Open
		{
			get { return _open; }
			set
			{
				_open = value;
				SendPropertyChanged(this, new PropertyChangedEventArgs("Open"));
			}
		}
		public string Save
		{
			get { return _save; }
			set
			{
				_save = value;
				SendPropertyChanged(this, new PropertyChangedEventArgs("Save"));
			}
		}
		public string SaveAs
		{
			get { return _saveAs; }
			set
			{
				_saveAs = value;
				SendPropertyChanged(this, new PropertyChangedEventArgs("SaveAs"));
			}
		}
		public MainWindowLocalization()
		{
			_title = "Word Game Generator";
			_file = "File";
			_new = "New";
			_open = "Open";
			_save = "Save";
			_saveAs = "Save As…";
			_prizeLevelsPanel = new PrizeLevelsPanelUserControlLocalization();
			_gameSetupPanel = new GameSetupUserControlLocalization();
			_divisionPanel = new DivisionsPanelUserControlLocalization();
			_errorService = new ErrorServiceLocalization();
		}

		public PrizeLevelsPanelUserControlLocalization PrizeLevelsPanel
		{
			get { return _prizeLevelsPanel; }
			set
			{
				_prizeLevelsPanel = value ?? new PrizeLevelsPanelUserControlLocalization();
				SendPropertyChanged(this, new PropertyChangedEventArgs("PrizeLevelsPanel"));
			}
		}

		public GameSetupUserControlLocalization GameSetupPanel
		{
			get { return _gameSetupPanel; }
			set
			{
				_gameSetupPanel = value ?? new GameSetupUserControlLocalization();
				SendPropertyChanged(this, new PropertyChangedEventArgs("GameSetupPanel"));
			}
		}

		public DivisionsPanelUserControlLocalization DivisionPanel
		{
			get { return _divisionPanel; }
			set
			{
				_divisionPanel = value ?? new DivisionsPanelUserControlLocalization();
				SendPropertyChanged(this, new PropertyChangedEventArgs("DivisionPanel"));
			}
		}

		public ErrorServiceLocalization ErrorService
		{
			get { return _errorService; }
			set
			{
				_errorService = value;

				SendPropertyChanged(this, new PropertyChangedEventArgs("ErrorService"));
            }
		}
		private string _file;
		private string _new;
		private string _open;
		private string _save;
		private string _saveAs;
		private PrizeLevelsPanelUserControlLocalization _prizeLevelsPanel;
		private GameSetupUserControlLocalization _gameSetupPanel;
		private DivisionsPanelUserControlLocalization _divisionPanel;
		private ErrorServiceLocalization _errorService;
	}
}
