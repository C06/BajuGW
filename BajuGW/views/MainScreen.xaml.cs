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
using System.Threading;
using System.Threading.Tasks;
using System.IO;
//using System.Windows.Controls.Frame;


namespace BajuGW
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainScreen : Window
    {
        private Thread processingThread;
        private PXCMSenseManager senseManager;
        private PXCMFaceModule faceModule;
        private PXCMFaceConfiguration faceConfig;
        private PXCMFaceData faceData;

        private Int32 numOfFace;

        private PXCMTouchlessController touchlessController;
        bool isMeasureBtnClicked = true;
        bool isWardrobeBtnClicked = false;
        bool isStoreBtnClicked = false;
        string lastCategory = "";
        string lastQuery = "";
        string lastOnlineCategory = "";
        string lastOnlineQuery = "";
        private Controller controller;

        public MainScreen(Controller controller)
        {
            InitializeComponent();
            this.controller = controller;
            /*
            senseManager = PXCMSenseManager.CreateInstance();
            senseManager.EnableStream(PXCMCapture.StreamType.STREAM_TYPE_COLOR, 640, 480, 30);
            senseManager.EnableFace();
            senseManager.EnableTouchlessController();
            senseManager.Init();

            // Configure the Touchless Controller
            touchlessController = senseManager.QueryTouchlessController();
            touchlessController.SubscribeEvent(OnTouchlessControllerUXEvent);

            // Configure the Face Module
            faceModule = senseManager.QueryFace();
            faceConfig = faceModule.CreateActiveConfiguration();
            faceConfig.EnableAllAlerts();

            // Start the worker thread
            processingThread = new Thread(new ThreadStart(ProcessingThread));
            processingThread.Start();
             */ 
        }
        /*
        private void ProcessingThread()
        {
            // Start AcquireFrame/ReleaseFrame loop
            while (senseManager.AcquireFrame(true) >= pxcmStatus.PXCM_STATUS_NO_ERROR)
            {
                PXCMCapture.Sample sample = senseManager.QuerySample();
                System.Drawing.Bitmap colorBitmap = new System.Drawing.Bitmap(640, 480);
                PXCMImage.ImageData colorData = new PXCMImage.ImageData();
                PXCMImage image = sample.color;

                // Get color image data
                if (sample != null)
                {
                    sample.color.AcquireAccess(PXCMImage.Access.ACCESS_READ, PXCMImage.PixelFormat.PIXEL_FORMAT_RGB32, out colorData);
                    colorBitmap = colorData.ToBitmap(0, sample.color.info.width, sample.color.info.height);
                }

                // Get the face
                faceModule = senseManager.QueryFace();
                if (faceModule != null)
                {
                    faceData = faceModule.CreateOutput();
                    faceData.Update();
                    numOfFace = faceData.QueryNumberOfDetectedFaces();
                }

                // Update the user interface
                UpdateUI(colorBitmap);

                // Release the frame
                if (faceData != null) faceData.Dispose();
                colorBitmap.Dispose();
                sample.color.ReleaseAccess(colorData);
                sample.color.Dispose();
                senseManager.ReleaseFrame();
            }
        }
        
        private void UpdateUI(System.Drawing.Bitmap bitmap)
        {
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(delegate()
            {
                if (bitmap != null)
                {
                    BitmapSource bitmapSource = ConvertBitmap.BitmapToBitmapSource(bitmap);

                    // Mirror the bitmap
                    TransformedBitmap mirroredBitmap = new TransformedBitmap();
                    mirroredBitmap.BeginInit();
                    mirroredBitmap.Source = bitmapSource;
                    ScaleTransform transformation = new ScaleTransform(-1, 1);
                    mirroredBitmap.Transform = transformation;
                    mirroredBitmap.EndInit();
                    bitmapSource = mirroredBitmap;

                    // Display the color stream
                    //imgColorStream.ImageSource = bitmapSource;
                    //registerStream.ImageSource = bitmapSource;

                    bitmap.Dispose();
                }
            }));
        }
         */ 

        public void refresh()
        {
            controller.refresh();

            List<Cloth> clothes = controller.getClothesFromWardrobe(lastQuery, lastCategory);
            loadClothesFromWardrobe(clothes, itemDisplay);

            //tambahkan pakaian favorit ke favorite display
            clothes = controller.getFavorites();
            loadClothesFromWardrobe(clothes, favDisplay);

            List<string> categories = controller.getCategories();
            catDisplayOption.ItemsSource = categories;

            categoryListBox.ItemsSource = categories;
        }

        private void loveBtn_Click(object sender, RoutedEventArgs e)
        {
            Cloth cloth = (Cloth) ((Button) sender).CommandParameter;
            if (cloth.isFavorite == 0)
                controller.setFavorite(cloth.id);
            else
                controller.setUnfavorite(cloth.id);
            this.refresh();
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
            storeBtn.IsHitTestVisible = true;
            wardrobeBtn.IsHitTestVisible = true;
            measureBtn.IsHitTestVisible = true;
            

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
            homeMeasureScreen.Visibility = System.Windows.Visibility.Collapsed;
            bodyMeasureScreen.Visibility = System.Windows.Visibility.Visible;
        }

        private void clothBtnClicked(object sender, RoutedEventArgs e)
        {
            homeMeasureScreen.Visibility = System.Windows.Visibility.Collapsed;
            clothMeasureScreen.Visibility = System.Windows.Visibility.Visible;
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

        private void bBackBtnClicked(object sender, RoutedEventArgs e)
        {
            bodyMeasureScreen.Visibility = System.Windows.Visibility.Collapsed;
            homeMeasureScreen.Visibility = System.Windows.Visibility.Visible;
        }

        private void bBackBtnHover(object sender, MouseEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("backButtonHover");
            bBackBtn.Background = brush;
        }

        private void bBackBtnIdle(object sender, MouseEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("backButton");
            bBackBtn.Background = brush;
        }

        private void cBackBtnClicked(object sender, RoutedEventArgs e)
        {
            clothMeasureScreen.Visibility = System.Windows.Visibility.Collapsed;
            homeMeasureScreen.Visibility = System.Windows.Visibility.Visible;
        }

        private void cBackBtnHover(object sender, MouseEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("backButtonHover");
            cBackBtn.Background = brush;
        }

        private void cBackBtnIdle(object sender, MouseEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("backButton");
            cBackBtn.Background = brush;
        }

        private void searchAreaActive(object sender, RoutedEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("formFieldActive");
            searchArea.Background = brush;
        }

        private void searchAreaInactive(object sender, RoutedEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("formFieldInactive");
            searchArea.Background = brush;
        }

        private void searchBtn_Click(object sender, RoutedEventArgs e)
        {
            lastQuery = searchArea.Text;
            List<Cloth> clothes = controller.getClothesFromWardrobe(lastQuery, lastCategory);
            loadClothesFromWardrobe(clothes, itemDisplay);
        }

        private void onlineSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            lastOnlineQuery = onlineSearchArea.Text;
            List<OnlineCloth> result = new List<OnlineCloth>();

            foreach (OnlineStore store in Controller.stores)
            {
                if (store == null)
                    continue;
                List<OnlineCloth> clothes = store.getClothes(lastOnlineQuery, lastOnlineCategory);
                foreach (OnlineCloth cloth in clothes)
                {
                    result.Add(cloth);
                }
            }

            loadClothesFromStore(result, onlineItemDisplay);
        }

        private void loadClothesFromWardrobe(List<Cloth> clothes, WrapPanel container) {
            container.Children.Clear();

            foreach (Cloth cloth in clothes)
            {
                Grid grid = new Grid();
                grid.Height = 140;
                grid.Width = 120;
                grid.Margin = new Thickness(15.0);

                grid.HorizontalAlignment = HorizontalAlignment.Left;
                grid.VerticalAlignment = VerticalAlignment.Top;

                Rectangle clothPicture = new Rectangle();
                clothPicture.Fill = new ImageBrush(cloth.picture);

                //definisi baris dan kolom
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(20);
                grid.RowDefinitions.Add(row);
                row = new RowDefinition();
                row.Height = new GridLength(80);
                grid.RowDefinitions.Add(row);
                row = new RowDefinition();
                row.Height = new GridLength(40);
                grid.RowDefinitions.Add(row);


                //hover
                Button hoverItem = new Button();
                hoverItem.Height = 140;
                hoverItem.Width = 120;
                hoverItem.Opacity = 0.1;
                hoverItem.Background = new SolidColorBrush(Colors.Transparent);
                hoverItem.CommandParameter = cloth;
                hoverItem.Click += itemDetailClicked;

                //nama pakaian
                TextBlock nameItem = new TextBlock();
                nameItem.Text = cloth.name;
                nameItem.TextAlignment = System.Windows.TextAlignment.Center;
                nameItem.VerticalAlignment = System.Windows.VerticalAlignment.Top;

                //tombol favorite
                Button loveBtn = new Button();
                loveBtn.Height = 40;
                loveBtn.Width = 40;
                loveBtn.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                loveBtn.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;

                var brush = new ImageBrush();
                if (cloth.isFavorite == 0)
                    brush.ImageSource = (ImageSource)FindResource("loveButton");
                else
                    brush.ImageSource = (ImageSource)FindResource("hateButton");

                loveBtn.Background = brush;
                loveBtn.CommandParameter = cloth;
                loveBtn.Click += loveBtn_Click;


                //penempatan elemen gui di grid
                Grid.SetRow(clothPicture, 1);
                Grid.SetRowSpan(clothPicture, 2);
                grid.Children.Add(clothPicture);
                Grid.SetRow(nameItem, 0);
                grid.Children.Add(nameItem);
                Grid.SetRow(hoverItem, 0);
                Grid.SetRowSpan(hoverItem, 3);
                grid.Children.Add(hoverItem);
                Grid.SetRow(loveBtn, 2);
                grid.Children.Add(loveBtn);
                

                //tambahkan grid ke elemen lain sebagai anak
                container.Children.Add(grid);
            }
        }

        public void refreshStore()
        {
            controller.refreshStore();
            List<OnlineCloth> result = new List<OnlineCloth>();
            foreach (OnlineStore store in Controller.stores)
            {
                if (store == null)
                    continue;
                List<OnlineCloth> clothes = store.getClothes(lastOnlineQuery, lastOnlineCategory);
                foreach (OnlineCloth cloth in clothes)
                {
                    result.Add(cloth);
                }
                
            }
            loadClothesFromStore(result, onlineItemDisplay);

            List<string> categories = controller.getOnlineCategories();
            onlineCatDisplayOption.ItemsSource = categories;
        }

        private void loadClothesFromStore(List<OnlineCloth> clothes, WrapPanel container)
        {
            container.Children.Clear();

            foreach (OnlineCloth cloth in clothes)
            {
                Grid grid = new Grid();
                grid.Height = 140;
                grid.Width = 120;
                grid.Margin = new Thickness(15.0);

                grid.HorizontalAlignment = HorizontalAlignment.Left;
                grid.VerticalAlignment = VerticalAlignment.Top;

                Rectangle clothPicture = new Rectangle();
                clothPicture.Fill = new ImageBrush(cloth.picture);

                //definisi baris dan kolom
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(20);
                grid.RowDefinitions.Add(row);
                row = new RowDefinition();
                row.Height = new GridLength(80);
                grid.RowDefinitions.Add(row);
                row = new RowDefinition();
                row.Height = new GridLength(40);
                grid.RowDefinitions.Add(row);

                //hover
                Button hoverItem = new Button();
                hoverItem.Height = 140;
                hoverItem.Width = 120;
                hoverItem.Opacity = 0.1;
                hoverItem.Background = new SolidColorBrush(Colors.Transparent);
                hoverItem.CommandParameter = cloth;
                hoverItem.Click += onlineItemDetailClicked;

                //nama pakaian
                TextBlock nameItem = new TextBlock();
                nameItem.Text = cloth.name;
                nameItem.TextAlignment = System.Windows.TextAlignment.Center;
                nameItem.VerticalAlignment = System.Windows.VerticalAlignment.Top;

                //tombol favorite
                Button loveBtn = new Button();
                loveBtn.Height = 40;
                loveBtn.Width = 40;
                loveBtn.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                loveBtn.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;

                var brush = new ImageBrush();
                if (cloth.isFavorite == 0)
                    brush.ImageSource = (ImageSource)FindResource("loveButton");
                else
                    brush.ImageSource = (ImageSource)FindResource("hateButton");

                loveBtn.Background = brush;
                loveBtn.CommandParameter = cloth;
                loveBtn.Click += onlineAddToFavBtnClicked;

                //penempatan elemen gui di grid
                Grid.SetRow(clothPicture, 1);
                Grid.SetRowSpan(clothPicture, 2);
                grid.Children.Add(clothPicture);
                Grid.SetRow(nameItem, 0);
                grid.Children.Add(nameItem);
                Grid.SetRow(hoverItem, 0);
                Grid.SetRowSpan(hoverItem, 3);
                grid.Children.Add(hoverItem);
                Grid.SetRow(loveBtn, 2);
                grid.Children.Add(loveBtn);


                //tambahkan grid ke elemen lain sebagai anak
                container.Children.Add(grid);
            }
        }

        private void loadClothesFromSuggestion(List<Cloth> clothes, WrapPanel container)
        {
            container.Children.Clear();

            foreach (Cloth cloth in clothes)
            {
                Grid grid = new Grid();
                grid.Height = 140;
                grid.Width = 120;
                grid.Margin = new Thickness(15.0);

                grid.HorizontalAlignment = HorizontalAlignment.Left;
                grid.VerticalAlignment = VerticalAlignment.Top;

                Rectangle clothPicture = new Rectangle();
                clothPicture.Fill = new ImageBrush(cloth.picture);

                //definisi baris dan kolom
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(20);
                grid.RowDefinitions.Add(row);
                row = new RowDefinition();
                row.Height = new GridLength(80);
                grid.RowDefinitions.Add(row);
                row = new RowDefinition();
                row.Height = new GridLength(40);
                grid.RowDefinitions.Add(row);

                //hover
                Rectangle hoverItem = new Rectangle();
                hoverItem.Height = 140;
                hoverItem.Width = 120;
                hoverItem.Opacity = 0.7;
                //hoverItem.Fill = new SolidColorBrush(Colors.Black);

                //nama pakaian
                TextBlock nameItem = new TextBlock();
                nameItem.Text = cloth.name;
                nameItem.TextAlignment = System.Windows.TextAlignment.Center;
                nameItem.VerticalAlignment = System.Windows.VerticalAlignment.Top;

                //tombol favorite
                Button loveBtn = new Button();
                loveBtn.Height = 40;
                loveBtn.Width = 40;
                loveBtn.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                loveBtn.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;

                var brush = new ImageBrush();
                brush.ImageSource = (ImageSource)FindResource("loveButton");

                loveBtn.Background = brush;
                loveBtn.CommandParameter = cloth;
                loveBtn.Click += loveSuggestionBtn_Clicked;


                //penempatan elemen gui di grid
                Grid.SetRow(clothPicture, 1);
                Grid.SetRowSpan(clothPicture, 2);
                grid.Children.Add(clothPicture);
                Grid.SetRow(nameItem, 0);
                grid.Children.Add(nameItem);
                Grid.SetRow(hoverItem, 0);
                Grid.SetRowSpan(hoverItem, 3);
                grid.Children.Add(hoverItem);
                Grid.SetRow(loveBtn, 2);
                grid.Children.Add(loveBtn);


                //tambahkan grid ke elemen lain sebagai anak
                container.Children.Add(grid);
            }
        }

        private void loveSuggestionBtn_Clicked(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Cloth cloth = (Cloth)button.CommandParameter;
            controller.setFavorite(cloth.id);
            this.refresh();
            ((Grid)button.Parent).Visibility = Visibility.Collapsed;
        }

        private void catDisplayOption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            String selected = catDisplayOption.SelectedItem.ToString();
            selected = selected.Equals("all") ? "" : selected;
            lastCategory = selected;
            List<Cloth> clothes = controller.getClothesFromWardrobe(lastQuery, lastCategory);
            loadClothesFromWardrobe(clothes, itemDisplay);
        }

        private void onlineCatDisplayOption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            String selected = onlineCatDisplayOption.SelectedItem.ToString();
            selected = selected.Equals("all") ? "" : selected;
            lastOnlineCategory = selected;

            List<OnlineCloth> result = new List<OnlineCloth>();
            foreach (OnlineStore store in Controller.stores)
            {
                if (store == null)
                    continue;
                List<OnlineCloth> clothes = store.getClothes(lastOnlineQuery, lastOnlineCategory);
                foreach (OnlineCloth cloth in clothes)
                {
                    result.Add(cloth);
                }

            }
            loadClothesFromStore(result, onlineItemDisplay);
        }

        private void suggestBtnClicked(object sender, RoutedEventArgs e)
        {
            popupScreen.Visibility = Visibility.Visible;
            deactivate.IsHitTestVisible = true;
            suggestionScreen.Visibility = Visibility.Visible;
            List<Cloth> clothes = controller.getSuggestion();
            loadClothesFromSuggestion(clothes, suggestDisplay);
        }

        private void pExitBtnClicked(object sender, RoutedEventArgs e)
        {
            suggestDisplay.Children.Clear();
            popupScreen.Visibility = Visibility.Collapsed;
            deactivate.IsHitTestVisible = false;
            suggestionScreen.Visibility = Visibility.Collapsed;
        }

        private void eExitBtnClicked(object sender, RoutedEventArgs e)
        {
            popupScreen.Visibility = Visibility.Collapsed;
            deactivate.IsHitTestVisible = false;
            eCatScreen.Visibility = Visibility.Collapsed;
        }

        private void eExitBtnHover(object sender, MouseEventArgs e)
        {

        }

        private void eExitBtnIdle(object sender, MouseEventArgs e)
        {

        }

        private void newCatBtnClicked(object sender, RoutedEventArgs e)
        {
            editCategoryTextScreen.Visibility = Visibility.Visible;
            
            confirmEditCategoryText.Click -= confirmEditCatBtnClicked;
            confirmEditCategoryText.Click += confrimAddCatBtnClicked;
        }

        private void newCatBtnHover(object sender, MouseEventArgs e)
        {

        }

        private void newCatBtnIdle(object sender, MouseEventArgs e)
        {

        }

        private void editCatBtnClicked(object sender, RoutedEventArgs e)
        {
            if (categoryListBox.SelectedItem == null)
            {
                return;
            }
            editCategoryTextScreen.Visibility = Visibility.Visible;
            

            confirmEditCategoryText.Click -= confrimAddCatBtnClicked;
            confirmEditCategoryText.Click += confirmEditCatBtnClicked;
            
            confirmEditCategoryText.CommandParameter = categoryListBox.SelectedItem.ToString();
        }

        private void confrimAddCatBtnClicked(object sender, RoutedEventArgs e)
        {
            string newName = editCategoryTextField.Text;
            if (newName != null && !newName.Equals("all") && !newName.Equals(""))
            {
                controller.addCategory(newName);
                editCategoryTextField.Text = "";
                editCategoryTextScreen.Visibility = Visibility.Hidden;
                confirmEditCategoryText.CommandParameter = null;
                this.refresh();
            }
        }

        private void confirmEditCatBtnClicked(object sender, RoutedEventArgs e)
        {
            string category = confirmEditCategoryText.CommandParameter.ToString();
            string newName = editCategoryTextField.Text;
            if (category != null && !category.Equals("all") && newName != null && !newName.Equals("all") && !newName.Equals(""))
            {
                controller.editCategory(category, newName);
                editCategoryTextField.Text = "";
                editCategoryTextScreen.Visibility = Visibility.Hidden;
                confirmEditCategoryText.CommandParameter = null;
                this.refresh();
            }
        } 

        private void editCatBtnHover(object sender, MouseEventArgs e)
        {

        }

        private void editCatBtnIdle(object sender, MouseEventArgs e)
        {

        }

        private void deleteCatBtnClicked(object sender, RoutedEventArgs e)
        {
            string category = categoryListBox.SelectedItem.ToString();
            if (category != null && !category.Equals("all"))
            {
                controller.deleteCategory(category);
                this.refresh();
            }
        }

        private void deleteCatBtnHover(object sender, MouseEventArgs e)
        {

        }

        private void deleteCatBtnIdle(object sender, MouseEventArgs e)
        {

        }

        private void catBtnClicked(object sender, RoutedEventArgs e)
        {
            popupScreen.Visibility = Visibility.Visible;
            deactivate.IsHitTestVisible = true;
            eCatScreen.Visibility = Visibility.Visible;
        }

        private void catBtnHover(object sender, MouseEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("catButtonHover");
            catBtn.Background = brush;
        }

        private void catBtnIdle(object sender, MouseEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = (ImageSource)FindResource("catButton");
            catBtn.Background = brush;
        }

        private void store1Button_Click(object sender, RoutedEventArgs e)
        {
            if (Controller.stores[0] == null)
            {
                Controller.stores[0] = new OnlineStore(0, Controller.supportedStore[0], controller.getUsername());
                controller.addConnectedStore(0);
            }
            else
            {
                Controller.stores[0] = null;
                controller.removeConnectedStore(0);
            }
            
            refreshStore();
        }

        private void mBackBtnClicked(object sender, RoutedEventArgs e)
        {
            mixMatchScreen.Visibility = Visibility.Collapsed;
            itemDetailScreen.Visibility = Visibility.Visible;
            itemDetailScreen.IsHitTestVisible = true;
            mixMatchScreen.IsHitTestVisible = false;
        }

        private void mBackBtnHover(object sender, MouseEventArgs e)
        {

        }

        private void mBackBtnIdle(object sender, MouseEventArgs e)
        {

        }

        private void mixMatchBtnHover(object sender, MouseEventArgs e)
        {

        }

        private void mixMatchBtnIdle(object sender, MouseEventArgs e)
        {

        }

        private void mixMatchBtnClicked(object sender, RoutedEventArgs e)
        {
            mixMatchScreen.Visibility = Visibility.Visible;
            itemDetailScreen.Visibility = Visibility.Collapsed;
            itemDetailScreen.IsHitTestVisible = false;
            mixMatchScreen.IsHitTestVisible = true;
        }

        private void exitDetailBtnClicked(object sender, RoutedEventArgs e)
        {
            popupScreen.Visibility = Visibility.Collapsed;
            deactivate.IsHitTestVisible = false;
            itemDetail.Visibility = Visibility.Collapsed;
        }

        private void itemDetailClicked(object sender, RoutedEventArgs e)
        {
            Cloth cloth = (Cloth)((Button)sender).CommandParameter;
            popupScreen.Visibility = Visibility.Visible;
            deactivate.IsHitTestVisible = true;
            itemDetail.Visibility = Visibility.Visible;
            itemName.Text = cloth.name;
            itemSizeText.Text = "" + cloth.clothWidth + " x " + cloth.clothHeight;
            itemImage.Fill = new ImageBrush(cloth.picture);

            addToFavBtn.Content = (cloth.isFavorite == 0) ? "Add to favorites" : "Remove from\nfavorites";
            addToFavBtn.CommandParameter = cloth;
            deleteItemBtn.CommandParameter = cloth;
            addToCatBtn.CommandParameter = cloth;
            mixMatchBtn.CommandParameter = cloth;
        }

        private void onlineItemDetailClicked(object sender, RoutedEventArgs e)
        {
            OnlineCloth cloth = (OnlineCloth)((Button)sender).CommandParameter;
            popupScreen.Visibility = Visibility.Visible;
            deactivate.IsHitTestVisible = true;
            onlineItemDetail.Visibility = Visibility.Visible;
            onlineItemName.Text = cloth.name;
            onlineItemSizeText.Text = "" + cloth.clothWidth + " x " + cloth.clothHeight;
            onlineItemImage.Fill = new ImageBrush(cloth.picture);

            onlineAddToFavBtn.Content = (cloth.isFavorite == 0) ? "Add to favorites" : "Remove from\nfavorites";
            onlineAddToFavBtn.CommandParameter = cloth;
            buyBtn.CommandParameter = cloth;
            fittingBtn.CommandParameter = cloth;
        }

        private void buyBtnClicked(object sender, RoutedEventArgs e)
        {
            OnlineCloth cloth = (OnlineCloth)((Button)sender).CommandParameter;
            controller.buyCloth(cloth.store, cloth.id);
            this.refresh();
        }

        private void addToFavBtnClicked(object sender, RoutedEventArgs e)
        {
            Cloth cloth = (Cloth)((Button)sender).CommandParameter;
            if (cloth.isFavorite == 0)
                controller.setFavorite(cloth.id);
            else
                controller.setUnfavorite(cloth.id);
            this.refresh();
            addToFavBtn.Content = (cloth.isFavorite == 0) ? "Add to favorites" : "Remove from\nfavorites";
        }

        private void onlineAddToFavBtnClicked(object sender, RoutedEventArgs e) {
            OnlineCloth cloth = (OnlineCloth)((Button)sender).CommandParameter;
            if (cloth.isFavorite == 0)
                controller.setOnlineFavorite(cloth.store, cloth.id);
            else
                controller.setOnlineUnfavorite(cloth.store, cloth.id);
            this.refresh();
            addToFavBtn.Content = (cloth.isFavorite == 0) ? "Add to favorites" : "Remove from\nfavorites";
            this.refreshStore();
        }

        private void deleteItemBtnClicked(object sender, RoutedEventArgs e)
        {
            Cloth cloth = (Cloth)((Button)sender).CommandParameter;
            controller.deleteCloth(cloth.id);
            this.refresh();
            popupScreen.Visibility = Visibility.Collapsed;
            deactivate.IsHitTestVisible = false;
            itemDetail.Visibility = Visibility.Collapsed;
        }

        private void addToCatBtnClicked(object sender, RoutedEventArgs e)
        {
            Cloth cloth = (Cloth)((Button)sender).CommandParameter;
            
            selectCategoryScreen.Visibility = Visibility.Visible;
            List<string> categories = controller.getCategories();
            List<string> filtered = new List<string>();

            foreach (string cat in categories) {
                if (!cloth.category.Contains(cat))
                {
                    filtered.Add(cat);
                }
            }
            filtered.Remove("all");

            selectCategoryOption.ItemsSource = filtered;

            confirmSelectCategory.CommandParameter = cloth;
            //String selected = selectCategoryOption.SelectedItem.ToString();
        }

        private void confirmSelectCategoryClicked(object sender, RoutedEventArgs e)
        {
            
            Cloth cloth = (Cloth)((Button)sender).CommandParameter;
            if (selectCategoryOption.SelectedItem == null)
                return;

            selectCategoryScreen.Visibility = Visibility.Collapsed;
            String selected = selectCategoryOption.SelectedItem.ToString();
            controller.setClothCategory(cloth.id, selected);
            selectCategoryOption.SelectedItem = null;
        }

        private void OnTouchlessControllerUXEvent(PXCMTouchlessController.UXEventData data)
        {
            if (this.Dispatcher.CheckAccess())
            {
                switch (data.type)
                {
                    case PXCMTouchlessController.UXEventData.UXEventType.UXEvent_Select:
                        {
                            Console.WriteLine("Select");
                            MouseInjection.ClickLeftMouseButton();
                        }
                        break;
                    case PXCMTouchlessController.UXEventData.UXEventType.UXEvent_CursorVisible:
                        {
                            Console.WriteLine("Cursor Visible");
                            MainWindow.Cursor = MainWindow.Cursor;
                            //DisplayArea.Cursor = Cursors.Arrow;
                        }
                        break;
                    case PXCMTouchlessController.UXEventData.UXEventType.UXEvent_CursorNotVisible:
                        {
                            Console.WriteLine("Cursor Not Visible");
                        }
                        break;
                    case PXCMTouchlessController.UXEventData.UXEventType.UXEvent_CursorMove:
                        {
                            System.Windows.Point point = new System.Windows.Point();
                            point.X = Math.Max(Math.Min(1.0F, data.position.x), 0.0F);
                            point.Y = Math.Max(Math.Min(1.0F, data.position.y), 0.0F);

                            System.Windows.Point LoginWindowPosition = MainWindow.PointToScreen(new System.Windows.Point(0d, 0d));

                            int mouseX = (int)(LoginWindowPosition.X + point.X * MainWindow.ActualWidth);
                            int mouseY = (int)(LoginWindowPosition.Y + point.Y * MainWindow.ActualHeight);

                            MouseInjection.SetCursorPos(mouseX, mouseY);
                        }
                        break;
                }
            }
            else
            {
                this.Dispatcher.Invoke(new Action(() => OnTouchlessControllerUXEvent(data)));
            }
        }
    }
}
