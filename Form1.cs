using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fiveStarWatches
{
    public partial class Form1 : Form
    {
        TimerKits[] kits = new TimerKits[5];

        public Form1()
        {
            InitializeComponent();
            kits[0] = new TimerKits(titleText, timeText, rapTimesText, stopTimerButton, resetButton,timer1);
            kits[1] = new TimerKits(titleText2, timeText2, rapTimesText2, stopTimerButton2, resetButton2,timer2);
            kits[2] = new TimerKits(titleText3, timeText3, rapTimesText3, stopTimerButton3, resetButton3,timer3);
            kits[3] = new TimerKits(titleText4, timeText4, rapTimesText4, stopTimerButton4, resetButton4,timer4);
            kits[4] = new TimerKits(titleText5, timeText5, rapTimesText5, stopTimerButton5, resetButton5,timer5);
        }

        private void stopTimerButton_click(object sender, EventArgs e)
        {
            TimerKits tk = findTimerKitsButton(sender);
            if (tk.State == TimerKits.STOP_STATE)
            {
                foreach (TimerKits t in kits)
                {
                    if (t.State == TimerKits.START_STATE)
                    {
                        doStop(t);
                    }
                }
                tk.RapTime.Text += DateTime.Now.ToString("T");
                tk.Timer.Start();
                tk.TimerButton.Text = "STOP";
                tk.State = TimerKits.START_STATE;
            }
            else if (tk.State == TimerKits.START_STATE)
            {
                doStop(tk);
            }
        }

        private TimerKits findTimerKitsButton(object sender)
        {
            TimerKits tk = null;

            foreach (TimerKits t in kits)
            {
                if (t.searchTimerButton((Button)sender))
                {
                    tk = t;
                }
            }

            return tk;
        }

        private TimerKits findTimerKitsTimer(object sender)
        {
            TimerKits tk = null;

            foreach (TimerKits t in kits)
            {
                if (t.searchTimerTimer((Timer)sender))
                {
                    tk = t;
                }
            }

            return tk;
        }

        private TimerKits findTimerKitsReset(object sender)
        {
            TimerKits tk = null;

            foreach (TimerKits t in kits)
            {
                if (t.searchTimerReset((Button)sender))
                {
                    tk = t;
                }
            }

            return tk;
        }

        private void doStop(TimerKits tk)
        {
            tk.RapTime.Text += "->" +
                DateTime.Now.ToString("T") + "\r\n";
            tk.Timer.Stop();
            tk.TimerButton.Text = "START";
            tk.State = TimerKits.STOP_STATE;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TimerKits tk = findTimerKitsTimer(sender);
            tk.Time++;
            setTimerText(tk);
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            TimerKits tk = findTimerKitsReset(sender);
            doStop(tk);
            tk.RapTime.Text = "";
            tk.Time = 0;
            setTimerText(tk);
        }

        private void setTimerText(TimerKits tk)
        {
            int hour = tk.Time / 3600;
            int min = tk.Time % 3600 / 60;
            int second = tk.Time % 3600 % 60;
            tk.NowTime.Text = hour.ToString("D2") + ":"
                + min.ToString("D2") + ":"
                + second.ToString("D2");
        }
    }

    public class TimerKits
    {
        TextBox title;
        TextBox nowTime;
        TextBox rapTime;
        Button timerButton;
        Button resetButton;

        public const int START_STATE = 0;
        public const int STOP_STATE = 1;
        int state = STOP_STATE;

        int time = 0;

        Timer timer;

        public TextBox Title
        {
            get
            {
                return title;
            }

            set
            {
                title = value;
            }
        }

        public TextBox NowTime
        {
            get
            {
                return nowTime;
            }

            set
            {
                nowTime = value;
            }
        }

        public TextBox RapTime
        {
            get
            {
                return rapTime;
            }

            set
            {
                rapTime = value;
            }
        }

        public Button TimerButton
        {
            get
            {
                return timerButton;
            }

            set
            {
                timerButton = value;
            }
        }

        public Button ResetButton
        {
            get
            {
                return resetButton;
            }

            set
            {
                resetButton = value;
            }
        }

        public int State
        {
            get
            {
                return state;
            }

            set
            {
                state = value;
            }
        }

        public Timer Timer
        {
            get
            {
                return timer;
            }

            set
            {
                timer = value;
            }
        }

        public int Time
        {
            get
            {
                return time;
            }

            set
            {
                time = value;
            }
        }

        public TimerKits(
            TextBox title,
            TextBox nowTime,
            TextBox rapTime,
            Button timerButton,
            Button resetButton,
            Timer timer
            )
        {
            this.Title = title;
            this.NowTime = nowTime;
            this.RapTime = rapTime;
            this.TimerButton = timerButton;
            this.ResetButton = resetButton;
            this.Timer = timer;
        }
        public bool searchTimerButton(Button sender)
        {
            return TimerButton == sender;
        }
        public bool searchTimerTimer(Timer sender)
        {
            return Timer == sender;
        }
        public bool searchTimerReset(Button sender)
        {
            return resetButton == sender;
        }
    }
}
