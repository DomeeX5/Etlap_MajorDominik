using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Etlap
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
			EtlapService etlapService = new EtlapService();
			Application application = new Application();
			application.Run(new MainWindow(etlapService));
		}
    }
}
