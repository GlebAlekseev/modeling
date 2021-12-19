using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Project_v3._0
{
    // Отвечает за управление дополнительного потока
    public class Handler
    {
        private ITERATION_CODE IterationCode;
        public static List<Data> DataList = new List<Data>();
        private Data AfterCollisionData = new Data();
        private Thread Thread;
        public static Handler newInstance() {return new Handler();}


        public bool Start(Data data)
        {
            if (Thread == null || Thread?.ThreadState == ThreadState.Stopped || Thread?.ThreadState == ThreadState.Unstarted || Thread?.ThreadState == ThreadState.Aborted)
            {
                DataList.Clear();
                DataList.Add(data);
                Thread = new Thread(new ThreadStart(ThreadHandler));
                Thread.Start();
                return true;
            }
            return false;
        }

        public bool Pause()
        {
            if (Thread?.ThreadState == ThreadState.Running || Thread?.ThreadState == ThreadState.WaitSleepJoin)
            {
                Thread?.Suspend();
                return true;
            }
            return false;
        }
        public bool Resume()
        {
            if (Thread?.ThreadState == ThreadState.Suspended)
            {
                Thread.Resume();
                return true;
            }
            return false;
        }
        public bool Stop()
        {
            if (Thread?.ThreadState == ThreadState.Running || Thread?.ThreadState == ThreadState.Suspended || Thread?.ThreadState == ThreadState.WaitSleepJoin || Thread?.ThreadState == ThreadState.Stopped)
            {
                if (Thread?.ThreadState == ThreadState.Suspended)
                    Thread.Resume();
                Thread.Abort();
                return true;
            }
            return false;
        }
        private void ThreadHandler()
        {
            while (true)
            {
                if (IterationCode == ITERATION_CODE.COLLISION)
                {
                    AfterCollisionData = DataList.Last<Data>();
                    DataList.Remove(AfterCollisionData);
                }

                DrawingInstruments.Display(DataList);

                ChartInstruments.UpdateDataChart(DataList);
                if (IterationCode == ITERATION_CODE.COLLISION)
                {
                    DataList.Add(AfterCollisionData);
                    IterationCode = ITERATION_CODE.NEXT;
                    continue;
                }

                IterationCode = Physics.Iteration(DataList);

                if (IterationCode == ITERATION_CODE.END)
                {
/*                    MainWindow.pseg = PERMISSION_SENSITIVE_ELEMENTS_GUI.ACCESS;
                    ViewMethods.UpdatePermissionSensitiveElementsGUI();
                    LifeCycle.OnRemoveOutputMode();
                    LifeCycle.OnCreateInputMode();*/
                    Stop();
                }

                Thread.Sleep(StaticResources.ThreadDelay);
            }
        }
    }
}
