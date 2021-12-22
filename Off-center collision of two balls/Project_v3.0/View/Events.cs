using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Forms;
using System.Windows;

namespace Project_v3._0
{
    public class Events
    {
        public static Point MM_Selecting_XY = new Point(0,0);
        public static Point MM_Phantom_XY = new Point(0,0);
        public static Point MM_VectorPoint_XY = new Point(0,0);
        public static System.Windows.Forms.PictureBox LastPictureBox {get;set;}
        public static void SetPictureBoxEvents(System.Windows.Forms.PictureBox PictureBox) {
            LastPictureBox = PictureBox;
            switch (MainWindow.mode)
            {
                case MODE.MOVEMENT:
                    PictureBox.MouseLeave += MouseLeave_Cancel;
                    PictureBox.MouseClick += MouseClick_ChoosePosition;
                    PictureBox.MouseMove += MouseMove_Phantom;
                    
                    break;
                case MODE.INPUT:
                    PictureBox.MouseMove += MouseMove_Selecting;
                    PictureBox.MouseClick += MouseClick_ChooseBall;
                    break;
                case MODE.VECTOR_SETUP:
                    PictureBox.MouseMove += MouseMove_Selecting_Vector;
                    PictureBox.MouseClick += MouseClick_EnterVector;
                    PictureBox.MouseLeave += MouseLeave_Cancel_VectorSelecting;
                    break;
                case MODE.OUTPUT:
                    break;
            }
        }
        

        public static void MouseMove_Selecting_Vector(object sender, System.Windows.Forms.MouseEventArgs e) {
            if (MM_VectorPoint_XY == new Point(e.X, e.Y))
                return;
            switch (MainWindow.location_system)
            {
/*                case LOCATION_SYSTEM.STRAIGHT:
                    if (MainWindow.clicked_vector_setup_ball == CLICKED_FOR_VECTOR_SETUP_BALL.BALL1)
                    {
                        MM_VectorPoint_XY = new Point(e.X, DrawingInstruments.LastDataList.Last<Data>().BallFirst.Position.y);
                    }
                    else
                    {
                        MM_VectorPoint_XY = new Point(e.X, e.Y);
                    }
                    break;
                case LOCATION_SYSTEM.ARBITRARY:
                    MM_VectorPoint_XY = new Point(e.X, e.Y);
                    break;*/
                default:
                    MM_VectorPoint_XY = new Point(e.X, e.Y);
                    break;
            }
           
            DrawingInstruments.RefreshLastDisplay();
        }
        public static void MouseClick_EnterVector(object sender, System.Windows.Forms.MouseEventArgs e) {
            switch (MainWindow.clicked_vector_setup_ball)
            {
                case CLICKED_FOR_VECTOR_SETUP_BALL.BALL1:
                    switch (MainWindow.location_system)
                    {
/*                        case LOCATION_SYSTEM.STRAIGHT:
                            DrawingInstruments.LastDataList.Last<Data>().BallFirst.Speed = new Vector(DrawingInstruments.LastDataList.Last<Data>().BallFirst.Position,
                                new Point(e.X, DrawingInstruments.LastDataList.Last<Data>().BallFirst.Position.y));

                            break;
                        case LOCATION_SYSTEM.ARBITRARY:
                            DrawingInstruments.LastDataList.Last<Data>().BallFirst.Speed = new Vector(DrawingInstruments.LastDataList.Last<Data>().BallFirst.Position, new Point(e.X, e.Y));
                            break;*/
                        default:
                            DrawingInstruments.LastDataList.Last<Data>().BallFirst.Speed = new Vector(DrawingInstruments.LastDataList.Last<Data>().BallFirst.Position, new Point(e.X, e.Y));
                            break;
                    }


                    Data.this_.SetDataFromInputs(DrawingInstruments.LastDataList.Last<Data>());
                    break;
                case CLICKED_FOR_VECTOR_SETUP_BALL.BALL2:
                    DrawingInstruments.LastDataList.Last<Data>().BallSecond.Speed = new Vector(DrawingInstruments.LastDataList.Last<Data>().BallSecond.Position, new Point(e.X, e.Y));
                    Data.this_.SetDataFromInputs(DrawingInstruments.LastDataList.Last<Data>());
                    break;
            }

            // Изменить координату
            LifeCycle.OnRemoveVectorSetupMode();
            LifeCycle.OnCreateInputMode();
            DrawingInstruments.RefreshLastDisplay();
        }
        public static void MouseLeave_Cancel_VectorSelecting(object sender, EventArgs e) {
            // Отмена ивентов и выход из режима
            LifeCycle.OnRemoveVectorSetupMode();
            LifeCycle.OnCreateInputMode();
            DrawingInstruments.RefreshLastDisplay();
        }

