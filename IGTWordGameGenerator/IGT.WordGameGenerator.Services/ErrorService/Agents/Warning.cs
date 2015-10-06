using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGT.WordGameGenerator.Services.ErrorService.Agents
{
    /// <summary>
    /// A warning that suggests that undesired behavior may occur, but not enough of a concern to prevent file generation
    /// </summary>
    public class Warning : ErrorServiceAgent
    {
        /// <summary>
        /// Constructor of warning
        /// </summary>
        /// <param name="senderId">The id of the object sending the warning</param>
        /// <param name="messageId">The id of the message the warning will contain</param>
        public Warning(int? senderId, int messageId, IList<string> illegalObjects) : base(senderId, messageId, illegalObjects)
        {
            IsError = false;
        }
    }
}
