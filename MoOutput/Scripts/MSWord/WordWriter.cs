using System;
using System.Reflection;
using Word = Microsoft.Office.Interop.Word;

using Mophi;

namespace Mophi.Output
{
	class WordWriter
	{
		/// word application object 
		private Word.Application _wordApp;

		/// word file object
		private Word.Document _wordDoc;

		/// default param
		private object nothing = Missing.Value;

		/// <summary>
		/// Create the file.
		/// </summary>
		public void CreateWord()
		{
			/// realization
			_wordApp = new Word.Application();
			Object myNothing = Missing.Value;

			_wordDoc = _wordApp.Documents.Add(ref myNothing, ref myNothing, ref myNothing, ref myNothing);
		}

		/// <summary>
		/// Close the file.
		/// </summary>
		/// <param name="psaveopt">
		/// Options:
		///   Microsoft.Office.Interop.Word.WdSaveOptions.wdPromptToSaveChanges = -2,
		///   Microsoft.Office.Interop.Word.wdSaveChanges = -1,
		///   Microsoft.Office.Interop.Word.wdDoNotSaveChanges = 0,
		/// </param>
		public void CloseWord(Word.WdSaveOptions psaveopt)
		{
			object saveopt = psaveopt;

			((Word._Document)_wordDoc).Close(ref saveopt, ref nothing, ref nothing);
			((Word._Application)_wordApp).Quit(ref saveopt, ref nothing, ref nothing);
		}

		/// <summary>
		/// Set page header.
		/// </summary>
		/// <param name="pPageHeader">Header content</param>, .
		public void SetPageHeader(string pPageHeader)
		{
			// add page header  
			_wordApp.ActiveWindow.View.Type = Word.WdViewType.wdOutlineView;
			_wordApp.ActiveWindow.View.SeekView = Word.WdSeekView.wdSeekPrimaryHeader;
			_wordApp.ActiveWindow.ActivePane.Selection.InsertAfter(pPageHeader);
			
			// set alignment
			_wordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
			
			// 跳出页眉设置   
			_wordApp.ActiveWindow.View.SeekView = Word.WdSeekView.wdSeekMainDocument;
		}

		/// <summary>
		/// Insert content.
		/// </summary>
		/// <param name="pText">Content text</param>
		/// <param name="pFontSize">Font size</param>
		/// <param name="pFontColor">Font color</param>
		/// <param name="pFontBold">Font bold</param>
		/// <param name="ptextAlignment">Text alignment</param>
		public void InsertText(string pText, int pFontSize, Word.WdColor pFontColor, int pFontBold, Word.WdParagraphAlignment ptextAlignment)
		{
			_wordApp.Application.Selection.Font.Size = pFontSize;
			_wordApp.Application.Selection.Font.Bold = pFontBold;
			_wordApp.Application.Selection.Font.Color = pFontColor;
			_wordApp.Application.Selection.ParagraphFormat.Alignment = ptextAlignment;
			_wordApp.Application.Selection.TypeText(pText);
		}


		/// <summary>
		/// Insert a new line.
		/// </summary>
		public void NewLine()
		{
			_wordApp.Application.Selection.TypeParagraph();
		}

		/// <summary>
		/// Insert a picture.
		/// </summary>
		/// <param name="pPictureFileName">File name</param>
		public void InsertPicture(string pPictureFileName)
		{
			object myNothing = Missing.Value;

			// mid-center
			_wordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
			_wordApp.Application.Selection.InlineShapes.AddPicture(pPictureFileName, ref myNothing, ref myNothing, ref myNothing);
		}

		/// <summary>
		/// Save as a MS-Word file.
		/// </summary>
		/// <param name="pFileName">File name</param>
		public void SaveWord(string pFileName)
		{
			object myNothing = Missing.Value;
			object myFileName = pFileName;
			object myWordFormatDocument = Word.WdSaveFormat.wdFormatDocument;
			object myLockd = false;
			object myPassword = "";
			object myAddto = true;

			try
			{
				_wordDoc.SaveAs(ref myFileName, ref myWordFormatDocument, ref myLockd, ref myPassword, ref myAddto, ref myPassword,
				ref myLockd, ref myLockd, ref myLockd, ref myLockd, ref myNothing, ref myNothing, ref myNothing,
				ref myNothing, ref myNothing, ref myNothing);
			}
			catch (Exception e)
			{
				Logger.Error("Save file failed." + e.StackTrace);
			}
		}
	}
}