        public static void RemovePictureBoxEvents() {
            if (LastPictureBox !=null)
            {
                LastPictureBox.MouseLeave -= MouseLeave_Cancel;
                LastPictureBox.MouseClick -= MouseClick_ChoosePosition;
                LastPictureBox.MouseClick -= MouseClick_ChooseBall;
                LastPictureBox.MouseMove -= MouseMove_Phantom;
                LastPictureBox.MouseMove -= MouseMove_Selecting;
                LastPictureBox.MouseMove -= MouseMove_Selecting_Vector;
                LastPictureBox.MouseClick -= MouseClick_EnterVector;
                LastPictureBox.MouseLeave -= MouseLeave_Cancel_VectorSelecting;
            }
        }
        public static void MouseLeave_Cancel(object sender, EventArgs e) {
            // Отмена ивентов и выход из режима
            LifeCycle.OnRemoveMovementMode();
            LifeCycle.OnCreateInputMode();
            DrawingInstruments.RefreshLastDisplay();
        }
        
        public static void MouseClick_ChoosePosition(object sender, System.Windows.Forms.MouseEventArgs e) {
            switch (MainWindow.clicked_ball)
            {
                case CLICKED_BALL.BALL1:
                    switch (MainWindow.location_system)
                    {
/*                        case LOCATION_SYSTEM.STRAIGHT:
                            DrawingInstruments.LastDataList.Last<Data>().BallFirst.Position = new Point(e.X, DrawingInstruments.LastDataList.Last<Data>().BallFirst.Position.y);
                            Data.this_.SetDataFromInputs(DrawingInstruments.LastDataList.Last<Data>());
                            // Установить поля для метода getInputsData
                            break;
                        case LOCATION_SYSTEM.ARBITRARY:
                            DrawingInstruments.LastDataList.Last<Data>().BallFirst.Position = new Point(e.X, e.Y);
                            Data.this_.SetDataFromInputs(DrawingInstruments.LastDataList.Last<Data>());
                            // Установить поля для метода getInputsData
                            break;*/
                        default:
                            DrawingInstruments.LastDataList.Last<Data>().BallFirst.Position = new Point(e.X, e.Y);
                            Data.this_.SetDataFromInputs(DrawingInstruments.LastDataList.Last<Data>());
                            break;
                    }
                    break;
                case CLICKED_BALL.BALL2:
                    DrawingInstruments.LastDataList.Last<Data>().BallSecond.Position = new Point(e.X, e.Y);
                    Data.this_.SetDataFromInputs(DrawingInstruments.LastDataList.Last<Data>());
                    break;
            }

            // Изменить координату
            LifeCycle.OnRemoveMovementMode();
            LifeCycle.OnCreateInputMode();
            DrawingInstruments.RefreshLastDisplay();
        }
        public static void MouseMove_Phantom(object sender, System.Windows.Forms.MouseEventArgs e) {
            if (MM_Phantom_XY == new Point(e.X, e.Y))
                return;
            
            switch (MainWindow.location_system)
            {
/*                case LOCATION_SYSTEM.STRAIGHT:
                    if (MainWindow.clicked_ball == CLICKED_BALL.BALL1)
                    {
                        MM_Phantom_XY = new Point(e.X, DrawingInstruments.LastDataList.Last<Data>().BallFirst.Position.y);
                    }
                    else {
                        MM_Phantom_XY = new Point(e.X, e.Y);
                    }
                    
                    break;
                case LOCATION_SYSTEM.ARBITRARY:
                    MM_Phantom_XY = new Point(e.X, e.Y);
                    break;*/
                default:
                    MM_Phantom_XY = new Point(e.X, e.Y);
                    break;
            }
            DrawingInstruments.RefreshLastDisplay();
        }
        public static void MouseClick_ChooseBall(object sender, System.Windows.Forms.MouseEventArgs e)
        {

            if (Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                // Обработка вектора
                if (Physics.isPointInBall(new Point(e.X, e.Y), Data.lastData.BallFirst.Position, Data.lastData.BallFirst.Radius))
                {
                    LifeCycle.OnRemoveInputMode();
                    MainWindow.clicked_vector_setup_ball = CLICKED_FOR_VECTOR_SETUP_BALL.BALL1;
                    LifeCycle.OnCreateVectorSetupMode();
                }
                else if (Physics.isPointInBall(new Point(e.X, e.Y), Data.lastData.BallSecond.Position, Data.lastData.BallSecond.Radius))
                {
                    LifeCycle.OnRemoveInputMode();
                    MainWindow.clicked_vector_setup_ball = CLICKED_FOR_VECTOR_SETUP_BALL.BALL2;
                    LifeCycle.OnCreateVectorSetupMode();
                }
                MM_VectorPoint_XY = new Point(e.X, e.Y);
                DrawingInstruments.RefreshLastDisplay();
            }
            else {
                if (Physics.isPointInBall(new Point(e.X, e.Y), Data.lastData.BallFirst.Position, Data.lastData.BallFirst.Radius))
                {
                    LifeCycle.OnRemoveInputMode();
                    MainWindow.clicked_ball = CLICKED_BALL.BALL1;
                    LifeCycle.OnCreateMovementMode();
                }
                else if (Physics.isPointInBall(new Point(e.X, e.Y), Data.lastData.BallSecond.Position, Data.lastData.BallSecond.Radius))
                {
                    LifeCycle.OnRemoveInputMode();
                    MainWindow.clicked_ball = CLICKED_BALL.BALL2;
                    LifeCycle.OnCreateMovementMode();
                }
                DrawingInstruments.RefreshLastDisplay();

            }

        }
        public static void MouseMove_Selecting(object sender, System.Windows.Forms.MouseEventArgs e) {
            if (MM_Selecting_XY == new Point(e.X,e.Y))
                return;
            
            MM_Selecting_XY = new Point(e.X,e.Y);
            if (Physics.isPointInBall(MM_Selecting_XY, Data.lastData.BallFirst.Position, Data.lastData.BallFirst.Radius))
            {
                MainWindow.selection_ball = SELECTION_BALL.BALL1;
                DrawingInstruments.RefreshLastDisplay();
            } else if (Physics.isPointInBall(MM_Selecting_XY, Data.lastData.BallSecond.Position, Data.lastData.BallSecond.Radius)) {
                MainWindow.selection_ball = SELECTION_BALL.BALL2;
                DrawingInstruments.RefreshLastDisplay();
            }   
            else {
                
                if (MainWindow.selection_ball != SELECTION_BALL.NONE)
                {
                    MainWindow.selection_ball = SELECTION_BALL.NONE;
                    DrawingInstruments.RefreshLastDisplay();
                }
               
            }
           
        }

