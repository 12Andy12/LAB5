using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;
using System.Text.RegularExpressions;
using System.IO;


namespace LAB5.ViewModels
{
	public class MainWindowViewModel : ViewModelBase
	{
		string InText = "";
		string OutText = "";
		string regex = "";
		public string MyRegex
		{
			get => regex;
			set
			{
				if (value != null)
					regex = value;
			}
		}

		public string Output
		{
			get => OutText;
			set
			{
				if(MyRegex == "")
					this.RaiseAndSetIfChanged(ref OutText, "Error");
				else
					this.RaiseAndSetIfChanged(ref OutText, value);
			}
		}

		public string Input
		{
			get => InText;
			set
			{
				OutText = "";
				if (MyRegex != "")
				{
					Regex rgx = new Regex(MyRegex);
					foreach (Match match in rgx.Matches(value))
						Output += match.Value + "\n";
					if (Output == "")
						Output = "Совпадений не найдено!";
				}
				this.RaiseAndSetIfChanged(ref InText, value);
			}
		}

	}
	
}
