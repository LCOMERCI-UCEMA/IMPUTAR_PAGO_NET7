using System.IO;
using System.Text;

namespace UCEMA.Imputar_Pago_SIGEU.NET7
{
   public class TextBoxConsole
   {
      private TextBox _consoleOutput;
      private TextBox _consoleError;
      private TextWriter _stdout;
      private TextWriter _stderr;

      public TextBoxConsole(TextBox consoleOutput, TextBox consoleError)
      {
         _consoleOutput = consoleOutput;
         _consoleError = consoleError;
         _stdout = Console.Out;
         _stderr = Console.Error;

         Console.SetOut(new TextBoxWriter(_consoleOutput));
         Console.SetError(new TextBoxWriter(_consoleError));
      }

      public void Clear()
      {
         ClearStdout();
         ClearStderr();
      }

      public void WriteLine()
      {
         System.Console.WriteLine();
      }

      public void WriteLine(string? value)
      {
         System.Console.WriteLine(value);
      }

      public void Write(char value)
      {
         System.Console.Write(value);
      }

      // ahora necesito la versión correspondiente de WriteLine
      public void WriteLine(char value)
      {
         System.Console.WriteLine(value);
      }

      public void ClearStdout()
      {
         _consoleOutput.Invoke((MethodInvoker) delegate ()
         {
            _consoleOutput.Clear();
         });
      }

      public void ClearStderr()
      {
         _consoleError.Invoke((MethodInvoker) delegate ()
         {
            _consoleError.Clear();
         });
      }

      private class TextBoxWriter : TextWriter
      {
         private TextBox _textbox;
         public TextBoxWriter(TextBox textbox)
         {
            _textbox = textbox;
            Encoding = Encoding.UTF8;
         }

         public override Encoding Encoding { get; }

         public override void WriteLine(string? value)
         {
            _textbox.Invoke((MethodInvoker) delegate ()
            {
               _textbox.AppendText(value + Environment.NewLine);
            });
         }

         public override void Write(char value)
         {
            _textbox.Invoke((MethodInvoker) delegate ()
            {
               _textbox.AppendText(value.ToString());
            });
         }

         // ahora necesito la versión correspondiente de WriteLine
         public override void WriteLine(char value)
         {
            _textbox.Invoke((MethodInvoker) delegate ()
            {
               _textbox.AppendText(value.ToString() + Environment.NewLine);
            });
         }
      }
   }
}

