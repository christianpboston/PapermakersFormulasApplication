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
    public partial class DragLoadConventional : ContentPage
    {
        public DragLoadConventional()
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
                label1.Text = "Drive volts (V)";
                label2.Text = "Drive amps (AMPS)";
                label3.Text = "Nominal fabric speed (m/min)";
                label4.Text = "Nominal fabric width (m)";
                label5.Text = "Drag load (kN/m)";
                s.Text = "SI";
                App.switched = true;
                answer.IsVisible = false;
            }
            else
            {
                label1.Text = "Drive volts (V)";
                label2.Text = "Drive amps (AMPS)";
                label3.Text = "Nominal fabric speed (ft/min)";
                label4.Text = "Nominal fabric width (in.)";
                label5.Text = "Drag load (lbf/in.)";
                s.Text = "US";
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
            if (string.IsNullOrWhiteSpace(V.Text) || string.IsNullOrWhiteSpace(A.Text) || string.IsNullOrWhiteSpace(v.Text) || string.IsNullOrWhiteSpace(S.Text))
            {
                label5.Text = "Please enter all values.";
            }
            else if (Double.TryParse(V.Text, out num) && Double.TryParse(A.Text, out num) && Double.TryParse(v.Text, out num) && Double.TryParse(S.Text, out num))
            {
                var volts = double.Parse(V.Text, CultureInfo.InvariantCulture);
                var amps = double.Parse(A.Text, CultureInfo.InvariantCulture);
                var speed = double.Parse(v.Text, CultureInfo.InvariantCulture);
                var width = double.Parse(S.Text, CultureInfo.InvariantCulture);

                if (!App.switched)
                {
                    var top = volts * amps;
                    var bottom = 0.226 * speed * width;
                    var load = (top / bottom) * 0.8;
                    label5.Text = "Drag load: " + String.Format("{0:n4}", load) + " lbf/in.";
                }
                else
                {
                    var top = volts * amps * 0.06;
                    var bottom = speed * width;
                    var load = (top / bottom) * 0.8;
                    label5.Text = "Drag load: " + String.Format("{0:n4}", load) + " kN/m";
                }
            }
            else
            {
                label5.Text = "Please enter a valid number.";
            }
        }
    }
}