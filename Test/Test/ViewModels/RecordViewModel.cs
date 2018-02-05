using System;
using Xamarin.Forms;
using Test.Views;

namespace Test.ViewModels
{
    public class RecordViewModel
    {
        public Command<Point> CanvasTappedCommand => new Command<Point>(OnCanvasTapped);
        private readonly Record.TapDel _tapDel;

        public void OnCanvasTapped(Point p)
        {
            _tapDel(p);
        }

        public RecordViewModel(Record.TapDel tapDel)
        {
            _tapDel = tapDel;
        }
    }
}
