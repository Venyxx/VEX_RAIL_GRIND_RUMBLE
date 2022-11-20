using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

    public class PositiveRunner : MonoBehaviour
    {
        public PathCreator pathCreator;
        private VertexPath vertexPath;
        public EndOfPathInstruction endOfPathInstruction;
        public float speed = 40;
        private float baseSpeed = 40;
        [SerializeField] public float distanceTravelled;

            void Start() 
            {
                
                if (pathCreator != null)
                {
                    // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                    pathCreator.pathUpdated += OnPathChanged;
                }
            }

            void Update()
            {
                
                if (pathCreator != null)
                {
                    distanceTravelled += speed * Time.deltaTime;
                    transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                    transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                }
            }

            // If the path changes during the game, update the distance travelled so that the follower's position on the new path
            // is as close as possible to its position on the old path
            void OnPathChanged() 
            {
                distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
            }

            public void SpeedAdjustment (float speedPass)
            {
                speed = speedPass;
            }

            public void SpeedReAlignment ()
            {
                speed = baseSpeed;
            }
    }
