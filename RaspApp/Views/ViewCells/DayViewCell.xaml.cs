using RaspApp.Models;
using RaspApp.Views.Pages;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RaspApp.Views.ViewCells
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DayViewCell : ViewCell
    {
        private bool IsBinded = false;
        public DayViewCell()
        {
            InitializeComponent();

        }
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            Day day = BindingContext as Day;
            if (!IsBinded)
            {
                IsBinded = true;
                foreach (TimeRange timeRange in day.TimeRanges)
                {
                    TimeRangeLayout.Children.Add(new LessonView()
                    {
                        BindingContext = timeRange
                    });
                }
            }
        }

    }
}