using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PaperScience
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HeadboxSliceFlowRate : ContentPage
    {
        public HeadboxSliceFlowRate()
        {
            InitializeComponent();
            
            if (App.switched)
            {
                units.IsToggled = true;
            }
            else
            {
                units.IsToggled = false;
            }
        }

        /*
        * Switch changed
        */
        private void Units_Toggled(object sender, ToggledEventArgs e)
        {
            bool isToggled = e.Value;

            if (isToggled)
            {
                s.Text = "SI";
                label1.Text = "B.D. (ton/day)";
                label2.Text = "Machine trim width (mm)";
                label3.Text = "Tray consistency (%)";
                label4.Text = "Headbox consistency (%)";
                App.switched = true;
                answer.IsVisible = false;
            }
            else
            {
                s.Text = "US";
                label1.Text = "B.D. (ton/day)";
                label2.Text = "Machine trim width (in.)";
                label3.Text = "Tray consistency (%)";
                label4.Text = "Headbox consistency (%)";
                App.switched = false;
                answer.IsVisible = false;
            }
        }

        /*
         * Calculate button pressed
         */
        private void Button_Clicked(object sender, EventArgs e)
        {
            answer.IsVisible = true;
            double num;
            if (string.IsNullOrWhiteSpace(bd.Text) || string.IsNullOrWhiteSpace(mt.Text) || string.IsNullOrWhiteSpace(tc.Text) || string.IsNullOrWhiteSpace(hc.Text))
            {
                label5.Text = "Please enter all values.";
            }
            else if (Double.TryParse(bd.Text, out num) && Double.TryParse(mt.Text, out num) && Double.TryParse(tc.Text, out num) && Double.TryParse(hc.Text, out num))
            {
                var tons = double.Parse(bd.Text, CultureInfo.InvariantCulture);
                var airDry = tons * 0.93;
                var machineTrim = double.Parse(mt.Text, CultureInfo.InvariantCulture);
                var trayConsistency = double.Parse(tc.Text, CultureInfo.InvariantCulture);
                var headboxConsistency = double.Parse(hc.Text, CultureInfo.InvariantCulture);
                var netConsistency = headboxConsistency - trayConsistency;

                if (!App.switched)
                {
                    var sliceFlow = (airDry / machineTrim * 16.76 * (1.5 - trayConsistency)) / (1.5 * netConsistency);
                    label5.Text = "Headbox slice flow: " + String.Format("{0:n4}", sliceFlow) + " GPM/in.";
                }
                else
                {
                    var machineTrimInMeters = machineTrim / 1000.0;
                    var sliceFlow = (airDry / machineTrimInMeters * 70.0 * (1.5 - trayConsistency)) / (1.5 * netConsistency);
                    label5.Text = "Headbox slice flow: " + String.Format("{0:n4}", sliceFlow) + " LPM/m";
                }
            }
            else
            {
                label5.Text = "Please enter a valid number.";
            }
        }
    }
}