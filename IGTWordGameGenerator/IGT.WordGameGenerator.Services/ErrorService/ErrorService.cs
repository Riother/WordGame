using IGT.WordGameGenerator.Localization.ErrorService;
using IGT.WordGameGenerator.Services.ErrorService.Agents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGT.WordGameGenerator.Services.ErrorService
{
	/// <summary>
	/// Manages Errors that occur from the client logic
	/// </summary>
	public class ErrorService : INotifyPropertyChanged
	{
		/// <summary>
		/// Notifies listeners that a property has changed
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		private static int _currentId = 0;
		private IList<Error> _unresolvedErrors;
		private IList<Warning> _unresolvedWarnings;

		private ErrorServiceLocalization _localization;
		private static ErrorService _instance;

		/// <summary>
		/// The singleton instance of the ErrorService
		/// </summary>
		public static ErrorService Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new ErrorService();
				}
				return _instance;
			}
		}

		public void SetLocalization(ErrorServiceLocalization localization)
		{
			_localization = localization;
			UpdateErrorText();
			UpdateWarningText();
		}

		private ErrorService()
		{
			_unresolvedErrors = new List<Error>();
			_unresolvedWarnings = new List<Warning>();
			SetLocalization(new ErrorServiceLocalization());
		}

		#region "error handling"
		private string _errorText;
		/// <summary>
		/// The text containing all of the error messages
		/// </summary>
		public string ErrorText
		{
			get
			{
				return _errorText;
			}
			set
			{
				_errorText = value;
				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs("ErrorText"));
				}
			}
		}

		private void UpdateErrorText()
		{
			StringBuilder errorText = new StringBuilder();
			foreach (var error in _unresolvedErrors)
			{
				errorText.AppendLine("    -" + String.Format(_localization.ErrorMessages[error.MessageId], error.IllegalObjects.ToArray()) + "    ");
			}
			ErrorText = errorText.ToString();
		}

		/// <summary>
		/// Reports Error to have display in Error box
		/// </summary>
		/// <param name="errorCode">The id of the message that needs to be displayed</param>
		/// <param name="illegalObjects">A string representation of the object that caused the problem</param>
		/// <param name="senderId">The id of the object that reported the error</param>
		/// <returns>The sender id</returns>
		public int? ReportError(int errorCode, List<string> illegalObjects, int? senderId)
		{
			if (senderId == null)
			{
				senderId = _currentId++;
			}
			if (errorCode < _localization.ErrorMessages.Length && errorCode >= 0)
			{
				//string errorMessage = String.Format(_localization.ErrorMessages[errorCode], illegalObjects.ToArray());
				Error newError = new Error(senderId, errorCode, illegalObjects);
				if (!_unresolvedErrors.Contains(newError))
				{
					_unresolvedErrors.Add(newError);
				}
				UpdateErrorText();
			}

			return senderId;
		}

		/// <summary>
		/// Removes error from Error display
		/// </summary>
		/// <param name="errorCode">The error id that needs to be removed</param>
		/// <param name="senderId">The id of the object resolving the error</param>
		public void ResolveError(int errorCode, int? senderId)
		{
			Error toBeRemoved = new Error(senderId, errorCode, null);
			if (_unresolvedErrors.Contains(toBeRemoved))
			{
				_unresolvedErrors.Remove(toBeRemoved);
				UpdateErrorText();
			}
		}

		/// <summary>
		/// Returns whether there are currently any errors in the project or not
		/// </summary>
		/// <returns>Returns true if there are any errors</returns>
		public bool HasErrors()
		{
			return _unresolvedErrors.Count > 0;
		}

		/// <summary>
		/// Removes all errors from ErrorService
		/// </summary>
		public void ClearErrors()
		{
			_unresolvedErrors.Clear();
			UpdateErrorText();
		}
		#endregion

		#region "warning handling"
		private string _warningText;
		/// <summary>
		/// The text containing all of the warning messages
		/// </summary>
		public string WarningText
		{
			get
			{
				return _warningText;
			}
			set
			{
				_warningText = value;
				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs("WarningText"));
				}
			}
		}

		private void UpdateWarningText()
		{
			StringBuilder warningText = new StringBuilder();
			foreach (var warning in _unresolvedWarnings)
			{
				warningText.AppendLine("    -" + string.Format(_localization.WarningMessages[warning.MessageId], warning.IllegalObjects.ToArray()) + "    ");
			}
			WarningText = warningText.ToString();
		}

		/// <summary>
		/// Notify ErrorService that a Warning exists in the client logic
		/// </summary>
		/// <param name="warningCode">The id of the message needing to be displayed</param>
		/// <param name="illegalObjects">The string representation of the objects that caused the warning</param>
		/// <param name="senderId">The ErrorService Id of the object sending the warning</param>
		/// <returns>The id of the sender</returns>
		public int? ReportWarning(int warningCode, List<string> illegalObjects, int? senderId)
		{
			if (senderId == null)
			{
				senderId = _currentId++;
			}
			if (warningCode >= 0 && warningCode < _localization.WarningMessages.Length)
			{
				//string warningMessage = string.Format(_localization.WarningMessages[warningCode], illegalObjects.ToArray());
				Warning newWarning = new Warning(senderId, warningCode, illegalObjects);
				if (!_unresolvedWarnings.Contains(newWarning))
				{
					_unresolvedWarnings.Add(newWarning);
				}
			}
			UpdateWarningText();

			return senderId;
		}

		/// <summary>
		/// Removes warning message from warnings display
		/// </summary>
		/// <param name="warningCode">The id of the warning to be removed</param>
		/// <param name="senderId">The ErrorService Id of the object resolving the warning</param>
		public void ResolveWarning(int warningCode, int? senderId)
		{
			Warning toBeRemoved = new Warning(senderId, warningCode, null);
			if (_unresolvedWarnings.Contains(toBeRemoved))
			{
				_unresolvedWarnings.Remove(toBeRemoved);
				UpdateWarningText();
			}
		}

		/// <summary>
		/// Returns whether there are any warnings displayed or not
		/// </summary>
		/// <returns>True if there are any warnings in the ErrorService</returns>
		public bool HasWarnings()
		{
			return _unresolvedWarnings.Count > 0;
		}

		/// <summary>
		/// Removes all warnings from ErrorService
		/// </summary>
		public void ClearWarnings()
		{
			_unresolvedWarnings.Clear();
			UpdateWarningText();
		}
		#endregion

		/// <summary>
		/// Reset _currentId for a new set of ids, removes all errors, and removes all warnings
		/// </summary>
		public void Clear()
		{
			_currentId = 0;
			ClearErrors();
			ClearWarnings();
		}
	}
}
