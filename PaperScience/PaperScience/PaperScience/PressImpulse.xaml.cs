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
    public partial class PressImpulse : ContentPage
    {
        public PressImpulse()
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
                label1.Text = "Press line load (kN/m)";
                label2.Text = "Nip speed (m/min)";
                App.switched = true;
                answer.IsVisible = false;
            }
            else
            {
                s.Text = "US";
                label1.Text = "Press line load (lbf/in.)";
                label2.Text = "Nip speed (ft/min)";
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
            if (string.IsNullOrWhiteSpace(pll.Text) || string.IsNullOrWhiteSpace(speed.Text))
            {
                label3.Text = "Please enter all values.";
            }
            else if (Double.TryParse(pll.Text, out num) && Double.TryParse(speed.Text, out num))
            {
                var pressLineLoad = double.Parse(pll.Text, CultureInfo.InvariantCulture);
                var s = double.Parse(speed.Text, CultureInfo.InvariantCulture);

                if (!App.switched)
                {
                    var pressImpulse = 5.0 * pressLineLoad / s;
                    label3.Text = "Press impulse: " + String.Format("{0:n4}", pressImpulse) + " PSI*sec";
                }
                else
                {
                    var pressImpulse = 0.060 * pressLineLoad / s;
                    label3.Text = "Press impulse: " + String.Format("{0:n4}", pressImpulse) + " MPa*sec";
                }
            }
            else
            {
                label3.Text = "Please enter a valid number.";
            }
        }
    }
}