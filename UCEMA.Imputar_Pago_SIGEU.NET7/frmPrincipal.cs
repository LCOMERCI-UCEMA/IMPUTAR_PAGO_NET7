using System;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;

using Entidades;
using Stream = Entidades.Stream;
using ComboBoxItem = Entidades.ComboBoxItem;
using ValidationException = Entidades.ValidationException;

using UCEMA.Imputar_Pago_SIGEU.NET7;

namespace UCEMA.Imputar_Pago_SIGEU.NET7
{
   public partial class frmPrincipal : Form
   {
      #region Constantes

      public const string CARPETA_REAL = "sigeu";
      public const string CARPETA_FAKE = "fake-sigeu";

      public const string RUTA_HOME_ORACLE = "/home/oracle";
      public const string RUTA_CARPETA_ARCHIVO_RENOMBRADO = "./output";

      public const string RUTA_IMPUTACION_VISA_DEBITO = RUTA_HOME_ORACLE + "/" + CARPETA_REAL + "/" + "visa";
      public const string RUTA_IMPUTACION_VISA_CREDITO = RUTA_HOME_ORACLE + "/" + CARPETA_REAL + "/" + "visa";
      public const string RUTA_IMPUTACION_AMERICANEXPRESS_CREDITO = RUTA_HOME_ORACLE + "/" + CARPETA_REAL + "/" + "americanexpress";
      public const string RUTA_IMPUTACION_MASTERCARD_CREDITO = RUTA_HOME_ORACLE + "/" + CARPETA_REAL + "/" + "mastercard";
      public const string RUTA_IMPUTACION_CBU_DEBITO = RUTA_HOME_ORACLE + "/" + CARPETA_REAL + "/" + "cbu";

      public const string RUTA_IMPUTACION_FAKE_VISA_DEBITO = RUTA_HOME_ORACLE + "/" + CARPETA_FAKE + "/" + "visa";
      public const string RUTA_IMPUTACION_FAKE_VISA_CREDITO = RUTA_HOME_ORACLE + "/" + CARPETA_FAKE + "/" + "visa";
      public const string RUTA_IMPUTACION_FAKE_AMERICANEXPRESS_CREDITO = RUTA_HOME_ORACLE + "/" + CARPETA_FAKE + "/" + "americanexpress";
      public const string RUTA_IMPUTACION_FAKE_MASTERCARD_CREDITO = RUTA_HOME_ORACLE + "/" + CARPETA_FAKE + "/" + "mastercard";
      public const string RUTA_IMPUTACION_FAKE_CBU_DEBITO = RUTA_HOME_ORACLE + "/" + CARPETA_FAKE + "/" + "cbu";

      public const string FAKE_COBRO_SH = "fake-cobro.sh";
      public const string LEVANTAR_COBRO_SH = "levantar_cobro.sh";
      public const string LEVANTAR_COBRO_VISA_CREDITO_SH = "levantar_cobro_visaCred.sh";
      public const string LEVANTAR_COBRO_MASTERCARD_CREDITO_SH = "levantar_cobro_master.sh";

      public const string REMOTE_LOCATION = "10.0.0.11";
      public const string REMOTE_USERNAME = "oracle";
      public const string REMOTE_PASSWORD = "mol8mol8";

      #endregion

      public static Dictionary<string, string>? rutasDestino, scriptsDeCobro;

      // Writers 
      //protected TextWriter writerStdout;
      //protected TextWriter writerStderr;
      protected TextBoxConsole tbConsole;

      /// <summary>
      /// Indica si la aplicación se está ejecutando en MODO de PRUEBA o no.
      /// </summary>
      /// 
      //public bool isFakeRun = true; // MODO PRUEBA.
      public bool isFakeRun = false;  // MODO PRODUCTIVO.

      public frmPrincipal()
      {
         InitializeComponent();

         tbConsole = new TextBoxConsole(txtStdout, txtStderr);

         rutasDestino = new Dictionary<string, string>
         {
            { "VISADEB",    isFakeRun ? RUTA_IMPUTACION_FAKE_VISA_DEBITO             : RUTA_IMPUTACION_VISA_DEBITO },
            { "VISACRED",   isFakeRun ? RUTA_IMPUTACION_FAKE_VISA_CREDITO            : RUTA_IMPUTACION_VISA_CREDITO },
            { "AMEXCRED",   isFakeRun ? RUTA_IMPUTACION_FAKE_AMERICANEXPRESS_CREDITO : RUTA_IMPUTACION_AMERICANEXPRESS_CREDITO },
            { "MASTERCRED", isFakeRun ? RUTA_IMPUTACION_FAKE_MASTERCARD_CREDITO      : RUTA_IMPUTACION_MASTERCARD_CREDITO },
            { "CBUDEB",     isFakeRun ? RUTA_IMPUTACION_FAKE_CBU_DEBITO              : RUTA_IMPUTACION_CBU_DEBITO }
         };

         scriptsDeCobro = new Dictionary<string, string>
         {
            { "VISADEB",    isFakeRun ? FAKE_COBRO_SH : LEVANTAR_COBRO_SH },
            { "VISACRED",   isFakeRun ? FAKE_COBRO_SH : LEVANTAR_COBRO_VISA_CREDITO_SH },
            { "AMEXCRED",   isFakeRun ? FAKE_COBRO_SH : LEVANTAR_COBRO_SH },
            { "MASTERCRED", isFakeRun ? FAKE_COBRO_SH : LEVANTAR_COBRO_MASTERCARD_CREDITO_SH },
            { "CBUDEB",     isFakeRun ? FAKE_COBRO_SH : LEVANTAR_COBRO_SH }
         };
      }

