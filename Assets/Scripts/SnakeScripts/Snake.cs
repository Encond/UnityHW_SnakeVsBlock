using System.Collections.Generic;
using System.Linq;
using SnakeScripts.Segment;
using UnityEngine;
using UnityEngine.Events;

namespace SnakeScripts
{
    [RequireComponent(typeof(TailGenerator))]
    [RequireComponent(typeof(SnakeInput))]
    public class Snake : MonoBehaviour
    {
        [SerializeField] private SnakeHead _snakeHead;
        [SerializeField] private float _speed;
        [SerializeField] private float _tailSpringiness;
        [SerializeField] private int _tailSize;

        private TailGenerator _tailGenerator;
        private SnakeInput _snakeInput;
        private List<SnakeSegment> _tail;

        public event UnityAction<int> SizeUpdated;

        private void OnEnable()
        {
            _snakeHead.BlockCollided += OnBlockCollided;
            _snakeHead.OnBonusPickUp += OnBonusPickUp;
        }

        private void OnDisable()
        {
            _snakeHead.BlockCollided -= OnBlockCollided;
            _snakeHead.OnBonusPickUp -= OnBonusPickUp;
            
        }

        private void Start()
        {
            _tailGenerator = GetComponent<TailGenerator>();
            _snakeInput = GetComponent<SnakeInput>();

            _tail = _tailGenerator.Generate(_tailSize);
            SizeUpdated?.Invoke(_tail.Count);
        }

        private void FixedUpdate()
        {
            Move(_snakeHead.transform.position + _snakeHead.transform.up * (_speed * Time.fixedDeltaTime));

            _snakeHead.transform.up = _snakeInput.GetDirectionClick(_snakeHead.transform.position);
        }

        private void Move(Vector2 nextPosition)
        {
            Vector2 prevPosition = _snakeHead.transform.position;

            foreach (SnakeSegment segment in _tail)
            {
                Vector2 tempSegmentPosition = segment.transform.position;
                segment.transform.position =
                    Vector2.Lerp(segment.transform.position, prevPosition, _tailSpringiness * Time.fixedDeltaTime);
                prevPosition = tempSegmentPosition;
            }

            _snakeHead.Move(nextPosition);
        }

        private void OnBlockCollided()
        {
            SnakeSegment deletedSegment = _tail.Last();
            _tail.Remove(deletedSegment);
            Destroy(deletedSegment.gameObject);

            SizeUpdated?.Invoke(_tail.Count);
        }
        
        private void OnBonusPickUp(int bonusValue)
        {
            _tail.AddRange(_tailGenerator.Generate(bonusValue));
            
            SizeUpdated?.Invoke(_tail.Count + bonusValue);
        }
    }
}