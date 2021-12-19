using System;
using System.Collections.Generic;

namespace Project_v3._0
{
    public static class LifeCycle
    {

        // Метод запускается при переходе в режим ввода
        public static void OnCreateInputMode() 
        {
            MainWindow.mode = MODE.INPUT;
            Console.WriteLine($"OnCreateInputMode");
            List<Data> tmpData = new List<Data>();
            Data Tdata = Data.this_.GetDataFromInputs();
            if (Tdata == null)
                return;
            tmpData.Add(Tdata);
            DrawingInstruments.Display(tmpData);
        }
        // Метод запускается при выходе и режима ввода
        public static void OnRemoveInputMode()
        {
            MainWindow.selection_ball = SELECTION_BALL.NONE;
            MainWindow.clicked_ball = CLICKED_BALL.NONE;
            MainWindow.clicked_vector_setup_ball = CLICKED_FOR_VECTOR_SETUP_BALL.NONE;
            Console.WriteLine($"OnRemoveInputMode");
        }

        // Метод запускается при переходе в режим вывода
        public static void OnCreateOutputMode()
        {
            MainWindow.mode = MODE.OUTPUT;
            Console.WriteLine($"OnCreateOutputMode");
        }
        // Метод запускается при выходе из режима вывода
        public static void OnRemoveOutputMode()
        {
            Console.WriteLine($"OnRemoveOutputMode");

        }

        // Метод запускается при переходе в режим перемещения шара
        public static void OnCreateMovementMode()
        {
            MainWindow.mode = MODE.MOVEMENT;
            Console.WriteLine($"OnCreateMovementMode");
        }
        // Метод запускается при выходе из режима перемещения шара
        public static void OnRemoveMovementMode()
        {
            Console.WriteLine($"OnRemoveMovementMode");
        }


        // Метод запускается при переходе в режим настройки веткора
        public static void OnCreateVectorSetupMode()
        {
            MainWindow.mode = MODE.VECTOR_SETUP;
            Console.WriteLine($"OnCreateVectorSetupMode");
        }
        // Метод запускается при выходе из режима настройки вектора
        public static void OnRemoveVectorSetupMode()
        {
            Console.WriteLine($"OnRemoveVectorSetupMode");
        }
        // Метод запускается при переходе в режим прямой системы 
        public static void OnCreateStraightSystem()
        {
            Console.WriteLine($"OnCreateStraightMode =В");

            if (Data.this_ != null)
            {
                MainWindow.location_system = LOCATION_SYSTEM.STRAIGHT;
            }
        }
        // Метод запускается при выходе из режима прямой системы 
        public static void OnRemoveStraightSystem()
        {
            Console.WriteLine($"OnRemoveStraightMode");
        }

        // Метод запускается при переходе в режим произвольной системы 
        public static void OnCreateArbitrarySystem()
        {
            MainWindow.location_system = LOCATION_SYSTEM.ARBITRARY;
            Console.WriteLine($"OnCreateArbitrarySystem");

            // Изменение положения центров шаров и скорости, отключение некоторых полей
            // Включить Y координаты 1 шара

        }
        // Метод запускается при выходе из режима произвольной системы 
        public static void OnRemoveArbitrarySystem()
        {
            Console.WriteLine($"OnRemoveArbitrarySystem");
        }




    }
}
