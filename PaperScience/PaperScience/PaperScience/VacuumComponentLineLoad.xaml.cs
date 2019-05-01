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
    public partial class VacuumComponentLineLoad : ContentPage
    {
        public VacuumComponentLineLoad()
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
                label1.Text = "Vacuum box width (m)";
                label2.Text = "Vacuum (kPa)";
                App.switched = true;
                answer.IsVisible = false;
            }
            else
            {
                s.Text = "US";
                label1.Text = "Vacuum box width (in.)";
                label2.Text = "Vacuum (in. Hg)";
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
            if (string.IsNullOrWhiteSpace(bw.Text) || string.IsNullOrWhiteSpace(v.Text))
            {
                label3.Text = "Please enter all values.";
            }
            else if (Double.TryParse(bw.Text, out num) && Double.TryParse(v.Text, out num))
            {
                var boxWidth = double.Parse(bw.Text, CultureInfo.InvariantCulture);
                var vacuum = double.Parse(v.Text, CultureInfo.InvariantCulture);

                if (!App.switched)
                {
                    var load = (boxWidth * vacuum) / 3.0;
                    label3.Text = "Line load: " + String.Format("{0:n4}", load) + " lbf/in.";
                }
                else
                {
                    var load = (boxWidth * vacuum) / 1.5;
                    label3.Text = "Line load: " + String.Format("{0:n4}", load) + " kN/m";
                }
            }
            else
            {
                label3.Text = "Please enter a valid number.";
            }
        }
    }
}