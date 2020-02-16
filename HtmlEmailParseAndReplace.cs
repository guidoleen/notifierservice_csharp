using System;
using System.Collections.Generic;
using System.Text;

namespace NotifierService
{
	public class HtmlEmailParseAndReplace : IParseAndReplacer<string>
	{
		public string ParseAndReplace(INotifier<string> _notifier)
		{
			var notifier = (HtmlEmailNotifier)_notifier; // Casting is nescessary >> The parser must know which properties are.
			FileReaderWriter fw = new FileReaderWriter($"{notifier.NotifierId}.html", $"{notifier.NotifierTemplateDir}");

			var template = fw.ReadFromFile();

			var result = NotifierServiceHelper.NotifierTemplateParser(notifier, template.Result);

			return result;
		}
	}
}
