using System;
using System.Collections.Generic;
using Unity.Entities;
using UnityEditor;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu]
    public class Rules : ScriptableObject
    {
        
        
        public MarbleCreationInfo[] GenerateMarbleBasedOnName(string teamName, int marblePerTeam = 15)
        {
            throw new NotImplementedException();
        }

        private (float, float) GetRadiusRange(string teamName)
        {
            return (0, 0);
        }

        private (float, float) GetFrictionRange(string teamName)
        {
            return (0, 0);
        }

        private (float, float) GetRestitutionRange(string teamName)
        {
            return (0, 0);
        }

        private (float, float) GetWeightRange(string teamName)
        {
            return (0, 0);
        }

        private (float, float) GetGravityFactorRange(string teamName)
        {
            return (0, 0);
        }
    }

    public struct MarbleCreationInfo : IComponentData
    {
        //visual
        public readonly Color color;
        public readonly float radius;

        //physics related
        public readonly float friction;
        public readonly float restitution;
        public readonly float weight;
        public readonly float gravity;

        public MarbleCreationInfo(Color color, float radius, float friction, float restitution, float weight, float gravity)
        {
            this.color = color;
            this.radius = radius;
            this.friction = friction;
            this.restitution = restitution;
            this.weight = weight;
            this.gravity = gravity;
        }
    }
}
