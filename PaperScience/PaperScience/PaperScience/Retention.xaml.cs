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
    public partial class Retention : ContentPage
    {
        public Retention()
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
                label1.Text = "Headbox consistency (%)";
                label2.Text = "Tray consistency (%)";
                App.switched = true;
                answer.IsVisible = false;
            }
            else
            {
                s.Text = "US";
                label1.Text = "Net consistency (%)";
                label2.Text = "Headbox consistency (%)";
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
            if (string.IsNullOrWhiteSpace(c1.Text) || string.IsNullOrWhiteSpace(c2.Text))
            {
                label3.Text = "Please enter all values.";
            }
            else if (Double.TryParse(c1.Text, out num) && Double.TryParse(c2.Text, out num))
            {
                var consistency1 = double.Parse(c1.Text, CultureInfo.InvariantCulture);
                var consistency2 = double.Parse(c2.Text, CultureInfo.InvariantCulture);

                if (!App.switched)
                {
                    var r = (consistency1 / consistency2) * 100.0;
                    label3.Text = "Retention: " + String.Format("{0:n4}", r) + " %";
                }
                else
                {
                    var r = ((consistency1 - consistency2) / consistency1) * 100.0;
                    label3.Text = "Retention: " + String.Format("{0:n4}", r) + " %";
                }
            }
            else
            {
                label3.Text = "Please enter a valid number.";
            }
        }
    }
}