        public static void SetSensitiveElementsEvents() {
            foreach (var Element in MainWindow.SensitivePermissionElementsList)
            {
                switch (Element)
                {
                    case System.Windows.Controls.TextBox elem:
                        elem.TextChanged += OnlyDigitsInput;

                        elem.MaxLength = 5;
                        break;
                    case System.Windows.Controls.RadioButton elem:
                        elem.Checked += RefreshPictureBox;
                        break;
                    default:
                        break;
                }
            }
        }
        private static void SliderRefreshDt(object sender, RoutedPropertyChangedEventArgs<double> e) {
            StaticResources.Dt = ((Slider)sender).Value / 1000;
            Data.this_.textbox_dt.Text = (((Slider)sender).Value / 1000).ToString();

        }
        private static void ResetDefaultDt(object sender, RoutedEventArgs e) {
            StaticResources.Dt = StaticResources.DefaultDt;
            Data.this_.slider_dt.Value = StaticResources.Dt*1000;
            Data.this_.textbox_dt.Text = StaticResources.Dt.ToString();
        }
        private static void TextBoxRefreshDt(object sender, TextChangedEventArgs e) {
            double digit;
            Double.TryParse(((System.Windows.Controls.TextBox)sender).Text, out digit);
            StaticResources.Dt = digit;
            Data.this_.slider_dt.Value = digit * 1000;
        }
        private static void TextBoxBarier(object sender, TextChangedEventArgs e) {
            int digit;
            int.TryParse(((System.Windows.Controls.TextBox)sender).Text, out digit);
            StaticResources.ChartPointsMax = digit;
            if (Handler.DataList.Count !=0)
            {
                try
                {
                    ChartInstruments.UpdateDataChart(Handler.DataList);
                }
                catch (Exception)
                {
                    return;
                }
                   
              
            }
        }
        private static void CheckedBarier(object sender, RoutedEventArgs e) {
            if (((System.Windows.Controls.CheckBox)sender).IsChecked == true)
            {
                MainWindow.chartPointsBarier = CHART_POINT_BARIER.TRUE;
            }
            else {
                MainWindow.chartPointsBarier = CHART_POINT_BARIER.FALSE;
            }
        }

