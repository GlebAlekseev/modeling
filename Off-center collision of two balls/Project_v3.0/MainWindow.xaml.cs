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
using System.Windows.Forms.DataVisualization.Charting;

namespace Project_v3._0
{

    public partial class MainWindow : Window
    {
        public static System.Windows.Forms.PictureBox pictureBox;
        public static System.Windows.Forms.PictureBox pictureBox_logo_ball_1;
        public static System.Windows.Forms.PictureBox pictureBox_logo_ball_2;
        public static List<object> SensitivePermissionElementsList = new List<object>();
        public static List<object> NoSensitivePermissionElementsList = new List<object>();
        public Handler Handler;
        public static PERMISSION_SENSITIVE_ELEMENTS_GUI pseg = PERMISSION_SENSITIVE_ELEMENTS_GUI.ACCESS;
        public static LOCATION_SYSTEM location_system = LOCATION_SYSTEM.ARBITRARY;
        public static MODE mode = MODE.INPUT;
        public static SELECTION_BALL selection_ball = SELECTION_BALL.NONE;
        public static CLICKED_BALL clicked_ball = CLICKED_BALL.NONE;
        public static CLICKED_FOR_VECTOR_SETUP_BALL clicked_vector_setup_ball = CLICKED_FOR_VECTOR_SETUP_BALL.NONE;
        public static CHART_POINT_BARIER chartPointsBarier = CHART_POINT_BARIER.FALSE;

        public static Chart Chart_1 = new Chart();
        public static Chart Chart_2 = new Chart();
        public static Chart Chart_3 = new Chart();
        public static Chart Chart_4 = new Chart();

        public MainWindow()
        {
            InitializeComponent();

            

            textbox_point_chart.Text = StaticResources.ChartPointsMax.ToString();


            pictureBox = new System.Windows.Forms.PictureBox();
            System.Windows.Forms.Integration.WindowsFormsHost hosts = new System.Windows.Forms.Integration.WindowsFormsHost();
            hosts.Child = pictureBox;
            grid.Children.Clear();
            grid.Children.Add(hosts);

            pictureBox_logo_ball_1 = new System.Windows.Forms.PictureBox();
            System.Windows.Forms.Integration.WindowsFormsHost host1 = new System.Windows.Forms.Integration.WindowsFormsHost();
            host1.Child = pictureBox_logo_ball_1;
            grid_logo_ball1.Children.Clear();
            grid_logo_ball1.Children.Add(host1);

            pictureBox_logo_ball_2 = new System.Windows.Forms.PictureBox();
            System.Windows.Forms.Integration.WindowsFormsHost host2 = new System.Windows.Forms.Integration.WindowsFormsHost();
            host2.Child = pictureBox_logo_ball_2;
            grid_logo_ball2.Children.Clear();
            grid_logo_ball2.Children.Add(host2);


            Handler = Handler.newInstance();
   
            SensitivePermissionElementsList = GetSensitivePermissionElementsList();
            NoSensitivePermissionElementsList = GetNoSensitivePermissionElementsList();
            
            if (GetDataFromInputs() == null)
            {
                throw new Exception("Неверные значения по умолчанию");
            }


     

            SetDataFromInputs(Data.DefaultArbitrary);
            LifeCycle.OnCreateArbitrarySystem();
            LifeCycle.OnCreateInputMode();
            Events.SetSensitiveElementsEvents();
            Events.SetNoSensitiveElementsEvents();
            DrawingInstruments.DisplayLogos();

            textbox_dt.Text = StaticResources.DefaultDt.ToString(); ;
            slider_dt.Value = StaticResources.DefaultDt * 1000;


            ChartInstruments.setChart("Импульс", "Время", Chart_1, "Pdt");
            ChartInstruments.setChart("Сумма импульсов", "Время", Chart_2, "Edt");
            ChartInstruments.setChart("Скорость", "Время", Chart_3, "Vdt");
            ChartInstruments.setChart("Энергия", "Время", Chart_4, "Edt");




            System.Windows.Forms.Integration.WindowsFormsHost hosts1 = new System.Windows.Forms.Integration.WindowsFormsHost();
            hosts1.Child = Chart_1;
            Data.gridChart_1.Children.Clear();
            Data.gridChart_1.Children.Add(hosts1);

            System.Windows.Forms.Integration.WindowsFormsHost hosts2 = new System.Windows.Forms.Integration.WindowsFormsHost();
            hosts2.Child = Chart_2;
            Data.gridChart_2.Children.Clear();
            Data.gridChart_2.Children.Add(hosts2);
            
            System.Windows.Forms.Integration.WindowsFormsHost hosts3 = new System.Windows.Forms.Integration.WindowsFormsHost();
            hosts3.Child = Chart_3;
            Data.gridChart_3.Children.Clear();
            Data.gridChart_3.Children.Add(hosts3);
            
            System.Windows.Forms.Integration.WindowsFormsHost hosts4 = new System.Windows.Forms.Integration.WindowsFormsHost();
            hosts4.Child = Chart_4;
            Data.gridChart_4.Children.Clear();
            Data.gridChart_4.Children.Add(hosts4);





        }


