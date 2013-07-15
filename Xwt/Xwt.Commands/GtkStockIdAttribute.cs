//
// GtkStockIdAttribute.cs
//
// Author:
//       David Lechner <david@lechnology.com>
//
// Copyright (c) 2013 David Lechner
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using Xwt;

namespace Xwt.Commands
{
	/// <summary>
	/// Specifies the Gtk stock item equivalent of a command.
	/// </summary>
	/// <remarks>
	/// StockId is one of the values returned by the members of <see cref="Gtk.Stock"/>
	/// from http://www.gtk.org/api/2.6/gtk/gtk-Stock-Items.html
	/// <example>[GtkStockId("gtk-new")]</example>
	/// </remarks>
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
	public class GtkStockIdAttribute : Attribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Xwt.Commands.GtkStockIdAttribute"/> class.
		/// </summary>
		/// <param name="selector">Stock item identifier.</param>
		public GtkStockIdAttribute (string stockId)
		{
			StockId = stockId;
		}

		public string StockId { get; private set; }
	}
}