        public static void SetNoSensitiveElementsEvents() {
            foreach (var Element in MainWindow.NoSensitivePermissionElementsList)
            {
                switch (Element)
                {
                    case System.Windows.Controls.TextBox elem:
                        elem.TextChanged += OnlyNoSensitiveDigitsInput;
                        elem.MaxLength = 5;
                        break;
                    case System.Windows.Controls.Slider elem:
                        elem.ValueChanged += SliderRefreshDt;
                        break;
                    case System.Windows.Controls.Button elem:
                        elem.Click += ResetDefaultDt;
                        break;
                    case System.Windows.Controls.CheckBox elem:
                        elem.Click += CheckedBarier;
                       /* elem.Click += ResetDefaultDt;*/
                        break;
                    default:
                        break;
                }
            }
        }
        public static void OnlyDigitsInput(object sender, EventArgs e)
        {
            double digit;
            string text = ((System.Windows.Controls.TextBox)sender).Text;
            if (text == "")
            {
                text = "0";
            }


            if (Double.TryParse(text,out digit))
            {
                ((System.Windows.Controls.TextBox)sender).TextChanged += RefreshPictureBox;
                RefreshPictureBox(1);
            }
            else
            {
                ViewMethods.SavePositionCaret(sender);
                ((System.Windows.Controls.TextBox)sender).TextChanged += RefreshPictureBox;
            }
           
        }
        public static void OnlyNoSensitiveDigitsInput(object sender, EventArgs e)
        {
            double digit;
            string text = ((System.Windows.Controls.TextBox)sender).Text;
            if (text == "")
            {
                text = "0";
            }
            if (Double.TryParse(text,out digit))
            {
                
                switch (((System.Windows.Controls.TextBox)sender).Name)
                {
                    case "textbox_point_chart":
                        // set 
                        ((System.Windows.Controls.TextBox)sender).TextChanged += TextBoxBarier;
                        break;
                    case "textbox_dt":
                        if (digit < 15)
                        {
                            ((System.Windows.Controls.TextBox)sender).TextChanged += TextBoxRefreshDt;
                        }
                        break;
                }
            }
            else
            {
                ViewMethods.SavePositionCaret(sender);
            }
           
        }
        public static void RefreshPictureBox(object sender, EventArgs e=null) {
            List<Data> tmp = new List<Data>();
            Data Tdata = Data.this_.GetDataFromInputs();
            if (Tdata == null)
                return;
            tmp.Add(Tdata);
            DrawingInstruments.Display(tmp);
        }


    }
}
