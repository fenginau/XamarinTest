using System;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Math = System.Math;

namespace Test.Droid.Repos
{
    public class DrawView : View
    {

        private Bitmap _bitmap;
        private Canvas _canvas;
        private readonly Path _path;
        private readonly Paint _bitmapPaint;
        Context _context;
        private readonly Paint _circlePaint;
        private readonly Path _circlePath;
        private readonly Paint _paint;

        public DrawView(Context c) : base(c)
        {
            _context = c;
            _paint = new Paint
            {
                AntiAlias = true,
                Dither = true,
                Color = Color.Green,
                StrokeJoin = Paint.Join.Round,
                StrokeCap = Paint.Cap.Round,
                StrokeWidth = 12
            };
            _paint.SetStyle(Paint.Style.Stroke);

            _path = new Path();
            _bitmapPaint = new Paint(PaintFlags.Dither);
            _circlePath = new Path();
            _circlePaint = new Paint
            {
                AntiAlias = true,
                Color = Color.Blue,
                StrokeJoin = Paint.Join.Miter,
                StrokeWidth = 4f
            };
            _circlePaint.SetStyle(Paint.Style.Stroke);
        }

        protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
        {
            base.OnSizeChanged(w, h, oldw, oldh);
            _bitmap = Bitmap.CreateBitmap(w, h, Bitmap.Config.Argb8888);
            _canvas = new Canvas(_bitmap);
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
            canvas.DrawBitmap(_bitmap, 0, 0, _bitmapPaint);
            canvas.DrawPath(_path, _paint);
            canvas.DrawPath(_circlePath, _circlePaint);
        }

        private float _x, _y;
        private const float TouchTolerance = 4;

        private void TouchStart(float x, float y)
        {
            _path.Reset();
            _path.MoveTo(x, y);
            _x = x;
            _y = y;
        }

        private void TouchMove(float x, float y)
        {
            float dx = Math.Abs(x - _x);
            float dy = Math.Abs(y - _y);
            if (dx > TouchTolerance || dy > TouchTolerance)
            {
                _path.QuadTo(_x, _y, (x + _x) / 2, (y + _y) / 2);
                _x = x;
                _y = y;
                _circlePath.Reset();
                _circlePath.AddCircle(_x, _y, 30, Path.Direction.Cw);
            }
        }

        private void TouchUp()
        {
            _path.LineTo(_x, _y);
            _circlePath.Reset();
            _canvas.DrawPath(_path, _paint);
            _path.Reset();
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            float x = e.GetX();
            float y = e.GetY();

            switch (e.Action)
            {
                case MotionEventActions.Down:
                    TouchStart(x, y);
                    Invalidate();
                    break;
                case MotionEventActions.Move:
                    TouchMove(x, y);
                    Invalidate();
                    break;
                case MotionEventActions.Up:
                    TouchUp();
                    Invalidate();
                    break;
            }

            return true;
        }
    }
}