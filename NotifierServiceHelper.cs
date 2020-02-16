using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Reflection;

namespace NotifierService
{
	public static class NotifierServiceHelper
	{
		private static string RegexGetPropery = @"\{\{(.*?)\}\}";
		private static string RegexRemoveBrackets = @"[\{\{(.*?)\}\}]";
		public static List<string> GetProperiesWithBracketsFromTextList(string input)
		{
			List<string> listOfProperties = new List<string>();
			Regex regex = new Regex(RegexGetPropery);
			var matches = regex.Matches(input);
			foreach (var match in matches)
			{
				listOfProperties.Add(match.ToString());
			}
			return listOfProperties;
		}

		public static string ReplacePropertyToValue(string template, string propertyName, object value)
		{
			Regex regex = new Regex("{{(" + propertyName + "?)}}");
			var result = regex.Replace(template, value.ToString());
			return result;
		}

		private static string ReplaceBracketsFromProperies(string input)
		{
			Regex regex = new Regex(RegexRemoveBrackets);
			return regex.Replace(input, "");
		}

		public static string RemoveDotFromFileName(string input)
		{
			Regex regex = new Regex(".");
			if(regex.Match(input).Success)
			{
				string[] firstPart = input.Split('.');
				input = firstPart[0];
			}
			return input;
		}

		public static string ConvertFromByteToBase64(byte[] bytes)
		{
			return Convert.ToBase64String(bytes);
		}

		public static string CreateImgTagFromBytesToBase64(byte[] bytes)
		{
			return $"<img src=\"data:image/jpg; base64, {ConvertFromByteToBase64(bytes)} \"/>";
		}

		public static string CreateImgTagFromLink(string url, string id, string width, string height, string style)
		{
			return $"<img border=0 width={width} height={height} style='{style}' id='{id}' src='{url}' >";
		}

		public static string NotifierTemplateParser(INotifier<string> _notifier, string _template)
		{
			var result = _template;
			var notifier = _notifier; // (HtmlEmailNotifier)
			PropertyInfo[] notifierproperties = notifier.GetType().GetProperties();

			foreach (var property in notifierproperties)
			{
				var properyValue = property.GetValue(notifier)?.ToString() ?? "";
				result = ReplacePropertyToValue(result, property.Name, properyValue);
			}
			return result;
		}
	}
}