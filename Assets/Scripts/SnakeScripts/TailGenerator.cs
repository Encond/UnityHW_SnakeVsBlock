using System.Collections.Generic;
using SnakeScripts.Segment;
using UnityEngine;

namespace SnakeScripts
{
    public class TailGenerator : MonoBehaviour
    {
        [SerializeField] private SnakeSegment _segmentTemplate;

        public List<SnakeSegment> Generate(int count)
        {
            List<SnakeSegment> snakeSegments = new List<SnakeSegment>();

            for (int i = 0; i < count; i++)
            {
                snakeSegments.Add(Instantiate(_segmentTemplate, transform));
            }

            return snakeSegments;
        }
    }
}