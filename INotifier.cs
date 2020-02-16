using System;
using System.Collections.Generic;
using System.Text;

namespace NotifierService
{
	public interface INotifier<T>
	{
		T GetNotifier(IParseAndReplacer<T> parser);
	}
}
