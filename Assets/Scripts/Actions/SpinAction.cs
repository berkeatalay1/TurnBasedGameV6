using UnityEngine;

namespace Actions
{
    public class SpinAction : BaseAction
    {

        private const float SpinAmount = 360f;
        private float _totalSpinAmount = 0f;

        public void Spin()
        {
            IsActive = true;
            transform.Rotate(Vector3.up, 90f);
        }

        private void Update()
        {
            if (!IsActive) return;
            var spinAddAmount = SpinAmount * Time.deltaTime;
            transform.eulerAngles += new Vector3(0, spinAddAmount, 0);
            _totalSpinAmount+= spinAddAmount;

            if (_totalSpinAmount >= SpinAmount)
            {
                IsActive = false;
                _totalSpinAmount = 0f;
            }
        }
    }
}