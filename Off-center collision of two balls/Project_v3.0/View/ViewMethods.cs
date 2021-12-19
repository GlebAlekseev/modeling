using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;


namespace Project_v3._0
{
    public class ViewMethods
    {
        public static void SavePositionCaret(object sender)
        {
            // Исходное положение каретки
            int pos = (sender as TextBox).CaretIndex;
            // Обработка случая, при котором строка пустая
            if ((sender as TextBox).Text.Length == 0)
            {
                (sender as TextBox).Text = "";
                (sender as TextBox).CaretIndex = pos;
            }
            else
            {
                // Удаление символа
                (sender as TextBox).Text = (sender as TextBox).Text.Remove((sender as TextBox).CaretIndex - 1, 1);
                // Перемещение каретки
                (sender as TextBox).CaretIndex = pos - 1;
            }
        }

        public static void UpdatePermissionSensitiveElementsGUI() {
                if (MainWindow.pseg == PERMISSION_SENSITIVE_ELEMENTS_GUI.ACCESS)
                {
                    EnableElements(MainWindow.SensitivePermissionElementsList);
                }
                else if (MainWindow.pseg == PERMISSION_SENSITIVE_ELEMENTS_GUI.DENIED)
                {
                    DisableElements(MainWindow.SensitivePermissionElementsList);
                }
        }

        public static void EnableElements(List<object> listObjects)
        {
            foreach (var Element in listObjects)
            {
                switch (MainWindow.location_system)
                {
                    case LOCATION_SYSTEM.STRAIGHT:
                        switch (Element)
                        {
                            case TextBox elem:
                                elem.Dispatcher.Invoke(new Action(() => {
                                    switch (elem.Name)
                                    {
                                        case "textbox_cy1":
                                            break;
                                        case "textbox_vy1":
                                            break;
                                        default:
                                            elem.IsEnabled = true;
                                            break;
                                    }
           
                                }));
                                break;
                            case Button elem:
                                elem.Dispatcher.Invoke(new Action(() => {
                                    elem.IsEnabled = true;
                                }));
                                break;
                            case RadioButton elem:
                                elem.Dispatcher.Invoke(new Action(() => {
                                    elem.IsEnabled = true;
                                }));
                                break;
                            default:
                                break;
                        }
                        break;
                    case LOCATION_SYSTEM.ARBITRARY:
                        switch (Element)
                        {
                            case TextBox elem:
                                elem.Dispatcher.Invoke(new Action(() => {
                                    elem.IsEnabled = true;
                                }));
                                break;
                            case Button elem:
                                elem.Dispatcher.Invoke(new Action(() => {
                                    elem.IsEnabled = true;
                                }));
                                break;
                            case RadioButton elem:
                                elem.Dispatcher.Invoke(new Action(() => {
                                    elem.IsEnabled = true;
                                }));
                                break;
                            default:
                                break;
                        }
                        break;

                }

            }
        }

        public static void DisableElements(List<object> listObjects)
        {
            foreach (var Element in listObjects)
            {
                switch (Element)
                {
                    case TextBox elem:
                        elem.Dispatcher.Invoke(new Action(() => {
                            elem.IsEnabled = false;
                        }));
                        break;
                    case Button elem:
                        elem.Dispatcher.Invoke(new Action(() => {
                            elem.IsEnabled = false;
                        }));
                        break;
                    case RadioButton elem:
                        elem.Dispatcher.Invoke(new Action(() => {
                            elem.IsEnabled = false;
                        }));
                        break;
                    default:
                        break;
                }
            }
        }
        public static double CheckTextBox(TextBox textBox, bool status = true)
        {
            if (status)
            {
                foreach (var Element in MainWindow.SensitivePermissionElementsList)
                {
                    switch (Element)
                    {
                        case System.Windows.Controls.TextBox elem:
                            try
                            {
                                CheckTextBox(elem, false);
                            }
                            catch (Exception)
                            {
                                continue;
                            }
                            break;
                    }
                }
            }

            double digit;
            if (Double.TryParse(textBox.Text, out digit))
            {
                textBox.Background = new SolidColorBrush(Colors.White);
                textBox.Foreground = new SolidColorBrush(Colors.Black);
                return digit;
            }
            else
            {
                textBox.Background = new SolidColorBrush(Colors.Red);
                textBox.Foreground = new SolidColorBrush(Colors.White);
                throw new Exception("В поле введено неверное значение");
            }

        }


    }

}
