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
    public partial class FourdrinierShakeNumber : ContentPage
    {
        public FourdrinierShakeNumber()
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
                label1.Text = "Amplitude (mm)";
                label2.Text = "Frequency (strokes/min)";
                label3.Text = "Wire speed (m/min)";
                App.switched = true;
                answer.IsVisible = false;
            }
            else
            {
                s.Text = "US";
                label1.Text = "Amplitude (in.)";
                label2.Text = "Frequency (strokes/min)";
                label3.Text = "Wire speed (ft/min)";
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
            if (string.IsNullOrWhiteSpace(a.Text) || string.IsNullOrWhiteSpace(f.Text) || string.IsNullOrWhiteSpace(ws.Text))
            {
                label4.Text = "Please enter all values.";
            }
            else if (Double.TryParse(a.Text, out num) && Double.TryParse(f.Text, out num) && Double.TryParse(ws.Text, out num))
            {
                var amplitude = double.Parse(a.Text, CultureInfo.InvariantCulture);
                var frequency = double.Parse(f.Text, CultureInfo.InvariantCulture);
                var speed = double.Parse(ws.Text, CultureInfo.InvariantCulture);

                var shakeNumber = (amplitude * Math.Pow(frequency, 2))/ speed;
                if (!App.switched)
                {
                    label4.Text = "Shake number: " + String.Format("{0:n4}", shakeNumber);
                }
                else
                {
                    label4.Text = "Shake number: " + String.Format("{0:n4}", shakeNumber);
                }
            }
            else
            {
                label4.Text = "Please enter a valid number.";
            }
        }
    }
}