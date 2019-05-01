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
    public partial class PaperWebDraw : ContentPage
    {
        public PaperWebDraw()
        {
            InitializeComponent();
            
        }

        /*
         * Calculate button pressed
         */
        private void Button_Clicked(object sender, EventArgs e)
        {
            answer.IsVisible = true;
            double num;
            if (string.IsNullOrWhiteSpace(Sf.Text) || string.IsNullOrWhiteSpace(Si.Text))
            {
                label3.Text = "Please enter all values.";
            }
            else if (Double.TryParse(Sf.Text, out num) && Double.TryParse(Si.Text, out num))
            {
                var speedFinal = double.Parse(Sf.Text, CultureInfo.InvariantCulture);
                var speedInitial = double.Parse(Si.Text, CultureInfo.InvariantCulture);

                var draw = ((speedFinal - speedInitial) / speedInitial) * 100.0;
                label3.Text = "Draw: " + String.Format("{0:n4}", draw) + " %";
            }
            else
            {
                label3.Text = "Please enter a valid number.";
            }
        }
    }
}