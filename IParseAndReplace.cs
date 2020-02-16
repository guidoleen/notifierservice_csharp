using System;
using System.Collections.Generic;
using System.Text;

namespace NotifierService
{
	public interface IParseAndReplacer<T>
	{
		T ParseAndReplace(INotifier<T> notifier);
	}
}
