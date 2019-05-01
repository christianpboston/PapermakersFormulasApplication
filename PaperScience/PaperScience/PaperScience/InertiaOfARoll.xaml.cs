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
    public partial class InertiaOfARoll : ContentPage
    {
        public InertiaOfARoll()
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
                label1.Text = "Density (kg/m\u00B3)";
                label2.Text = "Length (m)";
                label3.Text = "Outside diameter (m)";
                label4.Text = "Inside diameter (m)";
                App.switched = true;
                answer.IsVisible = false;
            }
            else
            {
                s.Text = "US";
                label1.Text = "Density (lb/in\u00B3)";
                label2.Text = "Length (in.)";
                label3.Text = "Outside diameter (in.)";
                label4.Text = "Inside diameter (in.)";
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
            if (string.IsNullOrWhiteSpace(w.Text) || string.IsNullOrWhiteSpace(L.Text) || string.IsNullOrWhiteSpace(Do.Text) || string.IsNullOrWhiteSpace(Di.Text))
            {
                label5.Text = "Please enter all values.";
            }
            else if (Double.TryParse(w.Text, out num) && Double.TryParse(L.Text, out num) && Double.TryParse(Do.Text, out num) && Double.TryParse(Di.Text, out num))
            {
                var density = double.Parse(w.Text, CultureInfo.InvariantCulture);
                var length = double.Parse(L.Text, CultureInfo.InvariantCulture);
                var outsideDiameter = double.Parse(Do.Text, CultureInfo.InvariantCulture);
                var insideDiameter = double.Parse(Di.Text, CultureInfo.InvariantCulture);

                if (!App.switched)
                {
                    var inertia = 0.000682 * density * length * (Math.Pow(outsideDiameter, 4) - Math.Pow(insideDiameter, 4));
                    label5.Text = "Inertia: " + String.Format("{0:n4}", inertia) + " lbf*ft\u00B2";
                }
                else
                {
                    var inertia = 0.09817 * density * length * (Math.Pow(outsideDiameter, 4) - Math.Pow(insideDiameter, 4));
                    label5.Text = "Inertia: " + String.Format("{0:n4}", inertia) + " kg*m\u00B2";
                }
            }
            else
            {
                label5.Text = "Please enter a valid number.";
            }
        }
    }
}