using System;
using System.Collections.Generic;
using System.Text;

namespace NotifierService
{
	public class HtmlEmailNotifier : INotifier<string>
	{
		public string From { get; set; }
		public string To { get; set; }
		public string Logo { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Gender { get; set; }
		public string NotifierId { get; set; }
		public string NotifierTemplateDir { get; set; }
		public string Message { get; set; }

		public string GetNotifier(IParseAndReplacer<string> parser)
		{
			return $"{parser.ParseAndReplace(this)}";
		}
	}
}
