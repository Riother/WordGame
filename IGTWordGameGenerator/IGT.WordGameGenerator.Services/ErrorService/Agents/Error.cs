using IGT.WordGameGenerator.Services.ErrorService.Agents;
using System.Collections.Generic;

namespace IGT.WordGameGenerator.Services.ErrorService.Agents
{
	/// <summary>
	/// A severe issue with the logic provided from the user. Prevents file generation
	/// </summary>
	public class Error : ErrorServiceAgent
    {
        /// <summary>
        /// Constructor of severe issues
        /// </summary>
        /// <param name="senderId">The id of the object sending the error</param>
        /// <param name="messageId">The id of the message that needs to be displayed</param>
        public Error(int? senderId, int messageId, IList<string> illegalObjects) : base(senderId, messageId, illegalObjects)
        {
            IsError = true;
        }
    }
}
