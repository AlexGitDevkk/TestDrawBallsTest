using UnityEngine;

namespace wzebra.drawballs.core
{
    public class Eraser : BallsTrigger
    {
        protected override void AfterStart()
        {
            OnBallCollide += BallCollide;
        }

        private void BallCollide(Ball ball)
        {
            _drawer.GetPuller().ReleaseObject(ball.gameObject, true);
        }
    }
}