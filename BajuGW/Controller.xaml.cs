using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BajuGW
{
    public partial class Controller : Application
    {
        private LoginScreen loginScreen;
        private MainScreen mainScreen;

        public Controller()
        {
            mainScreen = new MainScreen(this);
            loginScreen = new LoginScreen(this);
            this.MainWindow = mainScreen;
            this.MainWindow.Show();
        }

        public void showLoginScreen(Window caller) {
            caller.Close();
            this.MainWindow = loginScreen;
            this.MainWindow.Show();
        }

        public void showMainScreen(Window caller) {
            caller.Close();
            this.MainWindow = mainScreen;
            this.MainWindow.Show();
        }
    }
}
