using System;
using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace Joy
{
    public class GameView : SKCanvasView
    {
		Game game;
		SKSize gameSize;
        DateTime previousThinkCallDateTime;

        public GameView()
        {
            EnableTouchEvents = true;
        }

        public void Load(Game game)
        {
            this.game = game;

            game.Load();
            previousThinkCallDateTime = DateTime.Now;
            gameSize = new SKSize(game.Width, game.Height);
        }

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            base.OnPaintSurface(e);

            var timeSincePreviousCall = DateTime.Now - previousThinkCallDateTime;
            game.Think(timeSincePreviousCall);
            previousThinkCallDateTime = DateTime.Now;

            game.ClearLastTouchPoint();

            game.NativeCanvas = e.Surface.Canvas;
            game.Paint();
            InvalidateSurface();
        }

        protected override void OnTouch(SKTouchEventArgs e)
        {
            base.OnTouch(e);

            var virtualPoint = e.Location.ToVirtual(CanvasSize, gameSize);
            game.PromoteTouchAt(virtualPoint);
        }
    }
}
