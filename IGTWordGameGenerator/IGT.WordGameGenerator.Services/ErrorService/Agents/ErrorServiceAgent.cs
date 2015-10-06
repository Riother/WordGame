using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGT.WordGameGenerator.Services.ErrorService.Agents
{
	/// <summary>
	/// An issue that is managed by the ErrorService
	/// </summary>
	public abstract class ErrorServiceAgent
	{
		/// <summary>
		/// Id of the object sending the agent
		/// </summary>
		public int? SenderId { get; set; }
		/// <summary>
		/// Id of the message that needs to be added
		/// </summary>
		public int MessageId { get; set; }
		/// <summary>
		/// True if Agent is an Error, false if a Warning
		/// </summary>
		public bool IsError { get; set; }
		public IList<string> IllegalObjects { get; set; }
		/// <summary>
		/// Constructor for ErrorServiceAgent
		/// </summary>
		/// <param name="senderId">The ErrorServiceId of the object that reported the Agent</param>
		/// <param name="messageId">The message id that will determine the message to display on the ErrorService</param>
		public ErrorServiceAgent(int? senderId, int messageId, IList<string> illegalObjects)
		{
			SenderId = senderId;
			MessageId = messageId;
			IllegalObjects = illegalObjects;
		}

		/// <summary>
		/// Compares Agent against another Agent
		/// </summary>
		/// <param name="obj">The object being compared against</param>
		/// <returns>True if the sender id and message id are equal, and if the agents are both errors or both warnings</returns>
		public override bool Equals(object obj)
		{
			if (obj is ErrorServiceAgent)
			{
				return GetHashCode() == (obj as ErrorServiceAgent).GetHashCode() && IsError == (obj as ErrorServiceAgent).IsError;
			}
			return false;
		}

		/// <summary>
		/// Returns the hash representation of the agent
		/// </summary>
		/// <returns>The hashcode representation of agent</returns>
		public override int GetHashCode()
		{
			return (MessageId.GetHashCode() * 397);
		}
	}
}
