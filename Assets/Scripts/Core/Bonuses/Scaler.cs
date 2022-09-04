using UnityEngine;

namespace wzebra.drawballs.core
{
    public class Scaler : BallsTrigger
    {
        [SerializeField, Range(0.1f, 3f)] private float _scale = 1;

        protected override void AfterStart()
        {
            OnBallCollide += BallScale;
        }

        private void BallScale(Ball ball)
        {
            ball.SetScale(ball.GetScale() * _scale, true);
        }
    }
}