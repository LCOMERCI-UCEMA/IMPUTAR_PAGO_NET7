using System.IO;
using System.Text;

namespace UCEMA.Imputar_Pago_SIGEU.NET7
{
   public class TextBoxOutput : TextWriter
   {
      TextBox output; //Textbox used to show Console's output.

      /// <summary>
      /// Custom <i>TextBox</i>-Class used to print the Console output.
      /// </summary>
      /// <param name="_output">Textbox used to show Console's output.</param>
      public TextBoxOutput(TextBox _output)
      {
         output = _output;

         output.ScrollBars = ScrollBars.Both;
         output.WordWrap = true;
         output.ReadOnly = true;
         output.Multiline = true;

         NewLine = "\r\n";
      }

      /// <summary>
      ///  Appends text to the textbox and to the logfile
      /// </summary>
      /// <param name="value">Input-string which is appended to the textbox.</param>
      public override void Write(char value)
      {
         base.Write(value);
         output.AppendText(value.ToString());//Append char to the textbox
      }

      public void Write(char value, bool clear)
      {
         base.Write(value);
         output.AppendText(value.ToString());//Append char to the textbox
      }

      public override void WriteLine(string? line)
      {
         base.WriteLine(line?.Replace("\n", NewLine));
         output.AppendText(line?.Replace("\n", NewLine) + NewLine); //Append line to the textbox
      }

      public void Clear()
      {
         output.Text = String.Empty;//Append char to the textbox
      }

      public override Encoding Encoding
      {
         get { return System.Text.Encoding.UTF8; }
      }
   }
}
