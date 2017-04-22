using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace TKF
{
	public class CSVUtil
	{
		/// <summary>
		/// csvデータを配列で取得
		/// </summary>
		/// <returns>The data.</returns>
		/// <param name="csvPath">Csv path.</param>
		static public List<string[]> GetListFromLocalResource (string csvFilePath)
		{
			TextAsset csv = Resources.Load (csvFilePath) as TextAsset;
			return GetList (csv.text);
		}

		/// <summary>
		/// Gets the list.
		/// </summary>
		/// <returns>The list.</returns>
		/// <param name="csv">Csv.</param>
		static public List<string[]> GetList (string csv)
		{
			return LoadFromString (csv);
		}

		/// <summary>
		/// Loads from file.
		/// </summary>
		/// <returns>The from file.</returns>
		/// <param name="file_name">File name.</param>
		public static List<string[]> LoadFromFile (string file_name)
		{
			return LoadFromString (File.ReadAllText (file_name));
		}

		/// <summary>
		/// Loads from string.
		/// </summary>
		/// <returns>The from string.</returns>
		/// <param name="file_contents">File contents.</param>
		public static List<string[]> LoadFromString (string file_contents)
		{
			int file_length = file_contents.Length;
			// read char by char and when a , or \n, perform appropriate action
			int cur_file_index = 0; // index in the file
			List<string[]> allList = new List<string[]> ();
			List<string> cur_line = new List<string> (); // current line of data
			int cur_line_number = 0;
			StringBuilder cur_item = new StringBuilder ("");
			bool inside_quotes = false; // managing quotes
			while (cur_file_index < file_length) {
				char c = file_contents [cur_file_index++];
				switch (c) {
				case '"':
					if (!inside_quotes) {
						inside_quotes = true;
					} else {
						if (cur_file_index == file_length) {
							// end of file
							inside_quotes = false;
							goto case '\n';
						} else if (file_contents [cur_file_index] == '"') {
							// double quote, save one
							cur_item.Append ("\"");
							cur_file_index++;
						} else {
							// leaving quotes section
							inside_quotes = false;
						}
					}
					break;
				case '\r':
				// ignore it completely
					break;
				case ',':
					goto case '\n';
				case '\n':
					if (inside_quotes) {
						// inside quotes, this characters must be included
						cur_item.Append (c);
					} else {
						// end of current item
						cur_line.Add (cur_item.ToString ());
						cur_item.Length = 0;
						if (c == '\n' || cur_file_index == file_length) {
							// also end of line, call line reader
							cur_line_number++;
							allList.Add (cur_line.ToArray ());
							cur_line.Clear ();
						}
					}
					break;
				default:
				// other cases, add char
					cur_item.Append (c);
					break;
				}
			}
			cur_line.Add (cur_item.ToString ());
			allList.Add (cur_line.ToArray ());
			return allList;
		}
	}
}