using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Kontrol_2_Client
{
	public class Logger
	{
		public static log4net.ILog GetLogger([CallerFilePath] string filename = "")
		{
			return log4net.LogManager.GetLogger(filename);
		}

	}
}
