using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace Project_v3._0
{
    public static class DrawingInstruments
    {
      
        public static System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(Data.Height * 2, Data.Width * 2);
        public static List<Data> LastDataList = new List<Data>();
        public static void RefreshLastDisplay() {
            if (LastDataList.Count != 0)
            {
                DrawingInstruments.Display(LastDataList);
            }
            else {
                List<Data> tmp = new List<Data>();
                Data Tdata = Data.this_.GetDataFromInputs();
                if (Tdata == null)
                    return;
                tmp.Add(Tdata);
                DrawingInstruments.Display(tmp);
            }
           
        }
        public static void DisplayLogos() {
            System.Drawing.Bitmap bitmap_logo_1 = new System.Drawing.Bitmap((int)Data.this_.grid_logo_ball1.Height * 2, (int)Data.this_.grid_logo_ball1.Width * 2);
            System.Drawing.Bitmap bitmap_logo_2 = new System.Drawing.Bitmap((int)Data.this_.grid_logo_ball2.Height * 2, (int)Data.this_.grid_logo_ball2.Width * 2);


            Graphics graphics1;
            Graphics graphics2;
            graphics1 = System.Drawing.Graphics.FromImage(bitmap_logo_1);
            graphics2 = System.Drawing.Graphics.FromImage(bitmap_logo_2);


            Color bg = ColorTranslator.FromHtml("#FFE5E5E5");
            graphics1.Clear(bg);
            graphics2.Clear(bg);
            Sketch sketchNew;

            sketchNew = new Sketch();
            sketchNew.Point = new Point(0, 0);
            sketchNew.Rectangle = new System.Drawing.Rectangle(sketchNew.Point, new System.Drawing.Size((int)(Data.this_.grid_logo_ball1.Width/1.1), (int)(Data.this_.grid_logo_ball1.Height/1.1)));
            sketchNew.Pen = new System.Drawing.Pen(StaticResources.DefaultColorBall1, 0);
            sketchNew.Brushes = new SolidBrush(StaticResources.DefaultColorBall1);
            graphics1.DrawEllipse(sketchNew.Pen, sketchNew.Rectangle);
            graphics1.FillEllipse(sketchNew.Brushes, sketchNew.Rectangle);     

            sketchNew = new Sketch();
            sketchNew.Point = new Point(0, 0);
            sketchNew.Rectangle = new System.Drawing.Rectangle(sketchNew.Point, new System.Drawing.Size((int)(Data.this_.grid_logo_ball2.Width/1.1), (int)(Data.this_.grid_logo_ball2.Height/1.1)));
            sketchNew.Pen = new System.Drawing.Pen(StaticResources.DefaultColorBall2, 0);
            sketchNew.Brushes = new SolidBrush(StaticResources.DefaultColorBall2);
            graphics2.DrawEllipse(sketchNew.Pen, sketchNew.Rectangle);
            graphics2.FillEllipse(sketchNew.Brushes, sketchNew.Rectangle);

            MainWindow.pictureBox_logo_ball_1.Image = bitmap_logo_1;
            MainWindow.pictureBox_logo_ball_2.Image = bitmap_logo_2;

        }
        public static void CheckIntersection(Data data) {
            bool stIntersection;
            switch (MainWindow.mode)
            {
                case MODE.VECTOR_SETUP:
                    stIntersection = Physics.IsIntersection(data, Events.MM_VectorPoint_XY);
                    break;
                case MODE.MOVEMENT:
                    stIntersection = Physics.IsIntersection(data, Events.MM_Phantom_XY);
                    break;
                case MODE.INPUT:
                    stIntersection = Physics.IsIntersection(data);
                    break;
                default:
                    return;
            }

            Data.this_.Dispatcher.Invoke(new Action(() =>
            {
                if (stIntersection)
                {
                    Data.this_.textBlockItersection.Text = $"Столкнутся: Да ";
                }
                else
                {
                    Data.this_.textBlockItersection.Text = $"Столкнутся: Нет";
                }

            }));
        }
        public static void Display(List<Data> DataList) {
            LastDataList = DataList;

            CheckIntersection(LastDataList.Last<Data>());

            System.Drawing.Graphics graphics = GetField();
            Console.WriteLine($"\t Display {DataList.Last<Data>().BallFirst.Position.y} {DataList.Last<Data>().BallFirst.Speed.PointVector.y}");

            switch (MainWindow.mode)
            {
                case MODE.MOVEMENT:
                    // Отрисовка перемещения мышкой
                    // Получить данные о перемещении мыши или из глоабала или из статика
                    DrawBalls(graphics, DataList,phantom:Events.MM_Phantom_XY, ballChoose: MainWindow.clicked_ball);
                    // Отрисовать выбранный мяч в положении MM_Phantom_XY

                    break;
                case MODE.INPUT:
                    DrawBalls(graphics, DataList, track: false, contour: true);
                    // Отрисовка после изменения данных
                    // Выделение
                    break;
                case MODE.OUTPUT:
                    DrawBalls(graphics,DataList, track: true, contour: false);
                    // Отрисовка итерации
                    // Траектория
                    break;
                case MODE.VECTOR_SETUP:
                    // Отрисовка вектора
                    DrawBalls(graphics, DataList, vector: true, clicked_vector:MainWindow.clicked_vector_setup_ball);
                    break;
                default:
                    break;
            }
            DisplayBitmap();
        }
        private static void DrawBalls(Graphics graphics ,List<Data> DataList_,bool track = false,bool contour=false,Point phantom = null,
            CLICKED_BALL ballChoose = CLICKED_BALL.NONE, bool vector = false, CLICKED_FOR_VECTOR_SETUP_BALL clicked_vector = CLICKED_FOR_VECTOR_SETUP_BALL.NONE) {
            List<Data> DataList = new List<Data>();
            switch (MainWindow.location_system)
            {
                case LOCATION_SYSTEM.STRAIGHT:
                    foreach (var Data in DataList_)
                    {
                        Data tmp = Data;
                        tmp.BallFirst.Position = tmp.BallFirst.Position - tmp.BallSecond.Position + new Point(Data.Width * 2 / 3, Data.Height / 2);
                        tmp.BallSecond.Position = new Point(Data.Width*2/3,Data.Height/2);

                        tmp.BallFirst.Speed = tmp.BallFirst.Speed - tmp.BallSecond.Speed;
                        tmp.BallSecond.Speed = new Vector(new Point(0, 0));

                        DataList.Add(tmp);
                    }
                    break;
                case LOCATION_SYSTEM.ARBITRARY:
                    DataList = DataList_;
                    break;
                default:
                    DataList = DataList_;
                    break;
            }


            if (track)
                DrawTrack(graphics, DataList);
            List<Ball> balls = new List<Ball>();
            balls.Add(DataList.Last<Data>().BallFirst);
            balls.Add(DataList.Last<Data>().BallSecond);
            int i = 0;
            foreach (var ball in balls)
            {
                Ball TMP = ball;
                Sketch sketch;
             
                if (phantom != null && i == 0 && MainWindow.clicked_ball == CLICKED_BALL.BALL1)
                {
                    Ball tmpBall = new Ball();
                    tmpBall.Color = TMP.Color;
                    tmpBall.Mass = TMP.Mass;
                    tmpBall.Radius= TMP.Radius;
                    tmpBall.Speed= TMP.Speed;
                    tmpBall.TrackColor = TMP.TrackColor;
                    tmpBall.Position= Events.MM_Phantom_XY;
                    TMP = tmpBall;
                }
                if (phantom != null && i == 1 && MainWindow.clicked_ball == CLICKED_BALL.BALL2)
                {
                    Ball tmpBall = new Ball();
                    tmpBall.Color = TMP.Color;
                    tmpBall.Mass = TMP.Mass;
                    tmpBall.Radius = TMP.Radius;
                    tmpBall.Speed = TMP.Speed;
                    tmpBall.TrackColor = TMP.TrackColor;
                    tmpBall.Position = Events.MM_Phantom_XY;
                    TMP = tmpBall;
                }


                sketch = GetSketch(TMP);
                DrawSketch(graphics,sketch);
                Console.WriteLine($"DrawVector{Events.MM_VectorPoint_XY.x} {Events.MM_VectorPoint_XY.y}");
                if (i==0 && vector && clicked_vector == CLICKED_FOR_VECTOR_SETUP_BALL.BALL1)
                {
                    DrawVector(graphics, TMP,phantomPoint:Events.MM_VectorPoint_XY);
                }
                else if (i == 1 && vector && clicked_vector == CLICKED_FOR_VECTOR_SETUP_BALL.BALL2)
                {
                    DrawVector(graphics, TMP, phantomPoint: Events.MM_VectorPoint_XY);
                }
                else {
                    // Отрисовка веткора
                    DrawVector(graphics, TMP);
                }
               
                if (i ==0 && MainWindow.selection_ball == SELECTION_BALL.BALL1)
                    DrawContour(graphics, sketch);
                if (i ==1 && MainWindow.selection_ball == SELECTION_BALL.BALL2)
                    DrawContour(graphics, sketch);
                i++;  
            }
        }
        private static void DrawVector(Graphics graphics, Ball ball, Point phantomPoint = null)
        {
            Pen pen = new Pen(ball.ColorVector);
            pen.CustomEndCap = new System.Drawing.Drawing2D.AdjustableArrowCap(4, 4);
            Point newP = ball.Speed.PointVector + ball.Position;
            if (phantomPoint != null)
                newP = phantomPoint;
            graphics.DrawLine(pen, ball.Position, newP);
        }

        private static void DrawContour(Graphics graphics,Sketch sketch) {
            Sketch sketchNew = new Sketch();
            sketchNew.Point = new Point(sketch.Point.X, sketch.Point.Y) - new Point(5, 5);
            sketchNew.Rectangle = new System.Drawing.Rectangle(sketchNew.Point, new System.Drawing.Size((int)(sketch.Rectangle.Width + 10), (int)(sketch.Rectangle.Height + 10)));
            sketchNew.Pen = new System.Drawing.Pen(sketch.Color, 0);
            sketchNew.Brushes = new SolidBrush(sketch.Color);
            graphics.DrawEllipse(sketchNew.Pen, sketchNew.Rectangle);
        }
        private static Sketch GetSketch(Ball ball, Point positionMouse = null)
        {
            Sketch sketch = new Sketch();
            sketch.Point = ball.Position - new Point(ball.Radius, ball.Radius);
            sketch.Rectangle = new System.Drawing.Rectangle(sketch.Point, new System.Drawing.Size(ball.Radius * 2, ball.Radius * 2));
            sketch.Pen = new System.Drawing.Pen(ball.Color, 0);
            sketch.Brushes = new SolidBrush(ball.Color);
            sketch.Color =ball.Color;
            return sketch;
        }
        private static void DrawSketch(Graphics graphics, Sketch sketch)
        {
            graphics.DrawEllipse(sketch.Pen, sketch.Rectangle);
            graphics.FillEllipse(sketch.Brushes, sketch.Rectangle);
        }
        private static void DrawTrack(Graphics graphics,List<Data> DataList) {
            for (int i = 0; i < DataList.Count - 1; i++)
            {
                float x1 = (float)DataList.ElementAt(i).BallFirst.Position.x;
                float y1 = (float)DataList.ElementAt(i).BallFirst.Position.y;
                float x1_after = (float)DataList.ElementAt(i + 1).BallFirst.Position.x;
                float y1_after = (float)DataList.ElementAt(i + 1).BallFirst.Position.y;
                graphics.DrawLine(new System.Drawing.Pen(DataList.ElementAt(i + 1).BallFirst.TrackColor, 1), x1, y1, x1_after, y1_after);

                float x2 = (float)DataList.ElementAt(i).BallSecond.Position.x;
                float y2 = (float)DataList.ElementAt(i).BallSecond.Position.y;
                float x2_after = (float)DataList.ElementAt(i + 1).BallSecond.Position.x;
                float y2_after = (float)DataList.ElementAt(i + 1).BallSecond.Position.y;
                graphics.DrawLine(new System.Drawing.Pen(DataList.ElementAt(i + 1).BallSecond.TrackColor, 1), x2, y2, x2_after, y2_after);
            }
        }
        private static Graphics GetField()
        {
            Graphics graphics;
            graphics = System.Drawing.Graphics.FromImage(DrawingInstruments.bitmap);
            graphics.Clear(Color.White);
            graphics.DrawLine(new Pen(Color.LightGray), 0, Data.Height / 2, Data.Width, Data.Height / 2);
            graphics.DrawLine(new Pen(Color.LightGray), Data.Width / 2, 0, Data.Width / 2, Data.Height);
            return graphics;
        }
        private static void DisplayBitmap() {
                MainWindow.pictureBox.Image = DrawingInstruments.bitmap;
                Events.RemovePictureBoxEvents();
                Events.SetPictureBoxEvents(MainWindow.pictureBox);
        }
    }
    class Sketch
    {
        public System.Drawing.SolidBrush Brushes { get; set; }
        public System.Drawing.Color Color { get; set; }
        public System.Drawing.Point Point { get; set; }
        public System.Drawing.Pen Pen { get; set; }
        public System.Drawing.Rectangle Rectangle { get; set; }

    }
}
