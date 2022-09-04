using UnityEngine;

namespace wzebra.drawballs.core
{
    public class Multiplyier : BallsTrigger
    {
        [SerializeField, Range(2, 10)] private int _factor = 2;

        [SerializeField] private float _pushForce = 1;

        private float _shiftValue = 0.2f;

        protected override void AfterStart()
        {
            OnBallCollide += Multiply;
        }

        private void Multiply(Ball ball)
        {
            Vector3 velocity = ball.GetFreezer().GetRigidbody().velocity;

            ball.GetFreezer().GetRigidbody().MovePosition(ball.GetFreezer().GetRigidbody().position + Vector3.left * _shiftValue);

            ball.GetFreezer().GetRigidbody().velocity += Vector3.left * _pushForce;

            for (int i = 0; i < _factor - 1; i++)
            {
                Ball newBall = _drawer.SpawnBall(ball.transform.position, ball.GetScale(), ball.GetColor());

                newBall.GetFreezer().Unfreeze();
                newBall.GetFreezer().GetRigidbody().velocity = velocity;

                float t = ((float)(i + 1) / (float)(_factor - 1));

                newBall.GetFreezer().GetRigidbody().AddForce(Vector3.Lerp(Vector3.left * _pushForce, Vector3.right * _pushForce, t), ForceMode.Force);
                newBall.transform.position += Vector3.Lerp(Vector3.left * _shiftValue, Vector3.right * _shiftValue, t);
                AddToCreatedList(newBall);
            }
        }
    }
}