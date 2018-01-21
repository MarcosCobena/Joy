using System;
using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace Joy
{
    public class GameView : SKCanvasView
    {
		Game game;
        bool gatherPaintingSize;
        bool isFirstPaint = true;
        DateTime previousThinkCallDateTime;

        public GameView()
        {
            EnableTouchEvents = true;
        }

        public void Load(Game game)
        {
            this.game = game;

            if (game.PaintingSize == SKSize.Empty)
            {
                gatherPaintingSize = true;
            }

            game.Load();
            previousThinkCallDateTime = DateTime.Now;
        }

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            base.OnPaintSurface(e);

            var timeSincePreviousCall = DateTime.Now - previousThinkCallDateTime;
            game.Think(timeSincePreviousCall);
            previousThinkCallDateTime = DateTime.Now;

            game.ClearLastTouchPoint();

            game.NativeCanvas = e.Surface.Canvas;

            if (isFirstPaint)
            {
                if (gatherPaintingSize)
                {
                    game.PaintingSize = e.Info.Size;
                }

                isFirstPaint = false;
            }

            game.Paint();

            InvalidateSurface();
        }

        protected override void OnTouch(SKTouchEventArgs e)
        {
            base.OnTouch(e);

            var virtualPoint = e.Location.ToVirtual(CanvasSize, game.PaintingSize);
            game.PromoteTouchAt(virtualPoint);
        }
    }
}
