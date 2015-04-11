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
namespace BajuGW
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainScreen : Window
    {
        bool isMeasureBtnClicked = true;
        bool isWardrobeBtnClicked = false;
        bool isStoreBtnClicked = false;

        private Controller controller;
        
        //bool isAltLoginBtnClicked = false;

        public MainScreen(Controller controller)
        {
            InitializeComponent();
            this.controller = controller;
        }

        public void refresh()
        {
            clothGallery.Children.Clear();
            
            List<Cloth> clothes = controller.getClothes("");
            foreach (Cloth cloth in clothes)
            {
                Grid grid = new Grid();
                grid.Background = new ImageBrush(cloth.picture);
                grid.Height = 140;
                grid.Width = 120;
                grid.Margin = new Thickness(15.0);
                grid.ShowGridLines = true;

                grid.HorizontalAlignment = HorizontalAlignment.Left;
                grid.VerticalAlignment = VerticalAlignment.Top;
                
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(20);
                grid.RowDefinitions.Add(row);
                row = new RowDefinition();
                row.Height = new GridLength(100);
                grid.RowDefinitions.Add(row);
                row = new RowDefinition();
                row.Height = new GridLength(20);
                grid.RowDefinitions.Add(row);

                /* TextBlock name = new TextBlock();
                name.Text = cloth.name;
                name.Visibility = Visibility.Hidden;
                name.Name = "clothName";
                Grid.SetRow(name, 0);
                grid.Children.Add(name);
                */

                grid.MouseEnter += showDetails;
                grid.MouseLeave += hideDetails;

                clothGallery.Children.Add(grid);
            }
        }



        private void hideDetails(object sender, MouseEventArgs e)
        {
            
        }

        private void showDetails(object sender, MouseEventArgs e)
        {
            
        }



        private void measureBtnClicked(object sender, RoutedEventArgs e)
        {
            isMeasureBtnClicked = true;
            isWardrobeBtnClicked = false;
            isStoreBtnClicked = false;
            measureScreen.Visibility = System.Windows.Visibility.Visible;
            wardrobeScreen.Visibility = System.Windows.Visibility.Collapsed;
            storeScreen.Visibility = System.Windows.Visibility.Collapsed;
            title.Text = "SIZE MEASUREMENT";
            measureBtn.Opacity = 1.0;
            wardrobeBtn.Opacity = 0.5;
            storeBtn.Opacity = 0.5;
        }

        private void measureBtnHover(object sender, MouseEventArgs e)
        {
            measureBtn.Opacity = 1.0;
        }

        private void measureBtnIdle(object sender, MouseEventArgs e)
        {
            if(!isMeasureBtnClicked)
            measureBtn.Opacity = 0.5;
        }

        private void wardrobeBtnClicked(object sender, RoutedEventArgs e)
        {
            isMeasureBtnClicked = false;
            isWardrobeBtnClicked = true;
            isStoreBtnClicked = false;
            measureScreen.Visibility = System.Windows.Visibility.Collapsed;
            wardrobeScreen.Visibility = System.Windows.Visibility.Visible;
            storeScreen.Visibility = System.Windows.Visibility.Collapsed;
            title.Text = "WARDROBE";
            wardrobeBtn.Opacity = 1.0;
            measureBtn.Opacity = 0.5;
            storeBtn.Opacity = 0.5;
        }

        private void wardrobeBtnHover(object sender, MouseEventArgs e)
        {
            wardrobeBtn.Opacity = 1.0;
        }

        private void wardrobeBtnIdle(object sender, MouseEventArgs e)
        {
            if (!isWardrobeBtnClicked)
            wardrobeBtn.Opacity = 0.5;
        }

        private void storeBtnClicked(object sender, RoutedEventArgs e)
        {
            isMeasureBtnClicked = false;
            isWardrobeBtnClicked = false;
            isStoreBtnClicked = true;
            measureScreen.Visibility = System.Windows.Visibility.Collapsed;
            wardrobeScreen.Visibility = System.Windows.Visibility.Collapsed;
            storeScreen.Visibility = System.Windows.Visibility.Visible;
            title.Text = "STORE";
            storeBtn.Opacity = 1.0;
            wardrobeBtn.Opacity = 0.5;
            measureBtn.Opacity = 0.5;
        }

        private void storeBtnHover(object sender, MouseEventArgs e)
        {
            storeBtn.Opacity = 1.0;
        }

        private void storeBtnIdle(object sender, MouseEventArgs e)
        {
            if (!isStoreBtnClicked)
            storeBtn.Opacity = 0.5;
        }

        private void bodyBtnHover(object sender, MouseEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("bodyButtonHover");
            bodyBtn.Background = brush;
        }

        private void bodyBtnIdle(object sender, MouseEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("bodyButtonIdle");
            bodyBtn.Background = brush;
        }

        private void bodyBtnClicked(object sender, RoutedEventArgs e)
        {

        }

        private void clothBtnClicked(object sender, RoutedEventArgs e)
        {

        }

        private void clothBtnHover(object sender, MouseEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("clothButtonHover");
            clothBtn.Background = brush;
        }

        private void clothBtnIdle(object sender, MouseEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("clothButtonIdle");
            clothBtn.Background = brush;
        }

        private void settingBtnHover(object sender, MouseEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("settingButtonHover");
            settingBtn.Background = brush;
        }

        private void settingBtnIdle(object sender, MouseEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("settingButtonIdle");
            settingBtn.Background = brush;
        }

        private void settingBtnClicked(object sender, RoutedEventArgs e)
        {
            settingScreen.Visibility = System.Windows.Visibility.Visible;
            deactivator.Visibility = System.Windows.Visibility.Visible;
        }

        private void logoutBtnClicked(object sender, RoutedEventArgs e)
        {
            logoutConfirm.Visibility = System.Windows.Visibility.Visible;
            deactivator.Visibility = System.Windows.Visibility.Visible;

        }

        private void logoutBtnHover(object sender, MouseEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("logoutButtonHover");
            logoutBtn.Background = brush;
        }

        private void logoutBtnIdle(object sender, MouseEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("logoutButtonIdle");
            logoutBtn.Background = brush;
        }

        private void exitBtnClicked(object sender, RoutedEventArgs e)
        {
            settingScreen.Visibility = System.Windows.Visibility.Collapsed;
            deactivator.Visibility = System.Windows.Visibility.Collapsed;
        }

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

        private void saveBtnHover(object sender, MouseEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("saveSettingButtonHover");
            saveBtn.Background = brush;
        }

        private void saveBtnIdle(object sender, MouseEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("saveSettingButton");
            saveBtn.Background = brush;
        }

        private void saveBtnClicked(object sender, RoutedEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("saveSettingButton");
            saveBtn.Background = brush;
            settingScreen.Visibility = System.Windows.Visibility.Collapsed;
            deactivator.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void cancelSettingBtnClicked(object sender, RoutedEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("cancelSettingButton");
            cancelBtn.Background = brush;
            settingScreen.Visibility = System.Windows.Visibility.Collapsed;
            deactivator.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void cancelSettingBtnHover(object sender, MouseEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("cancelSettingButtonHover");
            cancelBtn.Background = brush;
            var bc = new BrushConverter();
            cancelBtn.Foreground = (Brush)bc.ConvertFrom("#F39C12");
        }

        private void cancelSettingBtnIdle(object sender, MouseEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("cancelSettingButton");
            cancelBtn.Background = brush;
            var bc = new BrushConverter();
            cancelBtn.Foreground = (Brush)bc.ConvertFrom("#ED2F59");
        }

       

     
        private void confirmLogoutButtonClicked(object sender, RoutedEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("saveSettingButton");
            confirmLogoutBtn.Background = brush;

            /*BajuGW.MainScreen main = new BajuGW.MainScreen();
            main.Show();
            this.Visibility = System.Windows.Visibility.Hidden;
             */

            

            logoutConfirm.Visibility = System.Windows.Visibility.Collapsed;
            deactivator.Visibility = System.Windows.Visibility.Collapsed;
            controller.logout();
            controller.showLoginScreen(this);
        }

        private void confirmLogoutButtonHover(object sender, MouseEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("saveSettingButtonHover");
            confirmLogoutBtn.Background = brush;
        }

        private void confirmLogoutButtonIdle(object sender, MouseEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("saveSettingButton");
            confirmLogoutBtn.Background = brush;
        }

        private void cancelLogoutBtnClicked(object sender, RoutedEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("cancelSettingButton");
            cancelLogoutBtn.Background = brush;
            logoutConfirm.Visibility = System.Windows.Visibility.Collapsed;
            deactivator.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void cancelLogoutBtnHover(object sender, MouseEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("cancelSettingButtonHover");
            cancelLogoutBtn.Background = brush;
            var bc = new BrushConverter();
            cancelLogoutBtn.Foreground = (Brush)bc.ConvertFrom("#F39C12");
      
        }

        private void cancelLogoutBtnIdle(object sender, MouseEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("cancelSettingButton");
            cancelLogoutBtn.Background = brush;
        }

        private void exitLogoutBtnClicked(object sender, RoutedEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("exitButton");
            exitLogoutBtn.Background = brush;
            logoutConfirm.Visibility = System.Windows.Visibility.Collapsed;
            deactivator.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void exitLogoutBtnHover(object sender, MouseEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("exitButtonHover");
            exitLogoutBtn.Background = brush;
        }

        private void exitLogoutBtnIdle(object sender, MouseEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("exitButton");
            exitLogoutBtn.Background = brush;
        }

      


     
        //======================================== Register Screen ============================================//

        //Definisi Event yang terjadi pada addNewAccountBtn
        //Event yang terjadi saat addNewAccountBtn diklik
        



    }
}