      private async void btnCargar_Click(object sender, EventArgs e)
      {
         tbConsole.Clear();

         if (cboTipoImputacion.SelectedIndex == 0)
         {
            MessageBox.Show("Debe seleccionar un tipo de imputación", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            tbConsole.WriteLine("Debe seleccionar un tipo de imputación");
            return;
         }

         ComboBoxItem oImputacionElegida = (ComboBoxItem) cboTipoImputacion.SelectedItem;
         tbConsole.WriteLine($"Se seleccionó la opción: \"{oImputacionElegida.Caption}\"");

         SetFiltroPorImputacion(dlgAbrirArchivo, oImputacionElegida);

         string sOutputFolder = RUTA_CARPETA_ARCHIVO_RENOMBRADO;

         if (dlgAbrirArchivo.ShowDialog() == DialogResult.OK)
         {
            string sArchivoImputacion = dlgAbrirArchivo.FileName;
            string sNuevoNombre = GetNombrePorImputacion(oImputacionElegida);
            string sNuevaRuta = $"{sOutputFolder}/{sNuevoNombre}";

            tbConsole.WriteLine($"Abrir: {sArchivoImputacion}...");
            tbConsole.WriteLine("Validando...");

            Infotrans(sOutputFolder);
            CrearCarpetaDeSalida(sOutputFolder);

            // Validacion del formato del archivo (contenidos)
            Boolean archivoValido = ValidarArchivo(sArchivoImputacion, oImputacionElegida.Code);

// ====================================================================================================
// Application.Exit(); return;
// ====================================================================================================

            if (!archivoValido)
            {
               //tbConsole.Clear();
               tbConsole.WriteLine($"ERROR: El archivo '{sArchivoImputacion}' no tiene el formato correcto ({oImputacionElegida.Code}).");
               return;
            }

            // Copiar el archivo a la carpeta de salida ya renombrado
            CopiarArchivo(oImputacionElegida, sOutputFolder, sArchivoImputacion, sNuevaRuta);

            string sArchivoOrigen = $"{sOutputFolder}/{GetNombrePorImputacion(oImputacionElegida)}";
            string sRutaDestino = rutasDestino[oImputacionElegida.Code];

            #region FAKE RUN

            if (isFakeRun)
            {
               await RunAsync(() =>
               {
                  // Fakear estructura de SIGEU en destino:
                  Stream streamFakearSIGEU = EjecutarComando("plink", $"-batch -ssh {REMOTE_USERNAME}@{REMOTE_LOCATION} -pw {REMOTE_PASSWORD} -m ./fake-sigeu.txt");
                  MostrarStream(streamFakearSIGEU);

                  // Copiar el archivo fake-cobro.sh a la carpeta de destino
                  Stream streamCopiarFakeCobro = TransferirArchivo($"./{FAKE_COBRO_SH}", $"{sRutaDestino}/");
                  MostrarStream(streamCopiarFakeCobro);

                  // Darle permisos de ejecución al archivo fake-cobro.sh
                  Stream streamChmodFakeCobro = EjecutarComandoRemoto("chmod", $"+x {sRutaDestino}/{FAKE_COBRO_SH}");
                  MostrarStream(streamChmodFakeCobro);

                  // Corregir los caracteres de control (CR) del archivo fake-cobro.sh
                  Stream streamFixControlChars = EjecutarComandoRemoto("sed", $"-i.bak -e 's/\r$//' {sRutaDestino}/{FAKE_COBRO_SH}");
                  MostrarStream(streamFixControlChars);
               });

            } // end if (isFakeRun) =========
            #endregion

            await RunAsync(() =>
            {
               // SUBIR IMPUTACION:
               Stream streamCopiarImputacion = EjecutarComando("pscp", $"-batch -pw mol8mol8 {sArchivoOrigen} oracle@10.0.0.11:{sRutaDestino}/");
               MostrarStream(streamCopiarImputacion);

               // LEVANTAR SCRIPT DE COBRO:
               Stream streamLevantarCobro = EjecutarComando("plink",
                  $"-batch -ssh {REMOTE_USERNAME}@{REMOTE_LOCATION} -pw {REMOTE_PASSWORD} " +
                     $"cd {sRutaDestino} && sh {scriptsDeCobro[oImputacionElegida.Code]}");
               MostrarStream(streamLevantarCobro);
            });

            // VERIFICAR la IMPUTACION:
            tbConsole.WriteLine("Verificamos imputaciones contra la DB...");
            dgvVerificacion.DataSource = await Task.Run(() =>
            {
               return Logica.Script.GetVerificacionImputacion();
            });
            tbConsole.WriteLine("Listo!");
         }
      }

      /// <summary>
      /// Ejecuta código en un hilo separado, mostrando una animación de carga en el formulario.
      /// </summary>
      /// 
      /// <param name="action">Código a ejecutar.</param>
      /// <param name="showLoading">Indica si se debe mostrar la animación de carga</param>
      /// <returns></returns>
      public async Task RunAsync(Action action, bool showLoading = true)
      {
         if (showLoading)
         {
            LoadingON();
         }

         await Task.Run(action);

         if (showLoading)
         {
            LoadingOFF();
         }
      }

      private void Infotrans(string texto)
      {
         tlblStatus.Text = texto;
      }

      private void frmPrincipal_Load(object sender, EventArgs e)
      {
         SetupTipoImputacion();
      }

      private void frmPrincipal_KeyDown(object sender, KeyEventArgs e)
      {
         switch (e.KeyCode)
         {
            case Keys.Escape:
               {
                  DialogResult res = MessageBox.Show("¿Está seguro que desea salir?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                  if (res == DialogResult.Yes)
                  {
                     Application.Exit();
                  }
                  break;
               }
         }
      }
      private void btnLimpiarStdout_Click(object sender, EventArgs e)
      {
         //((TextBoxOutput) writerStdout).Clear();
         tbConsole.ClearStdout();
      }

      private void btnLimpiarStderr_Click(object sender, EventArgs e)
      {
         //((TextBoxOutput) writerStderr).Clear();
         tbConsole.ClearStderr();
      }

      /**********************************************************
       *              Métodos privados de la clase               
       **********************************************************/

      private void LoadingON()
      {
         picLoading.Visible = true;
      }

      private void LoadingOFF()
      {
         picLoading.Visible = false;
      }

      private static void CrearCarpetaDeSalida(string sOutputFolder)
      {
         // Crear carpeta de salida si no existe
         if (!Directory.Exists(sOutputFolder))
         {
            DirectoryInfo oOutputFolderInfo = Directory.CreateDirectory(sOutputFolder);
         }
      }

      private static void CopiarArchivo(ComboBoxItem oImputacionElegida, string sOutputFolder, string sArchivoImputacion, string sNuevaRuta)
      {
         if (File.Exists(sNuevaRuta))
         {
            string sNombreBackup = GetNombrePorImputacion(oImputacionElegida, true);

            if (File.Exists($"{sOutputFolder}/{sNombreBackup}"))
            {
               File.Delete($"{sOutputFolder}/{sNombreBackup}");
            }
            File.Move(sNuevaRuta, $"{sOutputFolder}/{sNombreBackup}"); // Vamos sobreescribiendo el archivo de backup que se genere muy rápido.
         }

         File.Copy(sArchivoImputacion, sNuevaRuta);
      }

      private void SetFiltroPorImputacion(OpenFileDialog dlgAbrirArchivo, ComboBoxItem oImputacionElegida)
      {
         string sFiltroArchivos = "";

         switch (oImputacionElegida.Code)
         {
            case "VISADEB": sFiltroArchivos = "Visa (Débito) (LDEBLIQD*.*)|LDEBLIQD*.*"; break;
            case "VISACRED": sFiltroArchivos = "Visa (Crédito) (RDEBLIQC*.*)|RDEBLIQC*.*"; break;
            case "AMEXCRED": sFiltroArchivos = "American Express (Crédito) (*.*)|*.*"; break;
            case "MASTERCRED": sFiltroArchivos = "Mastercard (Crédito) (PROC*.*)|*.*"; break;
            case "CBUDEB": sFiltroArchivos = "CBU (Débito) (*.*)|*.*"; break;
         }

         sFiltroArchivos += "|Todos los archivos (*.*)|*.*";
         dlgAbrirArchivo.Filter = sFiltroArchivos;
      }

      private static string GetNombrePorImputacion(ComboBoxItem oImputacionElegida, Boolean esParaBackup = false)
      {
         string sNuevoNombre = "procesar.txt";

         switch (oImputacionElegida.Code)
         {
            case "VISADEB": sNuevoNombre = $"LDEBLIQD_{DateTime.Now:yyyyMMdd}.txt"; break;
            case "VISACRED": sNuevoNombre = $"RDEBLIQC_{DateTime.Now:yyyyMMdd}.txt"; break;
            case "AMEXCRED": sNuevoNombre = "DAS_UCEMA_RETURN.txt"; break;
            case "MASTERCRED": sNuevoNombre = "DA130D.txt"; break;
            case "CBUDEB": sNuevoNombre = $"{DateTime.Now:yyyyMMdd}.xxx"; break;
         }

         if (esParaBackup)
         {
            sNuevoNombre += $".{DateTime.Now:yyyyMMdd}.bak";
         }

         return sNuevoNombre;
      }

      private Boolean ValidarArchivo(string sArchivoImputacion, string codigoImputacion, int iCantLineasAValidar = 0)
      {
         StreamReader lector = new StreamReader(sArchivoImputacion);
         Boolean chequeoArchivo;
         int iCantLineasArchivo = GetCantLineasArchivo(sArchivoImputacion);

         if (iCantLineasAValidar > iCantLineasArchivo)
         {
            //MessageBox.Show("La cantidad de líneas a validar supera a la cantidad de líneas del archivo. Se validará todo el contenido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            tbConsole.WriteLine("La cantidad de líneas a validar supera a la cantidad de líneas del archivo. Se validará todo el contenido.");
            iCantLineasAValidar = iCantLineasArchivo;
         }

         if (iCantLineasAValidar == 0)
         {
            iCantLineasAValidar = iCantLineasArchivo;
         }

         List<Boolean> chequeoLineas;

         switch (codigoImputacion)
         {
            case "VISADEB": chequeoLineas = ValidarLineasArchivoVisaDebito(sArchivoImputacion, iCantLineasArchivo, ref lector, iCantLineasAValidar); break;
            case "VISACRED": chequeoLineas = ValidarLineasArchivoVisaCredito(sArchivoImputacion, iCantLineasArchivo, ref lector, iCantLineasAValidar); break;
            case "AMEXCRED": chequeoLineas = ValidarLineasArchivoAmericanExpressCredito(sArchivoImputacion, iCantLineasArchivo, ref lector, iCantLineasAValidar); break;
            case "MASTERCRED": chequeoLineas = ValidarLineasArchivoMastercardCredito(sArchivoImputacion, iCantLineasArchivo, ref lector, iCantLineasAValidar); break;
            case "CBUDEB": chequeoLineas = ValidarLineasArchivoCBUDebito(sArchivoImputacion, iCantLineasArchivo, ref lector, iCantLineasAValidar); break;

            default: throw new ValidationException($"Formato de archivo desconocido: '{codigoImputacion}'.");
         }

         lector.Close();

         chequeoArchivo = true; // Se asume true. Y si alguna condición falla, la validación falla.
         List<int> lineasFalladas = new List<int>();

         for (int i = 0; i < iCantLineasAValidar; i++)
         {
            if (chequeoLineas[i] == false)
            {
               lineasFalladas.Add(i + 1);
            }
            chequeoArchivo = chequeoArchivo && chequeoLineas[i];
         }

         if (lineasFalladas.Count > 0)
         {
            //Console.WriteLine($"Hubo errores en las líneas: {String.Join(", ", lineasFalladas.ToArray())}");
            //Console.WriteLine($"Lineas falladas en total: {lineasFalladas.Count}");
            tbConsole.WriteLine($"Hubo errores en las líneas: {String.Join(", ", lineasFalladas.ToArray())}");
            tbConsole.WriteLine($"Lineas falladas en total: {lineasFalladas.Count}");
         }
         else
         {
            //Console.WriteLine("No hubo errores al procesar el archivo.");
            //Console.WriteLine(GetCantLineasArchivo(sArchivoImputacion) + " líneas chequeadas correctamente.");
            tbConsole.WriteLine("No hubo errores al procesar el archivo.");
            tbConsole.WriteLine(GetCantLineasArchivo(sArchivoImputacion) + " líneas chequeadas correctamente.");
         }

         return chequeoArchivo;
      }

      private List<Boolean> ValidarLineasArchivoCBUDebito(string sArchivoImputacion, int iCantLineasArchivo, ref StreamReader lector, int iCantLineasAValidar = 0)
      {
         List<Boolean> chequeoLineas = new List<bool>();
         string sPattern = string.Empty;

         for (int i = 0; i < iCantLineasAValidar; i++)
         {
            Boolean esPrimeraLinea = (i == 0),
                    esUltimaLinea = (i == iCantLineasArchivo - 1);
            string sLinea = (lector.ReadLine() ?? "").Trim();
            bool isMatch;

            if (string.IsNullOrEmpty(sLinea))
            {
               continue;
            }

            if (esPrimeraLinea)
            {  // Ej:
               // 411055951202301252023012500170166090100035344CUOTAS    ARS020230125.xxxUniversidad del CEMA                20                                                                                                                                             
               // language=regex
               sPattern = @"^(?<instruccion>\d{3})(?<suffix>055951)(?<fecha1>\d{8})(?<fecha2>\d{8})(?<numero>\d{20})CUOTAS\s+ARS0(?<fecha3>\d{8})\.xxxUniversidad del CEMA\s+20";
               isMatch = Regex.IsMatch(sLinea, sPattern);

               if (isMatch)
               {
                  Match parseo = Regex.Match(sLinea, sPattern);

                  string instruccion = parseo.Groups["instruccion"].Value;
                  string fecha1 = parseo.Groups["fecha1"].Value;
                  string fecha2 = parseo.Groups["fecha2"].Value;
                  string numero = parseo.Groups["numero"].Value;
                  string fecha3 = parseo.Groups["fecha3"].Value;
               }

               chequeoLineas.Add(isMatch);
            }
            else
            {  // Ej:
               // 421055951  0035812               017030454000003814267900000000334000000    000000000000000000000020210108  000000300050317PCARGADO OK
               // 422055951  0035812               Apellido, Nombre
               // 423055951  0035812
               // 424055951  0035812               300050317
               // 421055951  0039075               017014074000000506303900000000334000000    000000000000000000000020210108  000000300050154PCARGADO OK
               // 422055951  0039075               Apellido, Nombre
               // 423055951  0039075
               // 424055951  0039075               300050154
               // ...                 ...

               // language=regex
               sPattern = @"^(?<instruccion>\d{3})(?<suffix>055951)\s+(?<codigo>\d+)\s+(?<datos>.*)$";
               isMatch = Regex.IsMatch(sLinea, sPattern);

               if (isMatch)
               {
                  Match parseo = Regex.Match(sLinea, sPattern);

                  string instruccion = parseo.Groups["instruccion"].Value;
                  string codigo = parseo.Groups["codigo"].Value;
                  string datos = parseo.Groups["datos"].Value;

                  switch (instruccion)
                  {
                     case "411": // La primera línea del archivo ya fue tratada en la línea
                        ; /***/
                        break; // Apertura de archivo

                     case "421":
                        // 017001832000000103652300000000601440000    000000000000000000000020220108  000000300089123PCARGADO OK
                        // language=regex
                        string sPattern421 = @"^(?<segmento1>\d{39})\s+(?<segmento2>\d{30})\s+(?<segmento3>.+)$";
                        Match parseo421 = Regex.Match(datos, sPattern421);
                        string segmento1 = parseo421.Groups["segmento1"].Value;
                        string segmento2 = parseo421.Groups["segmento2"].Value;
                        string segmento3 = parseo421.Groups["segmento3"].Value;

                        break; // Línea #1 del registro actual

                     case "422":
                        ;
                        break; // Línea #2 del registro actual 

                     case "423":
                        ;
                        break; // Línea #3 del registro actual 

                     case "424":
                        ;
                        break; // Línea #4 del registro actual 

                     case "491":
                        ;
                        break; // Cierre de archivo

                     default:
                        isMatch = false;
                        break;
                  }
               }

               chequeoLineas.Add(isMatch);
            }

            if (!isMatch)
            {
               //Console.WriteLine($"Error en la línea {i + 1}: \"{sLinea}\"");
               tbConsole.WriteLine($"Error en la línea {i + 1}: \"{sLinea}\"");
            }
         }

         return chequeoLineas;
      }

      private List<Boolean> ValidarLineasArchivoMastercardCredito(string sArchivoImputacion, int iCantLineasArchivo, ref StreamReader lector, int iCantLineasAValidar = 0)
      {
         List<Boolean> chequeoLineas = new List<bool>();
         string sPattern = string.Empty;

         for (int i = 0; i < iCantLineasAValidar; i++)
         {
            Boolean esPrimeraLinea = (i == 0),
                    esUltimaLinea = (i == iCantLineasArchivo - 1);
            string sLinea = (lector.ReadLine() ?? "").Trim();
            bool isMatch;

            if (string.IsNullOrEmpty(sLinea))
            {
               continue;
            }

            if (esPrimeraLinea)
            {  // Ej:
               // AC1DEB-AUT  162903591007122407120000490000014751550                                                                                                             
               // language=regex
               sPattern = @"^AC1DEB-AUT\s+(?<cabecera>\d{39})";
               isMatch = Regex.IsMatch(sLinea, sPattern);

               if (isMatch)
               {
                  Match parseo = Regex.Match(sLinea, sPattern);
                  string cabecera = parseo.Groups["cabecera"].Value;
               }
            }
            else
            {  // Ej:
               // AC255499965236330690000000000000012926000000008980000003140005/12280412                               100103928
               // AC255367000000364070000000000000016152000000009860000000000005/12280412                               100103018
               // AC255499963187870880000000000000018283000000160000000009130005/12280412                               100103951
               // AC255367000001307620000000000000019102000000029500000000000005/12280412                               100103148
               // AC252932980936081510000000000000020092000000029500000006140005/12280412                               100103152
               // ...                 ...
               // language=regex
               sPattern = @"^AC(?<seg60>\d{60})/(?<segvar1>\d+)\s+(?<segvar2>\d+)";
               isMatch = Regex.IsMatch(sLinea, sPattern);

               if (!isMatch)
               {
                  // extraigo los valores de la línea
                  Match parseo = Regex.Match(sLinea, sPattern);

                  string seg60 = parseo.Groups["seg60"].Value;
                  string segvar1 = parseo.Groups["segvar1"].Value;
                  string segvar2 = parseo.Groups["segvar2"].Value;

               }
            }

            chequeoLineas.Add(isMatch);
            if (!isMatch)
            {
               //Console.WriteLine($"Error en la línea {i + 1}: \"{sLinea}\"");
               tbConsole.WriteLine($"Error en la línea {i + 1}: \"{sLinea}\"");
            }
         }

         return chequeoLineas;
      }

      private List<Boolean> ValidarLineasArchivoAmericanExpressCredito(string sArchivoImputacion, int iCantLineasArchivo, ref StreamReader lector, int iCantLineasAValidar = 0)
      {
         List<Boolean> chequeoLineas = new List<Boolean>();
         string sPattern = string.Empty;
         string firma = string.Empty;

         for (int i = 0; i < iCantLineasAValidar; i++)
         {
            Boolean esPrimeraLinea = (i == 0),
                    esUltimaLinea = (i == iCantLineasArchivo - 1);
            string sLinea = (lector.ReadLine() ?? "").Trim();
            bool isMatch;

            if (string.IsNullOrEmpty(sLinea))
            {
               continue;
            }

            if (esPrimeraLinea)
            {  // Ej:
               // 199029406340000088225044000006000020180921                                                
               // language=regex
               sPattern = @"^(?<cabecera>\d+)";
               isMatch = Regex.IsMatch(sLinea, sPattern);

               if (isMatch)
               {
                  Match parseo = Regex.Match(sLinea, sPattern);
                  string cabecera = parseo.Groups["cabecera"].Value;

                  // language=regex
                  string sPatternFirma = @"(?<firma>\d{11})$";
                  // parseo la firma
                  Match parseoFirma = Regex.Match(cabecera, sPatternFirma);
                  firma = parseoFirma.Groups["firma"].Value;
               }
            }
            else if (esUltimaLinea)
            {  // Ej:
               // 399029406340000896211497000027500020210726                                                
               // language=regex
               sPattern = @"^(?<restante>\d{31})(?<firma>\d{11})$";
               isMatch = Regex.IsMatch(sLinea, sPattern);

               if (isMatch)
               {
                  Match parseo = Regex.Match(sLinea, sPattern);
                  string restante = parseo.Groups["restante"].Value;
                  string firma2 = parseo.Groups["firma"].Value;

                  if (firma != firma2)
                  {
                     isMatch = false;
                  }
               }
            }
            else
            {  // Ej:
               // 237159300072393700001680000000000020181008000527880000000000000000000000000034247
               // 237159300076097000001680000000000020181008000527820000000000000000000000000029489
               // 237159500017306200000744000000000020181008000533410000000000000000000000000034539
               // 237159500141453100001968000000000020181008000531640000000000000000000000000038034
               // 237641886397200600001680000000000020181008000529730000000376404839961000000037723
               // 237641292491300000001680000000000020181008000528230000000000000000000000000040544
               // ...                 ...
               // language=regex
               sPattern = @"^(?<datos>\d+)";
               isMatch = Regex.IsMatch(sLinea, sPattern);

               if (!isMatch)
               {
                  // extraigo los valores de la línea
                  Match parseo = Regex.Match(sLinea, sPattern);

                  string seg60 = parseo.Groups["seg60"].Value;
                  string segvar1 = parseo.Groups["segvar1"].Value;
                  string segvar2 = parseo.Groups["segvar2"].Value;
               }
            }

            chequeoLineas.Add(isMatch);
            if (!isMatch)
            {
               //Console.WriteLine($"Error en la línea {i + 1}: \"{sLinea}\"");
               tbConsole.WriteLine($"Error en la línea {i + 1}: \"{sLinea}\"");
            }
         }

         return chequeoLineas;
      }

      private List<Boolean> ValidarLineasArchivoVisaCredito(string sArchivoImputacion, int iCantLineasArchivo, ref StreamReader lector, int iCantLineasAValidar = 0)
      {
         List<Boolean> chequeoLineas = new List<bool>();
         string sPattern = string.Empty;
         string firma;

         for (int i = 0; i < iCantLineasAValidar; i++)
         {
            bool isMatch,
                  esPrimeraLinea = (i == 0),
                  esUltimaLinea = (i == iCantLineasArchivo - 1);
            string sLinea = (lector.ReadLine() ?? "").Trim();

            if (sLinea == "")
            {
               continue;
            }

            if (esPrimeraLinea)
            {  // Ej:
               // 0RDEBLIQC900000    0041200999202004271417
               // language=regex
               sPattern = @"^0RDEBLIQC900000\s+(?<firma>\d{22})";
               isMatch = Regex.IsMatch(sLinea, sPattern); // El campo de 22 dígitos se repite en la línea final (extraerlo p/validar final)

               if (isMatch)
               {
                  Match parseo = Regex.Match(sLinea, sPattern);
                  // extraigo la firma de la primera línea para compararla con la última
                  firma = parseo.Groups["firma"].Value;
               }
            }
            else if (esUltimaLinea)
            {  // Ej:
               // 9RDEBLIQC900000    00412009992020042714170000715000001499868226                                                                                                                                                                                                                                            *
               // language=regex
               sPattern = @"^9RDEBLIQC900000\s+{firma}(?<restante>\d{22})";
               isMatch = Regex.IsMatch(sLinea, sPattern);
            }
            else
            {  // Ej:
               // 102703308280005 0041200999433831000210432900028779270420      000000001670000  000000000009435000000000009435 0030886841         000                             00                             70120000100000204338310006703084270420      01 *
               // 102703308280005 0041200999459354000028235000029777270420      000000002770000  000000000010290000000000010290 0068919063         000                             00                             70120000100000300000000000000000270420      01 *
               // 102703308280005 0041200999493763800852023800029271270420      000000002070000  000000000010269000000000010269 0402940269         000                             00                             70120000100000400000000000000000270420      02 *
               // 102703308280005 0041200999455881800000033400029306270420      000000002070000  000000000010278000000000010278 0742508600         000                             00                             70120000100000504558818000001068270420      01 *
               // 102703308280005 0041200999405071011423263500029797270420      000000002216000  000000000043962000000000043962 0630957912         000                             00                             70120000100000600000000000000000270420      02 *
               // ...                 ...

               // language=regex
               sPattern = @"^
                  (?<seg15>\d{15})\s+
                  (?<seg45>\d{40,45})\s+
                  (?<seg15_2>\d{15})\s+
                  (?<seg30>\d{30})\s+
                  (?<seg10>\d{10})\s+
                  (?<operacion>[\s0-9a-zA-Z]{32})\s+
                  (?<seg38>\d{38})\s+
                  (?<seg2>\d{2})\s+
               \*$";
               isMatch = Regex.IsMatch(sLinea, sPattern);

               if (isMatch)
               {
                  // parseo los campos
                  Match parseo = Regex.Match(sLinea, sPattern);

                  string seg15 = parseo.Groups["seg15"].Value;
                  string seg45 = parseo.Groups["seg45"].Value;
                  string seg15_2 = parseo.Groups["seg15_2"].Value;
                  string seg30 = parseo.Groups["seg30"].Value;
                  string seg10 = parseo.Groups["seg10"].Value;
                  string operacion = parseo.Groups["operacion"].Value;
                  string seg38 = parseo.Groups["seg38"].Value;
                  string seg2 = parseo.Groups["seg2"].Value;

                  // parseo la operación
                  // language=regex
                  string sPatternOperacion = @"^(?<codigo>\d{3})(?<descripcion>.+)$";
                  Match parseoOperacion = Regex.Match(operacion, sPatternOperacion);

                  string codigo = parseoOperacion.Groups["codigo"].Value;
                  string descripcion = parseoOperacion.Groups["descripcion"].Value;
               }
            }

            chequeoLineas.Add(isMatch);

            if (!isMatch)
            {
               //Console.WriteLine($"Error en la línea {i + 1}: \"{sLinea}\"");
               tbConsole.WriteLine($"Error en la línea {i + 1}: \"{sLinea}\"");
            }
         }

         return chequeoLineas;
      }

      private List<Boolean> ValidarLineasArchivoVisaDebito(string sArchivoImputacion, int iCantLineasArchivo, ref StreamReader lector, int iCantLineasAValidar = 0)
      {
         List<Boolean> chequeoLineas = new List<bool>();
         string firma = "";
         string sPattern = string.Empty;

         for (int i = 0; i < iCantLineasAValidar; i++)
         {
            Boolean esPrimeraLinea = (i == 0),
                    esUltimaLinea = (i == iCantLineasArchivo - 1);
            string sLinea = (lector.ReadLine() ?? "").Trim();
            bool isMatch;

            if (string.IsNullOrEmpty(sLinea))
            {
               continue;
            }

            if (esPrimeraLinea)
            {  // Ej:
               // 0LDEBLIQD900000    0063651236202301111124                                                                                                            *
               // language=regex
               sPattern = @"^0LDEBLIQD900000\s+(?<firma>\d{22})";
               isMatch = Regex.IsMatch(sLinea, sPattern); // El campo de 22 dígitos se repite en la línea final (extraerlo p/validar final)

               if (isMatch)
               {
                  Match parseo = Regex.Match(sLinea, sPattern);
                  firma = parseo.Groups["firma"].Value;
               }
            }
            else if (esUltimaLinea)
            {  // Ej:
               // 9LDEBLIQD900000    00636512362023011111240000015000000103020000
               // language=regex
               sPattern = @"^9LDEBLIQD900000\s+{firma}(?<restante>\d{22})";
               isMatch = Regex.IsMatch(sLinea, sPattern);
            }
            else
            {  // Ej:
               // 14517660161267546   00130257202301080005000000005335000000000000056179           0  0               021DISPONIBLE INSUFICIENTE                       *
               // 14517660161267546   00130257202301080005000000005335000000000000056179           0  0               034SE REINTENTARA AUTORIZACION AUTOMATICA        *
               // 14815500004499873   00130731202301080005000000004123000000000000050482           0  0 *
               // 14517646470356005   00130001202301080005000000008536000000000000042671           0  0 *
               // 14055160010589552   00130031202301080005000000009069500000000000058903           0  0 *
               // 14517660162735459   00130426202301080005000000006225000000000000051886           0  0               019OPERACION NO PERMITIDA PARA ESA TARJETA       *
               // ...                 ...

               // language=regex
               sPattern = @"^(?<seg17>\d{17})\s+(?<seg50>\d{50})\s+(?<digito1>\d{1})\s+(?<digito2>\d{1})\s+(?<operacion>.*)\*$";
               isMatch = Regex.IsMatch(sLinea, sPattern);

               if (isMatch)
               {
                  // parseo los campos
                  Match parseo = Regex.Match(sLinea, sPattern);

                  string seg17 = parseo.Groups["seg17"].Value;
                  string seg50 = parseo.Groups["seg50"].Value;
                  string digito1 = parseo.Groups["digito1"].Value;
                  string digito2 = parseo.Groups["digito2"].Value;
                  string operacion = parseo.Groups["operacion"].Value;

                  // language=regex
                  string sPatternOperacion = @"^(?<codigo>\d{3})(?<descripcion>.*)$";
                  Match parseoOperacion = Regex.Match(operacion, sPatternOperacion);

                  string codigo = parseoOperacion.Groups["codigo"].Value;
                  string descripcion = parseoOperacion.Groups["descripcion"].Value;
               }
            }

            chequeoLineas.Add(isMatch);
            if (!isMatch)
            {
               //Console.WriteLine($"Error en la línea {i + 1}: \"{sLinea}\"");
               tbConsole.WriteLine($"Error en la línea {i + 1}: \"{sLinea}\"");
            }
         }

         return chequeoLineas;
      }

      int GetCantLineasArchivo(string sRuta)
      {
         using (StreamReader lector = new StreamReader(sRuta))
         {
            int i = 0;
            while (lector.ReadLine() != null) { i++; }
            return i;
         }
      }

      private Stream EjecutarComando(string comando, string argumentos = "", bool viaCmd = false)
      {
         int? exitCode;

         ProcessStartInfo processInfo;
         Process? process;
         string cmd, args;

         if (viaCmd)
         {
            cmd = "cmd.exe";
            args = $"/c {comando} {argumentos}"; // Ejecutar comando via cmd.exe
         }
         else
         {
            cmd = comando;
            args = argumentos;
         }

         processInfo = new ProcessStartInfo(cmd, args);
         processInfo.CreateNoWindow = true;
         processInfo.UseShellExecute = false;
         processInfo.RedirectStandardError = true;
         processInfo.RedirectStandardOutput = true;

         process = Process.Start(processInfo);
         process?.WaitForExit();

         string? output = process?.StandardOutput.ReadToEnd();
         string? error = process?.StandardError.ReadToEnd();

         exitCode = process?.ExitCode;
         process?.Close();

         tbConsole.WriteLine();
         tbConsole.WriteLine("==============================================================");
         tbConsole.WriteLine($"COMANDO:\t\t\"{cmd} {args}\"");
         tbConsole.WriteLine();

         return new Stream(
            (string.Empty),
            (string.IsNullOrEmpty(output) ? "(none)" : output),
            (string.IsNullOrEmpty(error) ? "(none)" : error)
         );
      }

      private Stream TransferirArchivo(string origen, string destino, string argumentos = "")
      {
         return EjecutarComando("pscp", $"-batch -pw {REMOTE_PASSWORD} {origen} {REMOTE_USERNAME}@{REMOTE_LOCATION}:{destino} {argumentos}");
      }

      private Stream EjecutarComandoRemoto(string comando, string argumentos = "")
      {
         return EjecutarComando("plink", $"-batch -ssh {REMOTE_USERNAME}@{REMOTE_LOCATION} -pw {REMOTE_PASSWORD} {comando} {argumentos}");
      }

      private Stream EjecutarScriptRemoto(string rutaScript)
      {
         return EjecutarComando("plink", $"-batch -ssh {REMOTE_USERNAME}@{REMOTE_LOCATION} -pw {REMOTE_PASSWORD} -m {rutaScript}");
      }

      private Stream LevantarCobro(string rutaScript, string nombreScript)
      {
         return EjecutarComando("plink", $"-batch -ssh {REMOTE_USERNAME}@{REMOTE_LOCATION} -pw {REMOTE_PASSWORD} cd {rutaScript} && sh {nombreScript}");
         //return EjecutarComando("plink", $"-batch -ssh {REMOTE_USERNAME}@{REMOTE_LOCATION} -pw {REMOTE_PASSWORD} -m ./levantar-cobro.txt");
      }

      private void MostrarStream(Stream stream)
      {
         tbConsole.WriteLine("==============================================================");
         tbConsole.WriteLine("STDOUT: " + stream.STDOUT);
         tbConsole.WriteLine("==============================================================");
         tbConsole.WriteLine("STDERR: " + stream.STDERR);
         tbConsole.WriteLine("==============================================================");
      }

      private void SetupTipoImputacion()
      {
         List<ComboBoxItem> opcionesCombobox = new List<ComboBoxItem>() {
            new ComboBoxItem { Code = "", Caption = "Seleccione tipo de imputación..." },
            new ComboBoxItem { Code = "VISADEB", Caption = "Visa (Débito)" },
            new ComboBoxItem { Code = "VISACRED", Caption = "Visa (Crédito)" },
            new ComboBoxItem { Code = "AMEXCRED", Caption = "American Express (Crédito)" },
            new ComboBoxItem { Code = "MASTERCRED", Caption = "Mastercard (Crédito)" },
            new ComboBoxItem { Code = "CBUDEB", Caption = "CBU (Débito)" },
         };

         cboTipoImputacion.ValueMember = "Code";
         cboTipoImputacion.DisplayMember = "Caption";
         cboTipoImputacion.DataSource = opcionesCombobox;
      }

   }
}
