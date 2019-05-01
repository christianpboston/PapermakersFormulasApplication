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
    public partial class NaturalFrequency : ContentPage
    {
        public NaturalFrequency()
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
                label1.Text = "Static deflection (m)";
                App.switched = true;
                answer.IsVisible = false;
            }
            else
            {
                s.Text = "US";
                label1.Text = "Static deflection (in.)";
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
            if (string.IsNullOrWhiteSpace(d.Text))
            {
                label2.Text = "Please enter all values.";
            }
            else if (Double.TryParse(d.Text, out num))
            {
                var deflection = double.Parse(d.Text, CultureInfo.InvariantCulture);

                if (!App.switched)
                {
                    var frequency = 3.127 / Math.Sqrt(deflection);
                    label2.Text = "Natural frequency: " + String.Format("{0:n4}", frequency) + " cycles/sec";
                }
                else
                {
                    var frequency = 0.4986 / Math.Sqrt(deflection);
                    label2.Text = "Natural frequency: " + String.Format("{0:n4}", frequency) + " cycles/sec";
                }
            }
            else
            {
                label2.Text = "Please enter a valid number.";
            }
        }
    }
}