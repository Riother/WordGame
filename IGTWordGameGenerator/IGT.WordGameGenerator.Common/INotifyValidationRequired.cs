using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGT.WordGameGenerator.Common
{
	public interface INotifyValidationRequired
	{
		event ValidationRequested ValidationRequest;
	}
}
