using Eq2Grau.Models;

using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;

namespace Eq2Grau.Controllers {

    public class HomeController : Controller {

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger) {
            _logger = logger;
        }


        /// <summary>
        /// Porta de entrada do pedidos feitos pelo Browser.
        /// M�todo para calcular as ra�zes de uma equa��o do 2� grau
        /// </summary>
        /// <param name="A">par�metro do x^2</param>
        /// <param name="B">par�metro do x</param>
        /// <param name="C">par�metro independente</param>
        /// <returns></returns>
        public IActionResult Index(string A, string B, string C) {
         /* ALGORITMO:
          * 1- ler par�metros A, B, C
          * 2- validar se se pode fazer opera��es com eles
          *    2.1- t�m de ser n�meros
          *    2.2- A != 0
          * 3- se posso calcular,
          *    3.1- determinar as ra�zes
          *         x=(-b +/- sqrt(b^2-4ac))/2/a
          *         3.1.1- calcular o delta: b^2-4ac
          *         3.1.2- se delta > 0, h� 2 ra�zes reais distintas
          *                              x1 e x2
          *                se delta = 0, h� 2 ra�zes reais iguais
          *                              x1=x2
          *                se delta < 0, h� duas ra�zes complexas conjugadas
          *                              x1 = -b/(2a) + sqrt(-delta)/(2a) i
          *                              x2 = -b/(2a) - sqrt(-delta)/(2a) i
          *   3.2- enviar a resposta para o cliente
          * se n�o posso calcular (else)
          *    enviar mensagem de erro para o cliente (browser)
          */

         // vars. auxiliares
         double auxA = 0, auxB = 0, auxC = 0;
         ViewBag.Mensagem = ""; // os ViewBag APENAS transportam conte�dos do Controller para a View

         // 1.
         if (string.IsNullOrWhiteSpace(A) || string.IsNullOrWhiteSpace(B) || string.IsNullOrWhiteSpace(C)) {
            // enviar mensagem para o utilizador
            ViewBag.Mensagem = "Os par�metros A, B e C s�o de preenchimento obrigat�rio.";

            // devolver controlo � View
            return View();
         }

         // 1.
         if (!double.TryParse(A, out auxA)) {
            // o A n�o � n�mero.
            // enviar mensagem para o utilizador
            ViewBag.Mensagem = "O par�metro A n�o � um n�mero";

            // devolver controlo � View
            return View();
         }

         // 1.
         if (!double.TryParse(B, out auxB)) {
            // o B n�o � n�mero.
            // enviar mensagem para o utilizador
            ViewBag.Mensagem = "O par�metro B n�o � um n�mero";

            // devolver controlo � View
            return View();
         }

         // 1.
         if (!double.TryParse(C, out auxC)) {
            // o C n�o � n�mero.
            // enviar mensagem para o utilizador
            ViewBag.Mensagem = "O par�metro C n�o � um n�mero";

            // devolver controlo � View
            return View();
         }


         // 2.
         if (auxA == 0) {
            // o A � ZERO.
            // enviar mensagem para o utilizador
            ViewBag.Mensagem = "O par�metro A n�o pode ser 0 (zero).";

            // devolver controlo � View
            return View();
         }


         // 3.
         double delta = Math.Pow(auxB, 2) - 4 * auxC;
         // 3.1
         if (delta > 0) {
            string x1 = Math.Round((-auxB + Math.Sqrt(delta)) / 2 / auxA, 2) + "";
            string x2 = Math.Round((-auxB - Math.Sqrt(delta)) / 2 / auxA, 2) + "";
            // enviar mensagem para o utilizador
            ViewBag.Mensagem = "A equa��o tem duas ra�zes reais distintas.";
            ViewBag.X1 = x1;
            ViewBag.X2 = x2;

            // devolver controlo � View
            return View();
         }

         if (delta == 0) {
            string x = Math.Round(-auxB / 2 / auxA, 2) + "";
            // enviar mensagem para o utilizador
            ViewBag.Mensagem = "A equa��o tem duas ra�zes reais, mas iguais.";
            ViewBag.X1 = x;
            ViewBag.X2 = x;

            // devolver controlo � View
            return View();
         }

         if (delta < 0) {
            string x1 = Math.Round((-auxB) / 2 / auxA, 2) + " + " + Math.Round(Math.Sqrt(-delta) / 2 / auxA, 2) + " i";
            string x2 = Math.Round((-auxB) / 2 / auxA, 2) + " - " + Math.Round(Math.Sqrt(-delta) / 2 / auxA, 2) + " i";
            // enviar mensagem para o utilizador
            ViewBag.Mensagem = "A equa��o tem duas ra�zes complexas conjugadas.";
            ViewBag.X1 = x1;
            ViewBag.X2 = x2;

            // devolver controlo � View
            return View();
         }

         // se chegar aqui, alguma coisa correu muito mal...
         // devolver controlo � View
         return View();
        }

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