        // События View-элементов
        // Кнопки управления
        private void startBtn_Click(object sender, RoutedEventArgs e)
        {
            Data newData = GetDataFromInputs();
            if (newData == null)
                return;
            if (Handler.Start(newData))
            {
                pseg = PERMISSION_SENSITIVE_ELEMENTS_GUI.DENIED;
                ViewMethods.UpdatePermissionSensitiveElementsGUI();
                LifeCycle.OnRemoveInputMode();
                LifeCycle.OnCreateOutputMode();
            }
        }
        private void pauseBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Handler.Pause())
            {

            }
        }
        private void resumeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Handler.Resume())
            {

            }
        }
        private void stopBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Handler.Stop())
            {
                pseg = PERMISSION_SENSITIVE_ELEMENTS_GUI.ACCESS;
                ViewMethods.UpdatePermissionSensitiveElementsGUI();
                LifeCycle.OnRemoveOutputMode();
                LifeCycle.OnCreateInputMode();

            }
        }


        // Автозаполнение
        private void button_autosv_Click(object sender, RoutedEventArgs e)
        {


            SetDataFromInputs(Data.GetRandom());
            Data current = GetDataFromInputs();
            if (current !=null)
            {
                List<Data> LData = new List<Data>();
                LData.Add(current);
                DrawingInstruments.Display(LData);
            }
            
        }


        // Изменение текста в инпуте
        private void textbox_dt_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void slider_dt_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
        }
        private void button_reset_tb_slider_Click(object sender, RoutedEventArgs e)
        {
        }

        //Фильтры
        private void inp_TextChanged(object sender, TextChangedEventArgs e)
        {
        }
        private void inp_dt_TextChanged(object sender, TextChangedEventArgs e)
        {
        }
        public Data GetDataFromInputs()
        {
            // Проверка на данные

            Data data = new Data();

            // Добавляем ссылку на поле для отрисовки
            Data.Height = (int)grid.Height;
            Data.Width = (int)grid.Width;
            Data.grid = grid;
            Data.gridChart_1 = gridChart_1;
            Data.gridChart_2 = gridChart_2;
            Data.gridChart_3 = gridChart_3;
            Data.gridChart_4 = gridChart_4;
            Data.this_ = this;
            Data.lastData = data;

            try
            {
                data.BallFirst = new Ball();
                data.BallSecond = new Ball();
                data.BallFirst.Color = StaticResources.DefaultColorBall1;
                data.BallSecond.Color = StaticResources.DefaultColorBall2;
                data.BallFirst.Speed = new Vector(new Point(ViewMethods.CheckTextBox(textbox_vx1), ViewMethods.CheckTextBox(textbox_vy1)));
                data.BallFirst.Mass = ViewMethods.CheckTextBox(textbox_m1);
                data.BallFirst.Radius = (int)ViewMethods.CheckTextBox(textbox_r1);
                data.BallFirst.Position = new Point(ViewMethods.CheckTextBox(textbox_cx1), ViewMethods.CheckTextBox(textbox_cy1));
                data.BallSecond.Speed = new Vector(new Point(ViewMethods.CheckTextBox(textbox_vx2), ViewMethods.CheckTextBox(textbox_vy2)));
                data.BallSecond.Mass = ViewMethods.CheckTextBox(textbox_m2);
                data.BallSecond.Radius = (int)ViewMethods.CheckTextBox(textbox_r2);
                data.BallSecond.Position = new Point(ViewMethods.CheckTextBox(textbox_cx2), ViewMethods.CheckTextBox(textbox_cy2));
            }
            catch (Exception)
            {

                return null;
            }

            return data;
        }
        public void SetDataFromInputs(Data data)
        {
            Console.WriteLine("SetDataFromInputs");
            data.Print();
            textbox_cy1.Text = data.BallFirst.Position.y.ToString();
            textbox_vy1.Text = data.BallFirst.Speed.PointVector.y.ToString();

          


            textbox_cx1.Text = data.BallFirst.Position.x.ToString();
            textbox_cx2.Text = data.BallSecond.Position.x.ToString();
            textbox_cy2.Text = data.BallSecond.Position.y.ToString();

            textbox_m1.Text = data.BallFirst.Mass.ToString();
            textbox_m2.Text = data.BallSecond.Mass.ToString();

            textbox_r1.Text = data.BallFirst.Radius.ToString();
            textbox_r2.Text = data.BallSecond.Radius.ToString();

            textbox_vx1.Text = data.BallFirst.Speed.PointVector.x.ToString();


           

            textbox_vx2.Text = data.BallSecond.Speed.PointVector.x.ToString();
            textbox_vy2.Text = data.BallSecond.Speed.PointVector.y.ToString();
        }
        private List<object> GetSensitivePermissionElementsList() {
            List<object> tmp = new List<object>();
            tmp.Add(textbox_cx1);
            tmp.Add(textbox_cx2);
            tmp.Add(textbox_cy1);
            tmp.Add(textbox_cy2);
            tmp.Add(textbox_m1);
            tmp.Add(textbox_m2);
            tmp.Add(textbox_r1);
            tmp.Add(textbox_r2);
            tmp.Add(textbox_vx1);
            tmp.Add(textbox_vx2);
            tmp.Add(textbox_vy1);
            tmp.Add(textbox_vy2);
            tmp.Add(button_autosv);
            tmp.Add(button_reset_data);
            tmp.Add(radiobtn_1_view);
            tmp.Add(radiobtn_2_view);
            return tmp;
        }
        private List<object> GetNoSensitivePermissionElementsList() {
            List<object> tmp = new List<object>();
            tmp.Add(textbox_dt);
            tmp.Add(slider_dt);
            tmp.Add(button_reset_tb_slider);
            tmp.Add(textbox_point_chart);
            tmp.Add(checkbox_point_chart);
            return tmp;
        }

        private void radiobtn_1_view_Checked(object sender, RoutedEventArgs e)
        {
            switch (location_system)
            {
                case LOCATION_SYSTEM.STRAIGHT:
                    break;
                case LOCATION_SYSTEM.ARBITRARY:
                    LifeCycle.OnRemoveArbitrarySystem();
                    LifeCycle.OnCreateStraightSystem();
                    break;

            }
           
        }

        private void radiobtn_2_view_Checked(object sender, RoutedEventArgs e)
        {
            switch (location_system)
            {
                case LOCATION_SYSTEM.STRAIGHT:
                    LifeCycle.OnRemoveStraightSystem();
                    LifeCycle.OnCreateArbitrarySystem();
                    break;
                case LOCATION_SYSTEM.ARBITRARY:
                    break;

            }

        }

        private void button_reset_data_Click(object sender, RoutedEventArgs e)
        {
            if (mode == MODE.INPUT)
            {
                SetDataFromInputs(Data.DefaultArbitrary);
            }

        }
    }
}
