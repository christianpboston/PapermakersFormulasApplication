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
    public partial class GasLaws : ContentPage
    {
        public GasLaws()
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
                label1.Text = "Current pressure (bar)";
                label2.Text = "Target pressure (bar)";
                label3.Text = "Current volume flowrate (m\u00B3/min)";
                label4.Text = "Target volume flowrate (m\u00B3/min)";
                App.switched = true;
                answer.IsVisible = false;
            }
            else
            {
                s.Text = "US";
                label1.Text = "Current pressure (in. Hg)";
                label2.Text = "Target pressure (in. Hg)";
                label3.Text = "Current volume flowrate (ft\u00B3/min)";
                label4.Text = "Target volume flowrate (ft\u00B3/min)";
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
            if (string.IsNullOrWhiteSpace(P1.Text) || string.IsNullOrWhiteSpace(P2.Text) || string.IsNullOrWhiteSpace(V1.Text))
            {
                label4.Text = "Please enter all values.";
            }
            else if (Double.TryParse(P1.Text, out num) && Double.TryParse(P2.Text, out num) && Double.TryParse(V1.Text, out num))
            {
                var currentPressure = double.Parse(P1.Text, CultureInfo.InvariantCulture);
                var targetPressure = double.Parse(P2.Text, CultureInfo.InvariantCulture);
                var currentVolumeFlowrate = double.Parse(V1.Text, CultureInfo.InvariantCulture);

                if (!App.switched)
                {
                    var targetVolumeFlowrate = ((29.92 - currentPressure) / (29.92 - targetPressure)) * currentVolumeFlowrate;
                    label4.Text = "Target volume flowrate: " + String.Format("{0:n4}", targetVolumeFlowrate) + " ft\u00B3/min";
                }
                else
                {
                    var targetVolumeFlowrate = ((1.0132 - currentPressure) / (1.0132 - targetPressure)) * currentVolumeFlowrate;
                    label4.Text = "Target volume flowrate: " + String.Format("{0:n4}", targetVolumeFlowrate) + " m\u00B3/min";
                }
            }
            else
            {
                label4.Text = "Please enter a valid number.";
            }
        }
    }
}