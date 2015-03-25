using MainScreen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
//using System.Windows.Controls.Frame;
//using System.Windows.Navigation;
namespace Test1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window 
    {
        bool isLoginBtnClicked=false;
        bool isAuthorizeBtnClicked = false;
        bool isRegisterBtnClicked=false;
        bool isAltLoginBtnClicked=false;
        NavigationService navService;
        
        public MainWindow()
        {
            InitializeComponent();
            _NavigationFrame.Navigate(new Page());
        }


        //======================================== Login Screen ============================================//
        //Definisi Event untuk Login Button di Sini
        //Event yang terjadi saat LoginBtn diklik
        private void loginBtnClicked(object sender, RoutedEventArgs e)
        {
            isLoginBtnClicked = true;
            //loginText.Visibility = System.Windows.Visibility.Hidden;
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("loginButtonClicked");
            loginBtn.Background = brush;
            loadingBar.Visibility = System.Windows.Visibility.Visible;
            MainScreen2 main = new MainScreen2();
            main.Show();
            this.Visibility = System.Windows.Visibility.Hidden;
            //this.navService.Navigate(new Uri("MainScreen.xaml", UriKind.RelativeOrAbsolute));
            //this.Frame.Navigate(typeof(MainScreen));
            //Uri uri = new Uri("MainScreen.xaml", UriKind.Relative);
            //his.NavigationService.Navigate(uri);
            
        }


        //Event yang terjadi saat Mouse berada di area LoginBtn
        private void loginBtnHover(object sender, MouseEventArgs e)
        {
            //Cursor = (Cursors)FindResource("E:/PPL/UItest/Test1/Test1/assets/circleTriple.cur");
            if (isLoginBtnClicked == false)
            {
                var brush = new ImageBrush();
                brush.ImageSource = (ImageSource)FindResource("loginButtonHover");
                loginBtn.Background = brush;
                loginBtn.Opacity = 0.5;
            }
        }

        //Event yang terjadi saat Mouse berada di luar area LoginBtn
        private void loginBtnIdle(object sender, MouseEventArgs e)
        {
            //Cursor = Cursors.Arrow;
            if (isLoginBtnClicked == false)

            // loginBtn.Content.Visibility = System.Windows.Visibility.Visible;
            {
                var brush = new ImageBrush();
                brush.ImageSource = (ImageSource)FindResource("loginButton");
                loginBtn.Background = brush;
                loginBtn.Opacity = 0.8;
            }
        }

        //Definisikan Event yang terjadi pada registerBtn di sini
        //Event yang terjadi saat registerBtn diklik
        private void registerBtnClicked(object sender, RoutedEventArgs e)
        {
            isRegisterBtnClicked = true;
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("registerButtonClicked");
            registerBtn.Background = brush;
            deactivator.Visibility = System.Windows.Visibility.Visible;
            registerForm.Visibility = System.Windows.Visibility.Visible;
        }

        //Event yang terjadi saat mouse berada di area registerBtn 
        private void registerBtnHover(object sender, MouseEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("registerButtonHover");
            registerBtn.Background = brush;
        }

        //Event yang terjadi saat mouse berada di luar registerBtn 
        private void registerBtnIdle(object sender, MouseEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("registerButton");
            registerBtn.Background = brush;
        }

        //======================================== Register Screen ============================================//
       
        //Definisi Event yang terjadi pada addNewAccountBtn
        //Event yang terjadi saat addNewAccountBtn diklik
        private void addNewAccountBtnClicked(object sender, RoutedEventArgs e)
        {
            //isRegisterBtnClicked = true;
            
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("addNewAccountButtonClicked");
            addNewAccountBtn.Background = brush;
            if (password.Password == rpassword.Password)
            {
                warning.Visibility = System.Windows.Visibility.Hidden;
                username.Text = "";
                uLabel.Visibility = System.Windows.Visibility.Visible;
                email.Text = "";
                eLabel.Visibility = System.Windows.Visibility.Visible;
                password.Password = "";
                pLabel.Visibility = System.Windows.Visibility.Visible;
                rpassword.Password = "";
                rpLabel.Visibility = System.Windows.Visibility.Visible;
                deactivator.Visibility = System.Windows.Visibility.Collapsed;
                registerForm.Visibility = System.Windows.Visibility.Hidden;
                
            }
            else
            {
                warning.Visibility = System.Windows.Visibility.Visible;
            }
        }

        //Event yang terjadi saat mouse berada pada area addNewAccountBtn
        private void addNewAccountBtnHover(object sender, MouseEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("addNewAccountButtonHover");
            addNewAccountBtn.Background = brush;
        }

        //Event yang terjadi saat mouse berada di luar area addNewAccountBtn
        private void addNewAccountBtnIdle(object sender, MouseEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("addNewAccountButton");
            addNewAccountBtn.Background = brush;
        }


        //Definisi Event yang terjadi pada cancelBtn
        //Event yang terjadi saat cancelBtn diklik
        private void cancelBtnClicked(object sender, RoutedEventArgs e)
        {
            //isRegisterBtnClicked = true;
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("cancelButtonClicked");
            cancelBtn.Background = brush;
            deactivator.Visibility = System.Windows.Visibility.Collapsed;
            registerForm.Visibility = System.Windows.Visibility.Hidden;

        }

        //Event yang terjadi saat mouse berada pada area cancelBtn
        private void cancelBtnHover(object sender, MouseEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("cancelButtonHover");
            cancelBtn.Background = brush;
            //cancelBtn.Foreground = Brushes.;
            var bc = new BrushConverter();
            cancelBtn.Foreground = (Brush)bc.ConvertFrom("#1F8492");
        }

        //Event yang terjadi saat mouse berada di luar area cancelBtn
        private void cancelBtnIdle(object sender, MouseEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("cancelButton");
            cancelBtn.Background = brush;
            var bc = new BrushConverter();
            cancelBtn.Foreground = (Brush)bc.ConvertFrom("#D07339");
        }

        //Definisi Event yang terjadi pada exitBtn
        //Event yang terjadi saat exitBtn diklik
        private void exitBtnClicked(object sender, RoutedEventArgs e)
        {
            //isRegisterBtnClicked = true;
            deactivator.Visibility = System.Windows.Visibility.Collapsed;
            registerForm.Visibility = System.Windows.Visibility.Hidden;

        }

        //Event yang terjadi saat mouse berada pada area exitBtn
        private void exitBtnHover(object sender, MouseEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("exitButtonHover");
            exitBtn.Background = brush;
        }

        private void exitBtnIdle(object sender, MouseEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("exitButton");
            exitBtn.Background = brush;
        }

       
        //Definisi Event Keyboard yang terjadi pada Form Field pada Register Form
        //Event yang terjadi saat fokus keyboard ada pada field username
        private void formFieldActive(object sender, RoutedEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("formFieldActive");
            username.Background = brush;
            uLabel.Visibility = System.Windows.Visibility.Hidden;
        }


        //Event yang terjadi saat fokus keyboard di luar field username
        private void formFieldInactive(object sender, RoutedEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("formFieldInactive");
            username.Background = brush;
            if(username.Text == "")
            {
                uLabel.Visibility = System.Windows.Visibility.Visible;
            }
        }

        //Event yang terjadi saat fokus keyboard ada pada field email
        private void eformFieldActive(object sender, RoutedEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("formFieldActive");
            email.Background = brush;
            eLabel.Visibility = System.Windows.Visibility.Hidden;

        }


        //Event yang terjadi saat fokus keyboard di luar field email
        private void eformFieldInactive(object sender, RoutedEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("formFieldInactive");
            email.Background = brush;
            if (email.Text == "")
            {
                eLabel.Visibility = System.Windows.Visibility.Visible;
            }
        }

        //Event yang terjadi saat fokus keyboard ada pada field password
        private void pformFieldActive(object sender, RoutedEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("formFieldActive");
            password.Background = brush;
            pLabel.Visibility = System.Windows.Visibility.Hidden;
        }


        //Event yang terjadi saat fokus keyboard di luar field password
        private void pformFieldInactive(object sender, RoutedEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("formFieldInactive");
            password.Background = brush;
            if (password.Password == "")
            {
                pLabel.Visibility = System.Windows.Visibility.Visible;
            }

        }
        //Event yang terjadi saat fokus keyboard ada pada field rpassword
        private void rpformFieldActive(object sender, RoutedEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("formFieldActive");
            rpassword.Background = brush;
            rpLabel.Visibility = System.Windows.Visibility.Hidden;
        }


        //Event yang terjadi saat fokus keyboard di luar field rpassword
        private void rpformFieldInactive(object sender, RoutedEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("formFieldInactive");
            rpassword.Background = brush;
            if (rpassword.Password == "")
            {
                rpLabel.Visibility = System.Windows.Visibility.Visible;
            }
        }

        //Definisi Event untuk authorizeButton di Sini
        //Event yang terjadi saat authorizeBtn diklik
        private void authorizeBtnClicked(object sender, RoutedEventArgs e)
        {
            isAuthorizeBtnClicked = true;
            //loginText.Visibility = System.Windows.Visibility.Hidden;
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("loginButtonClicked");
            authorizeBtn.Background = brush;
            //loadingBar.Visibility = System.Windows.Visibility.Visible;

        }


        //Event yang terjadi saat Mouse berada di area authorizeBtn
        private void authorizeBtnHover(object sender, MouseEventArgs e)
        {
            if (isAuthorizeBtnClicked == false)
            {
                var brush = new ImageBrush();
                brush.ImageSource = (ImageSource)FindResource("loginButtonHover");
                authorizeBtn.Background = brush;
            }
        }

        //Event yang terjadi saat Mouse berada di luar area authorizeBtn
        private void authorizeBtnIdle(object sender, MouseEventArgs e)
        {
            if (isAuthorizeBtnClicked == false)

            // loginBtn.Content.Visibility = System.Windows.Visibility.Visible;
            {
                var brush = new ImageBrush();
                brush.ImageSource = (ImageSource)FindResource("loginButton");
                authorizeBtn.Background = brush;
            }
        }


        //======================================== Alternative Login Screen ============================================//

        private void altLogin (object sender, MouseEventArgs e)
        {
            deactivator.Visibility = System.Windows.Visibility.Visible;
            altLoginForm.Visibility = System.Windows.Visibility.Visible;
        }
        private void altLogin2(object sender, RoutedEventArgs e)
        {
            deactivator.Visibility = System.Windows.Visibility.Visible;
            altLoginForm.Visibility = System.Windows.Visibility.Visible;
        }
        //Definisi Event yang terjadi pada altLoginBtn
        //Event yang terjadi saat altLoginBtn diklik
        private void altLoginBtnClicked(object sender, RoutedEventArgs e)
        {
            //isRegisterBtnClicked = true;

            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("altLoginButtonClicked");
            altLoginBtn.Background = brush;
            if (passwordLogin.Password != "" && usernameLogin.Text !="")
            {
                lwarning.Visibility = System.Windows.Visibility.Hidden;
                usernameLogin.Text = "";
                ulLabel.Visibility = System.Windows.Visibility.Visible;
                passwordLogin.Password = "";
                plLabel.Visibility = System.Windows.Visibility.Visible;
                deactivator.Visibility = System.Windows.Visibility.Collapsed;
                altLoginForm.Visibility = System.Windows.Visibility.Hidden;

            }
            else
            {
                lwarning.Visibility = System.Windows.Visibility.Visible;
            }
        }


        //Event yang terjadi saat mouse berada pada area altLoginBtn
        private void altLoginBtnHover(object sender, MouseEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("altLoginButtonHover");
            altLoginBtn.Background = brush;
        }

        //Event yang terjadi saat mouse berada di luar area altLoginBtn
        private void altLoginBtnIdle(object sender, MouseEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("altLoginButton");
            altLoginBtn.Background = brush;
        }


        //Definisi Event yang terjadi pada cancelAltLoginBtn
        //Event yang terjadi saat cancelAltLoginBtn diklik
        private void cancelAltLoginBtnClicked(object sender, RoutedEventArgs e)
        {
            //isRegisterBtnClicked = true;
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("cancelAltLoginButtonClicked");
            cancelAltLoginBtn.Background = brush;
            deactivator.Visibility = System.Windows.Visibility.Collapsed;
            altLoginForm.Visibility = System.Windows.Visibility.Collapsed;

        }

        //Event yang terjadi saat mouse berada pada area cancelAltLoginBtn
        private void cancelAltLoginBtnHover(object sender, MouseEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("cancelAltLoginButtonHover");
            cancelAltLoginBtn.Background = brush;
            //cancelAltLoginBtn.Foreground = Brushes.;
            var bc = new BrushConverter();
            cancelAltLoginBtn.Foreground = (Brush)bc.ConvertFrom("#D07339");
        }

        //Event yang terjadi saat mouse berada di luar area cancelAltLoginBtn
        private void cancelAltLoginBtnIdle(object sender, MouseEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("cancelAltLoginButton");
            cancelAltLoginBtn.Background = brush;
            var bc = new BrushConverter();
            cancelAltLoginBtn.Foreground = (Brush)bc.ConvertFrom("#1F8492");
        }

        //Definisi Event yang terjadi pada exitAltLoginBtn
        //Event yang terjadi saat exitAltLoginBtn diklik
        private void exitAltLoginBtnClicked(object sender, RoutedEventArgs e)
        {
            //isRegisterBtnClicked = true;
            deactivator.Visibility = System.Windows.Visibility.Collapsed;
            altLoginForm.Visibility = System.Windows.Visibility.Hidden;

        }

        //Event yang terjadi saat mouse berada pada area exitAltLoginBtn
        private void exitAltLoginBtnHover(object sender, MouseEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("exitButtonHover");
            exitAltLoginBtn.Background = brush;
        }

        private void exitAltLoginBtnIdle(object sender, MouseEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("exitButton");
            exitAltLoginBtn.Background = brush;
        }


        //Definisi Event Keyboard yang terjadi pada Form Field pada Register Form
        //Event yang terjadi saat fokus keyboard ada pada field usernameLogin
        private void ulformFieldActive(object sender, RoutedEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("formFieldActive");
            usernameLogin.Background = brush;
            ulLabel.Visibility = System.Windows.Visibility.Hidden;
        }


        //Event yang terjadi saat fokus keyboard di luar field usernameLogin
        private void ulformFieldInactive(object sender, RoutedEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("formFieldInactive");
            usernameLogin.Background = brush;
            if (usernameLogin.Text == "")
            {
                ulLabel.Visibility = System.Windows.Visibility.Visible;
            }
        }


        //Event yang terjadi saat fokus keyboard ada pada field passwordLogin
        private void plformFieldActive(object sender, RoutedEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("formFieldActive");
            passwordLogin.Background = brush;
            plLabel.Visibility = System.Windows.Visibility.Hidden;
        }


        //Event yang terjadi saat fokus keyboard di luar field passwordLogin
        private void plformFieldInactive(object sender, RoutedEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("formFieldInactive");
            passwordLogin.Background = brush;
            if (passwordLogin.Password == "")
            {
                plLabel.Visibility = System.Windows.Visibility.Visible;
            }

        }

        private void altLoginHover(object sender, MouseEventArgs e)
        {

        }

        private void altLoginIdle(object sender, MouseEventArgs e)
        {

        }

        private void esc(object sender, KeyEventArgs e)
        {
            
        }


        
    }
}
